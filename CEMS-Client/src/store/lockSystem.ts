/*
* ชื่อไฟล์: lockSystem.ts
* คำอธิบาย: ไฟล์นี้ใช้สำหรับจัดการสถานะการล็อกระบบ
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 29 ธันวาคม 2567
*/
import axios from "axios";
import { defineStore } from "pinia";

export const useLockStore = defineStore("lock", {
    state: () => ({
        isLocked: false as boolean,
    }),
    actions: {
        /*
        * คำอธิบาย: อัปเดตสถานะการล็อกระบบทั้ง
        * Input: -
        * Output: อัปเดตสถานะใน API และ state
        * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
        * วันที่จัดทำ/แก้ไข: 29 ธันวาคม 2567
        */
        async toggleLock() {
            try {
                // กำหนดสถานะใหม่
                const newStatus = this.isLocked ? 0 : 1;
                await axios.post(`${import.meta.env.VITE_BASE_URL}/api/system/lock`, { SttLock: newStatus });

                // อัปเดต state ตามผลลัพธ์
                this.isLocked = newStatus === 1;
            } catch (error) {
                console.error("ไม่สามารถอัปเดตสถานะระบบได้:", error);
            }
        },

        /*
        * คำอธิบาย: เช็คสถานะการล็อกระบบ
        * Input: -
        * Output: สถานะการล็อกระบบ
        * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
        * วันที่จัดทำ/แก้ไข: 29 ธันวาคม 2567
        */
        async fetchLockStatus() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/system/lockStatus`);
                this.isLocked = result.data.status === 1;
            } catch (error) {
                console.error("ไม่สามารถดึงสถานะระบบได้:", error);
            }
        },
    },
});
