<script setup lang="ts"></script>
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