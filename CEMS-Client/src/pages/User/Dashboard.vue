<script setup lang="ts">
/*
* ชื่อไฟล์: dashboard.vue
* คำอธิบาย: ไฟล์นี้แสดงภาพรวมคำขอเบิกต่างๆ และรายการรออนุมัติของผู้ใช้ทั่วไปที่มีสิทธิ์
* ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/
import { onMounted, ref } from "vue";
import { useDashboard } from "../../store/dashboard";
import { useDashboardDetail } from "../../store/dashboard";
import ChartDataLabels from "chartjs-plugin-datalabels";
import {
  Chart,
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



const dashboardDetailStore = useDashboardDetail() ;
const user = ref<any>(null);

onMounted(async () => {
  const storedUser = localStorage.getItem('user');
  if (storedUser) {
    try {
      user.value = await JSON.parse(storedUser)
      await dashboardDetailStore.getDashboardDetail(user)
    } catch (error) {
      console.log(error)
    }
  }
})


Chart.register(
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

const dashboardStore = useDashboard();
const projectData = ref<any>(null);

const usr_id = 9999 ;

const requisitionType = ref<any>(null);
const rqtNames: string[] = [];
const totalRqt: number[] = [];


/*
1. หา role ของ user ว่าเป็น role อะไร
2. เขียน if ตรวจสอบการดึง ถ้าเป็น user ธรรมดา จะ get project และ rqt เป็น by id 
3. ตรงกล่อง 4 อัน อาจสร้าง component ไว้ก่อน

*/

