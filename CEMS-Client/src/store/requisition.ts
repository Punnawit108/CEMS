/*
* ชื่อไฟล์: requisition.ts
* คำอธิบาย: ไฟล์ store API ของประเภทค่าเดินทางและประเภทรถ
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญื เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
*/

import axios from "axios";
import { defineStore } from "pinia";

import { Project, ExpenseManage, TravelManage, Expense } from "../types";



export const useRequisitionStore = defineStore('dropdown', {
    state: () => ({
        projects: [] as Project[],
        requisitionType: [] as ExpenseManage[],
        vehicleType: [] as TravelManage[],
        expense: [] as Expense[],
        selectedTravelType: null as any, // เพิ่ม state สำหรับเก็บประเภทการเดินทางที่เลือก
    }),
    getters: {
        // Getter สำหรับกรอง vehicleType
        filteredVehicleType: (state) => {
            return state.vehicleType.filter(vehicle => vehicle.vhType === state.selectedTravelType);
        },
        filteredProjectType: (state) => {
            return state.projects.filter(vehicle => vehicle.vhType === state.selectedTravelType);
        }
    },
    /*
    * คำอธิบาย: requisition.ts
    * Input: -
    * Output: ดึงข้อมูลโปรเจ็ค
    * ชื่อผู้เขียน/แก้ไข: พงศธร บุญญามา
    * วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
    */
    actions: {
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
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/dataType/requisition`);
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
        async createExpense(CreateExpense: any) {
            console.log(CreateExpense)

            try {
                const result = await axios.post(
                    `${import.meta.env.VITE_BASE_URL}/api/expense`, CreateExpense);
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
        async updateExpense(id: string, CreateExpense: any) {
            //  CreateExpense.rqStatus = "accept";
            // console.log(CreateExpense)
            try {
                const result = await axios.put(
                    `${import.meta.env.VITE_BASE_URL}/api/expense/${id}`, { CreateExpense });
                return result.data;

            } catch (error) {
                console.log(error)
            }
        },
        async getExpenseById(id: string) {
            //  CreateExpense.rqStatus = "accept";
            // console.log(CreateExpense)
            try {
                const result = await axios.get(
                    `${import.meta.env.VITE_BASE_URL}/api/expense/${id}`);
                return result.data;

            } catch (error) {
                console.log(error)
            }
        },

    }
});