<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonExpenseTypeGray.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonExpenseTypeGray
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
 </script>
<template>
      <!-- ประเภทค่าใช้จ่ายเทา -->
  <button
    class="btn-ประเภทค่าใช้จ่าย bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[35px] px-6 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ประเภทค่าใช้จ่าย</slot>
  </button>
</template>