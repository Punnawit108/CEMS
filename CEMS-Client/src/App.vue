<script setup lang="ts">
import { RouterView , useRoute} from "vue-router";
import Breadcrumb from "./components/template/Breadcrumb.vue";
import SideBar from "./components/template/SideBar.vue";
import Navbar from "./components/template/Navbar.vue";
import Login from "./pages/Login.vue";
import { computed } from "vue";


const route = useRoute();

const isLoginPage = computed(() => route.name === 'login');
</script>

<template>
  <div>
    <Login v-if="isLoginPage" class="fixed inset-0" />
    <div v-else>
      <div class="flex h-screen">
        <SideBar />
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
