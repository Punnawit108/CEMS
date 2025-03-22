<script setup lang="ts">
/**
* ชื่อไฟล์: ProjectFilter.vue
* คำอธิบาย: ไฟล์นี้แสดงหน้าจอจัดการผู้ใช้ ซึ่งแสดงตารางผู้ใช้ภายในระบบ พร้อมฟังก์ชั่นค้นหาและกรองข้อมูล
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 11 กุมภาพันธ์ 2568
*/
import { Project } from '../../types'

defineProps<{
  modelValue: string | number | undefined
  projects: Project[]
  loading?: boolean
}>()

defineEmits<{
  'update:modelValue': [value: string]
}>()
</script>

<template>
  <div class="h-[32px] min-w-[200px] flex-1">
    <form class="grid">
      <label for="SelectProject" class="py-0.5 text-[14px] text-black text-start">โครงการ</label>
      <div class="relative h-[32px] w-full justify-center items-center">
        <select
          :value="modelValue"
          @change="$emit('update:modelValue', ($event.target as HTMLSelectElement).value)"
          id="SelectProject"
          :disabled="loading"
          class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8"
        >
          <option value="">ทั้งหมด</option>
          <option v-for="project in projects" :key="project.pjId" :value="project.pjId">
            {{ project.pjName }}
          </option>
        </select>
        <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
  <svg width="13" height="8" viewBox="0 0 13 8" fill="none" xmlns="http://www.w3.org/2000/svg">
    <path fill-rule="evenodd" clip-rule="evenodd"
      d="M7.2071 7.2071C6.8166 7.5976 6.1834 7.5976 5.7929 7.2071L0.79289 2.20711C0.40237 1.81658 0.40237 1.18342 0.79289 0.79289C1.18342 0.40237 1.81658 0.40237 2.20711 0.79289L6.5 5.0858L10.7929 0.79289C11.1834 0.40237 11.8166 0.40237 12.2071 0.79289C12.5976 1.18342 12.5976 1.81658 12.2071 2.20711L7.2071 7.2071Z"
      fill="black" />
  </svg>
</div>
      </div>
    </form>
  </div>
</template>

<style scoped>
.custom-select {
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  background-image: none;
}

select, select option {
  background-color: white;
  color: #000000;
}

select:invalid, select option[value=""] {
  color: #999999;
}
</style>