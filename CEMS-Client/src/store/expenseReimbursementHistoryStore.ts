import axios from "axios";
import { defineStore } from "pinia";

interface ExpenseReimbursementHistory{
    id:number;
    name:string;
    expenseType:string;
    date:string;
    amount:string;
    status:number;
    project:string;

}
export let useExpenseReimbursementHistory = defineStore('expenseReimbursementHistory', {
    state: () => ({
        expenseReimbursementHistory: [] as ExpenseReimbursementHistory[]
    }),
    actions:{
        async getAllExpenseReimbursementHistory(){
            try{
                const result = await axios.get(`https://66a40b0044aa6370458338c7.mockapi.io/api/UserSetting`)
                this.expenseReimbursementHistory = result.data;
            }catch (error){
                console.error("Failed to fetch ExpenseReimbursementHistory data:", error)
            }
        },
        async getExpenseReimbursementHistoryById (id: string) {
            try {
                const result = await axios.get(`http://localhost:5247/api/ExpenseReimbursementHistory/${id}`)
                return result.data; 
            } catch (error) {
                console.error("Error fetching ExpenseReimbursementHistory by ID:", error);
            }
        }
    }
})
