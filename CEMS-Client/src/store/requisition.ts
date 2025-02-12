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
                vehicle.vhType === state.selectedTravelType && vehicle.vhVisible !== 1
            );
        },

    },
    /*
    * คำอธิบาย: requisition.ts
    * Input: -
    * Output: ดึงข้อมูลโปรเจ็ค
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
        * คำอธิบาย: requisition.ts
        * Input: -
        * Output: ดึงข้อมูลประเภทค่าใช้จ่าย
        * ชื่อผู้เขียน/แก้ไข: พงศธร บุญญามา
        * วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
        */
        async getAllRequisitionType() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/requisitiontype/list`);
                this.requisitionType = result.data;
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
        * คำอธิบาย: requisition.ts
        * Input: -
        * Output: ดึงข้อมูลประเภทรถ
        * ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
        * วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
        */
        async getAllvehicleType() {
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
        * คำอธิบาย: requisition.ts
        * Input: เพิ่มข้อมูลค่าใช้จ่าย ข้อมูลExpense
        * Output: -
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
        * คำอธิบาย: requisition.ts
        * Input: อัปเดตข้อมูลExpense
        * Output: แสดงข้อมูลExpense
        * ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
        * วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
        */
        // ฟังก์ชันสำหรับกการputต์ค่าใช้จ่ายใหม่
        async updateExpense(id: string, UpdateExpense: any) {
            //  CreateExpense.rqStatus = "accept";

            try {
                const result = await axios.put(
                    `${import.meta.env.VITE_BASE_URL}/api/expense/${id}`, UpdateExpense);
                return result.data;

            } catch (error) {

                console.log(error)
            }
        },
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
        async getUserEmail(usrId: string) {

            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/user/email/${usrId}`);
                return this.UserInstead = result.data;

            } catch (error) {
                console.log(error)
            }
        }, 
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

    }
});