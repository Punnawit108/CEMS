<script setup lang="ts">
/*
* ชื่อไฟล์: Button.vue
* คำอธิบาย: ไฟล์นี้แสดง components ปุ่มทั้งระบบ
* ชื่อผู้เขียน/แก้ไข: นครียา วัฒนศรี
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/
import { defineProps } from "vue";

const props = defineProps({
  type: {
    type: String,
    required: true,
  },
});
const emit = defineEmits(["click"]);
</script>

<template>


  <!-- อนุมัติ -->
  <button
    v-if="type === 'btn-approve'"
    class="btn-อนุมัติ bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>อนุมัติ</slot>
  </button>

  <!-- ยกเลิกสีกรอบเทา -->
  <button
    v-if="type === 'btn-cancleBorderGray'"
    class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    type="button"
    @click="$emit('click')"
  >
    <slot>ยกเลิก</slot>
  </button>

  <!-- ยกเลิกสีเทา -->
  <button
    v-if="type === 'btn-cancleGray'"
    class="btn-ยกเลิกเทา bg-grayNormal text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ยกเลิก</slot>
  </button>

  <!-- ยืนยัน -->
  <button
    type="button"
    v-if="type === 'btn-summit'"
    class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ยืนยัน</slot>
  </button>

  <!-- แก้ไขโครงการ + ปิดรับคำขอ + แก้ไขลำดับ-->
  <button
    v-if="type === 'btn-editProject'"
    class="btn- แก้ไขโครงการ bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>แก้ไขโครงการ+ ปิดรับคำขอ + แก้ไขลำดับ</slot>
  </button>

  <!-- แก้ไขคำขอเบิก -->
  <button
    v-if="type === 'btn-editRequest'"
    class="bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin "
    @click="$emit('click')"
  >
    <slot>แก้ไขคำขอเบิก</slot>
  </button>

  <!-- ส่งแก้ไข -->
  <button
    v-if="type === 'btn-editSend'"
    class="bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin "
    @click="$emit('click')"
  >
   
    <slot>ส่งแก้ไข</slot>
  </button>

  <!-- แก้ไขผู้ใช้ -->
  <button
    v-if="type === 'btn-editUser'"
    class="btn- แก้ไขผู้ใช้ bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>แก้ไขผู้ใช้</slot>
  </button>

  <!-- สร้างใบเบิกค่าใช้จ่าย -->
  <button
    v-if="type === 'btn-expense'"
    class="btn-สร้างใบเบิกค่าใช้จ่าย bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    
    <slot>สร้างใบเบิกค่าใช้จ่าย</slot>
  </button>

  <!-- ประเภทค่าใช้จ่าย -->
  <button
    v-if="type === 'btn-expenseType'"
    class="btn-สร้างใบเบิกค่าใช้จ่าย bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>เพิ่มประเภทค่าใช้จ่าย</slot>
  </button>

  <!-- ประเภทค่าใช้จ่ายเทา -->
  <button
    v-if="type === 'btn-expenseTypeGray'"
    class="btn-ประเภทค่าใช้จ่าย bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
    
  ><slot>ประเภทค่าใช้จ่าย</slot></button>

  <!-- ประเภทค่าใช้จ่ายเทาพื้นหลัง -->
  <button
    v-if="type === 'btn-expenseTypeGrayClick'"
    class="btn-ประเภทค่าใช้จ่าย bg-grayNormal border-2 border-grayNormal text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
    
  ><slot>ประเภทค่าใช้จ่าย</slot></button>

  <button
    v-if="type === 'btn-payment1'"
    class="btn-นำจ่าย bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>นำจ่าย</slot>
  </button>

  <button
    v-if="type === 'btn-print2'"
    id="btn-พิมพ์"
    class="btn-พิมพ์ bg-navyNormal text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
   
    <slot>ส่งออก</slot>
  </button>

  <button
    v-if="type === 'btn-private'"
    id="btn-ประเภทรถส่วนตัว"
    class="btn-ประเภทรถส่วนตัว bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    
    <slot>เพิ่มประเภทรถส่วนตัว</slot>
  </button>

  <button
    v-if="type === 'btn-public1'"
    class="btn-ประเภทรถสาธารณะ bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    
    <slot>เพิ่มประเภทรถสาธารณะ</slot>
  </button>

  <button
    v-if="type === 'btn-save'"
    id="btn-บันทึก"
    class="btn-บันทึก bg-grayNormal text-black rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    type = "button"
    @click="$emit('click')"
  >
    <slot>บันทึก</slot>
  </button>

  <button
    v-if="type === 'btn-search'"
    id="btn-ค้นหา"
    class="btn-ค้นหา bg-redNormal text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ค้นหา</slot>
  </button>

  <button
    v-if="type === 'btn-transport'"
    id="btn-ประเภทค่าเดินทาง"
    class="btn-ประเภทค่าเดินทาง bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ประเภทค่าเดินทาง</slot>
  </button>

  <button
    v-if="type === 'btn-transportClick'"
    id="btn-ประเภทค่าเดินทาง"
    class="btn-ประเภทค่าเดินทาง bg-grayNormal border-2 border-grayNormal text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ประเภทค่าเดินทาง</slot>
  </button>

  <button
    v-if="type === 'btn-unapprove'"
    id="btn-ไม่อนุมัติ"
    class="btn-ไม่อนุมัติ text-Content-Regular bg-redNormal text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center font-thin"
    @click="$emit('click')"
  >
    <slot>ไม่อนุมัติ</slot>
  </button>

  
</template>

