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
  <!-- เพิ่มโครงการ -->
  <button
    v-if="type === 'btn-addProject'"
    class="text-Content-Regular bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center font-thin"
    @click="$emit('click')"
  >
    <slot>เพิ่มโครงการ</slot>
  </button>

  <!-- เชิญผู้ใช่ -->
  <button
    v-if="type === 'btn-inviteUser'"
    class="btn-เชิญผู้ใช้ bg-redNormal text-white rounded-[6px] h-[40px] p-4 text-[14px] font-thin flex items-center justify-center"
    @click="$emit('click')"
  >
    <slot>เชิญผู้ใช้</slot>
  </button>

  <!-- อนุมัติ -->
  <button
    v-if="type === 'btn-approve'"
    class="btn-อนุมัติ bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>อนุมัติ</slot>
  </button>

  <!-- อนุมัติ (1) -->
  <button
    v-if="type === 'btn-approve(1)'"
    class="btn-อนุมัติ (1) bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>อนุมัติ (1)</slot>
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

  <!-- ยกเลิกสีแดง -->
  <button
    v-if="type === 'btn-cancleRed'"
    class="bg-redNormal text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ยกเลิก</slot>
  </button>

  <!-- ล้างคำค้นหา -->
  <button
    v-if="type === 'btn-clear'"
    class="bg-white border-2 border-redNormal text-redNormal rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>ล้างคำค้นหา</slot>
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

  <!-- แก้ไขโครงการ -->
  <button
    v-if="type === 'btn-editProject'"
    class="btn- แก้ไขโครงการ bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
    <slot>แก้ไขโครงการ</slot>
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
    v-if="type === 'btn-payment2'"
    id="btn-นำจ่าย (1)"
    class="btn-นำจ่าย (1) bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <slot>นำจ่าย (1)</slot>
  </button>

  <button
    v-if="type === 'btn-print1'"
    id="btn-พิมพ์"
    class="btn-พิมพ์ bg-navyNormal text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center"
    @click="$emit('click')"
  >
   
    <slot>พิมพ์</slot>
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
    v-if="type === 'btn-public2'"
    class="btn- ประเภทรถสาธารณะ bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <svg
      class="ml-[10px]"
      xmlns="http://www.w3.org/2000/svg"
      width="24"
      height="24"
      viewBox="0 0 24 24"
      fill="none"
    >
      <path
        d="M10.7143 11.2857V7.71429C10.7143 7.52485 10.639 7.34316 10.5051 7.20921C10.3711 7.07525 10.1894 7 10 7C9.81056 7 9.62888 7.07525 9.49492 7.20921C9.36097 7.34316 9.28571 7.52485 9.28571 7.71429V11.2857H5.71429C5.52485 11.2857 5.34316 11.361 5.20921 11.4949C5.07525 11.6289 5 11.8106 5 12C5 12.1894 5.07525 12.3711 5.20921 12.5051C5.34316 12.639 5.52485 12.7143 5.71429 12.7143H9.28571V16.2857C9.28571 16.4752 9.36097 16.6568 9.49492 16.7908C9.62888 16.9247 9.81056 17 10 17C10.1894 17 10.3711 16.9247 10.5051 16.7908C10.639 16.6568 10.7143 16.4752 10.7143 16.2857V12.7143H14.2857C14.4752 12.7143 14.6568 12.639 14.7908 12.5051C14.9247 12.3711 15 12.1894 15 12C15 11.8106 14.9247 11.6289 14.7908 11.4949C14.6568 11.361 14.4752 11.2857 14.2857 11.2857H10.7143Z"
        fill="white"
      />
      <path
        d="M10 21C14.9706 21 19 16.9706 19 12C19 7.02944 14.9706 3 10 3C5.02944 3 1 7.02944 1 12C1 16.9706 5.02944 21 10 21Z"
        stroke="white"
        stroke-width="2"
      />
    </svg>
    <slot></slot>
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

  <button
    v-if="type === 'btn-edit2'"
    class="btn- แก้ไขคำขอเบิก bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin"
    @click="$emit('click')"
  >
    <svg
      class="ml-[14px]"
      xmlns="http://www.w3.org/2000/svg"
      width="14"
      height="14"
      viewBox="0 0 20 20"
      fill="none"
    >
      <path
        d="M19.0218 0.949768C18.3848 0.340177 17.5377 0 16.6566 0C15.7755 0 14.9284 0.340177 14.2914 0.949768L0 15.2625V20H4.73045L19.0218 5.68729C19.6482 5.05857 20 4.20668 20 3.31853C20 2.43037 19.6482 1.57848 19.0218 0.949768ZM3.69536 17.496H2.50024V16.2991L13.0454 5.73903L14.2405 6.9351L3.69536 17.496Z"
        fill="white"
      />
    </svg>
    <slot></slot>
  </button>
</template>