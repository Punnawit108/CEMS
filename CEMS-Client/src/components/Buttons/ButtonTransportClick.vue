<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonTransportClick.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonTransportClick
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <button
    id="btn-ประเภทค่าเดินทาง"
    class="btn-ประเภทค่าเดินทาง bg-grayNormal border-2 border-grayNormal text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ประเภทค่าเดินทาง</slot>
  </button>
</template>