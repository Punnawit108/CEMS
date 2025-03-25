<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonUnApprove.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonUnApprove
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <button
    id="btn-ไม่อนุมัติ"
    class="btn-ไม่อนุมัติ text-Content-Regular bg-redNormal text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center font-thin"
    @click="$emit('click')"
  >
    <slot>ไม่อนุมัติ</slot>
  </button>
</template>