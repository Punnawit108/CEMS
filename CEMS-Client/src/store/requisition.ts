import axios from "axios";
import { defineStore } from "pinia";

import { Project } from "../types";
import { ExpenseManage } from "../types";
import { TravelManage } from "../types";
import { Expense } from "../types";

export const useDropdown = defineStore('dropdown', {
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
        async createExpense(CreateExpense: Expense) {
            try {
                const result = await axios.post(
                    `${import.meta.env.VITE_BASE_URL}/api/expense`,
                    CreateExpense
                );
                this.Expense.push(result.data); // เพิ่มข้อมูลที่ได้รับจาก API เข้าไปใน state
                return result.data;
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.error("Error posting expense:", error.message);
                } else {
                    console.error("Unexpected error:", error);
                }
                return { error: "Failed to create expense. Please try again later." };
            }
        },


    }
});

