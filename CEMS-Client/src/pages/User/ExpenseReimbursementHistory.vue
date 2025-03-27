<script setup lang="ts">
/*
 * ชื่อไฟล์: ExpenseReimbursementHistory.vue
 * คำอธิบาย: ไฟล์นี้แสดงประวัติการเบิกค่าใช้จ่าย
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 22 มีนาคม 2568
 */

import { useRouter } from "vue-router";
import Icon from "../../components/Icon/CIcon.vue";
import StatusBudge from "../../components/Status/StatusBudge.vue";
import Ctable from "../../components/Table/CTable.vue";
import Decimal from "decimal.js";
import { onMounted, ref, computed, watch } from "vue";
import { useExpenseReimbursement } from "../../store/expenseReimbursement";
import { storeToRefs } from "pinia";

// Import filters
import RequisitionSearchInput from "../../components/Filter/RequisitionSearchInput.vue";
import ProjectFilter from "../../components/Filter/ProjectFilter.vue";
import RequisitionTypeFilter from "../../components/Filter/RequisitionTypeFilter.vue";
import DateFilter from "../../components/Filter/DateFilter.vue";
import FilterButtons from "../../components/Filter/FilterButtons.vue";
import Pagination from "../../components/Pagination.vue";

const currentPage = ref(1);
const itemsPerPage = ref(10);
const columnNumber = ref(6);
const totalPages = computed(() => {
  return Math.ceil(filteredHistory.value.length / itemsPerPage.value);
});

const paginated = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage.value;
  const end = start + itemsPerPage.value;
  const pageItems = filteredHistory.value.slice(start, end);

  // Add empty rows if fewer than 10 items
  while (pageItems.length < itemsPerPage.value) {
    pageItems.push(null);
  }

  return pageItems;
});

const expenseReimbursementStore = useExpenseReimbursement();
const { expenseReimbursementHistory } = storeToRefs(expenseReimbursementStore);
const router = useRouter();
const user = ref<any>(null);
const loading = ref(false);

// สำหรับข้อมูลโครงการและประเภทการเบิก
const projects = ref<any[]>([]);
const requisitionTypes = ref<any[]>([]);

// Filters
const filters = ref({
  searchQuery: "",
  project: "",
  requisitionType: "",
  startDate: undefined as Date | undefined,
  endDate: undefined as Date | undefined,
});

// Last searched filters (สำหรับการกรองข้อมูลจริงๆ)
const lastSearchedFilters = ref({
  searchQuery: "",
  project: "",
  requisitionType: "",
  startDate: undefined as Date | undefined,
  endDate: undefined as Date | undefined,
});

// Date picker state
const startDateTemp = ref(new Date());
const endDateTemp = ref(new Date());
const isStartDatePickerOpen = ref(false);
const isEndDatePickerOpen = ref(false);

// Reset pagination when filters change
watch(
  lastSearchedFilters,
  () => {
    // TODO: เพิ่ม pagination หากจำเป็น
  },
  { deep: true }
);

// ฟังก์ชันแปลงปี คริสต์ศักราช เป็น พุทธศักราช (บวก 543)
const toBuddhistYear = (date: Date): Date => {
  if (!date) return date;
  const newDate = new Date(date);
  newDate.setFullYear(newDate.getFullYear() + 543);
  return newDate;
};

// ฟังก์ชันแปลงรูปแบบวันที่เป็นพุทธศักราชในรูปแบบ YYYY-MM-DD
const formatToBuddhistYYYYMMDD = (date: Date): string => {
  if (!date) return "";

  const buddhistDate = toBuddhistYear(date);
  const year = buddhistDate.getFullYear();
  const month = (buddhistDate.getMonth() + 1).toString().padStart(2, "0");
  const day = buddhistDate.getDate().toString().padStart(2, "0");

  return `${year}-${month}-${day}`;
};

