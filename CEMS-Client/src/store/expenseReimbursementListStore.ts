import axios from "axios";
import { defineStore } from "pinia";

interface ExpenseReimbursementList{
    rqId:number;
    rqUsrName:string;
    rqName:string;
    rqDatePay:string;
    rqExpenses:string;
    rqStatus:number;
    rqPjName:string;

}
export const useExpenseReimbursementList = defineStore('expenseReimbursementList', {
    state: () => ({
        expenseReimbursementList: [] as ExpenseReimbursementList[]
    }),
    actions:{
        async getAllExpenseReimbursementList(){
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/expense/list`)
                this.expenseReimbursementList = result.data;
            }catch (error){
                console.error("Failed to fetch ExpenseReimbursementList data:", error)
            }
        },
        async getExpenseReimbursementItemById(id: string) {
            try {
                const result = await axios.get(`https://66a40b0044aa6370458338c7.mockapi.io/api/UserSetting/${id}`);
                return result.data; 
            } catch (error) {
                console.error(`Error fetching ExpenseReimbursementList item with ID: ${id}`, error);
                throw error; 
            }
        },
        async deleteExpenseReimbursementItem(id: number) {
            try {
              await axios.delete(`https://66a40b0044aa6370458338c7.mockapi.io/api/UserSetting/${id}`);
              
              this.expenseReimbursementList = this.expenseReimbursementList.filter(item => item.rqId !== id);
            } catch (error) {
              console.error(`Failed to delete item with id ${id}:`, error);
            }
          },
    }
})