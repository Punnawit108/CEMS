<script setup lang="ts">
/*
 * ชื่อไฟล์: DateFilter.vue
 * คำอธิบาย: ไฟล์นี้เป็น Component สำหรับตัวเลือกวันที่ ใช้ในการกรองข้อมูลตามช่วงเวลาที่ต้องการ ประกอบด้วยฟังก์ชันการเลือกวันที่ การตรวจสอบความถูกต้อง และการแสดงผลในรูปแบบที่กำหนด
 * ชื่อผู้เขียน/แก้ไข: จิรภัทร มณีวงษ์
 * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
 */
// นำเข้า Component SingleDatePicker สำหรับใช้เป็นตัวเลือกวันที่
import SingleDatePicker from '../SingleDatePicker.vue'

interface Props {
  modelValue: Date // วันที่ที่เลือก
  loading?: boolean // สถานะ loading
  label: string // ชื่อ label (วันที่เริ่มต้น/วันที่สิ้นสุด)
  isOpen: boolean // สถานะการเปิด/ปิด datepicker
  confirmedDate?: Date // วันที่ที่ยืนยันแล้ว
  minDate?: Date // วันที่ต่ำสุดที่สามารถเลือกได้
  error?: boolean // สถานะข้อผิดพลาด (เพิ่มใหม่)
}

// กำหนดค่าเริ่มต้นให้กับ props ที่เป็น optional
const props = withDefaults(defineProps<Props>(), {
  loading: false, // ค่าเริ่มต้นของ loading คือ false
  error: false    // ค่าเริ่มต้นของ error คือ false
});

// กำหนด events ที่จะส่งออกไปยัง parent component
defineEmits<{
  'update:modelValue': [value: Date]  // event เมื่อมีการเปลี่ยนแปลงค่าวันที่
  'update:isOpen': [value: boolean]   // event เมื่อมีการเปลี่ยนแปลงสถานะเปิด/ปิด
  'confirm': [value: Date]            // event เมื่อยืนยันการเลือกวันที่
  'cancel': []                        // event เมื่อยกเลิกการเลือกวันที่
}>()
</script>

<template>
  <div class="h-[32px] min-w-[200px] flex-1">
    <form class="grid">
      <label class="py-0.5 text-[14px] text-start" :class="error ? 'text-red-500' : 'text-black'">{{ label }}</label>
      <div class="relative h-[32px] w-full date-picker-container">
        <SingleDatePicker
          :model-value="modelValue"
          @update:model-value="$emit('update:modelValue', $event)"
          placeholder="dd/mm/yyyy"
          :disabled="loading"
          :class="{ 'border-red-500': error }"
          class="w-full"
          @confirm="$emit('confirm', $event)"
          @cancel="$emit('cancel')"
          :confirmedDate="confirmedDate"
          :isOpen="isOpen"
          :min-date="minDate"
          @update:isOpen="$emit('update:isOpen', $event)"
        />
      </div>
    </form>
  </div>
</template>