// ฟังก์ชันแปลงรูปแบบวันที่สำหรับการกรอง (สำหรับแสดงในคอนโซล)
const formatDateForDisplay = (date: Date): string => {
  if (!date) return "";
  const day = date.getDate().toString().padStart(2, "0");
  const month = (date.getMonth() + 1).toString().padStart(2, "0");
  const year = date.getFullYear();
  const buddhistYear = year + 543;
  return `${day}/${month}/${buddhistYear}`;
};

// ฟังก์ชันแปลงวันที่จากรูปแบบสตริง DD/MM/YYYY เป็น Date object
const parseDateString = (dateStr: string): Date | null => {
  if (!dateStr) return null;

  try {
    // รองรับรูปแบบ DD/MM/YYYY
    const parts = dateStr.split('/');
    if (parts.length === 3) {
      const day = parseInt(parts[0], 10);
      const month = parseInt(parts[1], 10) - 1; // เดือนใน JavaScript เริ่มที่ 0
      let year = parseInt(parts[2], 10);

      // ถ้าเป็นปีพุทธศักราช ให้แปลงเป็นคริสต์ศักราช
      if (year > 2500) {
        year = year - 543;
      }

      const date = new Date(year, month, day);
      date.setHours(0, 0, 0, 0);

      // ตรวจสอบว่าวันที่ถูกต้อง
      if (
        date.getDate() === day &&
        date.getMonth() === month &&
        date.getFullYear() === year
      ) {
        return date;
      }
    }
  } catch (e) {
    console.error("Error parsing date string:", dateStr, e);
  }

  return null;
};

// ฟังก์ชันเปรียบเทียบวันที่ ไม่ว่าจะอยู่ในรูปแบบใด
const compareDates = (date1: string | Date | null | undefined, date2: string | Date | null | undefined): number => {
  if (!date1 && !date2) return 0;
  if (!date1) return -1;
  if (!date2) return 1;

  // แปลงวันที่ให้เป็น Date objects
  let d1: Date | null = null;
  let d2: Date | null = null;

  if (date1 instanceof Date) {
    d1 = new Date(date1);
    d1.setHours(0, 0, 0, 0);
  } else if (typeof date1 === 'string') {
    // ตรวจสอบรูปแบบของสตริง
    if (date1.includes('/')) {
      // รูปแบบ DD/MM/YYYY
      d1 = parseDateString(date1);
    } else if (date1.includes('-')) {
      // รูปแบบ YYYY-MM-DD
      const parts = date1.split('-');
      let year = parseInt(parts[0], 10);
      const month = parseInt(parts[1], 10) - 1;
      const day = parseInt(parts[2], 10);

      // ตรวจสอบปีพุทธศักราช
      if (year > 2500) {
        year = year - 543;
      }

      d1 = new Date(year, month, day);
      d1.setHours(0, 0, 0, 0);
    }
  }

  if (date2 instanceof Date) {
    d2 = new Date(date2);
    d2.setHours(0, 0, 0, 0);
  } else if (typeof date2 === 'string') {
    // ตรวจสอบรูปแบบของสตริง
    if (date2.includes('/')) {
      // รูปแบบ DD/MM/YYYY
      d2 = parseDateString(date2);
    } else if (date2.includes('-')) {
      // รูปแบบ YYYY-MM-DD
      const parts = date2.split('-');
      let year = parseInt(parts[0], 10);
      const month = parseInt(parts[1], 10) - 1;
      const day = parseInt(parts[2], 10);

      // ตรวจสอบปีพุทธศักราช
      if (year > 2500) {
        year = year - 543;
      }

      d2 = new Date(year, month, day);
      d2.setHours(0, 0, 0, 0);
    }
  }

  // ถ้าไม่สามารถแปลงเป็น Date ได้
  if (!d1 && !d2) return 0;
  if (!d1) return -1;
  if (!d2) return 1;

  // เปรียบเทียบวันที่
  if (d1 < d2) return -1;
  if (d1 > d2) return 1;
  return 0;
};

