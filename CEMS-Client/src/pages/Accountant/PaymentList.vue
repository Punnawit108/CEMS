<script setup lang="ts">
/*
 * ชื่อไฟล์: PaymentList.vue
 * คำอธิบาย: ไฟล์นี้แสดงรายการรอนำจ่าย
 * ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
 * วันที่จัดทำ/แก้ไข: 22 มีนาคม 2568
 */
import { useRouter } from "vue-router";
import Icon from "../../components/Icon/CIcon.vue";
import Ctable from "../../components/Table/CTable.vue";
import { usePayment } from "../../store/paymentStore";
import { onMounted, ref, computed, watch } from "vue";
import Decimal from "decimal.js";
import { storeToRefs } from "pinia";
import Pagination from "../../components/Pagination.vue";

const currentPage = ref(1);
const itemsPerPage = ref(10);
const columnNumber = ref(6);
const totalPages = computed(() => {
  return Math.ceil(filteredPayments.value.length / itemsPerPage.value);
});

const paginated = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage.value;
  const end = start + itemsPerPage.value;
  const pageItems = filteredPayments.value.slice(start, end);

  // Add empty rows if fewer than 10 items
  while (pageItems.length < itemsPerPage.value) {
    pageItems.push(null);
  }

  return pageItems;
});

// Import filters
import UserSearchInput from "../../components/Filter/UserSearchInput.vue";
import ProjectFilter from "../../components/Filter/ProjectFilter.vue";
import RequisitionTypeFilter from "../../components/Filter/RequisitionTypeFilter.vue";
import DateFilter from "../../components/Filter/DateFilter.vue";
import FilterButtons from "../../components/Filter/FilterButtons.vue";

const paymentStore = usePayment();
const { expense } = storeToRefs(paymentStore);
const router = useRouter();
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

// เพิ่มตัวแปรเก็บสถานะข้อผิดพลาด
const startDateError = ref(false);

