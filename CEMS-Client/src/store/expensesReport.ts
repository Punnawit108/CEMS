/**
* ชื่อไฟล์: expenses.ts
* คำอธิบาย: จัดการ state
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

import axios from "axios"
import { defineStore } from "pinia";
import ExpenseReportList from "../types/index";
import ExpenseReportGraph from "../types/index";


export const useExpensesListStore = defineStore("expenses", {
    state: () => ({
        expenses: [] as ExpenseReportList[],
    }),
    actions: {
        async getAllExpenses() {
            try {
                const result = await axios.get(`https://673c124496b8dcd5f3f86b39.mockapi.io/api/expense`);
                this.expenses = result.data;
            } catch (error) {
                console.error("Failed to fetch expenses:", error);
            }
        },
    },
});

export const useExpensesGraphStore = defineStore("expenses", {
    state: () => ({
        expenses: [] as ExpenseReportGraph[],
    }),
    actions: {
        async getAllExpenses() {
            try {
                const result = await axios.get(`https://673c124496b8dcd5f3f86b39.mockapi.io/api/expense`);
                this.expenses = result.data;
            } catch (error) {
                console.error("Failed to fetch expenses:", error);
            }
        },
    },
});