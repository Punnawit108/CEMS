/*
 * คำอธิบาย: ดึงข้อมูลของต่างๆมาแสดงในรูปแบบกราฟ
 * Input: -
 * Output: ภาพรวมข้อมูลต่างๆ
 * ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
 * วันที่จัดทำ/แก้ไข: 10 ธันวาคม 2567
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

  }),
  actions: {
    async getDashboardUser() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/user`
        );
        this.user = result.data;
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
    async getDasboardAdmin() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/admin`
        );
        this.admin = result.data;
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
    async getDasboardAccoutant() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/accountant`
        );
        this.accountant = result.data;
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
    async getDashboardProject() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/project`
        );
        return this.project = result.data;
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
    async getDashboardRequisitionType() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/requisitionType`
        );
        return this.project = result.data;
      } catch (error) {
        console.error("Failed to fetch dashboard:", error);
      }
    },
  },
});


