<script setup lang="ts"></script>
<template>
      <!-- ประเภทค่าใช้จ่ายเทา -->
  <button
    class="btn-ประเภทค่าใช้จ่าย bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[35px] px-6 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ประเภทค่าใช้จ่าย</slot>
  </button>
</template>