<script setup lang="ts">
/*
 * ชื่อไฟล์: ExpenseReport.vue
 * คำอธิบาย: ไฟล์นี้แสดงรายงานของคำขอเบิกค่าใช้จ่ายทั้งหมดในระบบ
 * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
 * วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
 */
import Icon from '../../components/template/CIcon.vue';
import { onMounted, ref } from "vue";
import ChartDataLabels from "chartjs-plugin-datalabels";
import Ctable from '../../components/template/CTable.vue';
import { useExpensesListStore, useExpensesGraphStore } from '../../store/expensesReport';
import ExpenseReportGraph from '../../types/index';
import Button from "../../components/template/Button.vue";
import axios from "axios";
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
// ตัวแปรแสดง/ซ่อน Modal
const showModal = ref(false);
const selectedType = ref<string | null>(null);

// ฟังก์ชัน Export ไฟล์
const exportFile = async () => {
    try {
        // URL สำหรับ Export ตามประเภทที่เลือก
        const url =
            selectedType.value === 'pdf'
                ? 'http://localhost:5247/api/pdf/export'
                : 'http://localhost:5247/api/excel/export';

        // เรียก API และกำหนด response เป็น blob
        const response = await axios.get(url, { responseType: 'blob' });

        // สร้าง Blob สำหรับดาวน์โหลดไฟล์
        const blob = new Blob([response.data], {
            type:
                selectedType.value === 'pdf'
                    ? 'application/pdf'
                    : 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
        });

        // สร้างลิงก์สำหรับดาวน์โหลด
        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.setAttribute('download', `ExportedExpenseData.${selectedType.value}`);
        document.body.appendChild(link);
        link.click();
        link.remove();

        // ปิด Modal
        showModal.value = false;
        selectedType.value = null; // รีเซ็ตค่า
    } catch (error) {
        console.error(`Error exporting ${selectedType.value}:`, error);
        alert('เกิดข้อผิดพลาดในการดาวน์โหลดไฟล์');
    }
};
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

const expensesListStore = useExpensesListStore();
const expensesGraphStore = useExpensesGraphStore();

// Bar chart setup
// ประเภทค่าใช้จ่าย
const expense: string[] = [];

// จำนวนเงินของแต่ละประเภทค่าใช้จ่าย
const amountMoney: number[] = [];

