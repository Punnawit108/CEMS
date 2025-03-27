<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonCancleBorderGray.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonCancleBorderGray
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
  <!-- ยกเลิกสีกรอบเทา -->
  <button
    class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[35px] px-6  flex items-center justify-center text-[14px] font-thin"
    type="button"
    @click="$emit('click')"
  >
    <slot>ยกเลิก</slot>
  </button>
</template>