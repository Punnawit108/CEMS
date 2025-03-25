<script setup lang="ts">
/*
 * ชื่อไฟล์: ExpenseReimbursementList.vue
 * คำอธิบาย: ไฟล์นี้แสดงรายการเบิกค่าใช้จ่าย
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
 */
import { useRouter, useRoute } from "vue-router";
import Icon from "../../components/Icon/CIcon.vue";
import Ctable from "../../components/Table/CTable.vue";
import StatusBudge from "../../components/Status/StatusBudge.vue";
import Decimal from "decimal.js";
import Button from "../../components/Buttons/Button.vue";

import { onMounted, ref, computed, watch } from "vue";
import { useExpenseReimbursement } from "../../store/expenseReimbursement";
import { useLockStore } from "../../store/lockSystem";
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
  return Math.ceil(filteredList.value.length / itemsPerPage.value);
});

const paginated = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage.value;
  const end = start + itemsPerPage.value;
  const pageItems = filteredList.value.slice(start, end);

  // Add empty rows if fewer than 10 items
  while (pageItems.length < itemsPerPage.value) {
    pageItems.push(null);
  }

  return pageItems;
});

const router = useRouter();
const expenseReimbursementStore = useExpenseReimbursement();
const { expenseReimbursementList } = storeToRefs(expenseReimbursementStore);
const user = ref<any>(null);
const route = useRoute();
const lockStore = useLockStore();
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
const isLock = ref(false);

const handleClick = () => {
  if (lockStore.isLocked) {
    isLock.value = true;
    setTimeout(() => {
      isLock.value = false;
    }, 1500);
  }
};



// showModal และ selectedItemId สำหรับการลบรายการ
const showModal = ref(false);
const selectedItemId = ref<string | null>(null);

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

