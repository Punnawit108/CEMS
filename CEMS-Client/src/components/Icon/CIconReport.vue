<script setup lang="ts">
/*
 * ชื่อไฟล์: CIconReport.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ไอคอนรายงาน
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
            <path stroke="red" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M3 15v4m6-6v6m6-4v4m6-6v6M3 11l6-5 6 5 5.5-5.5" />
        </svg>
    </div>
</template>