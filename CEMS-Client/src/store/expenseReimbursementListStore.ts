import axios from "axios";
import { defineStore } from "pinia";

interface ExpenseReimbursementList{
    id:number;
    name:string;
    expenseType:string;
    date:string;
    amount:string;
    status:number;
    project:string;

}
export const useExpenseReimbursementList = defineStore('expenseReimbursementList', {
    state: () => ({
        expenseReimbursementList: [] as ExpenseReimbursementList[]
    }),
    actions:{
        async getAllExpenseReimbursementList(){
            try{
                const result = await axios.get(`https://66a40b0044aa6370458338c7.mockapi.io/api/UserSetting`)
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
              
              this.expenseReimbursementList = this.expenseReimbursementList.filter(item => item.id !== id);
            } catch (error) {
              console.error(`Failed to delete item with id ${id}:`, error);
            }
          },
    }
})