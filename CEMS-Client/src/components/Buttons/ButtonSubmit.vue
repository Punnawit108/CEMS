<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonSubmit.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonSubmit
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
  <button
    type="button"
    class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ยืนยัน</slot>
  </button>
</template>