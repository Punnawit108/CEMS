<script setup lang="ts"></script>
<template>
      <!-- ประเภทค่าใช้จ่าย -->
  <button
    class="btn-สร้างใบเบิกค่าใช้จ่าย bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>เพิ่มประเภทค่าใช้จ่าย</slot>
  </button>

</template>