import { defineStore } from 'pinia';
import axios from 'axios';
import type { User } from '../types/index';

interface UpdateUserRoleDto {
  usrRolName: string;
  usrIsSeeReport: number;
}

export const useUsers = defineStore('users', {
  state: () => ({
    users: [] as User[]
  }),
  actions: {
    async getAllUsers() {
      try {
        const result = await axios.get('http://localhost:5247/api/user');
        this.users = result.data;
      } catch (error) {
        console.error('Failed to fetch users:', error);
        throw error;
      }
    },
    async editUserRole(userId: number, updateData: UpdateUserRoleDto) {
      try {
        await axios.put(`http://localhost:5247/api/user/${userId}`, updateData);
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