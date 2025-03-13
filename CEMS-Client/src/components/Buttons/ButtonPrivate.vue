<script setup lang="ts"></script>
<template>
      <button
    id="btn-ประเภทรถส่วนตัว"
    class="btn-ประเภทรถส่วนตัว bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>เพิ่มประเภทรถส่วนตัว</slot>
  </button>

</template>