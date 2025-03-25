<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonSave.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonSave
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <button
    id="btn-บันทึก"
    class="btn-บันทึก bg-grayNormal text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center text-[14px] font-thin"
    type="button"
    @click="$emit('click')"
  >
    <slot>บันทึก</slot>
  </button>

</template>