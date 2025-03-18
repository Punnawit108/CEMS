<script setup lang="ts">
/*
 * ชื่อไฟล์: ExpenseReport.vue
 * คำอธิบาย: ไฟล์นี้แสดงรายงานของคำขอเบิกค่าใช้จ่ายทั้งหมดในระบบ
 * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
 * วันที่จัดทำ/แก้ไข: 11 มีนาคม 2568
 */
import Icon from "../../components/Icon/CIcon.vue";
import { onMounted, ref, computed, watch } from "vue";
import ChartDataLabels from "chartjs-plugin-datalabels";
import Ctable from "../../components/Table/CTable.vue";
import {
  useExpensesListStore,
  useExpensesGraphStore,
} from "../../store/expensesReport";
import ExpenseReportGraph from "../../types/index";
import { useExportExpenseReportStore } from "../../store/exportExpenseReport";
import Button from "../../components/Buttons/Button.vue";
import Decimal from "decimal.js";
import { storeToRefs } from "pinia";
import Pagination from "../../components/Pagination.vue";
import { useRouter } from "vue-router";

const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalPages = computed(() => {
  return Math.ceil(filteredExpenses.value.length / itemsPerPage.value);
});

const paginated = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage.value;
  const end = start + itemsPerPage.value;
  const pageItems = filteredExpenses.value.slice(start, end);

  // Add empty rows if fewer than 10 items
  while (pageItems.length < itemsPerPage.value) {
    pageItems.push(null);
  }

  return pageItems;
});

// Import filters
import UserSearchInput from "../../components/filters/UserSearchInput.vue";
import ProjectFilter from "../../components/Filters/ProjectFilter.vue";
import RequisitionTypeFilter from "../../components/Filters/RequisitionTypeFilter.vue";
import DateFilter from "../../components/Filters/DateFilter.vue";
import FilterButtons from "../../components/Filters/FilterButtons.vue";

import {
  Chart,
  BarController,
  BarElement,
  PieController,
  ArcElement,
  Legend,
  Tooltip,
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  Title,
  CategoryScale,
} from "chart.js";

// Register Chart.js components, including for the bar chart
Chart.register(
  BarController,
  BarElement,
  PieController,
  ArcElement,
  Legend,
  Tooltip,
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  Title,
  CategoryScale,
  ChartDataLabels
);

const router = useRouter();

const toDetails = (id: string) => {
  router.push(`/disbursement/listWithdraw/detail/${id}`);
};

// ตัวแปรแสดง/ซ่อน Modal
const showModal = ref(false);
const selectedType = ref<string | null>(null);
const exportReportStore = useExportExpenseReportStore();
const loading = ref(false);

// Stores
const expensesListStore = useExpensesListStore();
const expensesGraphStore = useExpensesGraphStore();
const { expenses } = storeToRefs(expensesListStore);

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
const exportFilteredFile = async (fileType: string) => {
  // ตรวจสอบว่ามีการกรอกฟิลเตอร์หรือไม่
  const filters = {
    searchQuery: lastSearchedFilters.value.searchQuery,
    project: lastSearchedFilters.value.project,
    requisitionType: lastSearchedFilters.value.requisitionType,
    startDate: lastSearchedFilters.value.startDate ? lastSearchedFilters.value.startDate.toISOString() : undefined,
    endDate: lastSearchedFilters.value.endDate ? lastSearchedFilters.value.endDate.toISOString() : undefined,
  };

  // เรียกใช้งาน store เพื่อส่งค่าฟิลเตอร์ไปพร้อมกับการส่งออกไฟล์
  try {
    const exportStore = useExportExpenseReportStore();
    await exportStore.exportFile(fileType, filters);
  } catch (error) {
    console.error("Error during file export:", error);
  }
};
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

