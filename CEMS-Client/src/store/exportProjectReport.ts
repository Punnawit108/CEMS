
import axios from "axios";
import { defineStore } from "pinia";

export const useExportProjectReportStore = defineStore("exportProjectReport", {
  actions: {
    async exportPdf() {
      try {
        // กำหนด URL สำหรับการ Export PDF
        const url = `${import.meta.env.VITE_BASE_URL}/api/pdf/exportProject`;

        // เรียก API และรับ response เป็น Blob
        const response = await axios.get(url, { responseType: "blob" });

        // สร้าง Blob และดาวน์โหลดไฟล์ PDF
        const blob = new Blob([response.data], { type: "application/pdf" });
        const link = document.createElement("a");
        link.href = window.URL.createObjectURL(blob);
        link.setAttribute("download", "ExportedProjectData.pdf");
        document.body.appendChild(link);
        link.click();
        link.remove();
      } catch (error) {
        console.error("Error exporting PDF:", error);
        alert("เกิดข้อผิดพลาดในการดาวน์โหลดไฟล์ PDF");
      }
    },
  },
});