/*
 * ชื่อไฟล์: projectsReport.ts
 * คำอธิบาย: จัดการ state ของ project
 * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
 * วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
 */
import axios from "axios";
import { defineStore } from "pinia";
import { ProjectReport } from "../types/index";

export const useProjectsStore = defineStore("projects", {
  state: () => ({
    projects: [] as ProjectReport[],
  }),
  actions: {
    /*
     * คำอธิบาย: ดึงข้อมูลของโครงการทั้งหมด
     * Input: -
     * Output: ข้อมูลของโครงการทั้งหมด
     * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
     * วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
     */
    async getAllProjects() {
      try {
        const result = await axios.get(
          `${import.meta.env.VITE_BASE_URL}/api/project`
        );
        this.projects = result.data;
      } catch (error) {
        console.error("Failed to fetch projects:", error);
      }
    },
  },
});
