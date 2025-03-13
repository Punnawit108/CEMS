<script setup lang="ts"></script>
<template>
  <!-- ยกเลิกสีเทา -->
  <button
    class="btn-ยกเลิกเทา bg-grayNormal text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ยกเลิก</slot>
  </button>
</template>