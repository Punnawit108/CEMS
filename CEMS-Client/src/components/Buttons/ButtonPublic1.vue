<script setup lang="ts"></script>
<template>
      <button
    class="btn-ประเภทรถสาธารณะ bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>เพิ่มประเภทรถสาธารณะ</slot>
  </button>
</template>