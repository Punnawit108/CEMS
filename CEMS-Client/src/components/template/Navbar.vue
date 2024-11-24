<script setup lang="ts">
/**
 * ชื่อไฟล์ : navbar.vue
 * คำอธิบาย : ไฟล์นี้ Component navbar หรือ Header
 * Input : -
 * Output : ข้อมูล Component navbar หรือ Header
 * ชื่อผู้เขียน / แก้ไข : อังคณา อุ่นเสียม
 * วันที่จัดทำ / วัยที่แก้ไข : 11 พฤศจิกายน 2567
 */
import { useRoute } from 'vue-router';
import { computed } from 'vue';
import Icon from './CIcon.vue';
import Button from './Button.vue';
// ใช้ route เพื่อดึงข้อมูลเส้นทางปัจจุบัน
const route = useRoute();

// คำนวณข้อความของ navbar ตามเส้นทางปัจจุบัน
let name_navbar: string = '';
const navbarTitle = computed(() => {
    name_navbar = route.name as string


    return route.meta.breadcrumb
});

</script>

<template>
    <!-- navbar -->
    <div class="w-full inline-flex justify-between items-center pt-6">
        <div class="ml-6 inline-flex items-center">
            <div>
                <Icon :icon="name_navbar" :size="32" />
            </div>
            <div class=" ml-4 ">
                <H1 class=" text-[24px] text-[#000000]">
                    {{ navbarTitle }}
                </H1>
                <!-- ถ้าเป็นหน้า expense จะแสดง -->
            </div>
        </div>
        <div class="mr-6 inline-flex h-9 ">
            <div class=" mr-6 items-end " v-if="route.name === 'listWithdraw'">
                <RouterLink to="/disbursement/listWithdraw/createExpenseForm">
                    <Button :type="'btn-expense'"></Button>
                </RouterLink>
            </div>
            <div class=" mr-6 items-end " v-if="route.name === 'listWithdrawDetail'">
                <RouterLink to="/disbursement/listWithdraw/detail/:id">
                    <Button :type="'btn-print1'"></Button>
                </RouterLink>
            </div>
            <div class=" mr-6 items-end " v-if="route.name === 'listWithdrawDetail'">
                <RouterLink to="/disbursement/listWithdraw/detail/:id">
                    <Button :type="'btn-editRequest'"></Button>
                </RouterLink>
            </div>
            <div class="inline-flex justify-center items-center">
                <Icon :icon="'profile'" :size="32" />
            </div>
        </div>
    </div>

</template>
