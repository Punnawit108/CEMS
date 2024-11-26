import axios from "axios";
import { defineStore } from "pinia";
import { Expense } from "../types";

export const useExpense = defineStore('expense', {
    state: () => ({
        expense: [] as Expense[]
    }),
    actions:{
        async getAllApprovalHistory(){
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approvalList/history`)
                this.expense = result.data;
                
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },
        async getAllApprovalList(){
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/approvalList/list `)
                this.expense = result.data;
                
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        }
    }
})