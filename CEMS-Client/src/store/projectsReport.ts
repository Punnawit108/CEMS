/**
* ชื่อไฟล์: projects.ts
* คำอธิบาย: จัดการ state
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

import axios from "axios";
import { defineStore } from "pinia";
import ProjectReport from "../types/index";


export const useProjectsStore = defineStore("projects", {
    state: () => ({
        projects: [] as ProjectReport[],
    }),
    actions: {
        async getAllProjects() {
            try {
                const result = await axios.get(`https://673c124496b8dcd5f3f86b39.mockapi.io/api/project`);
                this.projects = result.data;
            } catch (error) {
                console.error("Failed to fetch projects:", error);
            }
        },
    },
});