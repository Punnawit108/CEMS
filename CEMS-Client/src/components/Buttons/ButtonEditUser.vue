<script setup lang="ts"></script>
<template>
      <!-- แก้ไขผู้ใช้ -->
  <button
    class="btn- แก้ไขผู้ใช้ bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>แก้ไขผู้ใช้</slot>
  </button>
</template>