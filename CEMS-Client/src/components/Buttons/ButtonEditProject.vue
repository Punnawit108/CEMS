<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonEditProject.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonEditProject
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
  <!-- ปิดรับคำขอ + แก้ไขลำดับ-->
  <button
    class="btn- แก้ไขโครงการ bg-yellow text-white rounded-[6px] h-[32px] px-6 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>แก้ไขลำดับ</slot>
  </button>
</template>
