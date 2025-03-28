/*
 * ชื่อไฟล์: notification.ts
 * คำอธิบาย: ไฟล์นี้ใช้สำหรับติดต่อข้อมูลหลังบ้าน
 * ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
 * วันที่จัดทำ/แก้ไข: 15 ธันวาคม 2567
 */
import axios from "axios";
import { ref, computed } from "vue";
import { defineStore } from "pinia";
import { Notification } from "../types/index";
import {
  startHubConnection,
  onNotificationReceived,
  fetchNotifications,
} from "../services/notificationService";

/*
 * คำอธิบาย: สำหรับจัดการข้อมูลแจ้งเตือน และการเชื่อมต่อกับ SignalR
 * Input: -
 * Output: ข้อมูลการแจ้งเตือน
 * ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
 * วันที่จัดทำ/แก้ไข: 15 ธันวาคม 2567
 */
export const useNotificationStore = defineStore("notificationStore", () => {
  const notifications = ref<Array<any>>([]);

  // โหลดข้อมูลการแจ้งเตือน
  const loadNotifications = async (usrId: string) => {
    const fetchedNotifications = await fetchNotifications(usrId);
    notifications.value = fetchedNotifications;
  };

  // เชื่อมต่อ SignalR
  const initSignalR = async (usrId: string) => {
    await startHubConnection();

    // รอ Event เมื่อมีการแจ้งเตือนใหม่
    onNotificationReceived(async () => {
      await loadNotifications(usrId); // อัปเดตข้อมูลแจ้งเตือนใหม่
    });
  };

  // ตัวแปรคำนวณจำนวน unread notifications
  const unreadCount = computed(() => {
    return notifications.value.filter(
      (notification) => notification.ntStatus === "unread"
    ).length;
  });

  return {
    notifications,
    loadNotifications,
    initSignalR,
    unreadCount, // เพิ่ม unreadCount ที่คำนวณอัตโนมัติ
  };
});

export const useNotification = defineStore("notifications", {
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
        await axios.put(
          `${import.meta.env.VITE_BASE_URL}/api/notification/${NtId}`,
          { NtStatus: "read" }
        );

        // อัปเดตข้อมูลใน state
        const notification = this.notifications.find(
          (noti) => noti.NtId === NtId
        );
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
      const result = await axios.get(
        `${import.meta.env.VITE_BASE_URL}/api/notification/list`
      );
      return (this.notifications = result.data);
    },
  },
});