const filteredHistory = computed(() => {
  if (!expenseReimbursementHistory.value) return [];

  // Log การกรองข้อมูล
  console.log(
    "Filtering history with filters:",
    JSON.stringify(lastSearchedFilters.value)
  );

  if (lastSearchedFilters.value.startDate) {
    console.log(
      "Start date for filtering (แบบคริสต์ศักราช):",
      lastSearchedFilters.value.startDate
    );
    console.log(
      "Start date for filtering (แบบพุทธศักราช):",
      formatDateForDisplay(lastSearchedFilters.value.startDate)
    );
  }

  if (lastSearchedFilters.value.endDate) {
    console.log(
      "End date for filtering (แบบคริสต์ศักราช):",
      lastSearchedFilters.value.endDate
    );
    console.log(
      "End date for filtering (แบบพุทธศักราช):",
      formatDateForDisplay(lastSearchedFilters.value.endDate)
    );
  }

  return expenseReimbursementHistory.value.filter((item) => {
    // กรองตามข้อความค้นหา
    const matchesSearch =
      !lastSearchedFilters.value.searchQuery ||
      (item.rqName &&
        item.rqName
          .toLowerCase()
          .includes(lastSearchedFilters.value.searchQuery.toLowerCase()));

    // กรองตามโครงการ
    const matchesProject =
      !lastSearchedFilters.value.project ||
      (item.rqPjName && item.rqPjName === lastSearchedFilters.value.project);

    // กรองตามประเภทค่าใช้จ่าย
    const matchesRequisitionType =
      !lastSearchedFilters.value.requisitionType ||
      (item.rqRqtName &&
        item.rqRqtName === lastSearchedFilters.value.requisitionType);

    // ตรวจสอบวันที่ด้วยการใช้ฟังก์ชัน compareDates
    let matchesStartDate = true;
    let matchesEndDate = true;

    if (lastSearchedFilters.value.startDate && item.rqWithDrawDate) {
      // เปรียบเทียบวันที่เริ่มต้นกับวันที่ในข้อมูล
      matchesStartDate = compareDates(item.rqWithDrawDate, lastSearchedFilters.value.startDate) >= 0;
      console.log(`Start date check: "${item.rqWithDrawDate}" >= "${lastSearchedFilters.value.startDate}" = ${matchesStartDate}`);
    }

    if (lastSearchedFilters.value.endDate && item.rqWithDrawDate) {
      // เปรียบเทียบวันที่สิ้นสุดกับวันที่ในข้อมูล
      matchesEndDate = compareDates(item.rqWithDrawDate, lastSearchedFilters.value.endDate) <= 0;
      console.log(`End date check: "${item.rqWithDrawDate}" <= "${lastSearchedFilters.value.endDate}" = ${matchesEndDate}`);
    }

    return (
      matchesSearch &&
      matchesProject &&
      matchesRequisitionType &&
      matchesStartDate &&
      matchesEndDate
    );
  });
});

// สร้าง computed properties สำหรับดึงข้อมูลโครงการและประเภทการเบิกที่มีอยู่
const extractedProjects = computed(() => {
  if (!expenseReimbursementHistory.value) return [];

  // ดึงชื่อโครงการที่ไม่ซ้ำกัน
  const uniqueProjects = new Map();

  expenseReimbursementHistory.value.forEach((item) => {
    if (item.rqPjName) {
      // สมมติว่า id เป็นชื่อโครงการเพราะไม่มีข้อมูลชัดเจนเกี่ยวกับโครงสร้าง id โครงการ
      uniqueProjects.set(item.rqPjName, {
        pjId: item.rqPjName,
        pjName: item.rqPjName,
      });
    }
  });

  return Array.from(uniqueProjects.values());
});

const extractedRequisitionTypes = computed(() => {
  if (!expenseReimbursementHistory.value) return [];

  // ดึงประเภทการเบิกที่ไม่ซ้ำกัน
  const uniqueTypes = new Map();

  expenseReimbursementHistory.value.forEach((item) => {
    if (item.rqRqtName) {
      uniqueTypes.set(item.rqRqtName, {
        rqtId: item.rqRqtName,
        rqtName: item.rqRqtName,
      });
    }
  });

  return Array.from(uniqueTypes.values());
});

