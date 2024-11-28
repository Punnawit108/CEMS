<script setup lang="ts">
/**
 * ชื่อไฟล์: DisbursementApprover.vue
 * คำอธิบาย: ไฟล์นี้เป็น หน้าจอ DisbursementApprover
 * Input: -
 * Output: -
 * ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
 * วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
 */
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import Icon from "../../components/template/CIcon.vue";
import Button from "../../components/template/Button.vue";
import { useApprovalStore } from "../../store/approval";
import { useUserStore } from "../../store/user";
import { User } from "../../types";

const approvalStore = useApprovalStore();
const userStore = useUserStore();


// กำหนดตัวแปรควบคุมการแสดงผล
const isEditPage = ref(false);
const isPopupAddOpen = ref(false); // สำหรับเปิด/ปิด Popup
const isPopupEditOpen = ref(false); // สำหรับเปิด/ปิด Popup
const newApproverName = ref(""); // เก็บค่าชื่อที่เพิ่มใหม่
const isAddAlertOpen = ref(false); // ควบคุมการแสดง Alert Add
const isEditAlertOpen = ref(false); // ควบคุมการแสดง Alert Edit
let userNotRepeatWithApprovers = ref<User[]>();
const selectUserId = ref<string>("");

// ใช้ Vue Router
const route = useRoute();

// เปิด PopupAdd ผู้อนุมัติ
const openPopupAdd = () => {
  isPopupAddOpen.value = true;
};
// ปิด Popup
const closePopupAdd = () => {
  isPopupAddOpen.value = false;
  newApproverName.value = ""; // รีเซ็ตค่าเมื่อปิด
};

// เปิด PopupAdd ผู้อนุมัติ
const openPopupEdit = () => {
  isPopupEditOpen.value = true;
};
// ปิด Popup
const closePopupEdit = () => {
  isPopupEditOpen.value = false;
  newApproverName.value = ""; // รีเซ็ตค่าเมื่อปิด
};


const confirmAdd = async() => {
  // เปิด Popup Alert

  await approvalStore.addApprovers(selectUserId.value);
  isAddAlertOpen.value = true;

  isAddAlertOpen.value = false;
  closePopupAdd();
};

const confirmEdit = () => {
  // เปิด Popup Alert
  isEditAlertOpen.value = true;

  // ตั้งเวลาให้ Alert ปิดอัตโนมัติใน 1.5 วินาที
  setTimeout(() => {
    isEditAlertOpen.value = false; // ปิด Alert
    closePopupEdit(); // ปิด Popup แก้ไข
  }, 1500); // 1.5 วินาที
};

// ตรวจสอบ path เมื่อ component โหลด
onMounted(async() => {
  await approvalStore.getApprovers();
  await userStore.getAllUsers();

  userNotRepeatWithApprovers.value = userStore.users.filter((user:any) => {
    return !approvalStore.approvers.map((approver) => approver.usrId).includes(user.usrId)
  })
  
  if (route.path === "/systemSettings/disbursementApprover/edit") {
    isEditPage.value = true;
  } else {
    isEditPage.value = false;
  }
});
</script>

