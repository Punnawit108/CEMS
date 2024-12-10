/*
* ชื่อไฟล์: expenseManageStore.ts
* คำอธิบาย: ไฟล์ store API ของประเภทค่าเดินทางและประเภทรถ
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญื เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
*/
import { defineStore } from 'pinia';
import axios from 'axios';
import { ExpenseManage } from '../types';
import { TravelManage } from '../types';

export const useExpenseManageStore = defineStore('expenseManage', {
  state: () => ({
    expenseRows: [] as ExpenseManage[],
    vehicleRows: [] as TravelManage[],
  }),
  actions: {
    async getExpensesType() {
      try {
        const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/RequisitionType/`);
        this.expenseRows = result.data;
        console.log("Fetched expenses:", this.expenseRows);
      } catch (error) {
        console.error("Failed to fetch expense data:", error);
      }
         /*
        * คำอธิบาย: ดึงข้อมูลประเภทค่าใช้จ่ายทั้งหมด
        * Input: -
        * Output: ข้อมูลประเภทค่าใช้จ่ายทั้งหมด
        * ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
        * วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
        */
    },
    async getVehicles() {
          /*
        * คำอธิบาย: ดึงข้อมูลประเภทรถทั้งหมด
        * Input: -
        * Output: ข้อมูลประเภทรถทั้งหมด
        * ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
        * วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
        */
        try {
          const response = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/vehicle/`);
          console.log("API response:", response);  // ตรวจสอบ response จาก API
          this.vehicleRows = response.data;
      
          console.log("Fetched vehicles:", this.vehicleRows);
          
          if (this.vehicleRows && this.vehicleRows.length > 0) {
            console.log("There are", this.vehicleRows.length, "vehicles in the response.");
          } else {
            console.log("No vehicles found.");
          }
        } catch (error) {
          console.error("Failed to fetch vehicle data:", error);
        }
      }
      ,
  },
});