onMounted(async () => {
    try {
        await expensesListStore.getAllExpenses();
        await expensesGraphStore.getAllExpenses();
        expensesGraphStore.expensegraph.forEach((item: ExpenseReportGraph) => {
            expense.push(item.rqRqtName);
            amountMoney.push(item.rqSumExpenses);
        });
    } catch (error) {
        console.error("Error fetching expenses:", error);
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
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: "ประเภทค่าใช้จ่าย",
                            font: {
                                weight: "bold",
                                size: 14,
                            },
                            padding: {
                                top: 20,
                            },
                        },
                        ticks: {
                            font: {
                                weight: "bold",
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
                                weight: "bold",
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
                            weight: "bold",
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
    <div>
        <!-- path for test = /report/project -->

        <!-- begin::Filter -->
        <div class="flex w-full gap-6 mb-8">
            <!-- Filter ค้นหา -->

            <div class="h-fit w-[266px]">
                <form class="grid">
                    <label for="SearchBar" class="py-0.5 text-[14px] text-black text-start">ค้นหา</label>
                    <div class="relative h-[32px] w-[266px] justify-center items-center">
                        <div class="absolute transform -translate-y-1/2 pointer-events-none left-2 top-1/2">
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
            <div class="h-fit w-[266px]">
                <form class="grid">
                    <label for="ExpenseType" class="py-0.5 text-[14px] text-black text-start">ประเภทค่าใช้จ่าย</label>
                    <div class="relative h-[32px] w-[266px] justify-center items-center">
                        <select required
                            class="custom-select text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4">
                            <option value="" disabled selected hidden class="placeholder">
                                ประเภทค่าใช้จ่าย
                            </option>
                            <option value="Type1">ประเภทที่ 1</option>
                            <option value="Type2">ประเภทที่ 2</option>
                        </select>

                        <div class="absolute transform -translate-y-1/2 pointer-events-none right-2 top-1/2">
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
            <!-- Filter วันที่เบิก -->
            <div class="h-fit w-[266px]">
                <form class="grid">
                    <label for="Calendar" class="py-0.5 text-[14px] text-black text-start">วันที่เบิก</label>
                    <div class="relative h-[32px] w-[266px] justify-center items-center">
                        <input type="text" id="Calendar"
                            class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4"
                            placeholder="01/01/2567-31/12/2567" />

                        <div class="absolute transform -translate-y-1/2 pointer-events-none right-2 top-1/2">
                            <svg width="19" height="20" viewBox="0 0 19 20" fill="none"
                                xmlns="http://www.w3.org/2000/svg">
                                <path
                                    d="M3.31847 16.72C2.94262 16.72 2.62905 16.5998 2.37775 16.3596C2.12646 16.1193 2.00054 15.8196 2 15.4602V5.49922C2 5.1404 2.12592 4.84087 2.37775 4.60062C2.62959 4.36037 2.94316 4.23999 3.31847 4.23947H4.76176V2.5H5.64047V4.23947H11.4773V2.5H12.2932V4.23947H13.7365C14.1118 4.23947 14.4253 4.35985 14.6772 4.60062C14.929 4.84139 15.0547 5.14119 15.0541 5.5V9.20593C15.0541 9.31721 15.0152 9.41004 14.9374 9.4844C14.8597 9.55876 14.7626 9.59594 14.6462 9.59594C14.5298 9.59594 14.4327 9.55876 14.3549 9.4844C14.2771 9.41004 14.2382 9.31721 14.2382 9.20593V8.61934H2.81588V15.4602C2.81588 15.5798 2.8681 15.6898 2.97253 15.7902C3.07696 15.8905 3.192 15.9405 3.31765 15.9399H8.56785C8.68425 15.9399 8.78134 15.9771 8.85912 16.0515C8.93691 16.1258 8.9758 16.2187 8.9758 16.33C8.9758 16.4412 8.93691 16.5341 8.85912 16.6084C8.78134 16.6828 8.68425 16.72 8.56785 16.72H3.31847ZM13.7365 17.5C12.8276 17.5 12.0563 17.1973 11.4226 16.592C10.7895 15.9857 10.4729 15.2483 10.4729 14.3799C10.4729 13.5114 10.7895 12.7743 11.4226 12.1685C12.0558 11.5627 12.8268 11.2597 13.7357 11.2597C14.6445 11.2597 15.4158 11.5627 16.0495 12.1685C16.6832 12.7743 17 13.5114 17 14.3799C17 15.2483 16.6832 15.9857 16.0495 16.592C15.4158 17.1984 14.6448 17.501 13.7365 17.5ZM15.0868 16.0975L15.5322 15.6716L14.0498 14.2535V12.1303H13.4224V14.5062L15.0868 16.0975Z"
                                    fill="black" />
                            </svg>
                        </div>
                    </div>
                </form>
            </div>
            <div>
                <!-- ปุ่มเปิด Modal -->
                <Button :type="'btn-print2'" @click="showModal = true"
                    class="fixed right-0 mr-4 transform -translate-y-1/2 top-1/2">
                    ส่งออก
                </Button>

                <!-- Modal -->
                <div v-if="showModal" class="fixed inset-0 flex items-center justify-center bg-gray-800 bg-opacity-50">
                    <div class="p-6 bg-white rounded-lg shadow-2xl w-96">
                        <h2 class="mb-6 text-lg font-bold text-gray-700"></h2>

                        <!-- ปุ่มเลือกประเภทไฟล์ -->
                        <div class="flex justify-center flex   space-x-6">
                            <!-- ปุ่ม PDF -->
                            <button @click="selectedType = 'pdf'"
                                :class="['px-5 py-3 rounded-lg flex items-center justify-center transition-colors duration-200', selectedType === 'pdf' ? 'bg-blue-500 text-white' : 'bg-gray-100 hover:bg-gray-200']">
                                <!-- ไอคอน PDF -->
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"
                                    class="w-8 h-8 mr-2">
                                    <path
                                        d="M6 2a1 1 0 00-1 1v18a1 1 0 001 1h12a1 1 0 001-1V8.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0013.586 2H6zm7 2.414L18.586 10H13V4.414zM8 12h2v6H8v-6zm3 0h1.5c.828 0 1.5.672 1.5 1.5v3a1.5 1.5 0 01-1.5 1.5H11v-6zm3 0h2.5v6H14v-6z" />
                                </svg>

                            </button>

                            <!-- ปุ่ม XLSX -->
                            <button @click="selectedType = 'xlsx'"
                                :class="['px-5 py-3 rounded-lg flex items-center justify-center transition-colors duration-200', selectedType === 'xlsx' ? 'bg-green-500 text-white' : 'bg-gray-100 hover:bg-gray-200']">
                                <!-- ไอคอน XLSX -->
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"
                                    class="w-8 h-8 mr-2">
                                    <path
                                        d="M6 2a1 1 0 00-1 1v18a1 1 0 001 1h12a1 1 0 001-1V8.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0013.586 2H6zm7 2.414L18.586 10H13V4.414zM9 14h1.5l.75 1.5.75-1.5H14v4h-1.5v-1.5l-.75 1.5-.75-1.5V18H9v-4z" />
                                </svg>

                            </button>
                        </div>
                        <div class="flex justify-center space-x-20 mb-6 ">
                            <h class="text-sm text-gray-600 mt-2">PDF</h>
                            <h class="text-sm text-gray-600 mt-2">XLSX</h>
                        </div>
                        <!-- ปุ่มยืนยันและยกเลิก -->
                        <div class="flex justify-center space-x-4">
                            <Button :type="'btn-cancleBorderGray'" @click="showModal = false"
                                class="px-6 py-3 bg-gray-300 rounded-lg hover:bg-gray-400">
                                ยกเลิก
                            </button>
                            <Button :type="'btn-summit'" @click="exportFile" :disabled="!selectedType"
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
                <p class="mb-10 font-bold text-center text-black">
                    ยอดการเบิกของค่าใช้จ่ายแต่ละประเภท
                </p>
                <div class="w-3/4 h-full">
                    <canvas id="barChart"></canvas>
                </div>
            </div>
            <!-- end::Bar chart -->

            <!-- begin::Table -->
            <div class="w-full border-r-[2px] border-l-[2px] border-t-[2px] mt-12">
                <!-- Table Header -->
                <Ctable :table="'Table7-head'" />
                <!-- Table Data -->
                <!-- <Ctable :table="'Table7-data'" />    -->
                <table class="w-full text-center text-black table-auto">
                    <tbody>
                        <tr v-for="(expense, index) in expensesListStore.expenses" :key="index"
                            class="text-[14px] border-b-2 border-[#BBBBBB]">
                            <th class="py-[12px] px-2 w-14 h-[46px]">{{ index + 1 }}</th>
                            <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden" style="
                max-width: 224px;
                white-space: nowrap;
                text-overflow: ellipsis;
                overflow: hidden;
              " 
              :title="expense.rqUsrName">
                                {{ expense.rqUsrName }}
                            </th>
                            <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden" style="
                max-width: 224px;
                white-space: nowrap;
                text-overflow: ellipsis;
                overflow: hidden;
              " 
              :title="expense.rqName">
                                {{ expense.rqName }}
                            </th>
                            <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden" style="
                max-width: 224px;
                white-space: nowrap;
                text-overflow: ellipsis;
                overflow: hidden;
              " 
              :title="expense.rqName">
                                {{ expense.rqPjName }}
                            </th>
                            <th class="py-[12px] px-5 w-44 text-start">
                                {{ expense.rqRqtName }}
                            </th>
                            <th class="py-[12px] px-2 w-20 text-end">
                                11/10/2564
                            </th>
                            <th class="py-[12px] px-2 w-40 text-end">
                                {{ expense.rqExpenses }}
                            </th>
                            <th class="py-[10px] px-2 w-32 text-center">
                                <span class="flex justify-center">
                                    <Icon :icon="'viewDetails'" />
                                </span>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <!-- Table Footer -->
                <Ctable :table="'Table7-footer'" />
            </div>
        </div>
        <!-- end::Content -->
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
