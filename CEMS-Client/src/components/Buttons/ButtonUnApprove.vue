<script setup lang="ts"></script>
<template>
      <button
    id="btn-ไม่อนุมัติ"
    class="btn-ไม่อนุมัติ text-Content-Regular bg-redNormal text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center font-thin"
    @click="$emit('click')"
  >
    <slot>ไม่อนุมัติ</slot>
  </button>
</template>