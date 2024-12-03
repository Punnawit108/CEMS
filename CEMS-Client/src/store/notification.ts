import axios from "axios";
import { defineStore } from "pinia";
import { Notification } from '../types/index';

export const useNotification = defineStore('notifications', {

    state: () => ({
        notifications: [] as Notification[],

    }),

    actions: {
        async updateStatusNoti(NtId: number) {
            try {
                // อัปเดต statusNoti เป็น true
                await axios.put(`${import.meta.env.VITE_BASE_URL}/${NtId}`, { NtStatus: "read" });

                // อัปเดตข้อมูลใน state
                const notification = this.notifications.find((noti) => noti.NtId === NtId);
                if (notification) {
                    notification.NtStatus = "read";
                }
            } catch (error) {
                console.error("Failed to update NtStatus:", error);
            }
        },

        async getAllNotifications() {
            const result = await axios.get(`${import.meta.env.VITE_BASE_URL}`)
            this.notifications = result.data
        },
       


    }
})