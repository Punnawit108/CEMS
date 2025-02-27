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
import Decimal from 'decimal.js';


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

//รับค่า useDashboardDetail จาก store มาเก็บ
const dashboardDetailStore = useDashboardDetail();
const user = ref<any>(null);




//ค้นหา user
const loadUser = async () => {
  const storedUser = localStorage.getItem("user");
  if (storedUser) {
    try {
      user.value = JSON.parse(storedUser);
      await dashboardDetailStore.getDashboardDetail(user);
    } catch (error) {
      console.log("Error loading user:", error);
    }
  }
};

//นำเข้าเครื่องมือกราฟต่างๆ
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

//รับค่า useDashboard จาก store เข้ามา
const dashboardStore = useDashboard();
const projectData = ref<any>(null);

// Function to format decimal numbers
const formatDecimal = (value: number) => {
  return new Decimal(value).toFixed(2);
};

//ประกาศตัวแปร ประเภทค่าใช้จ่าย
const requisitionType = ref<any>(null);
const rqtNames: string[] = [];
const totalRqt: number[] = [];

//ประกาศตัวแปร ประเภทยอดรวมค่าใช้จ่าย ตามเดือน
const totalExpense = ref<any>();
/*
1. หา role ของ user ว่าเป็น role อะไร
2. เขียน if ตรวจสอบการดึง ถ้าเป็น user ธรรมดา จะ get project และ rqt เป็น by id 
3. ตรงกล่อง 4 อัน อาจสร้าง component ไว้ก่อน
*/

//ตัวแปรเดือน Line chart
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

