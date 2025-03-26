/*
* ชื่อไฟล์: detail.ts
* คำอธิบาย: ไฟล์ store API สำหรับการจัดการรายงาน PDF
* ชื่อผู้เขียน/แก้ไข: ปุณณะวิชญ์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 3 มีนาคม 2568
*/
import axios from "axios";
import { defineStore } from "pinia";
export const useExportDetailStore = defineStore("exportDetail", {
    actions: {
      async exportFile(expenseId: string) {
        try {
          const url = `${import.meta.env.VITE_BASE_URL}/api/detail/export?expenseId=${expenseId}`;
          const response = await axios.get(url, { responseType: "blob" });
          const blob = new Blob([response.data], { type: "application/pdf" });
          const link = document.createElement("a");
          link.href = window.URL.createObjectURL(blob);
          link.setAttribute("download", `ExportedExpenseData.pdf`);
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
  
