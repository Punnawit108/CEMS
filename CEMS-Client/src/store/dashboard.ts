/*
 * คำอธิบาย: ดึงข้อมูลของต่างๆมาแสดงในรูปแบบกราฟ
 * Input: -
 * Output: ภาพรวมข้อมูลต่างๆ
 * ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
 * วันที่จัดทำ/แก้ไข: 16 ธันวาคม 2567
 */

import axios from "axios";
import { defineStore } from "pinia";
import {
  DashboardForUser,
  DashboardForAccountant,
  DashboardForAdmin,
  DashboardProject,
  DashboardRequisitionType,
  DashboardPayment,
} from "../types";

//ดึงข้อมูล Api Dashboard ของ Role User
export const useDashboard = defineStore("dashboard", {
  state: () => ({
    user: [] as DashboardForUser[],
    admin: [] as DashboardForAdmin[],
    accountant: [] as DashboardForAccountant[],
    project: [] as DashboardProject[],
    requisitionType: [] as DashboardRequisitionType[],
    payment: [] as DashboardPayment[],
    totalExpense: [],
  }),
  actions: {
    /*
     * คำอธิบาย: ตรวจสอบการรับข้อมูลโครงการจาก API
     * Input: -
     * Output: ข้อมูลโครงการจาก API
     * ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
     * วันที่จัดทำ/แก้ไข: 10 ธันวาคม 2567
     */
    async getDashboardProject() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/project`
        );
        return (this.project = result.data);
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
    /*
     * คำอธิบาย: ตรวจสอบการรับข้อมูลโครงการจาก API แยกตาม user
     * Input: usrId
     * Output: ข้อมูลโครงการจาก API แยกตาม user
     * ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
     * วันที่จัดทำ/แก้ไข: 10 ธันวาคม 2567
     */
    async getDashboardProjectById(usrId: string) {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/project/${usrId}`
        );
        return (this.project = result.data);
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
    /*
     * คำอธิบาย: ตรวจสอบการรับข้อมูลประเภทค่าใช้จ่ายจาก API
     * Input: -
     * Output: ข้อมูลประเภทค่าใช้จ่ายจาก API
     * ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
     * วันที่จัดทำ/แก้ไข: 10 ธันวาคม 2567
     */
    async getDashboardRequisitionType() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/requisitionType`
        );
        return (this.project = result.data);
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
    /*
     * คำอธิบาย: ตรวจสอบการรับข้อมูลประเภทค่าใช้จ่ายจาก API แยกตาม user
     * Input: usrId
     * Output: ข้อมูลประเภทค่าใช้จ่ายจาก API แยกตาม user
     * ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
     * วันที่จัดทำ/แก้ไข: 10 ธันวาคม 2567
     */
    async getDashboardRequisitionTypeById(usrId: string) {
      try {
        const result = await axios.get(
          `${
            import.meta.env.VITE_BASE_URL
          }/api/dashboard/requisitionType/${usrId}`
        );
        return (this.requisitionType = result.data);
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
    /*
     * คำอธิบาย: ตรวจสอบการรับข้อมูลคำขอเบิกค่าใช้จ่ายจาก API
     * Input: -
     * Output: ข้อมูลคำขอเบิกค่าใช้จ่ายจาก API
     * ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
     * วันที่จัดทำ/แก้ไข: 10 ธันวาคม 2567
     */
    async getDashboardTotalExpense() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/totalexpense`
        );
        return (this.totalExpense = result.data);
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
    /*
     * คำอธิบาย: ตรวจสอบการรับข้อมูลคำขอเบิกค่าใช้จ่ายจาก API แยกตาม user
     * Input: usrId
     * Output: ข้อมูลคำขอเบิกค่าใช้จ่ายจาก API แยกตาม user
     * ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
     * วันที่จัดทำ/แก้ไข: 10 ธันวาคม 2567
     */
    async getDashboardTotalExpenseById(usrId: string) {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/totalexpense/${usrId}`
        );
        return (this.totalExpense = result.data);
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
  },
});

export const useDashboardDetail = defineStore("dashboardDetail", {
  state: () => ({
    dashboard: [] as Array<{ key: string; value: number }>,
  }),
  actions: {
    /*
     * คำอธิบาย: ดึงข้อมูลหน้า dashboard และปรับรูปแบบ Array ตาม role ของ user
     * Input: usrId
     * Output: ส่งข้อมูล dashboard
     * ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
     * วันที่จัดทำ/แก้ไข: 16 ธันวาคม 2567
     */
    async getDashboardDetail(user: any) {
      const usrId = user.value.usrId;
      const role = user.value.usrRolName.toLowerCase();
      try {
        if (role == "user" || role == "approver") {
          const result = await axios.get(
            `${import.meta.env.VITE_BASE_URL}/api/dashboard/${role}/${usrId}`
          );
          const data = result.data;
          if (role == "user") {
            this.dashboard = [
              { key: "คำขอรอดำเนินการ", value: data.rqTotalUserWaiting },
              { key: "คำขอเบิกจ่ายแล้ว", value: data.rqTotalUserComplete },
              { key: "โครงการที่ทำการเบิก", value: data.rqTotalUserProject },
              { key: "ยอดเบิกจ่ายแล้ว (บาท)", value: data.rqTotalExpense },
            ];
          } else if (role == "approver") {
            this.dashboard = [
              { key: "คำขอรออนุมัติ", value: data.totalRequisitionsWaiting },
              {
                key: "คำขออนุมัติแล้ว",
                value: data.totalRequisitionsAcceptedOrRejected,
              },
              { key: "รายการเบิกทั้งหมด", value: data.totalRequisitions },
              {
                key: "ยอดที่อนุมัติแล้ว (บาท)",
                value: data.totalRequisitionExpenses,
              },
            ];
          }
          return this.dashboard;
        } else {
          const result = await axios.get(
            `${import.meta.env.VITE_BASE_URL}/api/dashboard/${role}`
          );
          const data = result.data;
          if (role == "admin") {
            this.dashboard = [
              { key: "ผู้ใช้งานทั้งหมด", value: data.totalUser },
              { key: "คำขอเบิกจ่ายทั้งหมด", value: data.totalRqAccept },
              { key: "โครงการทั้งหมด", value: data.totalProject },
              {
                key: "ยอดเบิกจ่ายแล้ว (บาท)",
                value: data.totalRqAcceptExpense,
              },
            ];
          } else if (role == "accountant") {
            this.dashboard = [
              { key: "คำขอรอนำจ่าย", value: data.totalRqPay },
              { key: "นำจ่ายเสร็จสิ้น", value: data.totalRqComplete },
              { key: "รายการเบิกทั้งหมด", value: data.totalRequisition },
              { key: "ยอดเบิกจ่ายแล้ว (บาท)", value: data.totalRqExpense },
            ];
          }
          return this.dashboard;
        }
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
  },
});
