<script setup lang="ts">
 /*
 * ชื่อไฟล์: RequisitionSearchInput.vue
 * คำอธิบาย: ไฟล์นี้เป็น Component สำหรับช่องค้นหารายการเบิก ใช้ในการกรองข้อมูลตามชื่อรายการเบิก
 * ชื่อผู้เขียน/แก้ไข: จิรภัทร มณีวงษ์
 * วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
 */

// รับค่า props
defineProps<{
  modelValue: string  // ค่าข้อความค้นหา
  loading?: boolean   // สถานะ loading
}>()

// กำหนด events ที่จะส่งออกไปยัง parent component
defineEmits<{
  'update:modelValue': [value: string]  // event เมื่อมีการเปลี่ยนแปลงค่าข้อความค้นหา
}>()
</script>

<template>
  <div class="h-[32px] min-w-[200px] flex-1">
    <form class="grid" @submit.prevent>
      <label for="SearchRqName" class="py-0.5 text-[14px] text-black text-start">ค้นหารายการเบิก</label>
      <div class="relative h-[32px] w-full justify-center items-center">
        <!-- ไอคอนค้นหาด้านซ้ายของช่องค้นหา -->
        <div class="absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
          <svg width="19" height="20" viewBox="0 0 19 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z" 
              stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
          </svg>
        </div>
        <!-- 
          Input element สำหรับค้นหา:
          - :value="modelValue": ผูกค่าข้อความค้นหากับ prop modelValue
          - @input: ส่ง event อัพเดตค่าข้อความค้นหาไปยัง parent component เมื่อมีการพิมพ์
          - placeholder: แสดงข้อความช่วยเหลือเมื่อยังไม่มีการพิมพ์ค้นหา
          - :disabled="loading": ปิดการใช้งานเมื่ออยู่ในสถานะ loading
        -->
        <input 
          id="SearchRqName"
          type="text"
          :value="modelValue"
          @input="$emit('update:modelValue', ($event.target as HTMLInputElement).value)"
          class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-9"
          placeholder="ชื่อรายการเบิก"
          :disabled="loading"
        />
      </div>
    </form>
  </div>
</template>