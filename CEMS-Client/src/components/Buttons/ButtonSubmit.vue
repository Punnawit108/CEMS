<script setup lang="ts"></script>
<template>
  <button
    type="button"
    class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ยืนยัน</slot>
  </button>
</template>