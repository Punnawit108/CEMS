<script setup lang="ts">
/**
* ชื่อไฟล์: ProjectReport.vue
* คำอธิบาย: ไฟล์นี้แสดงรายงานของ Project
* Input: -
* Output: รายงานของ Project
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 10 พฤศจิกายน 2567
*/
// import Icon from '../../components/template/CIcon.vue';
import { onMounted } from "vue";
import ChartDataLabels from "chartjs-plugin-datalabels";
import Ctable from '../../components/template/Ctable.vue';
import { useProjectsStore } from '../../store/projectsReport';
import ProjectReport from '../../types/index';
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

// Bar chart setup
// โครงการ
const project: string[] = [];

// จำนวนเงินของแต่ละโครงการ
const amountMoney: number[] = [];

onMounted(async () => {
    await projectsStore.getAllProjects();
    const projects = projectsStore.projects;

    projects.forEach((item: ProjectReport) => {
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
                        barPercentage: 0.33, // ความหนาของแท่งกราฟ
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

    <!-- begin::Content -->
    <div class="flex flex-col items-center justify-center">

        <!-- begin::Bar chart -->
        <div class="flex flex-col items-center h-[500px] w-[1240px] mb-5">
            <p class="font-bold text-black mb-10 text-center">ยอดการเบิกของค่าใช้จ่ายแต่ละโครงการ</p>
            <div class=" h-full w-3/4">
                <canvas id="barChart"></canvas>
            </div>
        </div>
        <!-- end::Bar chart -->

        <!-- begin::Filter -->
        <div class="flex justify-between w-full mb-8 ">
            <!-- Filter ค้นหา -->
            <div class="h-fit w-[266px]">
                <form class="grid">
                    <label for="SearchBar" class="py-0.5 text-[14px] text-black text-start">ค้นหา</label>
                    <div class="relative h-[32px] w-[266px]  justify-center items-center">

                        <div class="absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                            <svg width="19" height="20" viewBox="0 0 19 20" fill="none"
                                xmlns="http://www.w3.org/2000/svg">
                                <path
                                    d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z"
                                    stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                            </svg>
                        </div>

                        <input type="text" id="SearchBar"
                            class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-9"
                            placeholder="ชื่อ-นามสกุล" />
                    </div>
                </form>
            </div>
            <!-- Filter ประเภทค่าใช้จ่าย -->
            <div class="h-fit w-[266px] ">
                <form class="grid">
                    <label for="ExpenseType" class="py-0.5 text-[14px] text-black text-start">ประเภทค่าใช้จ่าย</label>
                    <div class="relative h-[32px] w-[266px]  justify-center items-center">

                        <select required
                            class="custom-select text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4">
                            <option value="" disabled selected hidden class="placeholder">ประเภทค่าใช้จ่าย</option>
                            <option value="Type1">ประเภทที่ 1</option>
                            <option value="Type2">ประเภทที่ 2</option>
                        </select>

                        <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                            <svg width="13" height="8" viewBox="0 0 13 8" fill="none"
                                xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd" clip-rule="evenodd"
                                    d="M7.2071 7.2071C6.8166 7.5976 6.1834 7.5976 5.7929 7.2071L0.79289 2.20711C0.40237 1.81658 0.40237 1.18342 0.79289 0.79289C1.18342 0.40237 1.81658 0.40237 2.20711 0.79289L6.5 5.0858L10.7929 0.79289C11.1834 0.40237 11.8166 0.40237 12.2071 0.79289C12.5976 1.18342 12.5976 1.81658 12.2071 2.20711L7.2071 7.2071Z"
                                    fill="black" />
                            </svg>
                        </div>
                    </div>
                </form>
            </div>
            <!-- div ว่าง -->
            <div class="h-fit w-[266px] ">
            </div>
            <!-- div ว่าง -->
            <div class="h-fit w-[266px] ">
            </div>
        </div>
        <!-- end::Filter -->

        <!-- begin::Table -->
        <!-- <div class="w-full h-fit border-[2px] flex flex-col items-start"> -->
        <div class="w-full h-fit border-[2px] flex flex-col items-start">
            <!-- Table Header -->
            <Ctable :table="'Table4-head'" />
            <!-- Table Data -->
            <table class="table-auto w-full text-center text-black">
                <tbody>
                    <tr v-for="(project, index) in projectsStore.projects" :key="index"
                        class="text-[16px] border-b-2 border-[#BBBBBB] h-[46px]">
                        <th class="py-3 px-2 w-14">{{ index + 1 }}</th>
                        <th class="py-3 px-2 w-auto text-start truncate overflow-hidden"
                            style="max-width: 208px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                            {{ project.pjName }}
                        </th>
                        <th class="py-3 px-2 w-60 text-end font-[100]">{{ project.pjSumAmountExpenses }}</th>
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