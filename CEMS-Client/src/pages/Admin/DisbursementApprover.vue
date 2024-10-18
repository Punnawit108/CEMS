<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
import Button from '../../components/template/Button.vue';

// กำหนดตัวแปรควบคุมการแสดงผล
const isAddPage = ref(false);

// ใช้ Vue Router
const router = useRouter();
const route = useRoute();

// ฟังก์ชันสำหรับเปลี่ยนไปหน้า Add
const goToAdd = () => {
  isAddPage.value = true;
  router.push('/systemSettings/disbursementApprover/add');
};

// ฟังก์ชันสำหรับกลับไปหน้ารายการ
const goToList = () => {
  isAddPage.value = false;
  router.push('/systemSettings/disbursementApprover');
};

// ตรวจสอบ path เมื่อ component โหลด
onMounted(() => {
  if (route.path === '/systemSettings/disbursementApprover/add') {
    isAddPage.value = true;
  } else {
    isAddPage.value = false;
  }
});
</script>
<!-- path for test = /systemSettings/disbursementApprover -->
<!-- path for test = /systemSettings/disbursementApprover/add -->

<template>
  <!-- ปุ่มเปลี่ยนเส้นทาง -->
  <div class="h-[50px] flex justify-end items-center">
    <div v-if="isAddPage">
      <Button :type="'btn-cancleBorderGray'" @click="goToList" >ยกเลิก</Button>
      <span class="ml-5"></span>
      <Button :type="'btn-summit'" @click="goToList" class="ml-5">ยืนยัน</Button>
    </div>
    <div v-else>
      <Button :type="'btn-expense'" @click="goToAdd">ผู้มีสิทธิอนุมัติ</Button>
    </div>
  </div>

  <!-- ถ้าเป็นหน้ารายการแสดงรายการข้อมูล -->
  <div v-if="!isAddPage">
    <!-- แถบหัวข้อ -->
    <div class="h-[50px] flex items-center text-[14px] text-black font-bold w-">
      <p class="w-28 pl-2">ลำดับ</p>
      <p class="w-full pl-2">ชื่อ-นามสกุล</p>
      <p class="w-40 text-center"></p>
      
    </div>

    <!-- แถบเนื้อหา -->
    <div class="h-[50px] flex items-center text-[14px] text-black border-b border-[#BBBBBB]">
      <p class="w-28 pl-2">1</p>
      <p class="w-full pl-2">นายเทียนชัย คูเมือง</p>
      <p class="w-40 text-center"></p>
    </div>
  </div>



  <!-- ถ้าเป็นหน้าเพิ่มข้อมูลแสดงฟอร์มหรือรายการที่ต้องการจัดการ -->
  <div v-if="isAddPage">
    <!-- แถบหัวข้อ -->
    <div class="h-[50px] flex items-center text-[14px] text-black font-bold ">
      <p class="w-28 pl-2">ลำดับ</p>
      <p class="w-full pl-2">ชื่อ-นามสกุล</p>
      <p class="w-40 text-center">จัดการ</p>
    </div>

    <!-- แถบเนื้อหา -->
    <div class="h-[50px] flex items-center text-[14px] text-black border-b border-[#BBBBBB]">
      <p class="w-28 pl-2">1</p>
      <p class="w-full pl-2">นายเทียนชัย คูเมือง</p>
      <div class="w-40 text-center flex justify-between">
        <Icon :icon="'bin'" class="pl-8"/>
        <Icon :icon="'eye'" class="pr-8"/>
      </div>
    </div>

    <!-- แถบเพิ่ม เนื้อหา -->
    <div class="h-[50px] flex items-center text-[14px] text-black border-b border-[#BBBBBB]">
      <p class="w-28 pl-2">2</p>
      <div class="w-full pl-2">
        <form class="grid">
          <div class="relative">
              <select required
                  class="appearance-none flex justify-between w-[400px] bg-white border-solid border-[#B6B7BA] border rounded py-2.5 px-4 focus:outline-none ">
                  <option value="" disabled selected hidden class="placeholder">เลือกชื่อ-นามสกุล</option>
                  <option class="font-sm text-base" value="item">นาย ก.
                  </option>
                  <option class="font-sm text-base" value="item">นาย ข.</option>
              </select>
          </div>
      </form>
      </div>
      <div class="w-40 text-center flex">
      </div>
    </div>
  </div>
</template>

<style scoped>
/* สไตล์สามารถเพิ่มตามต้องการ */
</style>