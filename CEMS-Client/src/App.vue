<script setup lang="ts">
import { RouterView } from "vue-router";
import Sidebar from "./components/sidebarAccountant.vue";

</script>

<template>
  <div>
    <div>
    <Sidebar/>
    <div class="flex-1 overflow-y-auto bg-white">
      <RouterView />
    </div>
  </div>
  </div>
</template>


