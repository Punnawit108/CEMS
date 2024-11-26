import { defineStore } from "pinia";
import axios from "axios";

export const useExpenseManageStore = defineStore("expenseManage", {
  state: () => ({
    expenseRows: [] as Array<any>,
    vehicleRows: [] as Array<any>,
  }),
  actions: {
    async fetchExpenses() {
      try {
        const response = await axios.get("http://localhost:5247/api/RequisitionType");
        this.expenseRows = response.data;
      } catch (error) {
        console.error("Error fetching expenses:", error);
      }
    },
    async fetchVehicles() {
      try {
        const response = await axios.get("http://localhost:5247/api/vehicle");
        this.vehicleRows = response.data;
      } catch (error) {
        console.error("Error fetching vehicles:", error);
      }
    },
  },
});
