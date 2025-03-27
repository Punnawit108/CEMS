/*
 * ชื่อไฟล์: detail.ts
 * คำอธิบาย: ไฟล์นี้ใช้สำหรับจัดการข้อมูลจาก Api
 * ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ, นายพงศธร บุญญามา
 * วันที่จัดทำ/แก้ไข: 28 พฤศจิกายน 2567
 */

import axios from "axios";
import { defineStore } from "pinia";
import { ApproverRequisition, Expense } from "../types";
import { Approval } from "../types";

export const useDetailStore = defineStore("detailExpense", {
  state: () => ({
    requisition: [] as Expense[], // array ของ Approval
    approvals: [] as Approval[], // array ของ Approval
    approvalRequisitions: [] as ApproverRequisition[],
    selectedExpense: null as Expense | null,
  }),
  actions: {
    /*
     * คำอธิบาย: ดึงข้อมูลของผู้อนุมัติใบเบิกค่าใช้จ่าย
     * Input: rqId
     * Output: ข้อมูลของผู้อนุมัติใบเบิกค่าใช้จ่าย
     * ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
     * วันที่จัดทำ/แก้ไข: 28 พฤศจิกายน 2567
     */
    async getApprover(rqId: string) {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/approval/progress/${rqId}`
        );
        this.approvals = result.data;
        return this.approvals;
      } catch (error) {
        console.error("Error fetching approver data:", error);
      }
    },
    /*
     * คำอธิบาย: ดึงข้อมูลของใบเบิกค่าใช้จ่าย
     * Input: rqId
     * Output: ข้อมูลของใบเบิกค่าใช้จ่าย
     * ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
     * วันที่จัดทำ/แก้ไข: 28 พฤศจิกายน 2567
     */
    async getRequisition(rqId: string) {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/expense/${rqId}`
        );
        this.requisition = result.data;
        return this.requisition;
      } catch (error) {
        console.error("Error fetching approver data:", error);
      }
    },
    /*
     * คำอธิบาย: อนุมัติคำขอเบิกค่าใช้จ่าย
     * Input: data (aprId,aprApId,aprName,aprStatus,rqReason)
     * Output: สถานะการอนุมัติ
     * ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
     * วันที่จัดทำ/แก้ไข: 28 พฤศจิกายน 2567
     */
    async updateApprove(data: any) {
      try {
        const result = await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/approval/approve`,
          data
        );
        this.approvalRequisitions = result.data;

        return this.approvalRequisitions;
      } catch (error) {
        console.error("Error fetching approver data:", error);
      }
    },
    /*
     * คำอธิบาย: อนุมัติการนำจ่ายคำขอเบิกค่าใช้จ่าย
     * Input: data (usrId,rqId)
     * Output: สถานะการอนุมัติ
     * ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
     * วันที่จัดทำ/แก้ไข: 28 พฤศจิกายน 2567
     */
    async updateDisburse(data: any) {
      try {
        const result = await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/approval/disburse`,
          data
        );
        return result;
      } catch (error) {
        console.error("Error fetching approver data:", error);
      }
    },
  },
});
