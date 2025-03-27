<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonExpenseTypeGrayClick.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonExpenseTypeGrayClick
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <!-- ประเภทค่าใช้จ่ายเทาพื้นหลัง -->
  <button
    class="btn-ประเภทค่าใช้จ่าย bg-grayNormal border-2 border-grayNormal text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ประเภทค่าใช้จ่าย</slot>
  </button>

</template>