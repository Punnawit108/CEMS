<script setup lang="ts"></script>
<template>
      <!-- ประเภทค่าใช้จ่ายเทาพื้นหลัง -->
  <button
    class="btn-ประเภทค่าใช้จ่าย bg-grayNormal border-2 border-grayNormal text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ประเภทค่าใช้จ่าย</slot>
  </button>

</template>