/*
 * ชื่อไฟล์: exportProjectReport.ts
 * คำอธิบาย: ไฟล์ store API สำหรับการจัดการการส่งออกรายงานโครงการ
 * ชื่อผู้เขียน/แก้ไข: ปุณณะวิชญ์ เชียนพลแสน
 * วันที่จัดทำ/แก้ไข: 3 มีนาคม 2568
 */

import axios from "axios";
import { defineStore } from "pinia";

export const useExportProjectReportStore = defineStore("exportProjectReport", {
  actions: {
    /*
     * คำอธิบาย: ส่งออกข้อมูลรายงานโครงการ
     * Input: fileType
     * Output: ไฟล์ข้อมูลรายงานโครงการ
     * ชื่อผู้เขียน/แก้ไข: ปุณณะวิชญ์ เชียนพลแสน
     * วันที่จัดทำ/แก้ไข: 3 มีนาคม 2568
     */
    async exportFile(fileType: string) {
      try {
        // กำหนด URL สำหรับ Export ตามประเภทไฟล์ที่เลือก
        const url =
          fileType === "pdf"
            ? `${import.meta.env.VITE_BASE_URL}/api/pdf/exportProject`
            : `${import.meta.env.VITE_BASE_URL}/api/excelproject/export`;

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
        link.setAttribute("download", `ExportedExpenseData.${fileType}`);
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
