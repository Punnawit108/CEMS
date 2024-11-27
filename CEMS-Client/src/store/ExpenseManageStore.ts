import { defineStore } from 'pinia';
import axios from 'axios';
import { ExpenseManage } from '../types';
import { TravelManage } from '../types';

export const useExpenseManageStore = defineStore('expenseManage', {
  state: () => ({
    expenseRows: [] as ExpenseManage[],
    vehicleRows: [] as TravelManage[],
  }),
  actions: {
    async getExpensesType() {
      try {
        const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/RequisitionType/`);
        this.expenseRows = result.data;
        console.log("Fetched expenses:", this.expenseRows);
      } catch (error) {
        console.error("Failed to fetch expense data:", error);
      }
    },
    async getVehicles() {
        try {
          const response = await axios.get(`${import.meta.env.VITE_BASE_URL}/vehicle/`);
          console.log("API response:", response);  // ตรวจสอบ response จาก API
          this.vehicleRows = response.data;
      
          console.log("Fetched vehicles:", this.vehicleRows);
      
          if (this.vehicleRows && this.vehicleRows.length > 0) {
            console.log("There are", this.vehicleRows.length, "vehicles in the response.");
          } else {
            console.log("No vehicles found.");
          }
        } catch (error) {
          console.error("Failed to fetch vehicle data:", error);
        }
      }
      ,
  },
});
