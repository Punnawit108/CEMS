/*
* ชื่อไฟล์: requisition.ts
* คำอธิบาย: ไฟล์ store API ของประเภทค่าเดินทางและประเภทรถ
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญื เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
*/

import axios from "axios";
import { defineStore } from "pinia";
import { UserInstead } from "../types/index"
import { Project, ExpenseManage, TravelManage, Expense } from "../types";



export const useRequisitionStore = defineStore('dropdown', {
    state: () => ({
        projects: [] as Project[],
        requisitionType: [] as ExpenseManage[],
        vehicleType: [] as TravelManage[],
        expense: [] as Expense[],
        selectedTravelType: null as any, // เพิ่ม state สำหรับเก็บประเภทการเดินทางที่เลือก
        filteredVehicles: null as any,
        selectedTravel: null as any,
        selectedVehicleType: null as string | null,
        UserInstead: [] as UserInstead[],
        // เพิ่ม state สำหรับเก็บประเภทการเดินทางที่เลือก
    }),
    getters: {
        // Getter สำหรับกรอง vehicleType

        filteredVehicleType: (state) => {
            return state.vehicleType.filter(vehicle =>
                vehicle.vhType === state.selectedTravelType && vehicle.vhVisible == 1
            );
        },

    },
    /*
    * คำอธิบาย: ดึงข้อมูลโปรเจ็ค
    * Input: -
    * Output: ข้อมูลโปรเจ็ค
    * ชื่อผู้เขียน/แก้ไข: พงศธร บุญญามา
    * วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
    */
    actions: {
        // setTravelType(type) {
        //     this.selectedTravelType = type;
        // },
        async getAllProject() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/dataType/project`);
                this.projects = result.data;
                return this.projects;
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.error('Error fetching todos:', error.message);
                } else {

                    console.error('Unexpected error:', error);
                }
            }
        },

        /*
        * คำอธิบาย: ดึงข้อมูลประเภทค่าใช้จ่ายทั้งหมด
        * Input: -
        * Output: ข้อมูลประเภทค่าใช้จ่ายทั้งหมด
        * ชื่อผู้เขียน/แก้ไข: พงศธร บุญญามา
        * วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
        */
        async getAllRequisitionType() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/requisitiontype/list`);
                this.requisitionType = result.data.filter((item: ExpenseManage) => item.rqtVisible === 1);
                return this.requisitionType;
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.error('Error fetching todos:', error.message);
                } else {
                    console.error('Unexpected error:', error);
                }
            }
        },
        /*
        * คำอธิบาย: ดึงข้อมูลประเภทรถ
        * Input: -
        * Output: ข้อมูลประเภทรถ
        * ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
        * วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
        */
        async getAllVehicleType() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/dataType/vehicle`);
                this.vehicleType = result.data;
                return this.vehicleType;
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.error('Error fetching todos:', error.message);
                } else {
                    console.error('Unexpected error:', error);
                }
            }
        },
        /*
        * คำอธิบาย: เพิ่มข้อมูลค่าใช้จ่าย 
        * Input: ข้อมูล CreateExpense 
        * Output: สถานะการสร้างฟอร์ม
        * ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
        * วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
        */
        // ฟังก์ชันสำหรับการโพสต์ค่าใช้จ่ายใหม่
        async createExpense(CreateExpense: FormData) {
            // ตรวจสอบข้อมูลที่ส่งไป

            console.log(CreateExpense)
            try {
                const result = await axios.post(`${import.meta.env.VITE_BASE_URL}/api/expense`, CreateExpense, {
                    headers: {
                        "Content-Type": "multipart/form-data",
                    },
                });
                return result.data;

            } catch (error) {
                console.log(error)
            }
        },
        /*
        * คำอธิบาย: แก้ไขข้อมูลของฟอร์มคำขอเบิก
        * Input: id ของใบคำขอ, updateExpense ฟอร์มแก้ไข
        * Output: แสดงข้อมูลExpense
        * ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
        * วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
        */
        // ฟังก์ชันสำหรับกการputต์ค่าใช้จ่ายใหม่
        async updateExpense(id: string, updateExpense: FormData) {
            try {
                const result = await axios.put(
                    `${import.meta.env.VITE_BASE_URL}/api/expense/${id}`,
                    updateExpense,
                    {
                        headers: {
                            "Content-Type": "multipart/form-data",
                        },
                    }
                );
                return result.data;
            } catch (error) {
                console.error(error);
            }
        },
        /*
        * คำอธิบาย: ดึงข้อมูลประเภทค่าใช้จ่ายตาม id
        * Input: id
        * Output: ข้อมูลประเภทค่าใช้จ่ายตาม id
        * ชื่อผู้เขียน/แก้ไข: พงศธร บุญญามา
        * วันที่จัดทำ/แก้ไข: 13 กุมภาพันธ์ 2568
        */
        async getExpenseById(id: string) {
            //  CreateExpense.rqStatus = "accept";

            try {
                const result = await axios.get(
                    `${import.meta.env.VITE_BASE_URL}/api/expense/${id}`);
                console.log(result.data)
                return result.data;

            } catch (error) {
                console.log(error)
            }
        },
        /*
        * คำอธิบาย: ดึงข้อมูลอีเมลของผู้ใช้
        * Input: usrId
        * Output: ข้อมูลอีเมลของผู้ใช้
        * ชื่อผู้เขียน/แก้ไข: พงศธร บุญญามา
        * วันที่จัดทำ/แก้ไข: 13 กุมภาพันธ์ 2568
        */
        async getUserEmail(usrId: string) {

            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/user/email/${usrId}`);
                return this.UserInstead = result.data;

            } catch (error) {
                console.log(error)
            }
        },
        /*
        * คำอธิบาย: หาข้อมูล rqCode ที่เป็นอันล่าสุด
        * Input: -
        * Output: ข้อมูล rqCode 
        * ชื่อผู้เขียน/แก้ไข: พงศธร บุญญามา
        * วันที่จัดทำ/แก้ไข: 13 กุมภาพันธ์ 2568
        */
        async getRqCode() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/expense/next-rq-code`);
                return result.data.nextRqCode;
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.error('Error fetching todos:', error.message);
                } else {
                    console.error('Unexpected error:', error);
                }
            }
        },
        /*
        * คำอธิบาย: ใช้ในการลบข้อมูลไฟล์
        * Input: fId
        * Output: สถานะการลบข้อมูลไฟล์
        * ชื่อผู้เขียน/แก้ไข: พงศธร บุญญามา
        * วันที่จัดทำ/แก้ไข: 13 กุมภาพันธ์ 2568
        */
        async deleteFile(fId: number) {
            try {
                const result = await axios.delete(`${import.meta.env.VITE_BASE_URL}/api/expense/file/${fId}`);
                return result.data;
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.error('Error deleting file:', error.message);
                } else {
                    console.error('Unexpected error:', error);
                }
            }
        }

    }
});