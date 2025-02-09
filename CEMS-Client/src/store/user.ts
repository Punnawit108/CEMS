/*
* ชื่อไฟล์: user.ts
* คำอธิบาย: ไฟล์ store API ของข้อมูลของผู้ใช้
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/

import { defineStore } from 'pinia';
import axios from 'axios';
import type { User } from '../types/index';

interface UpdateUserRoleDto {
  usrRolName: string;
  usrIsSeeReport: number;
}

// นำเข้า Base URL จาก environment variable
const BASE_URL = import.meta.env.VITE_BASE_URL;

export const useUserStore = defineStore('users', {
  state: () => ({
    user: {} as User,
    users: [] as User[],
    token: localStorage.getItem("token") || null,
  }),
  actions: {
    /*
    * คำอธิบาย: เรียกข้อมูลของผู้ใช้ในระบบ
    * Input: -
    * Output: ข้อมูลผู้ใช้ภายในระบบ
    * ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
    * วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
    */
    async getAllUsers() {
      try {
        const result = await axios.get(`${BASE_URL}/api/user`);
        this.users = result.data;
      } catch (error) {
        console.error('Failed to fetch users:', error);
        throw error;
      }
    },

    async getLocalUsers() {
      try {
        const result = await axios.get(`${BASE_URL}/api/user/localUser`);
        this.users = result.data;
      } catch (error) {
        console.error('Failed to fetch users:', error);
        throw error;
      }
    },
        /*
    * คำอธิบาย: ดึงข้อมูลผู้ใช้ตาม id
    * Input: userId
    * Output: ข้อมูลของผู้ใช้
    * ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
    * วันที่จัดทำ/แก้ไข: 9 กุมภาพันธ์ 2568
    */
    async getUserById(userId : string) {

      console.log(userId)
      try {
        const result = await axios.get(`${BASE_URL}/api/user/${userId}`);
        this.user = result.data;
      } catch (error) {
        console.error('Failed to fetch users:', error);
        throw error;
      }
    },
    /*
    * คำอธิบาย: แก้ไขข้อมูลผู้ใช้ของแต่ละผู้ใช้ตาม id
    * Input: userId, updateData (usrRolName, usrIsSeeReport)
    * Output: ข้อมูลของผู้ใช้ที่ถูกแก้ไข
    * ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
    * วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
    */
    async editUserRole(userId: string, updateData: UpdateUserRoleDto) {
      try {
        await axios.put(`${BASE_URL}/api/user/${userId}`, updateData);
        // อัพเดทข้อมูลใน store
        const userIndex = this.users.findIndex(u => u.usrId === userId);
        if (userIndex !== -1) {
          this.users[userIndex].usrRolName = updateData.usrRolName;
          this.users[userIndex].usrIsSeeReport = updateData.usrIsSeeReport;
        }
      } catch (error) {
        console.error('Failed to update user:', error);
        throw error;
      }
    },
    /*
    * คำอธิบาย: เข้าสู่ระบบ
    * Input: username, password
    * Output: เข้าสู่ระบบเพื่อใช้งานระบบ
    * ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
    * วันที่จัดทำ/แก้ไข: 9 กุมภาพันธ์ 2568
    */
    async loginUser(username: string, password: string) {
      try {
        const result = await axios.post(`${BASE_URL}/api/auth/login`, { username, password });
        this.token = result.data.token;
        if (this.token) {
          localStorage.setItem("token", this.token);
          axios.defaults.headers.common["Authorization"] = `Bearer ${this.token}`;
        }
      } catch (error) {
        console.error('Failed to Login : ', error);
        throw error;
      }
    },

  }
});