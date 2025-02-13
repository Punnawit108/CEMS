<script setup lang="ts"></script>
<template>
      <button
    id="btn-บันทึก"
    class="btn-บันทึก bg-grayNormal text-black rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    type="button"
    @click="$emit('click')"
  >
    <slot>บันทึก</slot>
  </button>

</template>