import axios from "axios";
import { defineStore } from "pinia";
import { Notification } from '../types/NotificationTypes';

export const useNotification = defineStore('notifications', {

    state: () => ({
        notifications: [] as Notification[],

    }),

    actions: {
        

        async updateStatusNoti(id: number) {
            try {
                // อัปเดต statusNoti เป็น true
                await axios.put(`${import.meta.env.VITE_BASE_URL}/${id}`, { statusNoti: true });

                // อัปเดตข้อมูลใน state
                const notification = this.notifications.find((noti) => noti.id === id);
                if (notification) {
                    notification.statusNoti = true;
                }
            } catch (error) {
                console.error("Failed to update statusNoti:", error);
            }
        },
        async getAllNotifications() {
            const result = await axios.get(`${import.meta.env.VITE_BASE_URL}`)
            this.notifications = result.data
        },
        async getNotificationById(id: any) {
            const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/${id}`)

            return result.data
        },

        async addNotification(data: Notification) {
            await axios.post(`${import.meta.env.VITE_BASE_URL}`, data)
            this.getAllNotifications();


        },
        async editNotification(data: Notification) {
            await axios.put(`${import.meta.env.VITE_BASE_URL}/${data.id}`, data);
            this.getAllNotifications();

        },
        async deleteNotification(id: number) {
            await axios.delete(`${import.meta.env.VITE_BASE_URL}/${id}`);
            this.getAllNotifications();

        }
    }
})