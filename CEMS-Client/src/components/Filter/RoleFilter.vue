<script setup lang="ts">
/*
 * ชื่อไฟล์: RoleFilter.vue
 * คำอธิบาย: ไฟล์นี้เป็น Component สำหรับตัวเลือกบทบาท ใช้ในการกรองข้อมูลตามบทบาทที่ต้องการ
 * ชื่อผู้เขียน/แก้ไข: จิรภัทร มณีวงษ์
 * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
 */
// นำเข้า computed จาก Vue และ User interface จาก types
import { computed } from 'vue'
import { User } from '../../types'

interface Props {
  modelValue: string  // ค่าบทบาทที่เลือก
  users: User[]       // ข้อมูลผู้ใช้ทั้งหมด
  loading?: boolean   // สถานะ loading
}

// รับค่า props
const props = defineProps<Props>()

// กำหนด events ที่จะส่งออกไปยัง parent component
const emit = defineEmits<{
  'update:modelValue': [value: string]  // event เมื่อมีการเปลี่ยนแปลงค่าบทบาทที่เลือก
}>()

// คำนวณรายชื่อบทบาทที่ไม่ซ้ำกันจากข้อมูลผู้ใช้
const roles = computed(() => {
  // ใช้ Set เพื่อเก็บค่าที่ไม่ซ้ำกัน
  const roles = new Set(props.users.map(user => user.usrRolName))
  // แปลงกลับเป็น array และเรียงลำดับตามตัวอักษร
  return Array.from(roles).sort()
})
</script>

<template>
  <div class="h-[32px] min-w-[200px] flex-1">
    <form class="grid">
      <label for="SelectRole" class="py-0.5 text-[14px] text-black text-start">บทบาท</label>
      <div class="relative h-[32px] w-full justify-center items-center">
        <select
          :value="modelValue"
          @change="$emit('update:modelValue', ($event.target as HTMLSelectElement).value)"
          id="SelectRole"
          :disabled="loading"
          class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8"
        >
          <option value="">ทั้งหมด</option>
          <option v-for="role in roles" :key="role" :value="role">{{ role }}</option>
        </select>
        <!-- ไอคอนลูกศรสำหรับ dropdown (custom design) -->
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
/* กำหนด custom styles สำหรับ select element */
.custom-select {
  -webkit-appearance: none; /* ซ่อนลูกศรดั้งเดิมของ select บน webkit browsers */
  -moz-appearance: none; /* ซ่อนลูกศรดั้งเดิมของ select บน mozilla browsers */
  appearance: none; /* ซ่อนลูกศรดั้งเดิมของ select */
  background-image: none; /* ไม่ใช้ background image ดั้งเดิม */
}

/* กำหนดสีพื้นหลังและสีข้อความสำหรับ select และ option */
select, select option {
  background-color: white;
  color: #000000;
}

/* กำหนดสีข้อความสำหรับ option ที่ว่างหรือไม่ถูกต้อง */
select:invalid, select option[value=""] {
  color: #999999;
}
</style>