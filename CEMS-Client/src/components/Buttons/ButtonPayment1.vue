<script setup lang="ts"></script>
<template>
    
  <button

    class="btn-นำจ่าย bg-green text-white rounded-[6px] h-[35px] px-6 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>นำจ่าย</slot>
  </button>

</template>