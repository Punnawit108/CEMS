/*
* ชื่อไฟล์: project.ts
* คำอธิบาย: ไฟล์ store API ของข้อมูลประเภทค่าใช้จ่าย
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 8 ธันวาคม 2567
*/

import { defineStore } from 'pinia';
import axios from 'axios';
import type { Project } from '../types/index';

// นำเข้า Base URL จาก environment variable
const BASE_URL = import.meta.env.VITE_BASE_URL;

export const useProjectStore = defineStore('project', {
  state: () => ({
    projects: [] as Project[]
  }),
  actions: {
    /*
    * คำอธิบาย: เรียกข้อมูลประเภทค่าใช้จ่ายทั้งหมดในระบบ
    * Input: -
    * Output: ข้อมูลประเภทค่าใช้จ่ายภายในระบบ
    * ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
    * วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
    */
    async getAllProjects() {
      try {
        const result = await axios.get(`${BASE_URL}/api/project`);
        this.projects = result.data;
      } catch (error) {
        console.error('Failed to fetch projects:', error);
        throw error;
      }
    }
  }
});