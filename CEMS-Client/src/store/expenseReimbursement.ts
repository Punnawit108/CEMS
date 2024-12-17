/*
* ชื่อไฟล์ : expenseReimbursementListStore.ts
* คำอธิบาย: ไฟล์นี้ใช้สำหรับแสดงความก้าวหน้าของรายการคำขอเบิกค่าใช้จ่าย
* ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
*/

import axios from "axios";
import { defineStore } from "pinia";

interface ExpenseReimbursementList {
    rqId: number;
    rqUsrName: string;
    rqName: string;
    rqWithDrawDate: string;
    rqExpenses: string;
    rqStatus: number;
    rqPjName: string;
    rqRqtName: string;

}

interface ExpenseReimbursementHistory {
    rqId: number;
    rqUsrName: string;
    rqName: string;
    rqWithDrawDate: string;
    rqExpenses: string;
    rqStatus: number;
    rqPjName: string;
    rqRqtName: string;
}

export const useExpenseReimbursement = defineStore('expenseReimbursement', {
    state: () => ({
        expenseReimbursementList: [] as ExpenseReimbursementList[],
        expenseReimbursementHistory: [] as ExpenseReimbursementHistory[]
    }),
    actions: {
        /*
        * คำอธิบาย: ดึงข้อมูลรายการเบิกค่าใช้จ่ายทั้งหมดจาก API
        * Input: -
        * Output: ข้อมูลรายการเบิกค่าใช้จ่าย
        * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
        * วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
        */
        async getAllExpenseReimbursementList(id: string) {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/expense/list/${id}`)
                this.expenseReimbursementList = result.data;
            } catch (error) {
                console.error("Failed to fetch ExpenseReimbursementList data:", error)
            }
        },
        /*
       * คำอธิบาย: ดึงข้อมูลรายการเบิกค่าใช้จ่ายตาม ID
       * Input: ID
       * Output: ข้อมูลรายการเบิกค่าใช้จ่าย
       * ชื่อผู้เขียน/แก้ไข: นครียา วัฒนศรี
       * วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
       */
        async getExpenseReimbursementItemById(id: string) {
            try {
                const result = await axios.get(`https://66a40b0044aa6370458338c7.mockapi.io/api/UserSetting/${id}`);
                return result.data;
            } catch (error) {
                console.error(`Error fetching ExpenseReimbursementList item with ID: ${id}`, error);
                throw error;
            }
        },
        /*
        * คำอธิบาย: ลบรายการเบิกค่าใช้จ่ายตาม ID
        * Input: ID
        * Output: ลบข้อมูลรายการเบิกค่าใช้จ่าย
        * ชื่อผู้เขียน/แก้ไข: นครียา วัฒนศรี
        * วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
        */
        async deleteExpenseReimbursementItem(id: number) {
            try {
                await axios.delete(`https://66a40b0044aa6370458338c7.mockapi.io/api/UserSetting/${id}`);

                this.expenseReimbursementList = this.expenseReimbursementList.filter(item => item.rqId !== id);
            } catch (error) {
                console.error(`Failed to delete item with id ${id}:`, error);
            }
        },

        /*
               * คำอธิบาย: ดึงข้อมูลประวัติการเบิกค่าใช้จ่ายทั้งหมดจาก API
               * Input: -
               * Output: ข้อมูลประวัติการเบิกค่าใช้จ่าย
               * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
               * วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
               */
        async getAllExpenseReimbursementHistory(id: string) {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/expense/history/${id}`)
                this.expenseReimbursementHistory = result.data;
            } catch (error) {
                console.error("Failed to fetch ExpenseReimbursementHistory data:", error)
            }
        },
        /*
       * คำอธิบาย: ดึงข้อมูลประวัติการเบิกค่าใช้จ่ายตาม ID
       * Input: ID
       * Output: ข้อมูลประวัติการเบิกค่าใช้จ่าย
       * ชื่อผู้เขียน/แก้ไข: นครียา วัฒนศรี
       * วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
       */
        async getExpenseReimbursementHistoryById(id: string) { //
            try {
                const result = await axios.get(`http://localhost:5247/api/ExpenseReimbursementHistory/${id}`)
                return result.data;
            } catch (error) {
                console.error("Error fetching ExpenseReimbursementHistory by ID:", error);
            }
        }
    }
})