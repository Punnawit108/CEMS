<script setup lang="ts">
/*
* ชื่อไฟล์: Navbar.vue
* คำอธิบาย: ไฟล์นี้ Component Navbar หรือ Header
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒ์ นิระมล
* วันที่จัดทำ/แก้ไข: 29 ธันวาคม 2567
*/
import { useRoute } from "vue-router";
import { computed, onMounted, ref } from "vue";
import Icon from "./Icon/CIcon.vue";
import Button from "./Buttons/Button.vue";
import { useLockStore } from "../store/lockSystem";


const route = useRoute();
const lockStore = useLockStore();
const isShowLogout = ref(false);
const userData = ref();

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
  userData.value = JSON.parse(localStorage.getItem("user") || "{}");
  await lockStore.fetchLockStatus();

});

const showLogout = () => {
  isShowLogout.value = !isShowLogout.value;
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
    <div class="mr-6 inline-flex h-9 relative">

      <div v-if="isShowLogout"
        class="absolute right-0 top-12 bg-white shadow-lg rounded-lg w-[230px] flex justify-center flex-col p-2">
        <div class="flex flex-col items-center">
          <div class="flex items-center">
            <svg class="mr-2 w-[32px] h-[32px] text-gray-800 dark:text-white" aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
              <path stroke="gray" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M12 21a9 9 0 1 0 0-18 9 9 0 0 0 0 18Zm0 0a8.949 8.949 0 0 0 4.951-1.488A3.987 3.987 0 0 0 13 16h-2a3.987 3.987 0 0 0-3.951 3.512A8.948 8.948 0 0 0 12 21Zm3-11a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z">
              </path>
            </svg>
            <div>
              <p class="text-sm w-fit font-bold">{{ userData.usrFirstName + " " + userData.usrLastName }}</p>
              <p class="text-xs w-fit text-grayDark">{{ userData.usrRolName }}</p>
            </div>
          </div>
          
          <div class="bg-grayNormal h-[1px] w-[180px] my-2"></div>
          <Button :type="'btn-logout'"></Button>
        </div>

      </div>
      <div class="inline-flex justify-center items-center">
        <div @click="showLogout" class="cursor-pointer">
          <Icon :icon="'profile'" :size="32" />
        </div>
      </div>
    </div>
  </div>
</template>
