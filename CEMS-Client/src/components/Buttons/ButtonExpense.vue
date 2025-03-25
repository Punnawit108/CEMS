<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonExpense.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonExpense
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <!-- สร้างใบเบิกค่าใช้จ่าย -->
  <button
    class="btn-สร้างใบเบิกค่าใช้จ่าย bg-green text-white rounded-[6px] h-[35px] px-6 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>สร้างใบเบิกค่าใช้จ่าย</slot>
  </button>
</template>