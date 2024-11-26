import axios from "axios";
import { defineStore } from "pinia";

export const useApprovalStore = defineStore('todo', {
    state: () => ({
        approvers: [] as any[],
    }),
    actions: {
        async getApprovers() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/approval`)
                this.approvers = result.data;
            } catch (err) {
                console.log(err)
            }
        },
        
    }
})