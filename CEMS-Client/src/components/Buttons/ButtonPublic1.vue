<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonPublic.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonPublic
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <button
    class="btn-ประเภทรถสาธารณะ bg-green text-white rounded-[6px] h-[35px] px-6 flex items-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>เพิ่มประเภทรถสาธารณะ</slot>
  </button>
</template>