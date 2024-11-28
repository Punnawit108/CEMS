import axios from "axios";
import { defineStore } from "pinia";

interface ExpenseReimbursementHistory{
    rqId:number;
    rqUsrName:string;
    rqName:string;
    rqDatePay:string;
    rqExpenses:string;
    rqStatus:number;
    rqPjName:string;

}
export let useExpenseReimbursementHistory = defineStore('expenseReimbursementHistory', {
    state: () => ({
        expenseReimbursementHistory: [] as ExpenseReimbursementHistory[]
    }),
    actions:{
        async getAllExpenseReimbursementHistory(){
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/expense/History`)
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
