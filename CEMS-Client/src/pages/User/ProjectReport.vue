<script setup lang="ts">
/*
 * ชื่อไฟล์: ProjectReport.vue
 * คำอธิบาย: ไฟล์นี้แสดงรายงานของ Project
 * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
 * วันที่จัดทำ/แก้ไข: 11 มีนาคม 2568
 */
import { onMounted, ref, computed } from "vue";
import ChartDataLabels from "chartjs-plugin-datalabels";
import Ctable from "../../components/Table/CTable.vue";
import { useProjectsStore } from "../../store/projectsReport";
import Button from "../../components/Buttons/Button.vue";
import ProjectReport from "../../types/index";
import Decimal from "decimal.js";
import { useExportProjectReportStore } from "../../store/exportProjectReport";
import Pagination from "../../components/Pagination.vue";
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
import { storeToRefs } from "pinia";

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

const projectsStore = useProjectsStore();
const showModal = ref(false);
const selectedType = ref<string | null>(null);
const exportProjectReportStore = useExportProjectReportStore();
const { projects } = storeToRefs(projectsStore);
const loading = ref(false);

const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalPages = computed(() => {
  return Math.ceil(projects.value.length / itemsPerPage.value);
});

const paginated = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage.value;
  const end = start + itemsPerPage.value;
  const pageItems = projects.value.slice(start, end);

  // Add empty rows if fewer than 10 items
  while (pageItems.length < itemsPerPage.value) {
    pageItems.push(null);
  }

  return pageItems;
});

// const handleExport = (type: string) => {
//   selectedType.value = type; // อัปเดตประเภทที่เลือก
// };

const isAlertPrintOpen = ref(false); // ควบคุมการแสดง Alert ส่งออก

const exportFile = async () => {
  try {
    await exportProjectReportStore.exportFile('xlsx');
    showModal.value = false;
    isAlertPrintOpen.value = true;
    setTimeout(() => {
      isAlertPrintOpen.value = false; // ปิด popup หลังจาก 3 วินาที
    }, 1500);
  } catch (error) {
    console.error("Error exporting file:", error);
    if (selectedType.value) {
      alert(`เกิดข้อผิดพลาดในการส่งออกไฟล์`);
    }
  }
};

// Bar chart setup
// โครงการ
const project: string[] = [];
// จำนวนเงินของแต่ละโครงการ
const amountMoney: number[] = [];