// นำทางไปยังหน้ารายละเอียด
const toDetails = (id: string) => {
  router.push(`/disbursement/historyWithdraw/detail/${id}`);
};

// Filter handlers
const handleSearch = () => {
  lastSearchedFilters.value = {
    searchQuery: filters.value.searchQuery,
    project: filters.value.project,
    requisitionType: filters.value.requisitionType,
    startDate: filters.value.startDate,
    endDate: filters.value.endDate,
  };

  console.log(
    "Search with filters:",
    JSON.stringify(lastSearchedFilters.value)
  );

  if (filters.value.startDate) {
    console.log(
      "Start date for filtering (คริสต์ศักราช):",
      filters.value.startDate
    );
    console.log(
      "Start date for filtering (พุทธศักราช):",
      formatToBuddhistYYYYMMDD(filters.value.startDate)
    );
  }

  if (filters.value.endDate) {
    console.log(
      "End date for filtering (คริสต์ศักราช):",
      filters.value.endDate
    );
    console.log(
      "End date for filtering (พุทธศักราช):",
      formatToBuddhistYYYYMMDD(filters.value.endDate)
    );
  }
};

const handleReset = () => {
  // รีเซ็ตค่าปัจจุบัน
  filters.value = {
    searchQuery: "",
    project: "",
    requisitionType: "",
    startDate: undefined,
    endDate: undefined,
  };

  // รีเซ็ตค่าที่ใช้ในการค้นหาล่าสุด
  lastSearchedFilters.value = {
    searchQuery: "",
    project: "",
    requisitionType: "",
    startDate: undefined,
    endDate: undefined,
  };

  // รีเซ็ตวันที่
  startDateTemp.value = new Date();
  endDateTemp.value = new Date();
};

// เพิ่ม state ใหม่เพื่อตรวจสอบความผิดพลาดในการเลือกวันที่
const startDateError = ref(false);

// แก้ไขฟังก์ชันสำหรับการจัดการเมื่อเปิดปิด date picker ของวันที่สิ้นสุด
const handleEndDatePickerOpen = (isOpen: boolean) => {
  if (isOpen && !filters.value.startDate) {
    // ถ้าผู้ใช้พยายามเปิด date picker ของวันที่สิ้นสุดโดยที่ยังไม่ได้เลือกวันที่เริ่มต้น
    startDateError.value = true; // แสดงข้อผิดพลาดที่วันที่เริ่มต้น
    isEndDatePickerOpen.value = false; // ไม่เปิด date picker ของวันที่สิ้นสุด
  } else {
    isEndDatePickerOpen.value = isOpen; // เปิดหรือปิด date picker ตามปกติ
  }
};

// แก้ไขฟังก์ชัน confirmStartDate เพื่อล้างข้อผิดพลาด
const confirmStartDate = (date: Date) => {
  filters.value.startDate = date;
  startDateError.value = false; // ล้างข้อผิดพลาดเมื่อเลือกวันที่เริ่มต้นแล้ว
  console.log("Start date confirmed (คริสต์ศักราช):", date);
  console.log(
    "Start date confirmed (พุทธศักราช):",
    formatToBuddhistYYYYMMDD(date)
  );
};

const confirmEndDate = (date: Date) => {
  filters.value.endDate = date;
  console.log("End date confirmed (คริสต์ศักราช):", date);
  console.log(
    "End date confirmed (พุทธศักราช):",
    formatToBuddhistYYYYMMDD(date)
  );
};

const cancelStartDate = () => {
  if (!filters.value.startDate) {
    startDateTemp.value = new Date();
  } else {
    startDateTemp.value = filters.value.startDate;
  }
};

const cancelEndDate = () => {
  if (!filters.value.endDate) {
    endDateTemp.value = new Date();
  } else {
    endDateTemp.value = filters.value.endDate;
  }
};

