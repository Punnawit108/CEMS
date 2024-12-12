import axios from "axios";
import { defineStore } from "pinia";

export const useApprovalStore = defineStore('approvalList', {
    state: () => ({
        approvalList: [] as any[],
    }),
    actions: {
        /*
        * คำอธิบาย: เรียกข้อมูลรายการคำขออนุมัติที่อยู่ในสถานะ "waiting" สำหรับผู้ใช้ที่ระบุ
        * Input: id (หมายเลขประจำตัวผู้ใช้)
        * Output: ข้อมูลรายการคำขออนุมัติ
        * ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
        * วันที่จัดทำ/แก้ไข: 11 ธันวาคม 2567
        */
        async getApprovalList(id: number) {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approvalList/list/${id}`);
                return this.approvalList = result.data;
            } catch (err) {
                console.error("เกิดข้อผิดพลาดในการดึงข้อมูลรายการคำขออนุมัติ:", err);
            }
        },

        /*
        * คำอธิบาย: เรียกข้อมูลประวัติคำขออนุมัติที่ถูกอนุมัติหรือปฏิเสธสำหรับผู้ใช้ที่ระบุ
        * Input: id (หมายเลขประจำตัวผู้ใช้)
        * Output: ข้อมูลประวัติคำขออนุมัติ
        * ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
        * วันที่จัดทำ/แก้ไข: 11 ธันวาคม 2567
        */
        async getApprovalHistory(id: number) {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approvalList/history/${id}`);
                this.approvalList = result.data;
            } catch (err) {
                console.error("เกิดข้อผิดพลาดในการดึงข้อมูลประวัติคำขออนุมัติ:", err);
            }
        }
    }
});