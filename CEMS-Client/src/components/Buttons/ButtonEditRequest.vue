<script setup lang="ts"></script>
<template>
      <!-- แก้ไขคำขอเบิก -->
  <button
    class="bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>แก้ไขคำขอเบิก</slot>
  </button>
</template>