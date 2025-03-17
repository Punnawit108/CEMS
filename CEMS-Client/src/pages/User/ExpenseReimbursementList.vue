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
import RequisitionSearchInput from "../../components/Filters/RequisitionSearchInput.vue";
import ProjectFilter from "../../components/Filters/ProjectFilter.vue";
import RequisitionTypeFilter from "../../components/Filters/RequisitionTypeFilter.vue";
import DateFilter from "../../components/Filters/DateFilter.vue";
import FilterButtons from "../../components/Filters/FilterButtons.vue";
import Pagination from '../../components/Pagination.vue';

const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalPages = computed(() => {
  return Math.ceil(filteredList.value.length / itemsPerPage.value);
});

const paginated = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage.value;
  const end = start + itemsPerPage.value;
  return filteredList.value.slice(start, end);
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

const handleClick = () => {
  if (lockStore.isLocked) {
    alert("ไม่สามารถทำรายการเบิกได้ในขณะนี้");
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

    // ตรวจสอบวันที่ด้วยการเปรียบเทียบสตริง YYYY-MM-DD แบบพุทธศักราช
    let matchesStartDate = true;
    let matchesEndDate = true;

    if (lastSearchedFilters.value.startDate && item.rqWithDrawDate) {
      // แปลงวันที่จาก DatePicker (คริสต์ศักราช) เป็นรูปแบบ YYYY-MM-DD แบบพุทธศักราช
      const startDateStr = formatToBuddhistYYYYMMDD(
        lastSearchedFilters.value.startDate
      );

      // เปรียบเทียบกับวันที่ในฐานข้อมูล (ซึ่งเป็นพุทธศักราช)
      matchesStartDate = item.rqWithDrawDate >= startDateStr;

      // Debug
      console.log(
        `เปรียบเทียบ "${item.rqWithDrawDate}" >= "${startDateStr}" = ${matchesStartDate}`
      );
    }

    if (lastSearchedFilters.value.endDate && item.rqWithDrawDate) {
      // แปลงวันที่จาก DatePicker (คริสต์ศักราช) เป็นรูปแบบ YYYY-MM-DD แบบพุทธศักราช
      const endDateStr = formatToBuddhistYYYYMMDD(
        lastSearchedFilters.value.endDate
      );

      // เปรียบเทียบกับวันที่ในฐานข้อมูล (ซึ่งเป็นพุทธศักราช)
      matchesEndDate = item.rqWithDrawDate <= endDateStr;

      // Debug
      console.log(
        `เปรียบเทียบ "${item.rqWithDrawDate}" <= "${endDateStr}" = ${matchesEndDate}`
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

// Date handlers
const confirmStartDate = (date: Date) => {
  filters.value.startDate = date;
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
    <div class="mr-6 items-end flex justify-end mb-4">
      <RouterLink
        to="/disbursement/listWithdraw/createExpenseForm"
        v-if="!lockStore.isLocked"
      >
        <Button :type="'btn-expense'" @click="handleClick"></Button>
      </RouterLink>
    </div>

    <div
      class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-8"
    >
      <!-- ค้นหา -->
      <RequisitionSearchInput
        v-model="filters.searchQuery"
        :loading="loading"
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
      />

      <!-- วันที่สิ้นสุดขอเบิก -->
      <div class="flex flex-col">
        <DateFilter
          v-model="endDateTemp"
          :loading="loading"
          label="วันที่สิ้นสุดขอเบิก"
          :is-open="isEndDatePickerOpen"
          @update:is-open="isEndDatePickerOpen = $event"
          :confirmed-date="filters.endDate"
          @confirm="confirmEndDate"
          @cancel="cancelEndDate"
          class="mb-2"
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
    <div class="w-full border-r-[2px] border-l-[2px] border-t-[2px] mt-5">
      <Ctable :table="'Table9-head-New'" />
      <table class="table-auto w-full text-center text-black">
        <tbody>
          <tr v-if="loading">
            <td colspan="8" class="py-4">
              <div class="flex justify-center items-center">
                <div
                  class="animate-spin rounded-full h-8 w-8 border-b-2 border-[#B67D12]"
                ></div>
                <span class="ml-2">กำลังโหลดข้อมูล...</span>
              </div>
            </td>
          </tr>

          <tr v-else-if="!expenseReimbursementList?.length">
            <td colspan="8" class="py-4">ไม่มีข้อมูลรายการเบิกค่าใช้จ่าย</td>
          </tr>

          <tr v-else-if="filteredList.length === 0">
            <td colspan="8" class="py-4">
              ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา
            </td>
          </tr>

          <tr
            v-else
            v-for="(expenseReimbursementItem, index) in paginated"
            :key="expenseReimbursementItem.rqId"
            class="text-[14px] border-b-2 border-[#BBBBBB] hover:bg-gray-50"
          >
            <th class="py-[12px] px-2 w-14">{{ index + 1 + (currentPage - 1) * itemsPerPage  }}</th>
            <th
              class="py-[12px] px-2 w-48 text-start truncate overflow-hidden"
              style="
                max-width: 240px;
                white-space: nowrap;
                text-overflow: ellipsis;
                overflow: hidden;
              "
              :title="expenseReimbursementItem.rqName"
            >
              {{ expenseReimbursementItem.rqName }}
            </th>
            <th
              class="py-[12px] px-2 w-48 text-start truncate overflow-hidden"
              style="
                max-width: 240px;
                white-space: nowrap;
                text-overflow: ellipsis;
                overflow: hidden;
              "
              :title="expenseReimbursementItem.rqPjName"
            >
              {{ expenseReimbursementItem.rqPjName }}
            </th>
            <th class="py-[12px] px-5 w-32 text-start font-[100]">
              {{ expenseReimbursementItem.rqRqtName }}
            </th>
            <th class="py-[12px] px-2 w-20 text-end">
              {{ expenseReimbursementItem.rqWithDrawDate }}
            </th>
            <th class="py-[12px] px-5 w-32 text-end">
              {{
                new Decimal(expenseReimbursementItem.rqExpenses ?? 0).toFixed(2)
              }}
            </th>
            <th class="py-[12px] px-2 w-28 text-center">
              <span>
                <StatusBudge
                  :status="'sts-' + expenseReimbursementItem.rqStatus"
                />
              </span>
            </th>
            <th class="py-[10px] px-2 w-20 text-center">
              <span class="flex justify-center">
                <Icon
                  :icon="'viewDetails'"
                  v-on:click="toDetails(expenseReimbursementItem.rqId)"
                  class="cursor-pointer hover:text-[#B67D12]"
                />
                <Icon
                  v-if="expenseReimbursementItem.rqStatus === 'sketch'"
                  :icon="'bin'"
                  @click="openConfirmationModal(expenseReimbursementItem.rqId)"
                  class="cursor-pointer hover:text-red-500"
                />
              </span>
            </th>
          </tr>
        </tbody>
        <Pagination :currentPage="currentPage" :totalPages="totalPages"
        @update:currentPage="(page) => (currentPage = page)" />
      </table>
    </div>

    <!-- Modal for delete confirmation -->
    <div
      v-if="showModal"
      class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50"
    >
      <div class="modal-box bg-white w-[460px] h-[295px] rounded-lg shadow-lg">
        <div class="flex justify-center mt-[25px]">
          <svg
            width="70px"
            height="70px"
            viewBox="0 0 24 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
            <g
              id="SVGRepo_tracerCarrier"
              stroke-linecap="round"
              stroke-linejoin="round"
            ></g>
            <g id="SVGRepo_iconCarrier">
              <path
                d="M12 2C6.4898 2 2 6.4898 2 12C2 17.5102 6.4898 22 12 22C17.5102 22 22 17.5102 22 12C22 6.4898 17.5102 2 12 2ZM11.1837 8.42857C11.1837 8.02041 11.4898 7.61225 12 7.61225C12.5102 7.61225 12.8163 7.91837 12.8163 8.42857V12.5102C12.8163 12.9184 12.5102 13.3265 12 13.3265C11.4898 13.3265 11.1837 13.0204 11.1837 12.5102V8.42857ZM12 16.5918C11.4898 16.5918 10.9796 16.0816 10.9796 15.5714C10.9796 15.0612 11.4898 14.551 12 14.551C12.5102 14.551 13.0204 15.0612 13.0204 15.5714C13.0204 16.0816 12.5102 16.5918 12 16.5918Z"
                fill="#FFBE40"
              ></path>
            </g>
          </svg>
        </div>
        <p class="text-2xl font-bold text-black mt-1 flex justify-center">
          ยืนยันการลบคำขอเบิกค่าใช้จ่าย
        </p>
        <p class="text-lg font-bold text-[#B6B7BA] mt-1 flex justify-center">
          คุณยืนยันการลบคำขอเบิกค่าใช้จ่ายหรือไม่?
        </p>
        <div class="modal-action flex justify-center mt-6">
          <form method="dialog">
            <button
              @click="closeModal"
              class="bg-white border-solid border-[#B6B7BA] border-2 rounded px-7 py-2 text-[#B6B7BA] text-sm font-normal mr-3"
            >
              ยกเลิก
            </button>
            <button
              @click="confirmDelete"
              class="bg-[#12B669] border-solid border-[#12B669] border-2 rounded px-7 py-2 text-white text-sm font-normal"
            >
              ยืนยัน
            </button>
          </form>
        </div>
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
