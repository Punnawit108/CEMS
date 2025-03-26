/*
 * ชื่อไฟล์ : expenseReimbursementListStore.ts
 * คำอธิบาย: ไฟล์นี้ใช้สำหรับแสดงความก้าวหน้าของรายการคำขอเบิกค่าใช้จ่าย
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
 */

import axios from "axios";
import { defineStore } from "pinia";

interface ExpenseReimbursementList {
  rqId: string;
  rqUsrName: string;
  rqName: string;
  rqWithDrawDate: string;
  rqExpenses: string;
  rqStatus: string;
  rqPjName: string;
  rqRqtName: string;
}

interface ExpenseReimbursementHistory {
  rqId: string;
  rqUsrName: string;
  rqName: string;
  rqWithDrawDate: string;
  rqExpenses: string;
  rqStatus: string;
  rqPjName: string;
  rqRqtName: string;
}

export const useExpenseReimbursement = defineStore("expenseReimbursement", {
  state: () => ({
    expenseReimbursementList: [] as ExpenseReimbursementList[],
    expenseReimbursementHistory: [] as ExpenseReimbursementHistory[],
  }),
  actions: {
    /*
     * คำอธิบาย: ดึงข้อมูลรายการเบิกค่าใช้จ่ายทั้งหมดจาก API
     * Input: -
     * Output: ข้อมูลรายการเบิกค่าใช้จ่าย
     * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
     * วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
     */
    async getAllExpenseReimbursementList(userId: string) {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/expense/list/${userId}`
        );
        this.expenseReimbursementList = result.data;
      } catch (error) {
        console.error("Failed to fetch ExpenseReimbursementList data:", error);
      }
    },
    /*
     * คำอธิบาย: ลบรายการเบิกค่าใช้จ่ายตาม ID
     * Input: ID
     * Output: ลบข้อมูลรายการเบิกค่าใช้จ่าย
     * ชื่อผู้เขียน/แก้ไข: นครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
     */
    async deleteExpenseReimbursementItem(
      userId: string,
      requisitionId: string
    ) {
      try {
        await axios.delete(
          `${import.meta.env.VITE_BASE_URL}/api/expense/${requisitionId}`
        );

        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/expense/list/${userId}`
        );
        this.expenseReimbursementList = result.data;
      } catch (error) {
        console.error(`Failed to delete item with id ${requisitionId}:`, error);
      }
    },

    /*
     * คำอธิบาย: ดึงข้อมูลประวัติการเบิกค่าใช้จ่ายทั้งหมดจาก API
     * Input: -
     * Output: ข้อมูลประวัติการเบิกค่าใช้จ่าย
     * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
     * วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
     */
    async getAllExpenseReimbursementHistory(userId: string) {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/expense/history/${userId}`
        );
        this.expenseReimbursementHistory = result.data;
      } catch (error) {
        console.error(
          "Failed to fetch ExpenseReimbursementHistory data:",
          error
        );
      }
    },
  },
});
