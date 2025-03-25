<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonPrint2.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonPrint2
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <button
    id="btn-พิมพ์"
    class="btn-พิมพ์ bg-navyNormal text-white rounded-[6px] h-[35px] px-6 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>ส่งออก</slot>
  </button>
</template>