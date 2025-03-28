<script setup lang="ts">
/*
 * ชื่อไฟล์: CardNotification.vue
 * คำอธิบาย: ใช้สำหรับแสดงข้อมูลแจ้งเตือนแต่ละตัว
 * ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
 * วันที่จัดทำ/แก้ไข: 18 มีนาคม 2568
 */
import { useNotification } from "../store/notification";
import { defineProps, computed } from "vue";
import { useRouter } from "vue-router";

const notificationStore = useNotification();
const router = useRouter();

const props = defineProps(["notificationInfo"]);

const updateStatus = (NtId: number) => {
  notificationStore.updateStatusNoti(NtId);
};

const formatDateTime = (dateTime: string): string => {
  const date = new Date(dateTime);
  const day = date.getDate().toString().padStart(2, "0");
  const month = (date.getMonth() + 1).toString().padStart(2, "0");
  const year = date.getFullYear();
  const hours = date.getHours().toString().padStart(2, "0");
  const minutes = date.getMinutes().toString().padStart(2, "0");
  return `${day}/${month}/${year} เวลา ${hours}.${minutes} น.`;
};

const getStatusMessage = (rqStatus: string, rqProgress: string): string => {
  if (rqStatus === "waiting") {
    return "รอดำเนินการอนุมัติ";
  }
  if (rqStatus === "accept") {
    if (rqProgress === "paying") {
      return "รอนำจ่าย";
    }
    if (rqProgress === "complete") {
      return "ได้รับการนำจ่ายเรียบร้อยแล้ว";
    }
    return "ได้รับการอนุมัติเรียบร้อยแล้ว";
  }
  if (rqStatus === "edit") {
    return "แก้ไขเพิ่มเติม กรุณาตรวจสอบเหตุผล และแก้ไขข้อมูลที่จำเป็นเพื่อยื่นคำร้องใหม่อีกครั้ง";
  }
  if (rqStatus === "reject") {
    return "ไม่ผ่านการอนุมัติ กรุณาตรวจสอบเหตุผล และแก้ไขข้อมูลที่จำเป็นเพื่อยื่นคำร้องใหม่อีกครั้ง";
  }
  return "";
};

const sortedNotifications = computed(() => {
  return props.notificationInfo.sort((a: any, b: any) => {
    // เรียงลำดับ unread ก่อน read
    if (a.ntStatus === "unread" && b.ntStatus === "read") return -1;
    if (a.ntStatus === "read" && b.ntStatus === "unread") return 1;

    // เรียงลำดับตามวันที่ล่าสุดเป็นอันดับแรก
    const dateA = new Date(a.ntAprDate).getTime();
    const dateB = new Date(b.ntAprDate).getTime();
    return dateB - dateA; // จากใหม่ไปเก่า
  });
});

const navigateToDetail = (
  ntId: number,
  ntAprStatus: string,
  ntAprRqProgress: string
) => {
  if (
    ntAprStatus === "reject" ||
    ntAprRqProgress === "complete" ||
    ntAprRqProgress === "paying"
  ) {
    router.push({
      path: `/disbursement/historyWithdraw/detail/${ntId}`,
      query: { fromNotification: "true" }, // ส่ง query parameter เพื่อบอกว่าเข้ามาจากการแจ้งเตือน
    });
  } else if (ntAprStatus === "edit") {
    router.push({
      path: `/disbursement/listWithdraw/detail/${ntId}`,
      query: { fromNotification: "true" }, // ส่ง query parameter เพื่อบอกว่าเข้ามาจากการแจ้งเตือน
    });
  } else if (ntAprStatus === "waiting" || ntAprRqProgress === "accepting") {
    router.push({
      path: `/approval/list/detail/${ntId}`,
      query: { fromNotification: "true" }, // ส่ง query parameter เพื่อบอกว่าเข้ามาจากการแจ้งเตือน
    });
  }
};
</script>

<template>
  <section
    v-for="(item, index) in sortedNotifications"
    :key="item.ntId"
    @click="
      () => {
        updateStatus(item.ntId);
        navigateToDetail(
          item.ntAprRqId,
          item.ntAprStatus,
          item.ntAprRqProgress
        );
      }
    "
    class="flex justify-between py-6 pl-4 hover:cursor-pointer"
    :class="[
      item.ntStatus === 'unread' ? 'bg-white' : 'bg-[#f7f7f7]',
      index !== 9 ? 'border-b border-solid border-b-[#B6B7BA]' : '',
    ]"
  >
    <div
      class="flex overflow-hidden flex-col grow shrink pr-80 leading-snug min-w-[240px] w-[788px] max-md:max-w-full"
    >
      <h2 class="text-sm text-gray-800">
        <span>คำขอเบิกค่าใช้จ่าย </span>
        <strong>{{ item.ntAprRqPjName }}</strong>
      </h2>
      <p class="text-xs text-gray-500 max-md:max-w-full">
        รหัส : {{ item.ntAprRqCode }}
        {{ getStatusMessage(item.ntAprStatus, item.ntAprRqProgress) }}
      </p>
    </div>
    <time
      class="text-sm font-medium text-gray-400 flex justify-end mr-4 items-center"
    >
      {{ formatDateTime(item.ntAprDate) }}
    </time>
  </section>
</template>
