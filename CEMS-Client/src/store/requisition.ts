import axios from "axios";
import { defineStore } from "pinia";

import { Project } from "../types";
import { ExpenseManage } from "../types";

export const useDropdown = defineStore('dropdown' , {
    state: () => ({
        projects: [] as Project[],
        requisitionType : [] as ExpenseManage[] ,
    }),
    actions:{
        async getAllProject() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/dataType/project`);
                this.projects = result.data;
                return this.projects ;
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.error('Error fetching todos:', error.message);
                } else {
                    console.error('Unexpected error:', error);
                }
            }
        },
        async getAllRequisitionType() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/dataType/requisition`);
                this.requisitionType = result.data;
                return this.requisitionType ;
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.error('Error fetching todos:', error.message);
                } else {
                    console.error('Unexpected error:', error);
                }
            }
        },
    }
})