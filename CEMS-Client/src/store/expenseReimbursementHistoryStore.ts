/*
* คำอธิบาย: ไฟล์นี้ใช้สำหรับแสดงความก้าวหน้าของประวัติคำขอเบิกค่าใช้จ่าย
* ชื่อผู้เขียน/แก้ไข: นครียา วัฒนศรี
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/

import axios from "axios";
import { defineStore } from "pinia";

interface ExpenseReimbursementHistory{
    rqId:number;
    rqUsrName:string;
    rqName:string;
    rqDatePay:string;
    rqExpenses:string;
    rqStatus:number;
    rqPjName:string;

}
export let useExpenseReimbursementHistory = defineStore('expenseReimbursementHistory', {
    state: () => ({
        expenseReimbursementHistory: [] as ExpenseReimbursementHistory[]
    }),
    actions:{
        async getAllExpenseReimbursementHistory(){ //ดึงข้อมูลประวัติการเบิกค่าใช้จ่ายทั้งหมดจาก API
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/expense/History`)
                this.expenseReimbursementHistory = result.data;
            }catch (error){
                console.error("Failed to fetch ExpenseReimbursementHistory data:", error)
            }
        },
        async getExpenseReimbursementHistoryById (id: string) { //ดึงข้อมูลประวัติการเบิกค่าใช้จ่ายตาม ID
            try {
                const result = await axios.get(`http://localhost:5247/api/ExpenseReimbursementHistory/${id}`)
                return result.data; 
            } catch (error) {
                console.error("Error fetching ExpenseReimbursementHistory by ID:", error);
            }
        }
    }
})
