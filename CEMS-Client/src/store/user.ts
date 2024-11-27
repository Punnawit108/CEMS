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
    users: [] as User[]
  }),
  actions: {
    async getAllUsers() {
      try {
        const result = await axios.get(`${BASE_URL}/api/user`);
        this.users = result.data;
      } catch (error) {
        console.error('Failed to fetch users:', error);
        throw error;
      }
    },
    async editUserRole(userId: number, updateData: UpdateUserRoleDto) {
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
    }
  }
});