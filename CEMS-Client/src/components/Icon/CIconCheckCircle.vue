<script setup lang="ts">
/*
 * ชื่อไฟล์: CIconCheckCircle.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ไอคอนรูปเครื่องหมายถูก
 * ชื่อผู้เขียน/แก้ไข: จักรวรรดิ
 * วันที่จัดทำ/แก้ไข: 15 กุมภาพันธ์ 2568
 */
defineProps<{
    size?: number;
}>();
</script>

<template>
    <div>
        <svg :class="`w-[${size}px] h-[${size}px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
                d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
                clip-rule="evenodd" />
        </svg>
    </div>
</template>