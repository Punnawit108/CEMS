<script setup lang="ts">
import { RouterView } from "vue-router";
import Breadcrumb from "./components/template/Breadcrumb.vue";
import SideBar from "./components/template/SideBar.vue";
import Navbar from "./components/template/Navbar.vue";

</script>

<template>
  <div>
    <div class="flex h-screen">
      <SideBar/>
    <div class="flex-1 overflow-y-auto bg-white">
      <Navbar/>
      <Breadcrumb/>
      <div class="mx-[25px] my-[24px] ">
        <RouterView />
      </div>
    </div>
  </div>
  </div>
</template>


