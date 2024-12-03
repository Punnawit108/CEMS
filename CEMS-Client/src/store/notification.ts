/*
* ชื่อไฟล์: notification.ts
* คำอธิบาย: ไฟล์นี้ใช้สำหรับติดต่อข้อมูลหลังบ้าน
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
*/
import axios from "axios";
import { defineStore } from "pinia";
import { Notification } from '../types/index';

export const useNotification = defineStore('notifications', {

    state: () => ({
        notifications: [] as Notification[],
    }),

    actions: {
        /*
        * คำอธิบาย: อัปเดตสถานะของแจ้งเตือนจาก "ยังไม่อ่าน" เป็น "อ่านแล้ว"
        * Input: id แจ้งเตือน
        * Output: ข้อมููลการแจ้งเตือน id นั้นๆถูกเปลี่ยนสถานะ
        * ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
        * วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
        */
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
        /*
        * คำอธิบาย: ดึงข้อมูลแจ้งเตือนจาก back-end
        * Input: -
        * Output: ข้อมููลการแจ้งเตือน
        * ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
        * วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
        */
        async getAllNotifications() {
            const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api`)
            this.notifications = result.data
        },
        
    }
})