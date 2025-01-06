/*
 * ชื่อไฟล์: expenseManageStore.ts
 * คำอธิบาย: ไฟล์ store API ของประเภทค่าเดินทางและประเภทรถ
 * ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
 * วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
 */
import { defineStore } from "pinia";
import axios from "axios";
import { ExpenseManage } from "../types";
import { TravelManage } from "../types";

export const useExpenseManageStore = defineStore("expenseManage", {
  state: () => ({
    requitisitionType: [] as ExpenseManage[], // เก็บข้อมูลประเภทค่าใช้จ่าย
    vehiclePublic: [] as TravelManage[], // เก็บข้อมูลประเภทรถ
    vehiclePrivate: [] as TravelManage[],
    tempExpenseType: "", // ใช้เก็บค่าที่ผู้ใช้กรอกใน input
    expenseTypes: [] as string[], // เก็บประเภทค่าใช้จ่ายที่ยืนยันแล้ว
  }),
  actions: {
    // ฟังก์ชันสำหรับปุ่มยกเลิก
    closePopupAddExpense() {
      /*
       * คำอธิบาย: เคลียร์ค่า tempExpenseType ในกรณีกดยกเลิก
       * Input: -
       * Output: tempExpenseType ถูกเคลียร์
       */
      this.tempExpenseType = "";
    },

    // ฟังก์ชันสำหรับดึงข้อมูลประเภทค่าใช้จ่าย
    async getRequisitionType() {
      /*
       * คำอธิบาย: ดึงข้อมูลประเภทค่าใช้จ่ายทั้งหมดจาก API
       * Input: -
       * Output: expenseRows จะถูกอัปเดตด้วยข้อมูลที่ได้จาก API
       */
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/requisitiontype/list`
        );
        return (this.requitisitionType = result.data);
      } catch (error) {
        console.error("Failed to fetch expense data:", error);
        throw new Error("ไม่สามารถดึงข้อมูลประเภทค่าใช้จ่ายได้");
      }
    },

    async getVehiclePublic() {
      /*
       * คำอธิบาย: ดึงข้อมูลประเภทค่าใช้จ่ายทั้งหมดจาก API
       * Input: -
       * Output: expenseRows จะถูกอัปเดตด้วยข้อมูลที่ได้จาก API
       */
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/vehicle/public`
        );
        console.log("Fetched expenses:", result.data);
        return (this.vehiclePublic = result.data);
      } catch (error) {
        console.error("Failed to fetch expense data:", error);
        throw new Error("ไม่สามารถดึงข้อมูลประเภทรถสาธารณะได้");
      }
    },

    async getVehiclePrivate() {
      /*
       * คำอธิบาย: ดึงข้อมูลประเภทค่าใช้จ่ายทั้งหมดจาก API
       * Input: -
       * Output: expenseRows จะถูกอัปเดตด้วยข้อมูลที่ได้จาก API
       */
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/vehicle/private`
        );
        console.log("vhPrivate", result.data);
        return (this.vehiclePrivate = result.data);
      } catch (error) {
        console.error("Failed to fetch expense data:", error);
        throw new Error("ไม่สามารถดึงข้อมูลประเภทรถส่วนตัวได้");
      }
    },
    //post vehicle
    async createVehicle(vehicle: any) {
      try {
        const result = await axios.post(
          `${import.meta.env.VITE_BASE_URL}/api/vehicle`,
          vehicle
        );
        return result.data;
      } catch (error) {
        console.log(error);
      }
    },
    async createRequisitionType(requisitiontype: any) {
      try {
        const result = await axios.post(
          `${import.meta.env.VITE_BASE_URL}/api/requisitiontype`,
          requisitiontype
        );
        return result.data;
      } catch (error) {
        console.log(error);
      }
    },
    async changeRequisitionType(rqtId: number) {
      try {
        await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/requisitiontype/update/${rqtId}`
        );
        this.getRequisitionType()
      } catch (err) {
        console.log(err);
      }
    },
    async changeVehicle(vhId: number) {
      try {
        await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/vehicle/${vhId}`
        );
      } catch (err) {
        console.log(err);
      }
    },
    async deleteVehicle(vhId: number) {
      try {
          const result = await axios.delete(`${import.meta.env.VITE_BASE_URL}/api/vehicle/${vhId}`);
          this.getVehiclePrivate(); // เรียกฟังก์ชันนี้เพื่อดึงข้อมูลใหม่หลังจากลบแล้ว
  
          return result;
  
      } catch (error) {
          console.error("Error deleting disburse data:", error);
      }
  },
  async deleteExpense(rqtId: number) {
    try {
        const result = await axios.delete(`${import.meta.env.VITE_BASE_URL}/api/requisitiontype/${rqtId}`);
        //this.getRequisitionType(); // เรียกฟังก์ชันนี้เพื่อดึงข้อมูลใหม่หลังจากลบแล้ว

        return result;

    } catch (error) {
        console.error("Error deleting disburse data:", error);
    }
},
  
  },
});
