<script setup lang="ts">
/*
 * ชื่อไฟล์: CIconSystemSettingsUser.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ไอคอนตั้งค่าผู้ใช้
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
            <path stroke="red" stroke-width="1.5"
                d="M7 17v1a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1v-1a3 3 0 0 0-3-3h-4a3 3 0 0 0-3 3Zm8-9a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
        </svg>
    </div>
</template>