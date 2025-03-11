<script setup lang="ts"></script>
<template>
      <button
    id="btn-พิมพ์"
    class="btn-พิมพ์ bg-navyNormal text-white rounded-[6px] h-[35px] px-6 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>ส่งออก</slot>
  </button>
</template>