const customXAxisTitle = {
  id: "customXAxisTitle",
  afterDraw: (chart: any) => {
    const { ctx, chartArea } = chart;
    const xPos = chartArea.right + 70;
    const yPos = chartArea.bottom;
    ctx.save();
    ctx.textAlign = "right";
    ctx.textBaseline = "middle";
    ctx.font = "14px Sarabun";
    ctx.fillStyle = "#000";
    ctx.fillText("โครงการ", xPos, yPos);
    ctx.restore();
  },
};

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
    await projectsStore.getProjectsActive();

    // อัปเดตข้อมูลสำหรับตัวกรอง
    projectsStore.projects.forEach((item: ProjectReport) => {
      project.push(item.pjName);
      amountMoney.push(item.pjSumAmountExpenses);
    });
  } catch (error) {
    console.error("Error fetching project:", error);
  } finally {
    setTimeout(() => {
      loading.value = false;
    }, 500);
  }

  const barchart = document.getElementById("barChart") as HTMLCanvasElement;
  if (barchart) {
    new Chart(barchart, {
      type: "bar",
      data: {
        labels: project,
        datasets: [
          {
            label: "จำนวนเงิน (บาท)",
            data: amountMoney,
            backgroundColor: "#C81C1B",
            barPercentage: 0.2,
            datalabels: {
              display: false,
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
            beginAtZero: true,
            ticks: {
              color: "#000",
              font: {
                size: 14,
                family: "Sarabun",
              },
              stepSize: 10000,
            },
            border: {
              display: false,
            },
          },
        },
        plugins: {
          legend: {
            display: false,
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
    <div class="flex w-full gap-6 mb-8">
      <div class="relative w-full">
        <Button :type="'btn-print2'" @click="showModal = true"
          class="absolute right-0 mr-4 transform -translate-y-1/2 top-1/2">
          ส่งออก
        </Button>
      </div>
      <!-- Modal -->
      <!-- Popup สำหรับยืนยันการส่งออกรายงานโครงการ -->
      <div v-if="showModal" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
        <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
          <div class="flex justify-center mb-4">
            <svg :class="`w-[90px] h-[90px] text-gray-800 dark:text-white`" aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
              <path fill-rule="evenodd"
                d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
                clip-rule="evenodd" />
            </svg>
          </div>
          <h2 class="text-[24px] font-bold text-center text-black mb-4">
            ยืนยันส่งออกรายงานโครงการ
          </h2>
          <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
            คุณยืนยันส่งออกรายงานโครงการหรือไม่ ?
          </h2>
          <div class="flex justify-center space-x-4">
            <button @click="showModal = false"
              class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
              ยกเลิก
            </button>

            <button @click="exportFile"
              class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
              ยืนยัน
            </button>
          </div>
        </div>
      </div>

      <div v-if="isAlertPrintOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
        <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg flex flex-col justify-center items-center">
          <div class="mb-4">
            <svg xmlns="http://www.w3.org/2000/svg" width="80" height="80" viewBox="0 0 80 80" fill="none">
              <path
                d="M40 0C17.9433 0 0 17.9433 0 40C0 62.0567 17.9433 80 40 80C62.0567 80 80 62.0567 80 40C80 17.9433 62.0567 0 40 0ZM39.6967 51.3967C38.4067 52.6867 36.71 53.33 35.0067 53.33C33.3033 53.33 31.59 52.68 30.2867 51.38L21.0133 42.3933L25.6567 37.6033L34.9667 46.6267L54.33 27.6233L59.01 32.3733L39.6967 51.3967Z"
                fill="#12B669" />
            </svg>
          </div>
          <h2 class="text-[24px] font-bold text-center text-black" style="white-space: pre-line;">
            ส่งออกรายงานโครงการสำเร็จ
          </h2>
        </div>
      </div>
    </div>
    <!-- end::Filter -->

    <!-- begin::Content -->
    <div class="flex flex-col items-center justify-center">
      <div class="flex flex-col items-center h-[500px] w-[1240px] mb-5">
        <p class="mb-10 font-bold text-center text-black">
          ยอดการเบิกของค่าใช้จ่ายแต่ละโครงการ
        </p>
        <div v-if="loading" class="flex justify-center items-center">
          <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
          <span class="ml-2">กำลังโหลดข้อมูล...</span>
        </div>
        <div class="w-3/4 h-full">
          <canvas id="barChart" v-show="!loading"></canvas>
        </div>
      </div>
      <div class="w-full h-fit border-[2px] flex flex-col items-start border-[#BBBBBB]">
        <Ctable :table="'Table4-head'" />
        <table class="w-full text-center text-black table-auto">
          <tbody>
            <tr v-if="loading">
              <td colspan="100%" class="py-4">
                <div class="flex justify-center items-center">
                  <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-[#B67D12]"></div>
                  <span class="ml-2">กำลังโหลดข้อมูล...</span>
                </div>
              </td>
            </tr>
            <tr v-else v-for="(project, index) in paginated" :key="project ? project.pjId : `empty-${index}`"
              :class="project ? 'text-[14px] h-[46px] border-b-2 border-[#BBBBBB] hover:bg-gray-50' : 'h-[50px]'">
              <template v-if="project">
                <th class="px-2 py-3 w-12">{{ index + 1 + (currentPage - 1) * itemsPerPage }}</th>
                <th class="w-auto px-2 py-3 text-start" :title="project.pjName">
                  {{ project.pjName }}
                </th>
                <th class="py-3 px-2 w-60 text-end font-[100]">
                  {{
                  new Decimal(project.pjSumAmountExpenses ?? 0).toNumber().toLocaleString("en-US", {
                  minimumFractionDigits: 2,
                  maximumFractionDigits: 2,
                  })
                  }}
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
