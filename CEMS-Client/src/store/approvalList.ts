
import axios from "axios";
import { defineStore } from "pinia";

export const useApprovalStore = defineStore('approvalList', {
    state: () => ({
        approvalList: [] as any[],
    }),
    actions: {
        /*
        * คำอธิบาย: เรียกข้อมูลผู้อนุมัติในระบบ
        * Input: -
        * Output: ข้อมูลผู้อนุมัติในระบบ
        * ชื่อผู้เขียน/แก้ไข: นายพรช้ย เพิ่มพูลกิจ
        * วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
        */
        async getApprovalList() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approvalList`)
                this.approvalList = result.data;
            } catch (err) {
                console.log(err)
            }
        }

    }
})