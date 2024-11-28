/**
* ชื่อไฟล์: projectsReport.ts
* คำอธิบาย: จัดการ state
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

import axios from "axios";
import { defineStore } from "pinia";
import {ProjectReport} from "../types/index";


export const useProjectsStore = defineStore("projects", {
    state: () => ({
        projects: [] as ProjectReport[],
    }),
    actions: {
        async getAllProjects() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/project`);
                this.projects = result.data;
            } catch (error) {
                console.error("Failed to fetch projects:", error);
            }
        },
    },
});