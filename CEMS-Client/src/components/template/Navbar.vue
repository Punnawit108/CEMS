<script setup lang="ts">
/*
* ชื่อไฟล์: navbar.vue
* คำอธิบาย: ไฟล์นี้ Component navbar หรือ Header
* ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
* วันที่จัดทำ/แก้ไข: 29 ธันวาคม 2567
*/
import { useRoute } from 'vue-router';
import { computed, onMounted } from 'vue';
import Icon from './CIcon.vue';
import Button from './Button.vue';
import { useLockStore } from '../../store/lockSystem';

// ใช้ route เพื่อดึงข้อมูลเส้นทางปัจจุบัน
const route = useRoute();
const lockStore = useLockStore();

// คำนวณข้อความของ navbar ตามเส้นทางปัจจุบัน
let name_navbar: string = '';
const navbarTitle = computed(() => {
    name_navbar = route.name as string;
    return route.meta.breadcrumb;
});

onMounted(async () => {
    await lockStore.fetchLockStatus();
});

const handleClick = () => {
    if (lockStore.isLocked) {
        alert('ไม่สามารถทำรายการเบิกได้ในขณะนี้');
    }
};
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
            </div>
        </div>
        <div class="mr-6 inline-flex h-9 ">
            <!-- ปุ่มเมื่ออยู่ในหน้า listWithdraw -->
            <div class=" mr-6 items-end " v-if="route.name === 'listWithdraw'">
                <RouterLink to="/disbursement/listWithdraw/createExpenseForm" v-if="!lockStore.isLocked">
                    <Button :type="'btn-expense'"></Button>
                </RouterLink>
                <Button v-else :type="'btn-expense'" @click="handleClick" :disabled="lockStore.isLocked"></Button>
            </div>
            <!-- ปุ่มเมื่ออยู่ในหน้า listWithdrawDetail -->
            <div class=" mr-6 items-end " v-if="route.name === 'listWithdrawDetail'">
                <RouterLink to="/disbursement/listWithdraw/detail/:id" v-if="!lockStore.isLocked">
                    <Button :type="'btn-print1'"></Button>
                </RouterLink>
            </div>
            <div class=" mr-6 items-end " v-if="route.name === 'listWithdrawDetail'">
                <RouterLink :to="'/disbursement/listWithdraw/detail/' + route.params.id + '/editExpenseForm'">
                    <Button :type="'btn-editRequest'"></Button>
                </RouterLink>
            </div>
            <div class="inline-flex justify-center items-center">
                <Icon :icon="'profile'" :size="32" />
            </div>
        </div>
    </div>
</template>
