import axios from "axios";
import { defineStore } from "pinia";

export const useExportExpenseReportStore = defineStore("exportExpenseReport", {
  actions: {
    async exportFile(fileType: string, filters: Record<string, any>) {
      try {
        // ตรวจสอบค่าฟิลเตอร์ที่ส่งมา
        console.log("Filters to be sent:", filters);

        // ลบพารามิเตอร์ที่ว่างออกจาก filters
        Object.keys(filters).forEach((key) => {
          if (filters[key] === "" || filters[key] === null || filters[key] === undefined) {
            delete filters[key];
          }
        });

        const url =
          fileType === "pdf"
            ? `${import.meta.env.VITE_BASE_URL}/api/pdf/export`
            : `${import.meta.env.VITE_BASE_URL}/api/excel/export`;

        // ลอง log URL ที่จะถูกเรียก
        console.log("Request URL:", url, "Params:", filters);

        // เรียก API และส่งค่าฟิลเตอร์ไปพร้อมกับ request
        const response = await axios.get(url, {
          params: filters, // ส่งค่า filters เป็น query parameters
          responseType: "blob",
        });

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
