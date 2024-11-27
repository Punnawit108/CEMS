<script setup lang="ts">
import { RouterView , useRoute} from "vue-router";
import Breadcrumb from "./components/template/Breadcrumb.vue";
import SideBar from "./components/template/SideBar.vue";
import Navbar from "./components/template/Navbar.vue";
import Login from "./pages/Login.vue";
import { computed ,ref} from "vue";

const userRole = ref(''); 
const route = useRoute();

const isLoginPage = computed(() => route.name === 'login');
// ค่าบทบาทที่จะส่งไปที่ Sidebar

// ฟังก์ชันที่ใช้รับค่าบทบาท
const updateRole = (role: string) => {
  userRole.value = role; // อัพเดทค่า role
};
</script>

<template>
  <div>
    <Login @updateRole="updateRole" v-if="isLoginPage" class="fixed inset-0" />
    <div v-else>
      <div class="flex h-full">
        <SideBar :role="userRole"/>
        <div class="flex-1 overflow-y-auto bg-white">
          <Navbar />
          <Breadcrumb />
          <div class="mx-[25px] my-[24px]">
            <RouterView />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
