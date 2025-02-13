<script setup lang="ts"></script>
<template>
  <!-- แก้ไขโครงการ + ปิดรับคำขอ + แก้ไขลำดับ-->
  <button
    class="btn- แก้ไขโครงการ bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>แก้ไขโครงการ+ ปิดรับคำขอ + แก้ไขลำดับ</slot>
  </button>
</template>
