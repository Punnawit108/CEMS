<script setup lang="ts"></script>
<template>
  <!-- ยกเลิกสีกรอบเทา -->
  <button
    class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    type="button"
    @click="$emit('click')"
  >
    <slot>ยกเลิก</slot>
  </button>
</template>