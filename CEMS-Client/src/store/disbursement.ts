import axios from "axios";
import { defineStore } from "pinia";

interface Disbursement{
    id:number;
    name:string;
}
export const useDisbursement = defineStore('disbursement', {
    state: () => ({
        disbursement: [] as Disbursement[]
    }),
    actions:{
        async getAllDisbursement(){
            try{
                const result = await axios.get("https://66a3ee7844aa63704582ec67.mockapi.io/api/disbursement")
                this.disbursement = result.data;
            }catch (error){
                console.error("Failed to fetch disbursement data:", error)
            }
        },
    }
})