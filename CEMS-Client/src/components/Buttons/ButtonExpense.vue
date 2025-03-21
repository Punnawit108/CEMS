<script setup lang="ts"></script>
<template>
      <!-- สร้างใบเบิกค่าใช้จ่าย -->
  <button
    class="btn-สร้างใบเบิกค่าใช้จ่าย bg-green text-white rounded-[6px] h-[35px] px-6 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>สร้างรายการเบิกค่าใช้จ่าย</slot>
  </button>
</template>