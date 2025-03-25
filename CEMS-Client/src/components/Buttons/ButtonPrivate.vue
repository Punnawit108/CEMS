<script setup lang="ts">
/*
 * ชื่อไฟล์: ButtonPrivate.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ButtonPrivate
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
</script>
<template>
      <button
    id="btn-ประเภทรถส่วนตัว"
    class="btn-ประเภทรถส่วนตัว bg-green text-white rounded-[6px] h-[35px] px-6 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>เพิ่มประเภทรถส่วนตัว</slot>
  </button>

</template>