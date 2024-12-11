/*
* ชื่อไฟล์: ForUseStatus.vue
* คำอธิบาย: Store สำหรับจัดการข้อมูลเกี่ยวกับค่าใช้จ่ายในระบบอนุมัติ
* ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
* วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
*/
import axios from "axios";
import { defineStore } from "pinia";
import { Expense } from "../types";

export const useExpense = defineStore('expense', {
    state: () => ({
        expense: [] as Expense[] // ตัวแปรสำหรับเก็บรายการการเบิกจ่าย
    }),
    actions:{
        /* 
        * คำอธิบาย: ฟังก์ชันสำหรับดึงประวัติการอนุมัติทั้งหมดจาก API และบันทึกใน state
        * Input: -
        * Output: ค่าใช้จ่ายทั้งหมดจะถูกเก็บใน state.expense
        * ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
        * วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
        */
        async getAllApprovalHistory(){
            try{
                // เรียก API เพื่อนำข้อมูลประวัติการอนุมัติมาเก็บใน state
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approvalList/history`)
                this.expense = result.data; // บันทึกข้อมูลลงใน state
                
            }catch (error){
                // แสดงข้อความแจ้งข้อผิดพลาดหากการเรียก API ล้มเหลว
                console.error("Failed to fetch payment data:", error)
            }
        },

        /* 
        * คำอธิบาย: ฟังก์ชันสำหรับดึงรายการการอนุมัติทั้งหมดจาก API และบันทึกใน state
        * Input: ไม่มี
        * Output: รายการการอนุมัติทั้งหมดจะถูกเก็บใน state.expense
        * ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
        * วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
        */
        async getAllApprovalList(){
            try{
                // เรียก API เพื่อนำข้อมูลรายการการอนุมัติมาเก็บใน state
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approvalList/list `)
                this.expense = result.data; // บันทึกข้อมูลลงใน state
                
            }catch (error){
                // แสดงข้อความแจ้งข้อผิดพลาดหากการเรียก API ล้มเหลว
                console.error("Failed to fetch payment data:", error)
            }
        }
    }
})
