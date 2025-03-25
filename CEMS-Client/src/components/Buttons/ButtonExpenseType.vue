<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonExpenseType.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonExpenseType
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <!-- ประเภทค่าใช้จ่าย -->
  <button
    class="btn-สร้างใบเบิกค่าใช้จ่าย bg-green text-white rounded-[6px] h-[35px] px-6 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>เพิ่มประเภทค่าใช้จ่าย</slot>
  </button>

</template>