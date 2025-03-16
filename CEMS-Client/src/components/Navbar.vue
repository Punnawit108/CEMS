
 <script setup lang="ts">
 /*
 * ชื่อไฟล์: Navbar.vue
 * คำอธิบาย: ไฟล์นี้ Component Navbar หรือ Header
 * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒ์ นิระมล
 * วันที่จัดทำ/แก้ไข: 29 ธันวาคม 2567
 */
 import { ref, watchEffect, defineProps } from "vue";
 import { useRoute } from "vue-router";
 import { computed, onMounted } from "vue";
 import Icon from "./Icon/CIcon.vue";
 import Button from "./Buttons/Button.vue";
 import { useLockStore } from "../store/lockSystem";
 
 const route = useRoute();
 const lockStore = useLockStore();
 
 const navbarTitle = computed(() => {
     if (route.query.fromNotification) {
         return "การแจ้งเตือน"; // แสดง "การแจ้งเตือน" เมื่อมาจากการแจ้งเตือน
     }
     return route.meta.breadcrumb; // แสดง breadcrumb ตามปกติ
 });
 
 let name_navbar = computed(() => {
     if (route.query.fromNotification === 'true') {
         return 'notification'; // แสดงไอคอนการแจ้งเตือนเมื่อมาจากการแจ้งเตือน
     }
     return route.name as string; // แสดงไอคอนตามชื่อเส้นทางปกติ
 });
 
 onMounted(async () => {
   await lockStore.fetchLockStatus();
 });
 
 const handleClick = () => {
   if (lockStore.isLocked) {
     alert("ไม่สามารถทำรายการเบิกได้ในขณะนี้");
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
      <div class="ml-4">
        <H1 class="text-[24px] text-[#2B2D68] font-bold">
          {{ navbarTitle }}
        </H1>
      </div>
    </div>
    <div class="mr-6 inline-flex h-9">
      <!-- ปุ่มเมื่ออยู่ในหน้า listWithdraw -->
      <!-- <div class="mr-6 items-end" v-if="route.name === 'listWithdraw'">
        <RouterLink
          to="/disbursement/listWithdraw/createExpenseForm"
          v-if="!lockStore.isLocked"
        >
          <Button :type="'btn-expense'"></Button>
        </RouterLink>
        <Button v-else :type="'btn-expense'" @click="handleClick"></Button>
      </div> -->
      <!-- ปุ่มเมื่ออยู่ในหน้า listWithdrawDetail -->
      <!-- <div class="mr-6 items-end" v-if="route.name === 'listWithdrawDetail'">
        <RouterLink
          to="/disbursement/listWithdraw/detail/:id"
          v-if="!lockStore.isLocked"
        >
          <Button :type="'btn-print1'"></Button>
        </RouterLink>
      </div>
      <div class="mr-6 items-end" v-if="route.name === 'listWithdrawDetail'">
        <RouterLink
          :to="
            '/disbursement/listWithdraw/detail/' +
            route.params.id +
            '/editExpenseForm'
          "
        >
          <Button :type="'btn-editRequest'"></Button>
        </RouterLink>
      </div> -->
      <Button :type="'btn-logout'" class="mr-5"></Button>
      <div class="inline-flex justify-center items-center">
        <Icon :icon="'profile'" :size="32" />
      </div>
    </div>
  </div>
</template>
