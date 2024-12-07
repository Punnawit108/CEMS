<script setup lang="ts">
/*
* ชื่อไฟล์: CardNotification.vue
* คำอธิบาย: ใช้สำหรับแสดงข้อมูลแจ้งเตือนแต่ละตัว
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
*/
import { useNotification } from '../../store/notification';
import { Notification } from '../../types/index';
import { defineProps} from 'vue';
const notificationStore = useNotification();


const props = defineProps(["notificationInfo"]);

const updateStatus = (NtId: number) => {
    notificationStore.updateStatusNoti(NtId);

};

// ฟังก์ชันจัดรูปแบบวันที่
const formatDateTime = (dateTime: string): string => {
    const date = new Date(dateTime);
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${day}/${month}/${year} เวลา ${hours}.${minutes} น.`;
};

</script>

<template>
    <section v-for="item in notificationInfo" @click="updateStatus(item.ntId)" class="flex justify-between py-6 pl-4 border-b border-solid border-b-zinc-400 hover:bg-current">
        <div 
            class="flex overflow-hidden flex-col grow shrink pr-80 leading-snug min-w-[240px] w-[788px] max-md:max-w-full">
            <h2 class="text-sm text-gray-800">
                <span>คำขอเบิกค่าใช้จ่าย </span>
                <strong>{{item.ntAprRqPjName }}</strong>
            </h2>
            
            <p v-if="item.ntAprStatus === 'accept'" class="text-xs text-gray-500 max-md:max-w-full">
                รหัส : {{ item.ntAprRqId }}
                ได้รับการอนุมัติเรียบร้อยแล้ว
            </p>
            <p v-else-if="item.ntAprStatus === 'edit'" class="text-xs text-gray-500 max-md:max-w-full">
                รหัส : {{ item.ntAprRqId }}
                แก้ไขเพิ่มเติม กรุณาตรวจสอบเหตุผล และแก้ไขข้อมูลที่จำเป็นเพื่อยื่นคำร้องใหม่อีกครั้ง
            </p>
            <p v-else-if="item.ntAprStatus === 'reject'" class="text-xs text-gray-500 max-md:max-w-full">
                รหัส : {{ item.ntAprRqId }} 
                ไม่ผ่านการอนุมัติ กรุณาตรวจสอบเหตุผล และแก้ไขข้อมูลที่จำเป็นเพื่อยื่นคำร้องใหม่อีกครั้ง
            </p>
            <p v-else-if="item.ntAprStatus === 'waiting'" class="text-xs text-gray-500 max-md:max-w-full">
                รหัส : {{ item.ntAprRqId }} 
                รอดำเนินการอนุมัติ
            </p>
            <p v-else-if="item.ntAprRqProgress === 'paying'" class="text-xs text-gray-500 max-md:max-w-full">
                รหัส : {{ item.ntAprRqId }} 
                รอนำจ่าย
            </p>
            <p v-else-if="item.ntAprRqProgress === 'complete'" class="text-xs text-gray-500 max-md:max-w-full">
                รหัส : {{ item.ntAprRqId }} 
                ได้รับการนำจ่ายเรียบร้อยแล้ว
            </p>
        </div>
        <time class=" text-sm font-medium text-gray-400   flex justify-end mr-4 items-center">
            {{ formatDateTime(item.ntAprDate) }}
        </time>
    </section>
</template>
