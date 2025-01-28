/*
* ชื่อไฟล์: exportExpenseReport.ts
* คำอธิบาย: ไฟล์ store API สำหรับการจัดการการส่งออกรายงาน PDF และ Excel
* ชื่อผู้เขียน/แก้ไข: 
* วันที่จัดทำ/แก้ไข: 
*/

import axios from "axios";
import { defineStore } from "pinia";

export const useExportExpenseReportStore = defineStore("exportExpenseReport", {
  actions: {
    async exportFile(fileType: string) {
      try {
        // กำหนด URL สำหรับ Export ตามประเภทไฟล์ที่เลือก
        const url =
          fileType === "pdf"
            ? `${import.meta.env.VITE_BASE_URL}/api/pdf/export`
            : `${import.meta.env.VITE_BASE_URL}/api/excel/export`;

        // เรียก API และรับ response เป็น Blob
        const response = await axios.get(url, { responseType: "blob" });

        // สร้าง Blob และดาวน์โหลดไฟล์
        const blob = new Blob([response.data], {
          type:
            fileType === "pdf"
              ? "application/pdf"
              : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        const link = document.createElement("a");
        link.href = window.URL.createObjectURL(blob);
        link.setAttribute(
          "download",
          `ExportedExpenseData.${fileType}`
        );
        document.body.appendChild(link);
        link.click();
        link.remove();
      } catch (error) {
        console.error(`Error exporting ${fileType}:`, error);
        alert(`เกิดข้อผิดพลาดในการดาวน์โหลดไฟล์ ${fileType.toUpperCase()}`);
      }
    },
  },
});
