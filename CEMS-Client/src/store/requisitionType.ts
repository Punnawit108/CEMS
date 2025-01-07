/*
* ชื่อไฟล์: requisitionTypeStore.ts
* คำอธิบาย: ไฟล์ store API ของข้อมูลประเภทค่าใช้จ่าย
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/

import { defineStore } from 'pinia';
import axios from 'axios';
import type { ExpenseManage } from '../types/index';

const BASE_URL = import.meta.env.VITE_BASE_URL;

export const useRequisitionTypeStore = defineStore('requisitiontype', {
  state: () => ({
    requisitionTypes: [] as ExpenseManage[]
  }),
  actions: {
    /*
    * คำอธิบาย: เรียกข้อมูลของประเภทค่าใช้จ่ายทั้งหมดในระบบ
    * Input: -
    * Output: ข้อมูลประเภทค่าใช้จ่ายภายในระบบ
    * ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
    * วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
    */
    async getAllRequisitionTypes() {
      try {
        const result = await axios.get(`${BASE_URL}/api/requisitiontype/list`);
        this.requisitionTypes = result.data;
      } catch (error) {
        console.error('Failed to fetch requisition types:', error);
        throw error;
      }
    },
    
  }
});