onMounted(async () => {
  //ตรวจสอบว่าเป็น Role user หรือไม่ แล้วเก็บค่าลงตัวแปร
  await loadUser();
  if (user.value) {
    if (user.value?.usrRolName === "User") {
      totalExpense.value = await dashboardStore.getDashboardTotalExpenseById(
        user.value.usrId
      );
      projectData.value = await dashboardStore.getDashboardProjectById(
        user.value.usrId
      );
      requisitionType.value =
        await dashboardStore.getDashboardRequisitionTypeById(user.value.usrId);
    } else {
      totalExpense.value = await dashboardStore.getDashboardTotalExpense();
      projectData.value = await dashboardStore.getDashboardProject();
      requisitionType.value =
        await dashboardStore.getDashboardRequisitionType();
    }
    //วนลูปประเภทค่าใช้จ่าย แล้วเก็บค่าลงในตัวแปร
    requisitionType.value.forEach((item: any) => {
      rqtNames.push(item.rqtName);
      totalRqt.push(item.totalRqt);
    });
  }

  const ctx = document.getElementById("pieChart") as HTMLCanvasElement;
  if (ctx) {
    new Chart(ctx, {
      type: "pie",
      data: {
        labels: rqtNames,
        datasets: [
          {
            data: totalRqt,
            backgroundColor: [
              "#8979FF",
              "#FF928A",
              "#3CC3DF",
              "#FFAE4C",
              "#537FF1",
            ],
            borderWidth: 0,
          },
        ],
      },
      options: {
        animation: {
          animateScale: true,
          duration: 700, // ระยะเวลาของอนิเมชันในหน่วยมิลลิวินาที (เช่น 1000ms = 1 วินาที)
        },
        responsive: true,

        maintainAspectRatio: false,
        layout: {
          padding: 30, // ระยะห่างของกราฟ
        },
        plugins: {
          tooltip: {
            callbacks: {
              label: (tooltipItem: any) => {
                const total = tooltipItem.dataset.data.reduce(
                  (acc: number, val: number) => acc + val,
                  0
                );
                const value = tooltipItem.raw;
                const percentage = ((value / total) * 100).toFixed(2) + "%";
                return `${tooltipItem.label}: ${percentage}`;
              },
            },
          },
          datalabels: {
            display: false, // ปิดการแสดงผล Data Labels
          },
          legend: {
            position: "right",
            align: "center",
            // Style ของ Label
            labels: {
              padding: 16,
              //สไตล์ของจุด
              usePointStyle: true,
              pointStyle: "circle",
              //ขนาดของกล่อง
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
      //
      plugins: [
        {
          id: "lines",
          afterDatasetsDraw(chart) {
            const { ctx } = chart;
            const meta = chart.getDatasetMeta(0);

            // Check if chart data is available
            if (!chart.data.datasets[0].data) return;

            const centerX = chart.chartArea.width / 2 + chart.chartArea.left;
            const centerY = chart.chartArea.height / 2 + chart.chartArea.top;

            meta.data.forEach((segment, index) => {
              // Skip drawing lines for hidden segments
              if (!meta.data[index].hidden) {
                const startAngle = segment.startAngle;
                const endAngle = segment.endAngle;
                const middleAngle = (startAngle + endAngle) / 2;

                const outerRadius = segment.outerRadius;
                const endX =
                  centerX + (outerRadius + 20) * Math.cos(middleAngle);
                const endY =
                  centerY + (outerRadius + 15) * Math.sin(middleAngle);

                // Only proceed if the segment is visible
                if (chart.getDataVisibility(index)) {
                  const label = `${chart.data.labels[index]}: `;
                  const percentage = (
                    (chart.data.datasets[0].data[index] /
                      chart.data.datasets[0].data.reduce((a, b) => a + b, 0)) *
                    100
                  ).toFixed(2);
                  const percentageText = `${percentage}%`;

                  const labelWidth = ctx.measureText(label).width;
                  const percentageTextWidth =
                    ctx.measureText(percentageText).width;

                  const horizontalX =
                    endX > centerX
                      ? endX + labelWidth + percentageTextWidth + 10
                      : endX - labelWidth - percentageTextWidth - 10;

                  // Draw lines only for visible segments
                  ctx.save();
                  ctx.beginPath();
                  ctx.moveTo(centerX, centerY);
                  ctx.lineTo(endX, endY);
                  ctx.lineTo(horizontalX, endY);
                  ctx.strokeStyle = segment.options.backgroundColor || "#000";
                  ctx.lineWidth = 1;
                  ctx.stroke();
                  ctx.restore();

                  // Add text
                  const textX = (endX + horizontalX) / 2;
                  const textY = endY;

                  ctx.font = "12px Arial";
                  ctx.fillStyle = "#000";
                  ctx.textAlign = endX > centerX ? "right" : "left";
                  ctx.fillText(
                    label,
                    endX > centerX
                      ? horizontalX - percentageTextWidth
                      : horizontalX,
                    textY - 5
                  );

                  ctx.fillStyle =
                    chart.data.datasets[0].backgroundColor[index] || "#000";
                  ctx.fillText(
                    percentageText,
                    endX > centerX ? horizontalX : horizontalX + labelWidth,
                    textY - 5
                  );
                }
              }
            });
          },
        },
      ],
    });
  }

  //Line chart
  if (totalExpense.value != null) {
    const linechart = document.getElementById("lineChart") as HTMLCanvasElement;
    if (linechart) {
      new Chart(linechart, {
        type: "line",
        data: {
          labels: labels,
          datasets: [
            {
              label: "ยอดรวมการเบิกจ่าย (บาท)",
              data: totalExpense.value[0].totalExpense,
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
        <div v-for="(item, index) in dashboardDetailStore.dashboard" :key="index" class="columnDashboard shadowBox">
          <p class="font16">{{ item.key }}</p>
          <p class="font35">{{ item.value }}</p>
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
              <td class="text-right">{{ index + 1 }}</td>
              <td class="textOverflow">{{ project.pjName }}</td>
              <td class="text-right">
                {{ new Decimal(project.totalPjExpense ?? 0).toFixed(2) }}

              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Pie chart -->
      <div class="shadowBox summaryfloat pieChartBox items-center">
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
