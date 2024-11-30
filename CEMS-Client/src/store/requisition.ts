import axios from "axios";
import { defineStore } from "pinia";

import { Project, ExpenseManage, TravelManage, Expense } from "../types";



export const useRequisitionStore = defineStore('dropdown', {
    state: () => ({
        projects: [] as Project[],
        requisitionType: [] as ExpenseManage[],
        vehicleType: [] as TravelManage[],
        Expense: [] as Expense[],
        selectedTravelType: "" as string, // เพิ่ม state สำหรับเก็บประเภทการเดินทางที่เลือก
    }),
    getters: {
        // Getter สำหรับกรอง vehicleType
        filteredVehicleType: (state) => {
            return state.vehicleType.filter(vehicle => vehicle.vhType === state.selectedTravelType);
        }
    },
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


        // ฟังก์ชันสำหรับการโพสต์ค่าใช้จ่ายใหม่
        async createExpense(CreateExpense: any) {
            //  CreateExpense.rqStatus = "accept";
            // console.log(CreateExpense)
            try {
                const result = await axios.post(
                    `${import.meta.env.VITE_BASE_URL}/api/expense`, {CreateExpense});
                return result.data;

            } catch (error) {
                console.log(error)
            }
        },
        // ฟังก์ชันสำหรับการputต์ค่าใช้จ่ายใหม่
        async updateExpense(id: string,CreateExpense: any) {
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



    }
});

// export const createExpense = async (data: any): Promise<any> => {
//     try {
//         const response = await axios.post( `${import.meta.env.VITE_BASE_URL}/api/expense`, data);
//         return response.data;
//     } catch (error) {
//         throw new Error("Error creating expense");
//     }
// };
