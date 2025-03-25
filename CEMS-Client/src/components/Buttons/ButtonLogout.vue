<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonLogout.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonLogout
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
import { useRouter } from 'vue-router';

const routes = useRouter();

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('updateRole');
        localStorage.removeItem('user');
        routes.push('/login');
    }
</script>
<template>
    <button id="btn-logout"
        class="btn-logout text-Content-Regular bg-redNormal text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center font-thin"
        @click="handleLogout">
        <slot>ออกจากระบบ</slot>
    </button>
</template>