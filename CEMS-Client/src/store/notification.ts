import axios from "axios";
import { defineStore } from "pinia";
import { Notification } from '../types/index';

export const useNotification = defineStore('notifications', {

    state: () => ({
        notifications: [] as Notification[],

    }),
        /*
        * คำอธิบาย: อัปเดตสถานะของแจ้งเตือนจาก "ยังไม่อ่าน" เป็น "อ่านแล้ว"
        * Input: id แจ้งเตือน
        * Output: ข้อมููลการแจ้งเตือน id นั้นๆถูกเปลี่ยนสถานะ
        * ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
        * วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
        */
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
        /*
        * คำอธิบาย: ดึงข้อมูลแจ้งเตือนจาก back-end
        * Input: -
        * Output: ข้อมููลการแจ้งเตือน
        * ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
        * วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
        */
        async getAllNotifications() {
            const result = await axios.get(`${import.meta.env.VITE_BASE_URL}`)
            this.notifications = result.data
        },
       


    }
})