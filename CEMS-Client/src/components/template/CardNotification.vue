<script setup lang="ts">
import { useNotification } from '../../store/notification';
import { Notification } from '../../types/index';
import { defineProps, defineEmits } from 'vue';
const notificationStore = useNotification();
const props = defineProps<Notification>();

const updateStatus = (NtId: number) => {
    notificationStore.updateStatusNoti(NtId);

};

</script>

<template>
    <section @click="updateStatus(props.NtId)" class="flex justify-between py-6 pl-4 border-b border-solid border-b-zinc-400 ">
        <div
            class="flex overflow-hidden flex-col grow shrink pr-80 leading-snug min-w-[240px] w-[788px] max-md:max-w-full">
            <h2 class="text-sm text-gray-800">
                <span>คำขอเบิกค่าใช้จ่าย </span>
                <strong>{{ props.NtAprRqPjName }}</strong>
            </h2>
            <p class="text-xs text-gray-500 max-md:max-w-full">
                รหัส : {{ props.NtAprRqId }} {{ props.NtAprDate }}
                และแก้ไขข้อมูลที่จำเป็นเพื่อยื่นคำร้องใหม่อีกครั้ง
            </p>
        </div>
        <time class=" text-sm font-medium text-gray-400   flex justify-end mr-4 items-center">
            เมื่อวานนี้ เวลา 11.12 น.
        </time>
    </section>
</template>