// Lifecycle hooks
onMounted(async () => {
  // เริ่ม loading ทันทีเมื่อเข้าหน้านี้
  loading.value = true;

  try {
    const storedUser = localStorage.getItem("user"); // ดึงข้อมูลผู้ใช้จาก localStorage
    if (storedUser) {
      try {
        user.value = await JSON.parse(storedUser); // แปลงข้อมูลที่ได้จาก JSON String เป็น Object
      } catch (error) {
        console.log("Error loading user:", error); // ถ้าล้มเหลวแสดงข้อความ Error
      }
    }

    if (user.value) {
      // โหลดข้อมูลประวัติการเบิกค่าใช้จ่าย
      await expenseReimbursementStore.getAllExpenseReimbursementHistory(
        user.value.usrId
      );

      // อัปเดตข้อมูลสำหรับตัวกรอง
      projects.value = extractedProjects.value;
      requisitionTypes.value = extractedRequisitionTypes.value;

      // ตรวจสอบวันที่ในฐานข้อมูล
      if (
        expenseReimbursementHistory.value &&
        expenseReimbursementHistory.value.length > 0
      ) {
        console.log("------- ตรวจสอบรูปแบบวันที่จาก Database -------");

        // แสดงตัวอย่างข้อมูลวันที่รายการแรก
        const firstItem = expenseReimbursementHistory.value[0];
        const secondItem =
          expenseReimbursementHistory.value.length > 1
            ? expenseReimbursementHistory.value[1]
            : null;

        console.log("รูปแบบวันที่รายการแรก:", firstItem.rqWithDrawDate);
        console.log("ชนิดข้อมูล:", typeof firstItem.rqWithDrawDate);

        if (secondItem) {
          console.log("รูปแบบวันที่รายการที่สอง:", secondItem.rqWithDrawDate);
        }

        // ตรวจสอบว่าเป็นปีพุทธศักราชหรือคริสต์ศักราช
        if (
          firstItem.rqWithDrawDate &&
          firstItem.rqWithDrawDate.includes("-")
        ) {
          const year = parseInt(firstItem.rqWithDrawDate.split("-")[0], 10);
          if (year > 2500) {
            console.log("ข้อมูลวันที่ใช้ปีแบบพุทธศักราช (พ.ศ.)");
          } else {
            console.log("ข้อมูลวันที่ใช้ปีแบบคริสต์ศักราช (ค.ศ.)");
          }
        }

        // แสดงรูปแบบทั้งหมดที่พบ
        const formats = new Set();
        expenseReimbursementHistory.value.forEach((item) => {
          if (item.rqWithDrawDate) {
            if (item.rqWithDrawDate.includes("-")) {
              formats.add("YYYY-MM-DD");
            } else if (item.rqWithDrawDate.includes("/")) {
              formats.add("DD/MM/YYYY or MM/DD/YYYY");
            } else {
              formats.add("อื่นๆ: " + item.rqWithDrawDate);
            }
          }
        });

        console.log("รูปแบบวันที่ที่พบในข้อมูล:", Array.from(formats));
        console.log("------- จบการตรวจสอบรูปแบบวันที่ -------");
      }
    }
  } catch (error) {
    console.error("เกิดข้อผิดพลาดในการโหลดข้อมูล:", error);
  } finally {
    // รอสักครู่ก่อนปิด loading เพื่อให้มั่นใจว่า UI ได้ render แล้ว
    setTimeout(() => {
      loading.value = false;
    }, 500);
  }
});

</script>

