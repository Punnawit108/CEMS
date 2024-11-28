/**
* ชื่อไฟล์: expensesReport.ts
* คำอธิบาย: จัดการ state
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

import axios from "axios"
import { defineStore } from "pinia";
import { ExpenseReportList, ExpenseReportGraph } from "../types/index";


export const useExpensesListStore = defineStore("expenses", {
    state: () => ({
        expenses: [] as ExpenseReportList[],
    }),
    actions: {
        async getAllExpenses() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/expense/report`);
                this.expenses = result.data;
            } catch (error) {
                console.error("Failed to fetch expenses:", error);
            }
        },
    },
});

export const useExpensesGraphStore = defineStore("expensegraph", {
    state: () => ({
        expensegraph: [] as ExpenseReportGraph[],
    }),
    actions: {
        async getExpensesGraph() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/expense/graph`);
                this.expensegraph = result.data;
            } catch (error) {
                console.error("Failed to fetch expenses:", error);
            }
        },
    },
});