const filteredExpenses = computed(() => {
  if (!expenses.value) return [];

  // Log การกรองข้อมูล
  console.log(
    "Filtering expenses with filters:",
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

  return expenses.value.filter((item) => {
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

    // ตรวจสอบวันที่ด้วยการเปรียบเทียบสตริง YYYY-MM-DD แบบพุทธศักราช
    let matchesStartDate = true;
    let matchesEndDate = true;

    if (lastSearchedFilters.value.startDate && item.rqPayDate) {
      // แปลงวันที่จาก DatePicker (คริสต์ศักราช) เป็นรูปแบบ YYYY-MM-DD แบบพุทธศักราช
      const startDateStr = formatToBuddhistYYYYMMDD(
        lastSearchedFilters.value.startDate
      );

      // เปรียบเทียบกับวันที่ในฐานข้อมูล (ซึ่งเป็นพุทธศักราช)
      matchesStartDate = item.rqPayDate >= startDateStr;

      // Debug
      console.log(
        `เปรียบเทียบ "${item.rqPayDate}" >= "${startDateStr}" = ${matchesStartDate}`
      );
    }

    if (lastSearchedFilters.value.endDate && item.rqPayDate) {
      // แปลงวันที่จาก DatePicker (คริสต์ศักราช) เป็นรูปแบบ YYYY-MM-DD แบบพุทธศักราช
      const endDateStr = formatToBuddhistYYYYMMDD(
        lastSearchedFilters.value.endDate
      );

      // เปรียบเทียบกับวันที่ในฐานข้อมูล (ซึ่งเป็นพุทธศักราช)
      matchesEndDate = item.rqPayDate <= endDateStr;

      // Debug
      console.log(
        `เปรียบเทียบ "${item.rqPayDate}" <= "${endDateStr}" = ${matchesEndDate}`
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
  if (!expenses.value) return [];

  // ดึงชื่อโครงการที่ไม่ซ้ำกัน
  const uniqueProjects = new Map();

  expenses.value.forEach((item) => {
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
  if (!expenses.value) return [];

  // ดึงประเภทการเบิกที่ไม่ซ้ำกัน
  const uniqueTypes = new Map();

  expenses.value.forEach((item) => {
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

const handleExport = (type: string) => {
  selectedType.value = type; // อัปเดตประเภทที่เลือก
};

const exportFile = async () => {
  if (!selectedType.value) return;

  // กำหนดค่าฟิลเตอร์ที่จะส่งไป
  const filters = {
    searchQuery: lastSearchedFilters.value.searchQuery,
    project: lastSearchedFilters.value.project,
    requisitionType: lastSearchedFilters.value.requisitionType,
    startDate: lastSearchedFilters.value.startDate,
    endDate: lastSearchedFilters.value.endDate,
  };

  try {
    // ส่งฟิลเตอร์ไปยัง store
    await exportReportStore.exportFile(selectedType.value, filters);

    selectedType.value = null;
    showModal.value = false;
  } catch (error) {
    console.error("Error exporting file:", error);
    if (selectedType.value) {
      alert(
        `เกิดข้อผิดพลาดในการส่งออกไฟล์ ${selectedType.value.toUpperCase()}`
      );
    }
  }
};

// Bar chart setup
// ประเภทค่าใช้จ่าย
const expense: string[] = [];

// จำนวนเงินของแต่ละประเภทค่าใช้จ่าย
const amountMoney: number[] = [];

// แกน X Title
const customXAxisTitle = {
  id: "customXAxisTitle",
  afterDraw: (chart: any) => {
    const { ctx, chartArea } = chart;
    const xPos = chartArea.right + 120;
    const yPos = chartArea.bottom;

    ctx.save();
    ctx.textAlign = "right";
    ctx.textBaseline = "middle";
    ctx.font = "14px Sarabun";
    ctx.fillStyle = "#000";
    ctx.fillText("ประเภทค่าใช้จ่าย", xPos, yPos);
    ctx.restore();
  },
};

// แกน Y Title
const customYAxisTitle = {
  id: "customYAxisTitle",
  afterDraw: (chart: any) => {
    const { ctx, chartArea } = chart;
    const xPos = chartArea.left + 15;
    const yPos = chartArea.top - 50;

    ctx.save();
    ctx.textAlign = "right";
    ctx.textBaseline = "middle";
    ctx.font = "14px Sarabun";
    ctx.fillStyle = "#000";
    ctx.fillText("จำนวนเงิน (บาท)", xPos, yPos);
    ctx.restore();
  },
};

onMounted(async () => {
  loading.value = true;

  try {
    await expensesListStore.getAllExpenses();
    await expensesGraphStore.getAllExpenses();

    // อัปเดตข้อมูลสำหรับตัวกรอง
    projects.value = extractedProjects.value;
    requisitionTypes.value = extractedRequisitionTypes.value;

    expensesGraphStore.expensegraph.forEach((item: ExpenseReportGraph) => {
      expense.push(item.rqRqtName);
      amountMoney.push(item.rqSumExpenses);
    });
  } catch (error) {
    console.error("Error fetching expenses:", error);
  } finally {
    // รอสักครู่ก่อนปิด loading เพื่อให้มั่นใจว่า UI ได้ render แล้ว
    setTimeout(() => {
      loading.value = false;
    }, 500);
  }

  const barchart = document.getElementById("barChart") as HTMLCanvasElement;
  if (barchart) {
    new Chart(barchart, {
      type: "bar",
      data: {
        labels: expense,
        datasets: [
          {
            label: "จำนวนเงิน (บาท)",
            data: amountMoney,
            backgroundColor: "#C81C1B",
            barPercentage: 0.2, // ความหนาของแท่งกราฟ
            datalabels: {
              display: false, // ช่อนข้อมูลของ "จำนวนเงิน (บาท)" ที่ขึ้นบนแท่งกราฟ
            },
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        layout: {
          padding: {
            top: 60,
            left: 30,
            right: 120,
          },
        },
        scales: {
          x: {
            ticks: {
              color: "#000",
              font: {
                size: 14,
                family: "Sarabun",
              },
            },
            grid: {
              display: false,
            },
          },
          y: {
            beginAtZero: true, // แกน y เริ่มที่ 0
            ticks: {
              color: "#000",
              font: {
                size: 14,
                family: "Sarabun",
              },
              stepSize: 10000, // ค่าแกน y เพิ่มที่ละตามจำนวนที่ตั้ง
            },
            border: {
              display: false, // ลบเส้นแรกของแกน y
            },
          },
        },
        plugins: {
          legend: {
            display: false, // ซ่อนชนิดของแท่งกราฟ
          },
          tooltip: {
            callbacks: {
              label: (tooltipItem) => {
                const value = Number(tooltipItem.raw);
                const formatted = value.toLocaleString("en-US", {
                  minimumFractionDigits: 2,
                  maximumFractionDigits: 2,
                });
                return `จำนวนเงิน: ${formatted} บาท`;
              },
            },
            titleFont: {
              family: "Sarabun",
              size: 14,
            },
            bodyFont: {
              family: "Sarabun",
              size: 14,
            },
            footerFont: {
              family: "Sarabun",
              size: 14,
            },
          },
        },
      },
      plugins: [customXAxisTitle, customYAxisTitle],
    });
  } else {
    console.error("Canvas element for bar chart not found");
  }
});
</script>

<template>
  <div>
    <!-- begin::Filter -->
    <div class="relative w-full mb-6">
      <Button :type="'btn-print2'" @click="showModal = true"
        class="absolute right-0 transform -translate-y-1/2 top-1/2">
        ส่งออก
      </Button>
    </div>
    <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-8">
      <!-- ค้นหาชื่อผู้ใช้ -->
      <UserSearchInput v-model="filters.searchQuery" :loading="loading" label="ค้นหาชื่อผู้ใช้" />
      <!-- โครงการ -->
      <ProjectFilter v-model="filters.project" :projects="projects" :loading="loading" />
      <!-- ประเภทค่าใช้จ่าย -->
      <RequisitionTypeFilter v-model="filters.requisitionType" :requisition-types="requisitionTypes"
        :loading="loading" />
      <!-- วันที่เริ่มต้นขอเบิก -->
      <DateFilter v-model="startDateTemp" :loading="loading" label="วันที่เริ่มต้นขอเบิก"
        :is-open="isStartDatePickerOpen" @update:is-open="isStartDatePickerOpen = $event"
        :confirmed-date="filters.startDate" @confirm="confirmStartDate" @cancel="cancelStartDate" />
      <!-- วันที่สิ้นสุดขอเบิก -->
      <div class="flex flex-col">
        <DateFilter v-model="endDateTemp" :loading="loading" label="วันที่สิ้นสุดขอเบิก" :is-open="isEndDatePickerOpen"
          @update:is-open="isEndDatePickerOpen = $event" :confirmed-date="filters.endDate" @confirm="confirmEndDate"
          @cancel="cancelEndDate" class="mb-2" />
        <!-- ปุ่มค้นหาและรีเซ็ต -->
        <FilterButtons :loading="loading" @reset="handleReset" @search="handleSearch" />
      </div>
    </div>
    <!-- end::Filter -->

    <!-- begin::Content -->
    <div class="flex flex-col items-center justify-center">
      <!-- begin::Bar chart -->
      <div class="flex flex-col items-center h-[500px] w-[1240px] mb-5">
        <p class="mb-10 font-bold text-center text-black">
          ยอดการเบิกของค่าใช้จ่ายแต่ละประเภท
        </p>
        <div class="w-3/4 h-full">
          <canvas id="barChart"></canvas>
        </div>
      </div>
      <!-- end::Bar chart -->

      <!-- begin::Table -->
      <div class="w-full h-fit border-[2px] flex flex-col items-start">
        <!-- Table Header -->
        <Ctable :table="'Table7-head'" />
        <!-- Table Data -->
        <table class="w-full text-center text-black table-auto">
          <tbody>
            <tr v-if="loading">
              <td colspan="8" class="py-4">
                <div class="flex justify-center items-center">
                  <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
                  <span class="ml-2">กำลังโหลดข้อมูล...</span>
                </div>
              </td>
            </tr>
            <tr v-else-if="!expenses?.length">
              <td colspan="8" class="py-4">ไม่มีข้อมูลรายการเบิกค่าใช้จ่าย</td>
            </tr>
            <tr v-else-if="filteredExpenses.length === 0">
              <td colspan="8" class="py-4">ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา</td>
            </tr>
            <tr v-else v-for="(expense, index) in paginated" :key="expense ? expense.rqId : `empty-${index}`"
              :class="expense ? 'text-[14px] border-b-2 border-[#BBBBBB] hover:bg-gray-50' : ''">
              <template v-if="expense">
                <th class="py-3 px-2 w-14 h-[46px]">{{ index + 1 + (currentPage - 1) * itemsPerPage }}</th>
                <th class="py-3 px-2 text-start w-56 truncate overflow-hidden"
                  style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                  :title="expense.rqUsrName">
                  {{ expense.rqUsrName }}
                </th>
                <th class="py-3 px-2 text-start w-56 truncate overflow-hidden"
                  style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                  :title="expense.rqName">
                  {{ expense.rqName }}
                </th>
                <th class="py-3 px-2 text-start w-56 truncate overflow-hidden"
                  style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                  :title="expense.rqPjName">
                  {{ expense.rqPjName }}
                </th>
                <th class="py-3 px-5 text-start w-44">
                  {{ expense.rqRqtName }}
                </th>
                <th class="py-3 px-2 text-start w-24">
                  {{ expense.rqPayDate }}
                </th>
                <th class="py-3 px-2 text-end w-40">
                  {{
                    new Decimal(expense.rqExpenses ?? 0).toNumber().toLocaleString("en-US", {
                      minimumFractionDigits: 2,
                      maximumFractionDigits: 2,
                    })
                  }}
                </th>
                <th class="py-[10px] px-2 w-32 text-center">
                  <span class="flex justify-center">
                    <Icon :icon="'viewDetails'" @click="toDetails(expense.rqId.toString())"
                      class="cursor-pointer hover:text-[#B67D12]" />
                  </span>
                </th>
              </template>
              <template v-else>
                <td class="py-3">&nbsp;</td>
              </template>
            </tr>
          </tbody>
          <Pagination :currentPage="currentPage" :totalPages="totalPages"
            @update:currentPage="(page) => (currentPage = page)" />
        </table>
      </div>
    </div>
    <!-- end::Content -->

    <!-- Modal for export -->
    <div v-if="showModal" class="fixed inset-0 flex items-center justify-center bg-gray-800 bg-opacity-50 z-50">
      <div class="p-6 bg-white rounded-lg shadow-2xl w-96">
        <h2 class="mb-6 text-lg font-bold text-gray-700"></h2>
        <div>
          <div class="flex justify-center space-x-6">
            <!-- ปุ่ม PDF -->
            <button @click="handleExport('pdf')"
              :class="['px-5 py-3 rounded-lg flex items-center justify-center transition-colors duration-200', selectedType === 'pdf' ? 'bg-blue-500 text-white' : 'bg-gray-100 hover:bg-gray-200']">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="w-8 h-8 mr-2">
                <path
                  d="M6 2a1 1 0 00-1 1v18a1 1 0 001 1h12a1 1 0 001-1V8.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0013.586 2H6zm7 2.414L18.586 10H13V4.414zM8 12h2v6H8v-6zm3 0h1.5c.828 0 1.5.672 1.5 1.5v3a1.5 1.5 0 01-1.5 1.5H11v-6zm3 0h2.5v6H14v-6z" />
              </svg>
            </button>
            <!-- ปุ่ม XLSX -->
            <button @click="handleExport('xlsx')"
              :class="['px-5 py-3 rounded-lg flex items-center justify-center transition-colors duration-200', selectedType === 'xlsx' ? 'bg-green-500 text-white' : 'bg-gray-100 hover:bg-gray-200']">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="w-8 h-8 mr-2">
                <path
                  d="M6 2a1 1 0 00-1 1v18a1 1 0 001 1h12a1 1 0 001-1V8.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0013.586 2H6zm7 2.414L18.586 10H13V4.414zM9 14h1.5l.75 1.5.75-1.5H14v4h-1.5v-1.5l-.75 1.5-.75-1.5V18H9v-4z" />
              </svg>
            </button>
          </div>
          <div class="flex justify-center mb-6 space-x-20">
            <span class="mt-2 text-sm text-gray-600">PDF</span>
            <span class="mt-2 text-sm text-gray-600">XLSX</span>
          </div>
          <div class="flex justify-center space-x-4">
            <button @click="showModal = false" class="px-6 py-3 bg-gray-300 rounded-lg hover:bg-gray-400">
              ยกเลิก
            </button>
            <button @click="exportFile" :disabled="!selectedType"
              class="px-6 py-3 text-white bg-blue-500 rounded-lg hover:bg-blue-600 disabled:bg-gray-300">
              ยืนยัน
            </button>
          </div>
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

@media screen and (-webkit-min-device-pixel-ratio: 0) {
  .custom-select {
    background-image: url("data:image/svg+xml;utf8,<svg fill='transparent' height='24' viewBox='0 0 24 24' width='24' xmlns='http://www.w3.org/2000/svg'><path d='M7 10l5 5 5-5z'/></svg>");
    background-repeat: no-repeat;
    background-position-x: 100%;
    background-position-y: 5px;
  }
}
</style>