<script setup lang="ts">
/*
 * ชื่อไฟล์: CIconApproval.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ไอคอนอนุมัติ
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
            <path stroke="red" stroke-linecap="round" stroke-width="2"
                d="M9 8h10M9 12h10M9 16h10M4.99 8H5m-.02 4h.01m0 4H5" />
        </svg>
    </div>
</template>