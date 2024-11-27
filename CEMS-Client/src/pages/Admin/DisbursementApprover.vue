<script setup lang="ts">
/**
* ชื่อไฟล์: DisbursementApprover.vue
* คำอธิบาย: ไฟล์นี้เป็น หน้าจอ DisbursementApprover
* Input: -
* Output: -
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/
import { ref, onMounted, watch } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
import Button from '../../components/template/Button.vue';
import { useApprovalStore } from '../../store/approval';
import { useUserStore } from '../../store/user';
import { User } from '../../types';

// กำหนดตัวแปรควบคุมการแสดงผล
const isAddPage = ref(false);
let userNotRepeatWithApprovers = ref<User[]>([]);
const selectedUserId = ref<string>("");

// ใช้ Vue Router
const router = useRouter();
const route = useRoute();
const approvalStore = useApprovalStore();
const userStore = useUserStore();

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
onMounted(async () => {
  await approvalStore.getApprovers();
  await userStore.getAllUsers();

  // filter only people doesn't in approver
  userNotRepeatWithApprovers.value = userStore.users.filter((user:any) => {
    return !(approvalStore.approvers.map(approver =>  approver.usrId)).includes(user.usrId);
  })

  if (route.path === '/systemSettings/disbursementApprover/add') {
    isAddPage.value = true;
  } else {
    isAddPage.value = false;
  }
});

const addApprover = async() => {
  await approvalStore.addApprovers(selectedUserId.value);
}


</script>
<!-- path for test = /systemSettings/disbursementApprover -->
<!-- path for test = /systemSettings/disbursementApprover/add -->

<template>
  <div>
    <!-- ปุ่มเปลี่ยนเส้นทาง -->
    <div class="h-[50px] flex justify-end items-center">
      <div v-if="isAddPage">
        <Button :type="'btn-cancleBorderGray'" @click="goToList">ยกเลิก</Button>
        <span class="ml-5"></span>
        <Button :type="'btn-summit'" @click="addApprover" class="ml-5">ยืนยัน</Button>
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
      <div class="h-[50px] flex items-center text-[14px] text-black border-b border-[#BBBBBB]"
        v-for="(approver, index) in approvalStore.approvers">
        <p class="w-28 pl-2">{{ index + 1 }}</p>
        <p class="w-full pl-2">{{ approver.usrFirstName }} {{ approver.usrLastName }}</p>
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
      <div class="h-[50px] flex items-center text-[14px] text-black border-b border-[#BBBBBB]"
        v-for="(approver, index) in approvalStore.approvers">
        <p class="w-28 pl-2">{{ index + 1 }}</p>
        <p class="w-full pl-2">{{ approver.usrFirstName }} {{ approver.usrLastName }}</p>

        <div class="w-40 text-center flex justify-between">
          <Icon :icon="'bin'" class="pl-8" />
          <Icon :icon="'eye'" class="pr-8" />
        </div>
      </div>

      <!-- แถบเพิ่ม เนื้อหา -->
      <div class="h-[50px] flex items-center text-[14px] text-black border-b border-[#BBBBBB]">
        <p class="w-28 pl-2">{{ approvalStore.approvers.length + 1 }}</p>
        <div class="w-full pl-1.5">
          <form class="grid">
            <div class="relative">
              <select required v-model="selectedUserId"
                class="appearance-none flex justify-between w-[400px] bg-white border-solid border-[#B6B7BA] border rounded py-2.5 p-4 focus:outline-none">
                <option value="" disabled selected hidden class="placeholder">เลือกชื่อ-นามสกุล</option>
                <option class="font-sm text-base" :value="user.usrId" v-for="user in userNotRepeatWithApprovers">{{ user.usrFirstName }}
                  {{ user.usrLastName }}</option>
              </select>
            </div>
          </form>
        </div>
        <div class="w-40 text-center flex">
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* สไตล์สามารถเพิ่มตามต้องการ */
</style>