<script setup lang="ts">
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