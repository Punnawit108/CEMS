/*
 * ชื่อไฟล์: expenseManageStore.ts
 * คำอธิบาย: ไฟล์ store API ของประเภทค่าเดินทางและประเภทรถ
 * ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน และนางสาวนครียา วัฒนศรี
 * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
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
    /*
     * คำอธิบาย: เคลียร์ค่า tempExpenseType ในกรณีกดยกเลิก
     * Input: -
     * Output: tempExpenseType ถูกเคลียร์
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    // ฟังก์ชันสำหรับปุ่มยกเลิก
    closePopupAddExpense() {
      this.tempExpenseType = "";
    },
    /*
     * คำอธิบาย: ดึงข้อมูลประเภทค่าใช้จ่ายทั้งหมดจาก API
     * Input: -
     * Output: expenseRows จะถูกอัปเดตด้วยข้อมูลที่ได้จาก API
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    // ฟังก์ชันสำหรับดึงข้อมูลประเภทค่าใช้จ่าย
    async getRequisitionType() {
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
    /*
     * คำอธิบาย: ดึงข้อมูลประเภทรถสาธารณะทั้งหมดจาก API
     * Input: -
     * Output: vehiclePublicRows จะถูกอัปเดตด้วยข้อมูลที่ได้จาก API
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async getVehiclePublic() {
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
    /*
     * คำอธิบาย: ดึงข้อมูลประเภทรถส่วนตัวทั้งหมดจาก API
     * Input: -
     * Output: vehiclePrivateRows จะถูกอัปเดตด้วยข้อมูลที่ได้จาก API
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async getVehiclePrivate() {
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
    /*
     * คำอธิบาย: เพิ่มประเภทรถใหม่และบันทึกลงฐานข้อมูล
     * Input: ข้อมูลประเภทรถ
     * Output: ข้อมูลที่สร้างสำเร็จจาก API
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
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
    /*
     * คำอธิบาย: สร้างประเภทคำขอใหม่และบันทึกลงฐานข้อมูล
     * Input: ข้อมูลประเภทคำขอ (requisitiontype)
     * Output: ข้อมูลที่สร้างสำเร็จจาก API
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
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
    /*
     * คำอธิบาย: เปลี่ยนสถานะการใช้งานของประเภทคำขอ (เปิด/ปิด)
     * Input: รหัสประเภทคำขอ (rqtId)
     * Output: อัปเดตสถานะและดึงข้อมูลประเภทคำขอใหม่
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async changeRequisitionType(rqtId: number) {
      try {
        await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/requisitiontype/update/${rqtId}`
        );
        await this.getRequisitionType(); // เรียกข้อมูลใหม่เมื่อสำเร็จ
      } catch (err) {
        console.error("Error updating requisition type:", err);
      }
    },
    /*
     * คำอธิบาย: อัปเดตข้อมูลประเภทคำขอ
     * Input: ข้อมูลประเภทคำขอที่ต้องการอัปเดต
     * Output: อัปเดตข้อมูลและดึงข้อมูลประเภทคำขอใหม่
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async updateRequisitionType(data: any) {
      try {
        await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/requisitiontype`,
          data // ส่งข้อมูลไปใน body
        );
        await this.getRequisitionType(); // เรียกข้อมูลใหม่เมื่อสำเร็จ
      } catch (err) {
        console.error("Error updating requisition type:", err);
      }
    },
    /*
     * คำอธิบาย: เปลี่ยนสถานะการใช้งานของประเภทรถ
     * Input: รหัสประเภทรถที่ต้องการเปลี่ยนสถานะ
     * Output: สถานะการใช้งานถูกเปลี่ยนและดึงข้อมูลประเภทรถใหม่
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async changeVehicle(vhId: number) {
      try {
        await axios.put(`${import.meta.env.VITE_BASE_URL}/api/vehicle/${vhId}`);
      } catch (err) {
        console.log(err);
      }
    },
    /*
     * คำอธิบาย: ลบข้อมูลประเภทรถตามรหัสที่กำหนด
     * Input: รหัสประเภทรถ (vhId)
     * Output: ลบข้อมูลสำเร็จและดึงข้อมูลประเภทรถส่วนตัวใหม่
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async deleteVehicle(vhId: number) {
      try {
        const result = await axios.delete(
          `${import.meta.env.VITE_BASE_URL}/api/vehicle/${vhId}`
        );
        this.getVehiclePrivate(); // เรียกฟังก์ชันนี้เพื่อดึงข้อมูลใหม่หลังจากลบแล้ว

        return result;
      } catch (error) {
        console.error("Error deleting disburse data:", error);
      }
    },
    /*
     * คำอธิบาย: ลบข้อมูลประเภทค่าใช้จ่ายตามรหัสที่กำหนด
     * Input: รหัสประเภทค่าใช้จ่าย (rqtId)
     * Output: ผลลัพธ์จากการลบ
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async deleteExpense(rqtId: number) {
      try {
        const result = await axios.delete(
          `${import.meta.env.VITE_BASE_URL}/api/requisitiontype/${rqtId}`
        );
        //this.getRequisitionType(); // เรียกฟังก์ชันนี้เพื่อดึงข้อมูลใหม่หลังจากลบแล้ว
        return result;
      } catch (error) {
        console.error("Error deleting disburse data:", error);
      }
    },
    /*
     * คำอธิบาย: ตรวจสอบว่าประเภทคำขอถูกใช้งานในระบบหรือไม่
     * Input: รหัสประเภทคำขอ (rqtId)
     * Output: สถานะการใช้งาน (isInUse)
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async validationRequisitionTypes(rqtId: number) {
      try {
        const result = await axios.get(
          `${
            import.meta.env.VITE_BASE_URL
          }/api/requisitiontype/validation/${rqtId}`
        );
        return result.data.isInUse;
      } catch (error) {
        console.error("Failed to fetch requisition types:", error);
        throw error;
      }
    },
    /*
     * คำอธิบาย: ตรวจสอบว่าประเภทรถถูกใช้งานในระบบหรือไม่
     * Input: รหัสประเภทรถ (VhId)
     * Output: สถานะการใช้งาน (isInUse)
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async validationVehicle(VhId: number) {
      try {
        console.log(VhId);
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/vehicle/validation/${VhId}`
        );
        return result.data.isInUse;
      } catch (error) {
        console.error("Failed to fetch vehicle:", error);
        throw error;
      }
    },
    /*
     * คำอธิบาย: อัปเดตข้อมูลประเภทรถสาธารณะ
     * Input: ข้อมูลประเภทรถสาธารณะที่ต้องการอัปเดต
     * Output: ส่งคำขออัปเดตไปยัง API
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async changeVehiclePublic(data: any) {
      try {
        await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/vehicle/update/public`,
          data
        );
      } catch (err) {
        console.log(err);
      }
    },
    /*
     * คำอธิบาย: อัปเดตข้อมูลประเภทรถส่วนตัว
     * Input: ข้อมูลประเภทรถส่วนตัวที่ต้องการอัปเดต
     * Output: ส่งคำขออัปเดตไปยัง API
     * ชื่อผู้เขียน/แก้ไข: นางสาวนครียา วัฒนศรี
     * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
     */
    async changeVehiclePrivate(data: any) {
      try {
        await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/vehicle/update/private`,
          data
        );
      } catch (err) {
        console.log(err);
      }
    },
  },
});
