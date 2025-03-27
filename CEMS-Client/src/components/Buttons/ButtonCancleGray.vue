<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonCancleGray.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonCancleGray
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
  <!-- ยกเลิกสีเทา -->
  <button
    class="btn-ยกเลิกเทา bg-grayNormal text-white rounded-[6px] h-[40px] px-6  flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ยกเลิก</slot>
  </button>
</template>