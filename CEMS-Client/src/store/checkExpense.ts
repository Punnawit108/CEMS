/*
 * ชื่อไฟล์: checkExpense.ts
 * คำอธิบาย: จัดการสถานะการตรวจสอบค่าใช้จ่าย (checkExpense)
 * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
 * วันที่จัดทำ/แก้ไข: 8 มกราคม 2568
 */

import axios from "axios";
import { defineStore } from "pinia";

export const useCheckExpenseStore = defineStore("expense", {
  state: () => ({
    checkExpense: false,
  }),

  actions: {
    /*
     * คำอธิบาย: เช็คใบคำขอเบิกในระบบ
     * Input: -
     * Output: true/false
     * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
     * วันที่จัดทำ/แก้ไข: 8 มกราคม 2568
     */
    async fetchCheck() {
      const response = await axios.get(
        `${import.meta.env.VITE_BASE_URL}/api/expense/check`
      );
      this.checkExpense =
        !response.data || Object.keys(response.data).length === 0;
    },
  },
});
