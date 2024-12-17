/*
* ชื่อไฟล์: lockStore.ts
* คำอธิบาย: จัดการ state การล็อกระบบ
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 16 ธันวาคม 2567
*/
import axios from "axios";
import { defineStore } from "pinia";

export const useLockStore = defineStore("lock", {
    state: () => ({
        isLocked: false as boolean,
    }),
    actions: {
        /*
        * คำอธิบาย: อัปเดตสถานะล็อกระบบ
        * Input: lockStatus (boolean) - สถานะล็อก (true = ล็อก, false = ปลดล็อก)
        * Output: อัปเดตสถานะล็อกระบบใน state
        * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
        * วันที่จัดทำ/แก้ไข: 16 ธันวาคม 2567
        */
        async toggleLock(lockStatus: boolean) {
            try {
                await axios.post(`${import.meta.env.VITE_BASE_URL}/api/system/lock`, {
                    status: lockStatus ? 1 : 0,
                });
                this.isLocked = lockStatus;
            } catch (error) {
                console.error("Failed to update lock status:", error);
            }
        },
        /*
        * คำอธิบาย: ดึงสถานะล็อกระบบจาก API
        * Input: -
        * Output: อัปเดตสถานะล็อกระบบใน state
        * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
        * วันที่จัดทำ/แก้ไข: 16 ธันวาคม 2567
        */
        async fetchLockStatus() {
            try {
                const result = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/system/status`);
                this.isLocked = result.data.status === 1;
            } catch (error) {
                console.error("Failed to fetch lock status:", error);
            }
        },
    },
});