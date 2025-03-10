<script setup lang="ts"></script>

<template>
  <button
    class="btn-อนุมัติ bg-green text-white rounded-[6px] h-[35px]  px-6 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>อนุมัติ</slot>
  </button>
</template>
