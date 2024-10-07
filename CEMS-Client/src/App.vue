<script setup lang="ts">
import { RouterView } from "vue-router";
import SideBar from "./components/sideBar.vue";
</script>

<template>
  <div>
    <div class="flex h-screen">
    <SideBar />
    <div class="flex-1 overflow-y-auto bg-white">
      <RouterView />
    </div>
  </div>
  </div>
</template>


