/*
* ชื่อไฟล์: paymentStore.ts
* คำอธิบาย: จัดการ state
* ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/
import axios from "axios";
import { defineStore } from "pinia";
export const usePayment = defineStore('expense', {
    state: () => ({
        PaymentList: [] as any[],
        PaymentHistory: [] as any[]
    }),
    actions:{
        /*
        * คำอธิบาย: เรียกข้อมูลรายการรอนำจ่าย
        * Input: -
        * Output: ข้อมูลรายการรอนำจ่าย
        * ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
        * วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
        */
        async getAllPaymentList(){
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/payment/list`)
                this.PaymentList = result.data;
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },
        /*
        * คำอธิบาย: เรียกข้อมูลรายการรอนำจ่าย
        * Input: -
        * Output: ข้อมูลรายการรอนำจ่าย
        * ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
        * วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
        */
        async getPaymentById(id:string){
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/payment/${id}`)
                return result.data;
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },
        /*
        * คำอธิบาย: เรียกข้อมูลรายการประวัติการนำจ่าย
        * Input: -
        * Output: ข้อมูลรายการประวัติการนำจ่าย
        * ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
        * วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
        */
        async getAllPaymentHistory(id:string){
            try{
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/payment/History/${id}`)
                this.PaymentHistory = result.data;
            }catch (error){
                console.error("Failed to fetch payment data:", error)
            }
        },
    }
})