// Pie chart
onMounted(async() => {
  
  projectData.value = await dashboardStore.getDashboardProject()
  requisitionType.value = await dashboardStore.getDashboardRequisitionType()

  requisitionType.value.forEach((item: any) => {
    rqtNames.push(item.rqtName);
    totalRqt.push(item.totalRqt);
  });


  const ctx = document.getElementById("pieChart") as HTMLCanvasElement;
  if (ctx) {
    new Chart(ctx, {
      type: "pie",
      data: {
        labels: rqtNames,
        datasets: [
          {
            label: "ประเภทค่าใช้จ่ายของรายการเบิก",
            data: totalRqt,
            backgroundColor: [
              "#8979FF",
              "#FF928A",
              "#3CC3DF",
              "#FFAE4C",
              "#537FF1",
            ],
            hoverOffset: 5,
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        animation: {
          animateScale: true,
        },
        plugins: {
          tooltip: {
            callbacks: {
              label: (tooltipItem: any) => {
                const { dataset, raw } = tooltipItem;
                const value = raw as number; // แปลง raw ให้เป็น number
                const total = dataset.data.reduce(
                  (acc: number, val: number) => acc + val,
                  0
                ); // คำนวณผลรวม
                const percentage = ((value / total) * 100).toFixed(2) + "%"; // คำนวณเปอร์เซ็นต์
                return `${tooltipItem.label}: ${percentage}`;
              },
            },
          },
          datalabels: {
            formatter: (value: number, context: any) => {
              const total = context.chart.data.datasets[0].data.reduce(
                (acc: number, val: number) => acc + val,
                0
              ); // คำนวณผลรวม
              const percentage = ((value / total) * 100).toFixed(2) + "%"; // คำนวณเปอร์เซ็นต์
              return percentage; // ส่งกลับเปอร์เซ็นต์
            },
            color: "#fff", // สีของตัวอักษร
            anchor: "end",
            align: "end",
            font: {
              weight: "bold",
            },
          },

          legend: {
            position: "right",
            align: "center",

            labels: {
              padding: 16,
              usePointStyle: true,
              pointStyle: "circle",
              boxHeight: 8,
              boxWidth: 8,
              font: {
                size: 14,
                weight: "bold",
                family: "Sarabun",
              },
            },
          },
        },
      },
    });
  } else {
    console.error("Canvas element not found");
  }
});

// Line chart
const labels = [
  "January",
  "February",
  "March",
  "April",
  "May",
  "June",
  "July",
  "August",
  "September",
  "October",
  "November",
  "December",
];

onMounted(() => {
  const linechart = document.getElementById("lineChart") as HTMLCanvasElement;
  if (linechart) {
    new Chart(linechart, {
      type: "line",
      data: {
        labels: labels,
        datasets: [
          {
            label: "ยอดรวมการเบิกจ่าย (บาท)",
            data: [65, 59, 80, 81, 56, 55, 40, 90, 100, 105, 140, 80],

            fill: false,
            borderColor: "#8979FF",
            backgroundColor: "white",
            borderWidth: 2,
            tension: 0.4,
            datalabels: {
              align: "top",
              anchor: "center",
              color: "rgba(0, 0, 0, 0.7)", // ตัวอักษรสีจาง
              font: {
                weight: "normal",
                size: 10,
                family: "Sarabun",
              },
            },
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            beginAtZero: true,
            ticks: {
              font: {
                size: 12,
              },
            },
          },

          x: {
            beginAtZero: true,
            ticks: {
              font: {
                size: 12,
              },
            },
          },
        },
        layout: {
          padding: {
            left: 20,
            right: 20,
            top: 20,
            bottom: 20,
          },
        },
        plugins: {
          tooltip: {
            enabled: true,
            mode: "index",
            intersect: false,
            // callbacks: {
            //   label: (tooltipItem) => {
            //     return `ยอดเบิกจ่าย: ${tooltipItem.raw}`;
            //   },
            // },
          },
          legend: {
            position: "bottom",
            align: "center",
            labels: {
              padding: 16,
              usePointStyle: true,
              pointStyle: "line",
              boxHeight: 16,
              boxWidth: 16,
              font: {
                size: 14,
                weight: "bold",
                family: "Sarabun",
              },
            },
          },
        },
      },
    });
  }
});
</script>

<template>
  <!-- path for test = / -->
  <div class="flex flex-col items-center text-center">
    <!-- content -->

    <div class="mainfloat clearFix">
      <!-- Summary section -->
      <div class="grid summaryfloat grid-cols-4 gap-4 w-[817px] h-[128px] m-6 justify-items-stretch">
        <div v-for="(item  , index) in dashboardDetailStore.dashboard" :key="index" class="columnDashboard shadowBox ">
          <p class="font16">{{item.key}}</p>
          <p class="font35">{{item.value}}</p>
        </div>
      </div>

      <!-- Top project withdrawal table -->
      <div class="w-[295px] h-[555px] shadowBox projectfloat mr-6 my-6 p-1">
        <table class="projectWithDraw w-full text-left">
          <thead>
            <tr>
              <th colspan="3" class="w-max text-center p-2 font-bold text-lg">
                ลำดับการเบิกสูงสุดของโครงการ
              </th>
            </tr>
            <tr>
              <th class="w-[40px]">ลำดับ</th>
              <th class="w-[150px]">โครงการ</th>
              <th class="w-[80px] text-right">จำนวนเงิน</th>
            </tr>
          </thead>
          <tbody>
            <!-- แถว 1 -->
            <tr v-for="(project, index) in projectData">
              <td class="text-right">{{ index+1 }}</td>
              <td class="textOverflow">{{ project.pjName }}</td>
              <td class="text-right">{{ project.totalPjExpense }}</td>
            </tr>

          </tbody>
        </table>
      </div>

      <!-- Pie chart -->
      <div class="graphPie w-[817px] h-[400px] shadowBox ml-6 mb-6 mr-6 summaryfloat">
        <p class="font16 font-bold m-3 text-left">
          ประเภทค่าใช้จ่ายของรายการเบิก
        </p>
        <div>
          <canvas id="pieChart"></canvas>
        </div>
      </div>
    </div>

    <!-- Line chart -->
    <div class="w-[1136px] h-[550px] items-center shadowBox mx-6 mb-6">
      <p class="font16 font-bold m-3 text-left">ยอดการเบิกจ่ายจริง</p>
      <canvas id="lineChart"></canvas>
    </div>
  </div>
</template>