<template>
  <div class="content">
    <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-4">
      <!-- ค้นหา -->
      <RequisitionSearchInput v-model="filters.searchQuery" :loading="loading" />

      <!-- โครงการ -->
      <ProjectFilter v-model="filters.project" :projects="projects" :loading="loading" />

      <!-- ประเภทค่าใช้จ่าย -->
      <RequisitionTypeFilter v-model="filters.requisitionType" :requisition-types="requisitionTypes"
        :loading="loading" />

      <!-- วันที่เริ่มต้นขอเบิก -->
      <DateFilter v-model="startDateTemp" :loading="loading" label="วันที่เริ่มต้นขอเบิก"
        :is-open="isStartDatePickerOpen" @update:is-open="isStartDatePickerOpen = $event"
        :confirmed-date="filters.startDate" @confirm="confirmStartDate" @cancel="cancelStartDate"
        :error="startDateError" />

      <!-- วันที่สิ้นสุดขอเบิก -->
      <div class="flex flex-col">
        <DateFilter v-model="endDateTemp" :loading="loading" label="วันที่สิ้นสุดขอเบิก" :is-open="isEndDatePickerOpen"
          @update:is-open="handleEndDatePickerOpen" :confirmed-date="filters.endDate" @confirm="confirmEndDate"
          @cancel="cancelEndDate" :min-date="filters.startDate" class="mb-4" />

        <!-- ปุ่มค้นหาและรีเซ็ต -->
        <FilterButtons :loading="loading" @reset="handleReset" @search="handleSearch" />
      </div>
    </div>

    <div class="w-full h-fit border-[2px] flex flex-col items-start border-[#BBBBBB]">
      <!-- ตาราง -->
      <Ctable :table="'Table9-head-New'" />
      <table class="w-full text-center text-black table-auto">
        <tbody>
          <tr v-if="loading">
            <td colspan="100%" class="py-4">
              <div class="flex justify-center items-center">
                <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-black"></div>
                <span class="ml-2">กำลังโหลดข้อมูล...</span>
              </div>
            </td>
          </tr>
          <tr v-else-if="!expenseReimbursementHistory?.length || filteredHistory.length === 0" v-for="n in 10" :key="n"
            class="h-[50px]">
            <td colspan="100%" class="py-4 text-center">
              <span v-if="n === 5">
                {{ !expenseReimbursementHistory?.length ? 'ไม่มีข้อมูลประวัติการเบิกค่าใช้จ่าย' :
                  'ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา' }}
              </span>
            </td>
          </tr>

          <tr v-else v-for="(expenseReimbursementItem, index) in paginated"
            :key="expenseReimbursementItem ? expenseReimbursementItem.rqId : `empty-${index}`"
            :class="expenseReimbursementItem ? 'text-[14px] h-[46px] border-b-2 border-[#BBBBBB] hover:bg-gray-50' : 'h-[50px]'">
            <template v-if="expenseReimbursementItem">
              <th class="py-3 px-2 w-12">
                {{ index + 1 + (currentPage - 1) * itemsPerPage }}
              </th>
              <th class="py-3 px-2 w-1/4 text-start " :title="expenseReimbursementItem.rqName"
                style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">
                {{ expenseReimbursementItem.rqName }}
              </th>
              <th class="py-3 px-2 text-start " :title="expenseReimbursementItem.rqPjName"
                style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">
                {{ expenseReimbursementItem.rqPjName }}
              </th>
              <th class="py-3 px-2 w-32 text-start font-[100]">
                {{ expenseReimbursementItem.rqRqtName }}
              </th>
              <th class="py-3 px-2 w-24 text-start">
                {{ expenseReimbursementItem.rqWithDrawDate }}
              </th>
              <th class="py-3 px-2 w-32 text-end">
                {{
                  new Decimal(expenseReimbursementItem.rqExpenses ?? 0)
                    .toNumber()
                    .toLocaleString("en-US", {
                      minimumFractionDigits: 2,
                      maximumFractionDigits: 2,
                    })
                }}
              </th>
              <th class="py-3 px-2 w-28 text-center">
                <StatusBudge :status="'sts-' + expenseReimbursementItem.rqStatus" />
              </th>
              <th class="py-[10px] px-2 w-20 text-center">
                <span class="flex justify-center">
                  <Icon :icon="'viewDetails'" @click="toDetails(expenseReimbursementItem.rqId)"
                    class="cursor-pointer hover:text-[#B67D12]" />
                </span>
              </th>
            </template>
            <template v-else>
              <td>&nbsp;</td>
            </template>
          </tr>
        </tbody>
        <Pagination :currentPage="currentPage" :totalPages="totalPages"
          @update:currentPage="(page) => (currentPage = page)" />
      </table>
    </div>
  </div>
</template>