<template>
  <div>

    <!-- ปุ่มเปลี่ยนเส้นทาง -->
    <div class="items-center">
      <div>
        <div class="flex justify-between items-center">
          <div class="h-[60px] w-[266px] flex justify-center">
            <form class="grid w-full">
              <label for="SearchBar" class="py-0.5 text-[14px] text-black text-start">ค้นหา</label>
              <div class="relative h-[32px] w-full flex justify-center items-center">
                <div class="absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                  <svg width="19" height="20" viewBox="0 0 19 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path
                      d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z"
                      stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                  </svg>
                </div>
                <input type="text" id="SearchBar"
                  class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-9"
                  placeholder="รหัสพนักงาน ,ชื่อ-นามสกุล" />
              </div>
            </form>
          </div>
          <div>
            <Button :type="'btn-editProject'" class="">ปิดรับคำขอ</Button>
          </div>
        </div>
        <!-- ปุ่มแก้ไขลำดับ และผู้มีสิทธิอนุมัติ -->
        <div class="flex space-x-4 my-5 justify-end">
          <Button :type="'btn-editProject'" @click="openPopupEdit" class="my-5">แก้ไขลำดับ</Button>
          <Button :type="'btn-expense'" @click="openPopupAdd" class="my-5">ผู้มีสิทธิ์อนุมัติ</Button>
        </div>
      </div>
    </div>


    <!-- หน้าหลัก รายการแสดงรายการข้อมูล /-->
    <div>
      <!-- แถบหัวข้อ -->
      <div class="h-[50px] flex items-center justify-between text-[14px] text-black font-bold">
        <p class="w-20 text-center">ลำดับ</p>
        <p class="w-4/5 pl-2">ชื่อ-นามสกุล</p>
        <p class="w-56 text-end pr-2">จัดการ</p>
      </div>
      <!-- แถบเนื้อหา -->
      <div v-for="(approver, index) in approvalStore.approvers" :key="approver.usrId"
        class="h-[50px] flex items-center justify-between text-[14px] text-black border-b border-[#BBBBBB]">
        <p class="w-20 pl-6">{{ index + 1 }}</p>
        <p class="w-4/5 pl-2">{{ approver.usrFirstName }} {{ approver.usrLastName }}</p>
        <div class="ml-5 w-52 text-center flex items-center justify-between">
          <div class="">
            <select disabled
              class="appearance-none w-full h-[32px] bg-white border-2 border-[#d9d9d9] rounded-lg pl-1 text-[14px]">
              <option>
                {{ approver.apSequence }}
              </option>
            </select>
          </div>

          <Icon :icon="'bin'" class="mx-3" />
        </div>
      </div>
    </div>



    <!-- POPUP +ผู้มีสิทธิ์อนุมัติ -->
    <div v-if="isPopupAddOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <h2 class="text-[16px] font-bold text-center text-black mb-3">
          การเพิ่มผู้มีสิทธิ์อนุมัติ
        </h2>
        <div class="w-full my-3 flex justify-center">
          <form>
            <div class="relative">
              <select required v-model="selectUserId"
                class="appearance-none w-[350px] h-[40px] bg-white border border-[#d9d9d9] rounded-lg pl-4 pr-8 text-[14px] text-black focus:outline-none">
                <option value="" disabled selected hidden>
                  เลือกชื่อ-นามสกุล
                </option>
                <option class="text-black" :value="user.usrId" v-for = "user in userNotRepeatWithApprovers" >{{ user.usrFirstName }} {{ user.usrLastName }}</option>
              </select>
              <div class="absolute right-3 top-1/2 transform -translate-y-1/2 pointer-events-none">
                <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" fill="none" viewBox="0 0 24 24"
                  stroke="currentColor" stroke-width="2">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M19 9l-7 7-7-7" />
                </svg>
              </div>
            </div>
          </form>
        </div>
        <div class="flex justify-center space-x-4 mt-3">
          <button @click="closePopupAdd"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยกเลิก
          </button>
          <button @click="confirmAdd"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยืนยัน
          </button>
        </div>
      </div>
    </div>

    <!-- POPUP แก้ไขลำดับผู้อนุมัติ -->
    <div v-else-if="isPopupEditOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <h2 class="text-[16px] font-bold text-center text-black mb-3">
          แก้ไขลำดับผู้อนุมัติ
        </h2>
        <div class="w-full my-3 flex justify-center">
          <form>
            <div class="relative">
              <select required
                class="appearance-none w-[350px] h-[40px] bg-white border-2 border-[#d9d9d9] rounded-lg pl-4 pr-8 text-[14px] text-black focus:outline-none">
                <option value="" disabled selected hidden>
                  เลือกชื่อ-นามสกุล
                </option>
                <option class="text-black" value="item1">นาย ก.</option>
                <option class="text-black" value="item2">นาย ข.</option>
              </select>
              <div class="absolute right-3 top-1/2 transform -translate-y-1/2 pointer-events-none">
                <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" fill="none" viewBox="0 0 24 24"
                  stroke="currentColor" stroke-width="2">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M19 9l-7 7-7-7" />
                </svg>
              </div>
            </div>
          </form>
        </div>

        <div class="w-full my-3 flex justify-center">
          <select
            class="appearance-none w-[350px] h-[40px] bg-white border-2 border-[#d9d9d9] rounded-lg pl-4 pr-8 text-[14px] text-black focus:outline-none">
            <option value="" disabled selected hidden>
              ลำดับผู้อนุมัติ
            </option>
            <!-- <option v-for="i in maxIndexPlusOne" :key="i" :value="i" class="text-black">
              {{ "ลำดับที่ " + i }}
            </option> -->
          </select>
        </div>

        <div class="flex justify-center space-x-4 mt-3">
          <button @click="closePopupEdit"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยกเลิก
          </button>
          <button @click="confirmEdit"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยืนยัน
          </button>
        </div>
      </div>
    </div>

  </div>


  <!-- Alert -->
  <div v-if="isAddAlertOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <Icon :icon="'checkCircle'" />
      <h2 class="text-[24px] font-bold text-center text-black mt-3">ยืนยันการเพิ่มผู้มีสิทธิ์อนุมัติสำเร็จ</h2>
    </div>
    
  </div>
  <div v-if="isEditAlertOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <Icon :icon="'checkCircle'" />
      <h2 class="text-[24px] font-bold text-center text-black mb-3">ยืนยันการแก้ไขผู้มีสิทธิ์อนุมัติสำเร็จ</h2>
    </div>
  </div>
</template>

<style scoped>
/* เพิ่มสไตล์ตามความต้องการ */
</style>