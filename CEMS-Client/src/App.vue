<script setup lang="ts">
import { RouterView } from "vue-router";
import Navbar from "./components/template/navbar.vue";
import Breadcrumb from "./components/template/Breadcrumb.vue";
import SideBar from "./components/template/SideBar.vue";

</script>

<template>
  <div>
    <div class="flex h-screen">
      <SideBar/>
    <div class="flex-1 overflow-y-auto bg-white">
      <Navbar/>
      <Breadcrumb/>
      <RouterView />
    </div>
  </div>
  </div>
</template>


