<script setup lang="ts"></script>
<template>
      <button
    id="btn-ประเภทค่าเดินทาง"
    class="btn-ประเภทค่าเดินทาง bg-grayNormal border-2 border-grayNormal text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ประเภทค่าเดินทาง</slot>
  </button>
</template>