<script setup lang="ts">
/*
* ชื่อไฟล์: ProjectReport.vue
* คำอธิบาย: ไฟล์นี้แสดงรายงานของ Project
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/
// import Icon from '../../components/template/CIcon.vue';
import { onMounted, ref } from "vue";
import ChartDataLabels from "chartjs-plugin-datalabels";
import Ctable from '../../components/Table/CTable.vue';
import { useProjectsStore } from '../../store/projectsReport';
import Button from "../../components/Buttons/Button.vue";
import ProjectReport from '../../types/index';
import Decimal from "decimal.js";
import { useExportProjectReportStore } from "../../store/exportProjectReport";
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

const projectsStore = useProjectsStore();
const showModal = ref(false);
const selectedType = ref<string | null>(null);
const exportProjectReportStore = useExportProjectReportStore();

const handleExport = (type: string) => {
    selectedType.value = type; // อัปเดตประเภทที่เลือก
};


const exportFile = async () => {
    if (!selectedType.value) return;

    try {
        await exportProjectReportStore.exportFile(selectedType.value);

        selectedType.value = null;
        showModal.value = false;
    } catch (error) {
        console.error("Error exporting file:", error);
        if (selectedType.value) {
            alert(`เกิดข้อผิดพลาดในการส่งออกไฟล์ ${selectedType.value.toUpperCase()}`);
        }
    }
};
// Bar chart setup
// โครงการ
const project: string[] = [];

// จำนวนเงินของแต่ละโครงการ
const amountMoney: number[] = [];

onMounted(async () => {
    await projectsStore.getAllProjects();

    projectsStore.projects.forEach((item: ProjectReport) => {
        project.push(item.pjName);
        amountMoney.push(item.pjSumAmountExpenses);
    });

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
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: "โครงการ",
                            font: {
                                weight: 'bold',
                                size: 14,
                            },
                            padding: {
                                top: 20,
                            }
                        },
                        ticks: {
                            font: {
                                weight: 'bold',
                                size: 12,
                            },
                        },
                        grid: {
                            display: false,
                        },
                    },
                    y: {
                        beginAtZero: true, // แกน y เริ่มที่ 0
                        ticks: {
                            font: {
                                weight: 'bold',
                                size: 12,
                            },
                            stepSize: 500, // ค่าแกน y เพิ่มที่ละตามจำนวนที่ตั้ง
                        },
                        border: {
                            display: false, // ลบเส้นแรกของแกน y
                        },
                    },
                },
                plugins: {
                    title: {
                        display: true,
                        text: "จำนวนเงิน (บาท)",
                        align: "start",
                        padding: {
                            top: 10,
                            bottom: 30,
                        },
                        font: {
                            weight: 'bold',
                            size: 14,
                        },
                    },
                    legend: {
                        display: false, // ซ่อนชนิดของแท่งกราฟ
                    },
                    tooltip: {
                        callbacks: {
                            label: (tooltipItem) => {
                                return `จำนวนเงิน: ${tooltipItem.raw} บาท`;
                            },
                        },
                    },
                },
            },
        });
    } else {
        console.error("Canvas element for bar chart not found");
    }
});
</script>

<template>
    <!-- path for test = /report/project -->

    <!-- begin::Filter -->
    <div class="flex w-full gap-6 mb-8">
        <div class="relative w-full">
            <Button :type="'btn-print2'" @click="showModal = true"
                class="absolute right-0 mr-4 transform -translate-y-1/2 top-1/2">
                ส่งออก
            </Button>
        </div>
        <!-- Modal -->
        <div v-if="showModal" class="fixed inset-0 flex items-center justify-center bg-gray-800 bg-opacity-50">
            <div class="p-6 bg-white rounded-lg shadow-2xl w-96">
                <h2 class="mb-6 text-lg font-bold text-gray-700"></h2>

                <!-- ปุ่มเลือกประเภทไฟล์ -->
                <div>
                    <div class="flex justify-center space-x-6">
                        <!-- ปุ่ม PDF -->
                        <button @click="handleExport('pdf')"
                            :class="['px-5 py-3 rounded-lg flex items-center justify-center transition-colors duration-200', selectedType === 'pdf' ? 'bg-blue-500 text-white' : 'bg-gray-100 hover:bg-gray-200']">
                            <!-- ไอคอน PDF -->
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"
                                class="w-8 h-8 mr-2">
                                <path
                                    d="M6 2a1 1 0 00-1 1v18a1 1 0 001 1h12a1 1 0 001-1V8.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0013.586 2H6zm7 2.414L18.586 10H13V4.414zM8 12h2v6H8v-6zm3 0h1.5c.828 0 1.5.672 1.5 1.5v3a1.5 1.5 0 01-1.5 1.5H11v-6zm3 0h2.5v6H14v-6z" />
                            </svg>
                        </button>

                        <!-- ปุ่ม XLSX -->
                        <button @click="handleExport('xlsx')"
                            :class="['px-5 py-3 rounded-lg flex items-center justify-center transition-colors duration-200', selectedType === 'xlsx' ? 'bg-green-500 text-white' : 'bg-gray-100 hover:bg-gray-200']">
                            <!-- ไอคอน XLSX -->
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"
                                class="w-8 h-8 mr-2">
                                <path
                                    d="M6 2a1 1 0 00-1 1v18a1 1 0 001 1h12a1 1 0 001-1V8.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0013.586 2H6zm7 2.414L18.586 10H13V4.414zM9 14h1.5l.75 1.5.75-1.5H14v4h-1.5v-1.5l-.75 1.5-.75-1.5V18H9v-4z" />
                            </svg>
                        </button>
                    </div>

                    <div class="flex justify-center mb-6 space-x-20">
                        <span class="mt-2 text-sm text-gray-600">PDF</span>
                        <span class="mt-2 text-sm text-gray-600">XLSX</span>
                    </div>

                    <!-- ปุ่มยืนยันและยกเลิก -->
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
    <!-- end::Filter -->

    <!-- begin::Content -->
    <div class="flex flex-col items-center justify-center">

        <!-- begin::Bar chart -->
        <div class="flex flex-col items-center h-[500px] w-[1240px] mb-5">
            <p class="mb-10 font-bold text-center text-black">ยอดการเบิกของค่าใช้จ่ายแต่ละโครงการ</p>
            <div class="w-3/4 h-full ">
                <canvas id="barChart"></canvas>
            </div>
        </div>
        <!-- end::Bar chart -->

        <!-- begin::Table -->
        <!-- <div class="w-full h-fit border-[2px] flex flex-col items-start"> -->
        <div class="w-full border-r-[2px] border-l-[2px] border-t-[2px] mt-12">
            <!-- Table Header -->
            <Ctable :table="'Table4-head'" />
            <!-- Table Data -->
            <table class="w-full text-center text-black table-auto">
                <tbody>
                    <tr v-for="(project, index) in projectsStore.projects" :key="index"
                        class="text-[16px] border-b-2 border-[#BBBBBB] h-[46px]">
                        <th class="px-2 py-3 w-14">{{ index + 1 }}</th>
                        <th class="w-auto px-2 py-3 overflow-hidden truncate text-start"
                            style="max-width: 208px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                            {{ project.pjName }}
                        </th>
                        <th class="py-3 px-2 w-60 text-end font-[100]">{{ new Decimal(project.pjSumAmountExpenses ?? 0).toFixed(2)  }}</th>
                    </tr>
                </tbody>
            </table>
            <!-- Table Footer -->
            <Ctable :table="'Table4-footer'" />
        </div>
        <!-- end::Table -->

    </div>
    <!-- end::Content -->
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
@media screen and (-webkit-min-device-pixel-ratio:0) {
    .custom-select {
        background-image: url("data:image/svg+xml;utf8,<svg fill='transparent' height='24' viewBox='0 0 24 24' width='24' xmlns='http://www.w3.org/2000/svg'><path d='M7 10l5 5 5-5z'/></svg>");
        background-repeat: no-repeat;
        background-position-x: 100%;
        background-position-y: 5px;
    }
}
</style>