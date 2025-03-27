<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonPayment1.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonPayment1
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
    
  <button

    class="btn-นำจ่าย bg-green text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>นำจ่าย</slot>
  </button>

</template>