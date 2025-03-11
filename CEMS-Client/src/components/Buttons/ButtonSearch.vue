<script setup lang="ts"></script>
<template>
      <button
    id="btn-ค้นหา"
    class="btn-ค้นหา bg-redNormal text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ค้นหา</slot>
  </button>
</template>