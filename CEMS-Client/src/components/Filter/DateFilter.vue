<script setup lang="ts">
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

const props = withDefaults(defineProps<Props>(), {
  loading: false,
  error: false
});

defineEmits<{
  'update:modelValue': [value: Date]
  'update:isOpen': [value: boolean] 
  'confirm': [value: Date]
  'cancel': []
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