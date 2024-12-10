/*
 * คำอธิบาย: ดึงข้อมูลของต่างๆมาแสดงในรูปแบบกราฟ
 * Input: -
 * Output: ภาพรวมข้อมูลต่างๆ
 * ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
 * วันที่จัดทำ/แก้ไข: 6 ธันวาคม 2567
 */

import axios from "axios";
import { defineStore } from "pinia";
import {
  Dashboard,
  DashboardForUser,
  DashboardForAccountant,
  DashboardForAdmin,
} from "../types";

//ดึงข้อมูล Api Dashboard ของ Role User
export const useDashboardForUser = defineStore("dashboard", {
  state: () => ({
    expenses: [] as DashboardForUser[],
  }),
  actions: {
    async getAllExpenses() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/user`
        );
        this.expenses = result.data;
      } catch (error) {
        console.error("Failed to fetch expenses:", error);
      }
    },
  },
});

//ดึงข้อมูล Api Dashboard ของ Role Admin
export const useDashboardForAdmin = defineStore("dashboard", {
  state: () => ({
    expenses: [] as DashboardForAdmin[],
  }),
  actions: {
    async getAllExpenses() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/admin`
        );
        this.expenses = result.data;
      } catch (error) {
        console.error("Failed to fetch expenses:", error);
      }
    },
  },
});

//ดึงข้อมูล Api Dashboard ของ Role Accountant
export const useDashboardForAccountant = defineStore("dashboard", {
  state: () => ({
    expenses: [] as DashboardForAccountant[],
  }),
  actions: {
    async getAllExpenses() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/dashboard/accountant`
        );
        this.expenses = result.data;
      } catch (error) {
        console.error("Failed to fetch expenses:", error);
      }
    },
  },
});
