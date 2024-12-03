/*
* ชื่อไฟล์: approval.ts
* คำอธิบาย: ไฟล์ store API ของการอนุมัติและผู้อนุมัติทั้งหมด
* ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
*/
import axios from "axios";
import { defineStore } from "pinia";

export const useApprovalStore = defineStore('approval', {
    state: () => ({
        approvers: [] as any[],
    }),
    actions: {
        /*
        * คำอธิบาย: เรียกข้อมูลผู้อนุมัติในระบบ
        * Input: -
        * Output: ข้อมูลผู้อนุมัติในระบบ
        * ชื่อผู้เขียน/แก้ไข: นายพรช้ย เพิ่มพูลกิจ
        * วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
        */
        async getApprovers() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approval`)
                this.approvers = result.data;
            } catch (err) {
                console.log(err)
            }
        },
        /*
        * คำอธิบาย: เพิ่มผู้อนุมัติในระบบ
        * Input: usrId
        * Output: มีการเพิ่มผู้อนุมัติในระบบ
        * ชื่อผู้เขียน/แก้ไข: นายพรช้ย เพิ่มพูลกิจ
        * วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
        */
        async addApprovers(usrId : any) {
            try {
                await axios.post(`${import.meta.env.VITE_BASE_URL}/api/approval`,{apUsrId : usrId})
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approval`)
                this.approvers = result.data;
            } catch (err) {
                console.log(err)
            }
        },

    }
})