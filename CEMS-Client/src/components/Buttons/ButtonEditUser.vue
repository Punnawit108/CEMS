<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonEditUser.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonEditUser
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <!-- แก้ไขผู้ใช้ -->
  <button
    class="btn- แก้ไขผู้ใช้ bg-yellow text-white rounded-[6px] h-[35px] px-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>แก้ไขผู้ใช้</slot>
  </button>
</template>