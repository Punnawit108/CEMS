import axios from "axios";
import { defineStore } from "pinia";
import { Notification } from '../types/index';

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
       


    }
})