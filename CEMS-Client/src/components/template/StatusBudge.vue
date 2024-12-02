<script setup lang="ts">
/*
* ชื่อไฟล์: StatusBudge.vue
* คำอธิบาย: ไฟล์นี้เป็น Component Status
* ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/

import { defineProps } from 'vue';

const props = defineProps<{
    status: String
}>();

</script>

<template>
    <div class="flex flex-col items-center text-center ">

        <!-- content -->
        <div
            class="flex gap-4 justify-center items-center px-4 text-xs leading-snug text-center text-white whitespace-nowrap bg-emerald-500 rounded-3xl">
            <!-- อนุมัติ -->
            <span v-if="status==='sts-accept'" class="!text-white rounded-[28px] bg-[#12B669] w-[82px] h-[24px] flex justify-center items-center font-sarabun">อนุมัติ</span>
            <!-- รออนุมัติ -->
            <span v-if="status==='sts-waiting'" class="!text-white rounded-[28px] bg-[#1976D2] w-[75px] h-[24px] flex justify-center items-center font-sarabun">รออนุมัติ</span>
            <!-- ไม่อนุมัติ -->
            <span v-if="status==='sts-reject'" class="!text-white rounded-[28px] bg-[#E1032B] w-[74px] h-[24px] flex justify-center items-center font-sarabun">ไม่อนุมัติ</span>
            <!-- แก้ไข -->
            <span v-if="status==='sts-edit'" class="!text-white rounded-[28px] bg-[#FFBE40] w-[58px] h-[24px] flex justify-center items-center font-sarabun">แก้ไข</span>
            <!-- แบบร่าง -->
            <span v-if="status==='sts-sketch'" class="!text-white rounded-[28px] bg-[#B6B7BA] w-[72px] h-[24px] flex justify-center items-center font-sarabun">แบบร่าง</span>
            <!-- accept -->
            <!-- <span v-if="status==='sts-accept'" class="!text-white rounded-[28px] bg-[#12B669] w-[72px] h-[24px] flex justify-center items-center font-sarabun">accept</span> -->
            <!-- reject -->
            <!-- <span v-if="status==='sts-reject'" class="!text-white rounded-[28px] bg-[#E1032B] w-[72px] h-[24px] flex justify-center items-center font-sarabun">reject</span> -->
        </div>
        <!-- content -->
    </div>
</template>