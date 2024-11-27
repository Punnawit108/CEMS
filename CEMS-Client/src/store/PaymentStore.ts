/**
* ชื่อไฟล์: paymentStore.ts
* คำอธิบาย: จัดการ state
* ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/
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
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/payment/list`)
                this.expense = result.data;
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },async getPaymentById(id:string){
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/payment/${id}`)
                return result.data;
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },async getAllPaymentHistory(){
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/payment/History`)
                this.expense = result.data;
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },
    }
})

