<script setup lang="ts">
/*
 * ชื่อไฟล์: CIconCirclePlus.vue
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
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
            <path stroke="white" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M12 7.757v8.486M7.757 12h8.486M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
        </svg>
    </div>
</template>