const filteredList = computed(() => {
  if (!expenseReimbursementList.value) return [];

  // Log การกรองข้อมูล
  console.log(
    "Filtering list with filters:",
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

  return expenseReimbursementList.value.filter((item) => {
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
  if (!expenseReimbursementList.value) return [];

  // ดึงชื่อโครงการที่ไม่ซ้ำกัน
  const uniqueProjects = new Map();

  expenseReimbursementList.value.forEach((item) => {
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
  if (!expenseReimbursementList.value) return [];

  // ดึงประเภทการเบิกที่ไม่ซ้ำกัน
  const uniqueTypes = new Map();

  expenseReimbursementList.value.forEach((item) => {
    if (item.rqRqtName) {
      uniqueTypes.set(item.rqRqtName, {
        rqtId: item.rqRqtName,
        rqtName: item.rqRqtName,
      });
    }
  });

  return Array.from(uniqueTypes.values());
});

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

// ไปหน้า detail
const toDetails = (id: string) => {
  router.push(`/disbursement/listWithdraw/detail/${id}`);
};

// ฟังก์ชันเปิด Modal ยืนยัน
const openConfirmationModal = (id: string) => {
  selectedItemId.value = id; // กำหนด ID ของรายการที่ต้องการลบ
  showModal.value = true; // เปิด Modal
};

// ฟังก์ชันปิด Modal
const closeModal = () => {
  showModal.value = false; // ปิด Modal
  selectedItemId.value = null; // ล้างค่า ID
};

// ฟังก์ชันยืนยันการลบ
const confirmDelete = async () => {
  if (selectedItemId.value !== null) {
    try {
      await expenseReimbursementStore.deleteExpenseReimbursementItem(
        user.value.usrId,
        selectedItemId.value
      ); // ลบรายการ
    } catch (error) {
      console.error("Failed to delete item:", error);
    } finally {
      closeModal(); // ปิด Modal
    }
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
        await lockStore.fetchLockStatus();
      } catch (error) {
        console.log("Error loading user:", error); // ถ้าล้มเหลวแสดงข้อความ Error
      }
    }

    if (user.value) {
      // โหลดข้อมูลรายการเบิกค่าใช้จ่าย
      await expenseReimbursementStore.getAllExpenseReimbursementList(
        user.value.usrId
      );

      // อัปเดตข้อมูลสำหรับตัวกรอง
      projects.value = extractedProjects.value;
      requisitionTypes.value = extractedRequisitionTypes.value;
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
  <div>
    <div class=" items-end flex justify-end mb-4">
      <RouterLink to="/disbursement/listWithdraw/createExpenseForm" v-if="!lockStore.isLocked">
        <Button :type="'btn-expense'"></Button>
      </RouterLink>
      <div v-else>
        <Button :type="'btn-expense'" @click="handleClick"></Button>
      </div>
    </div>

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

    <!-- Table -->
    <div class="w-full h-fit border-[2px] flex flex-col items-start border-[#BBBBBB]">
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

          <tr v-else-if="!expenseReimbursementList?.length || filteredList.length === 0" v-for="n in 10" :key="n"
            class="h-[50px]">
            <td colspan="100%" class="py-4 text-center">
              <span v-if="n === 5">
                {{ !expenseReimbursementList?.length ? 'ไม่มีข้อมูลรายการเบิกค่าใช้จ่าย' :
                  'ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา' }}
              </span>
            </td>
          </tr>

          <tr v-else v-for="(item, index) in paginated" :key="item ? item.rqId : `empty-${index}`"
            :class="item ? 'text-[14px] h-[46px] border-b-2 border-[#BBBBBB] hover:bg-gray-50' : 'h-[50px]'">
            <template v-if="item">
              <th class="py-3 px-2 w-12">
                {{ index + 1 + (currentPage - 1) * itemsPerPage }}
              </th>
              <th class="py-3 px-2 w-1/4 text-start" :title="item.rqName"
                style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">
                {{ item.rqName }}
              </th>
              <th class=" py-3 px-2 text-start " :title="item.rqPjName"
                style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">
                {{ item.rqPjName }}
              </th>

              <th class="py-3 px-2 w-32 text-start font-[100]">
                {{ item.rqRqtName }}
              </th>
              <th class="py-3 px-2 w-24 text-start">
                {{ item.rqWithDrawDate }}
              </th>
              <th class="py-3 px-2 w-32 text-end">
                {{
                  new Decimal(item.rqExpenses ?? 0).toNumber().toLocaleString("en-US", {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2,
                  })
                }}
              </th>
              <th class="py-3 px-2 w-28 text-start">
                <StatusBudge :status="'sts-' + item.rqStatus" />
              </th>
              <th class="py-[10px] px-2 w-20 text-center">
                <span class="flex justify-center">
                  <Icon :icon="'viewDetails'" @click="toDetails(item.rqId)"
                    class="cursor-pointer hover:text-[#B67D12]" />
                  <Icon v-if="item.rqStatus === 'sketch'" :icon="'bin'" @click="openConfirmationModal(item.rqId)"
                    class="cursor-pointer hover:text-red-500" />
                </span>
              </th>
            </template>
            <template v-else>
              <td>&nbsp;</td>
            </template>
          </tr>
        </tbody>
        <Pagination :current-page="currentPage" :total-pages="totalPages"
          @update:currentPage="(page) => (currentPage = page)" />
      </table>
    </div>

    <!-- Modal for delete confirmation -->
    <div v-if="showModal" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
      <div class="modal-box bg-white w-[460px] h-[295px] rounded-lg shadow-lg">
        <div class="flex justify-center mt-[25px]">
          <svg width="90px" height="90px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
            <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
            <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g>
            <g id="SVGRepo_iconCarrier">
              <path
                d="M12 2C6.4898 2 2 6.4898 2 12C2 17.5102 6.4898 22 12 22C17.5102 22 22 17.5102 22 12C22 6.4898 17.5102 2 12 2ZM11.1837 8.42857C11.1837 8.02041 11.4898 7.61225 12 7.61225C12.5102 7.61225 12.8163 7.91837 12.8163 8.42857V12.5102C12.8163 12.9184 12.5102 13.3265 12 13.3265C11.4898 13.3265 11.1837 13.0204 11.1837 12.5102V8.42857ZM12 16.5918C11.4898 16.5918 10.9796 16.0816 10.9796 15.5714C10.9796 15.0612 11.4898 14.551 12 14.551C12.5102 14.551 13.0204 15.0612 13.0204 15.5714C13.0204 16.0816 12.5102 16.5918 12 16.5918Z"
                fill="#FFBE40"></path>
            </g>
          </svg>
        </div>
        <p class="text-2xl font-bold text-black mt-1 flex justify-center">
          ยืนยันการลบรายการเบิกค่าใช้จ่าย
        </p>
        <p class="text-[18px] text-center text-[#7E7E7E] mt-2">
          คุณยืนยันการลบรายการเบิกค่าใช้จ่ายหรือไม่?
        </p>
        <div class="mt-4 flex justify-center">
          <form method="dialog">
            <button @click="closeModal"
              class="bg-white border-solid border-[#B6B7BA] border-2 rounded px-7 py-2 text-[#B6B7BA] text-sm font-normal mr-3">
              ยกเลิก
            </button>
            <button @click="confirmDelete"
              class="bg-[#12B669] border-solid border-[#12B669] border-2 rounded px-7 py-2 text-white text-sm font-normal">
              ยืนยัน
            </button>
          </form>
        </div>
      </div>
    </div>

    <div v-if="isLock" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <div class="flex justify-center mb-3">
          <svg :class="`w-[90px] h-[90px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black">
          ระบบปิดรับรายเบิกค่าใช้จ่าย
        </h2>
      </div>
    </div>
  </div>
</template>

<style scoped>
.custom-select {
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  background-image: none;
}

.custom-select::-ms-expand {
  display: none;
}

select,
select option {
  background-color: white;
  color: #000000;
}

select:invalid,
select option[value=""] {
  color: #999999;
}

[hidden] {
  display: none;
}

/* Additional styles to ensure the dropdown arrow is hidden in WebKit browsers */
@media screen and (-webkit-min-device-pixel-ratio: 0) {
  .custom-select {
    background-image: url("data:image/svg+xml;utf8,<svg fill='transparent' height='24' viewBox='0 0 24 24' width='24' xmlns='http://www.w3.org/2000/svg'><path d='M7 10l5 5 5-5z'/></svg>");
    background-repeat: no-repeat;
    background-position-x: 100%;
    background-position-y: 5px;
  }
}
</style>
