import axios from "axios";
import { defineStore } from 'pinia';
import { Expense } from '../types';
import { Approval } from '../types'; 

export const useDetailStore = defineStore('detailExpense', {
    state: () => ({
        approvals: [] as Approval[] , // array ของ Approval
        selectedExpense: null as Expense | null,
    }),
    actions: {
        async setSelectedExpense(expense: Expense) {
            this.selectedExpense = expense;
            await this.getApprover(this.selectedExpense.rqId)
        },
        async getApprover(rqId: Number) {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approval/progress/${rqId}`);
                this.approvals = result.data;
            } catch (error) {
                console.error("Error fetching approver data:", error);
            }
        },
    },
});