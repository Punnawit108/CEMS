import axios from "axios";
import { defineStore } from "pinia";
import { Expense } from "../types";


export const usePayment = defineStore('expense', {
    state: () => ({
        expense: [] as Expense[]
    }),
    actions:{
        async getAllPaymentList(){
            try{
                const result = await axios.get(``)
                this.expense = result.data;
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },async getPaymentById(){
            try{
                const result = await axios.get(``)
                return result.data;
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },async getAllPaymentHistory(){
            try{
                const result = await axios.get(`http://localhost:5247/api/payment/History`)
                this.expense = result.data;
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },
    }
})