// Reset pagination when filters change
watch(
  lastSearchedFilters,
  () => {
    currentPage.value = 1;
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

// ฟังก์ชันตรวจสอบว่าวันที่เป็นปีพุทธศักราชหรือไม่ (โดยการเช็คว่าปีมากกว่า 2400 หรือไม่)
const isBuddhistYear = (date: Date | string): boolean => {
  if (!date) return false;

  try {
    let year: number;

    if (date instanceof Date) {
      // ถ้าเป็นวัตถุ Date
      year = date.getFullYear();
    } else {
      // ถ้าเป็นสตริงในรูปแบบ YYYY-MM-DD
      year = parseInt(date.split("-")[0], 10);
    }

    // ถ้าปีมากกว่า 2500 มักจะเป็นปีพุทธศักราช (เนื่องจาก ค.ศ. 2000 = พ.ศ. 2543)
    return year > 2500;
  } catch (e) {
    console.error("Error parsing date:", e);
    return false;
  }
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

// ฟังก์ชันแปลงรูปแบบวันที่เป็นคริสต์ศักราชในรูปแบบ YYYY-MM-DD
const formatToChristianYYYYMMDD = (date: Date): string => {
  if (!date) return "";

  const year = date.getFullYear();
  const month = (date.getMonth() + 1).toString().padStart(2, "0");
  const day = date.getDate().toString().padStart(2, "0");

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
    const parts = dateStr.split("/");
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
const compareDates = (
  date1: string | String | Date | null | undefined,
  date2: string | String | Date | null | undefined
): number => {
  if (!date1 && !date2) return 0;
  if (!date1) return -1;
  if (!date2) return 1;

  // แปลงข้อมูลให้เป็น string primitive ก่อน ในกรณีที่เป็น String object
  const safeDate1 =
    typeof date1 === "object" && date1 instanceof String
      ? date1.toString()
      : date1;
  const safeDate2 =
    typeof date2 === "object" && date2 instanceof String
      ? date2.toString()
      : date2;

  // แปลงวันที่ให้เป็น Date objects
  let d1: Date | null = null;
  let d2: Date | null = null;

  if (safeDate1 instanceof Date) {
    d1 = new Date(safeDate1);
    d1.setHours(0, 0, 0, 0);
  } else if (typeof safeDate1 === "string") {
    // ตรวจสอบรูปแบบของสตริง
    if (safeDate1.includes("/")) {
      // รูปแบบ DD/MM/YYYY
      d1 = parseDateString(safeDate1);
    } else if (safeDate1.includes("-")) {
      // รูปแบบ YYYY-MM-DD
      const parts = safeDate1.split("-");
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

  if (safeDate2 instanceof Date) {
    d2 = new Date(safeDate2);
    d2.setHours(0, 0, 0, 0);
  } else if (typeof safeDate2 === "string") {
    // ตรวจสอบรูปแบบของสตริง
    if (safeDate2.includes("/")) {
      // รูปแบบ DD/MM/YYYY
      d2 = parseDateString(safeDate2);
    } else if (safeDate2.includes("-")) {
      // รูปแบบ YYYY-MM-DD
      const parts = safeDate2.split("-");
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

// เพิ่มฟังก์ชันสำหรับจัดการการเปิด DatePicker ของวันที่สิ้นสุด
const handleEndDatePickerOpen = (isOpen: boolean) => {
  if (isOpen && !filters.value.startDate) {
    // ถ้าผู้ใช้พยายามเปิด datepicker ของวันที่สิ้นสุดโดยที่ยังไม่ได้เลือกวันที่เริ่มต้น
    startDateError.value = true; // แสดงข้อผิดพลาดที่วันที่เริ่มต้น
    isEndDatePickerOpen.value = false; // ไม่เปิด datepicker ของวันที่สิ้นสุด
  } else {
    isEndDatePickerOpen.value = isOpen; // เปิดหรือปิด datepicker ตามปกติ
  }
};

const filteredPayments = computed(() => {
  if (!expense.value) return [];

  // Log การกรองข้อมูล
  console.log(
    "Filtering payments with filters:",
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
    console.log(
      "Start date formatted (YYYY-MM-DD พุทธศักราช):",
      formatToBuddhistYYYYMMDD(lastSearchedFilters.value.startDate)
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
    console.log(
      "End date formatted (YYYY-MM-DD พุทธศักราช):",
      formatToBuddhistYYYYMMDD(lastSearchedFilters.value.endDate)
    );
  }

  return expense.value.filter((item) => {
    // กรองตามชื่อผู้ใช้
    const matchesSearch =
      !lastSearchedFilters.value.searchQuery ||
      (item.rqUsrName &&
        item.rqUsrName
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

    if (lastSearchedFilters.value.startDate && item.rqWithdrawDate) {
      // เปรียบเทียบวันที่เริ่มต้นกับวันที่ในข้อมูล
      matchesStartDate =
        compareDates(
          item.rqWithdrawDate,
          lastSearchedFilters.value.startDate
        ) >= 0;
      console.log(
        `Start date check: "${item.rqWithdrawDate}" >= "${lastSearchedFilters.value.startDate}" = ${matchesStartDate}`
      );
    }

    if (lastSearchedFilters.value.endDate && item.rqWithdrawDate) {
      // เปรียบเทียบวันที่สิ้นสุดกับวันที่ในข้อมูล
      matchesEndDate =
        compareDates(item.rqWithdrawDate, lastSearchedFilters.value.endDate) <=
        0;
      console.log(
        `End date check: "${item.rqWithdrawDate}" <= "${lastSearchedFilters.value.endDate}" = ${matchesEndDate}`
      );
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
  if (!expense.value) return [];

  // ดึงชื่อโครงการที่ไม่ซ้ำกัน
  const uniqueProjects = new Map();

  expense.value.forEach((item) => {
    if (item.rqPjName) {
      uniqueProjects.set(item.rqPjName, {
        pjId: item.rqPjName,
        pjName: item.rqPjName,
      });
    }
  });

  return Array.from(uniqueProjects.values());
});

const extractedRequisitionTypes = computed(() => {
  if (!expense.value) return [];

  // ดึงประเภทการเบิกที่ไม่ซ้ำกัน
  const uniqueTypes = new Map();

  expense.value.forEach((item) => {
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

  // ล้างสถานะข้อผิดพลาด
  startDateError.value = false;
};

// Date handlers
const confirmStartDate = (date: Date) => {
  filters.value.startDate = date;
  startDateError.value = false; // ล้างข้อผิดพลาดเมื่อเลือกวันที่เริ่มต้นแล้ว
  console.log("Start date confirmed (คริสต์ศักราช):", date);
  console.log(
    "Start date converted to คริสต์ศักราช (YYYY-MM-DD):",
    formatToChristianYYYYMMDD(date)
  );
  console.log(
    "Start date converted to พุทธศักราช (YYYY-MM-DD):",
    formatToBuddhistYYYYMMDD(date)
  );
};

const confirmEndDate = (date: Date) => {
  filters.value.endDate = date;
  console.log("End date confirmed (คริสต์ศักราช):", date);
  console.log(
    "End date converted to คริสต์ศักราช (YYYY-MM-DD):",
    formatToChristianYYYYMMDD(date)
  );
  console.log(
    "End date converted to พุทธศักราช (YYYY-MM-DD):",
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

// เมื่อ Component ถูก Mounted ให้ดึงข้อมูลรายการรอนำจ่าย
onMounted(async () => {
  loading.value = true;

  try {
    await paymentStore.getAllPaymentList();

    // อัปเดตข้อมูลสำหรับตัวกรอง
    projects.value = extractedProjects.value;
    requisitionTypes.value = extractedRequisitionTypes.value;
  } catch (error) {
    console.error("Error fetching payment list:", error);
  } finally {
    // รอสักครู่ก่อนปิด loading เพื่อให้มั่นใจว่า UI ได้ render แล้ว
    setTimeout(() => {
      loading.value = false;
    }, 500);
  }
});

const toDetails = (id: string) => {
  router.push(`/payment/list/detail/${id}`);
};
</script>
<!-- path for test = /payment/List -->
<template>
  <!-- content -->
  <div>
    <!-- Filter -->
    <div
      class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-4"
    >
      <!-- ค้นหา -->
      <UserSearchInput
        v-model="filters.searchQuery"
        :loading="loading"
        label="ค้นหา"
      />

      <!-- โครงการ -->
      <ProjectFilter
        v-model="filters.project"
        :projects="projects"
        :loading="loading"
      />

      <!-- ประเภทค่าใช้จ่าย -->
      <RequisitionTypeFilter
        v-model="filters.requisitionType"
        :requisition-types="requisitionTypes"
        :loading="loading"
      />

      <!-- วันที่เริ่มต้นขอเบิก -->
      <DateFilter
        v-model="startDateTemp"
        :loading="loading"
        label="วันที่เริ่มต้นขอเบิก"
        :is-open="isStartDatePickerOpen"
        @update:is-open="isStartDatePickerOpen = $event"
        :confirmed-date="filters.startDate"
        @confirm="confirmStartDate"
        @cancel="cancelStartDate"
        :error="startDateError"
      />

      <!-- วันที่สิ้นสุดขอเบิก -->
      <div class="flex flex-col">
        <DateFilter
          v-model="endDateTemp"
          :loading="loading"
          label="วันที่สิ้นสุดขอเบิก"
          :is-open="isEndDatePickerOpen"
          @update:is-open="handleEndDatePickerOpen"
          :confirmed-date="filters.endDate"
          @confirm="confirmEndDate"
          @cancel="cancelEndDate"
          :min-date="filters.startDate"
          class="mb-4"
        />

        <!-- ปุ่มค้นหาและรีเซ็ต -->
        <FilterButtons
          :loading="loading"
          @reset="handleReset"
          @search="handleSearch"
        />
      </div>
    </div>

    <!-- Table -->
    <div
      class="w-full h-fit border-[2px] flex flex-col items-start border-[#BBBBBB]"
    >
      <!-- ตาราง -->
      <Ctable :table="'Table7-head'" />
      <table class="w-full text-center text-black table-auto">
        <tbody>
          <tr v-if="loading">
            <td colspan="100%" class="py-4 text-center">
              <div class="flex justify-center items-center">
                <div
                  class="animate-spin rounded-full h-8 w-8 border-b-2 border-black"
                ></div>
                <span class="ml-2">กำลังโหลดข้อมูล...</span>
              </div>
            </td>
          </tr>

          <tr
            v-else-if="!expense?.length || filteredPayments.length === 0"
            v-for="n in 10"
            :key="n"
            class="h-[50px]"
          >
            <td colspan="100%" class="py-4 text-center">
              <span v-if="n === 5">
                {{
                  !expense?.length
                    ? "ไม่มีข้อมูลรายการรอนำจ่าย"
                    : "ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา"
                }}
              </span>
            </td>
          </tr>

          <tr
            v-else
            v-for="(paymentItem, index) in paginated"
            :key="paymentItem ? paymentItem.rqId : `empty-${index}`"
            :class="
              paymentItem
                ? 'text-[14px] h-[46px] border-b-2 border-[#BBBBBB] hover:bg-gray-50'
                : 'h-[50px]'
            "
          >
            <template v-if="paymentItem">
              <th class="py-3 px-2 w-12 h-[46px]">
                {{ index + 1 + (currentPage - 1) * itemsPerPage }}
              </th>
              <th
                class="py-3 px-2 text-start w-1/4"
                style="
                  max-width: 200px;
                  white-space: nowrap;
                  text-overflow: ellipsis;
                  overflow: hidden;
                "
              >
                {{ paymentItem.rqUsrName }}
              </th>
              <th
                class="py-3 px-2 w-44 text-start"
                style="
                  max-width: 200px;
                  white-space: nowrap;
                  text-overflow: ellipsis;
                  overflow: hidden;
                "
              >
                {{ paymentItem.rqName }}
              </th>
              <th
                class="py-3 px-2 w-44 text-start"
                style="
                  max-width: 200px;
                  white-space: nowrap;
                  text-overflow: ellipsis;
                  overflow: hidden;
                "
              >
                {{ paymentItem.rqPjName }}
              </th>
              <th class="py-3 px-2 w-32 text-start">
                {{ paymentItem.rqRqtName }}
              </th>
              <th class="py-3 px-2 w-24 text-start">
                {{ paymentItem.rqWithdrawDate }}
              </th>
              <th class="py-3 px-2 w-32 text-end">
                {{
                  new Decimal(paymentItem.rqExpenses ?? 0)
                    .toNumber()
                    .toLocaleString("en-US", {
                      minimumFractionDigits: 2,
                      maximumFractionDigits: 2,
                    })
                }}
              </th>
              <th class="py-3 px-2 w-20 text-center">
                <span
                  class="flex justify-center cursor-pointer hover:text-[#B67D12]"
                  v-on:click="toDetails(paymentItem.rqId)"
                >
                  <Icon :icon="'viewDetails'" />
                </span>
              </th>
            </template>
            <template v-else>
              <td>&nbsp;</td>
            </template>
          </tr>
        </tbody>
        <Pagination
          :currentPage="currentPage"
          :totalPages="totalPages"
          :columnNumber="columnNumber"
          @update:currentPage="(page) => (currentPage = page)"
        />
      </table>
    </div>
  </div>
  <!-- content -->
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
