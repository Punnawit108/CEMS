/*
 * ชื่อไฟล์: approval.ts
 * คำอธิบาย: ไฟล์ store API ของการอนุมัติและผู้อนุมัติทั้งหมด
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ, ธีรวัฒน์ นิระมล
 * วันที่จัดทำ/แก้ไข: 30 ธันวาคม 2567
 */
import axios from "axios";
import { defineStore } from "pinia";
import { ApproverSequence } from "../types";

export const useApprovalStore = defineStore("approval", {
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
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/approval`
        );
        this.approvers = result.data;
      } catch (err) {
        console.log(err);
      }
    },
    /*
     * คำอธิบาย: เพิ่มผู้อนุมัติในระบบ
     * Input: usrId
     * Output: มีการเพิ่มผู้อนุมัติในระบบ
     * ชื่อผู้เขียน/แก้ไข: นายพรช้ย เพิ่มพูลกิจ
     * วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
     */
    async addApprovers(usrId: any) {
      try {
        await axios.post(`${import.meta.env.VITE_BASE_URL}/api/approval`, {
          apUsrId: usrId,
        });
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/approval`
        );
        this.approvers = result.data;
      } catch (err) {
        console.log(err);
      }
    },
    /*
     * คำอธิบาย: แก้ไขลำดับผู้อนุมัติในระบบ
     * Input: apId, apSequence
     * Output: มีการแก้ไขลำดับผู้อนุมัติในระบบ
     * ชื่อผู้เขียน/แก้ไข: นายพรช้ย เพิ่มพูลกิจ
     * วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
     */
    async changeSequence(approverSequence: ApproverSequence) {
      try {
        await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/approval/swapSequence`,
          approverSequence
        );
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/approval`
        );
        this.approvers = result.data;
      } catch (err) {
        console.log(err);
      }
    },
    /*
     * คำอธิบาย: ลบผู้อนุมัติในระบบ
     * Input: approverId
     * Output: มีการลบผู้อนุมัติในระบบ
     * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิะรมล
     * วันที่จัดทำ/แก้ไข: 30 ธันวาคม 2567
     */
    async deleteApprover(approverId: number) {
      try {
        await axios.delete(
          `${import.meta.env.VITE_BASE_URL}/api/approval/${approverId}`
        );
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/approval`
        );
        this.approvers = result.data;
      } catch (err) {
        console.log(err);
      }
    },
  },
});
