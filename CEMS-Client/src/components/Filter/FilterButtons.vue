<script setup lang="ts">
 /*
 * ชื่อไฟล์: FilterButtons.vue
 * คำอธิบาย: ไฟล์นี้เป็น Component สำหรับปุ่มค้นหาและล้างการกรอง ใช้ร่วมกับ Component กรองข้อมูลอื่นๆ
 * ชื่อผู้เขียน/แก้ไข: จิรภัทร มณีวงษ์
 * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
 */

interface Props {
  loading?: boolean   // สถานะ loading
}

// รับค่า props
const props = defineProps<Props>()

// กำหนด events ที่จะส่งออกไปยัง parent component
const emit = defineEmits<{
  'reset': []     // event เมื่อกดปุ่มล้าง (reset)
  'search': []    // event เมื่อกดปุ่มค้นหา (search)
}>()
</script>

<template>
  <div class="h-[32px]">
    <div class="grid grid-cols-2 gap-4">
      <!-- 
        ปุ่มล้างการกรอง:
        - ใช้สีพื้นหลังโปร่งใส และขอบสีน้ำตาล
        - เปลี่ยนสีพื้นหลังเมื่อเลื่อนเมาส์ผ่าน (hover)
        - :disabled="loading": ปิดการใช้งานเมื่ออยู่ในสถานะ loading
        - @click: ส่ง event reset เมื่อคลิกปุ่ม
      -->
      <button 
        type="button"
        class="bg-transparent hover:bg-gray-50 text-[#B67D12] rounded-md text-sm border border-[#B67D12] transition-colors h-[32px]"
        :disabled="loading"
        @click="$emit('reset')"
      >
        ล้าง
      </button>
      <!-- 
        ปุ่มค้นหา:
        - ใช้สีพื้นหลังน้ำตาล และข้อความสีขาว
        - เปลี่ยนสีพื้นหลังเป็นน้ำตาลเข้มเมื่อเลื่อนเมาส์ผ่าน (hover)
        - :disabled="loading": ปิดการใช้งานเมื่ออยู่ในสถานะ loading
        - @click: ส่ง event search เมื่อคลิกปุ่ม
      -->
      <button 
        type="button"
        class="bg-[#B67D12] hover:bg-[#9e6c0f] text-white rounded-md text-sm transition-colors h-[32px]"
        :disabled="loading"
        @click="$emit('search')"
      >
        ค้นหา
      </button>
    </div>
  </div>
</template>