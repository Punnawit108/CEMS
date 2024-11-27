<script setup lang="ts">
/**
* ชื่อไฟล์: DisbursementApprover.vue
* คำอธิบาย: ไฟล์นี้เป็น หน้าจอ DisbursementApprover
* Input: -
* Output: -
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
*/
import { ref, onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
import Button from '../../components/template/Button.vue';
import { useApprovalStore } from '../../store/approval';
import { useDisbursement } from "../../store/disbursement";

// กำหนดตัวแปรควบคุมการแสดงผล
const isEditPage = ref(false);
const isPopupOpen = ref(false); // สำหรับเปิด/ปิด Popup
const newApproverName = ref(""); // เก็บค่าชื่อที่เพิ่มใหม่
const disbursement = useDisbursement();

// ใช้ Vue Router
const router = useRouter();
const route = useRoute();
const approvalStore = useApprovalStore();



// ฟังก์ชันสำหรับไปหน้าแก้ไข
const goToEdit = () => {
  isEditPage.value = false;
  router.push("/systemSettings/disbursementApprover/edit").then(() => {
    window.location.reload(); // รีเฟรชหน้าเมื่อเปลี่ยนเส้นทางสำเร็จ
  });
};

// ฟังก์ชันสำหรับกลับไปหน้ารายการ
const goToList = () => {
  isEditPage.value = false;
  router.push("/systemSettings/disbursementApprover").then(() => {
    disbursement.getAllDisbursement(); // โหลดข้อมูลใหม่
  });
};

// เปิด Popup
const openPopup = () => {
  isPopupOpen.value = true;
};

// ปิด Popup
const closePopup = () => {
  isPopupOpen.value = false;
  newApproverName.value = ""; // รีเซ็ตค่าเมื่อปิด
};

// บันทึกข้อมูลผู้มีสิทธิอนุมัติใหม่
const saveApprover = () => {
  if (newApproverName.value.trim()) {
    // เพิ่มข้อมูลใหม่ใน store
    disbursement.disbursement.push({
      id: Date.now(),
      name: newApproverName.value,
    });

    // ปิด Popup และเปลี่ยนเส้นทางไปหน้ารายการ
    closePopup();

    // ถ้าเป็นหน้าแก้ไข ให้กลับไปที่หน้ารายการ
    if (isEditPage.value) {
      isEditPage.value = false; // เปลี่ยนสถานะ
      goToList();
    } else {
      alert("ข้อมูลบันทึกเรียบร้อยแล้ว");
    }
  } else {
    alert("กรุณากรอกชื่อผู้มีสิทธิอนุมัติ");
  }
};

// ฟังก์ชันสำหรับบันทึกการแก้ไข
const saveEdit = () => {
  // คุณสามารถเพิ่ม logic สำหรับการบันทึกข้อมูลที่แก้ไขได้ที่นี่
  // ตัวอย่าง: อัปเดตข้อมูลใน store หรือส่งข้อมูลไปยัง backend
  alert("บันทึกการแก้ไขเรียบร้อย");
  goToList();   // หลังจากบันทึกสำเร็จ กลับไปหน้ารายการ
};

// ตรวจสอบ path เมื่อ component โหลด
onMounted(() => {
  disbursement.getAllDisbursement();
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
    <div class="h-[80px] items-center">
      <div v-if="isEditPage">
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
          <!-- ปุ่มยกเลิก ยืนยัน แก้ไข -->
          <div class="flex space-x-4 my-5">
            <button @click="goToList" class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin mr-3">ยกเลิก</button>
            <button @click="saveEdit" class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">ยืนยัน</button>
          </div>
        </div>
      </div>

      <div v-else-if="!isEditPage">
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
          <!-- ปุ่มแก้ไขลำดับ และผู้มีสิทธิอนุมัติ -->
          <div class="flex space-x-4 my-5">
            <Button :type="'btn-editProject'" @click="goToEdit">แก้ไขลำดับ</Button>
            <Button :type="'btn-expense'" @click="openPopup">ผู้มีสิทธิอนุมัติ</Button>
          </div>
        </div>
      </div>
    </div>


    <!-- ถ้าเป็นหน้ารายการแสดงรายการข้อมูล -->
    <div v-if="!isEditPage">
      <!-- แถบหัวข้อ -->
      <div class="h-[50px] flex items-center text-[14px] text-black font-bold">
        <p class="w-28 pl-2">ลำดับ</p>
        <p class="w-full pl-2">ชื่อ-นามสกุล</p>
        <p class="w-40 text-center"></p>
      </div>
      <!-- แถบเนื้อหา -->
      <div class="h-[50px] flex items-center text-[14px] text-black border-b border-[#BBBBBB]"
        v-for="(approver, index) in approvalStore.approvers">
        <p class="w-28 pl-2">{{ index + 1 }}</p>
        <p class="w-full pl-2">{{ approver.usrFirstName }} {{ approver.usrLastName }}</p>
        <div class="w-40 text-center flex justify-between">
          <p class="w-28 pl-2">{{ index + 1 }}</p>
        </div>
      </div>
    </div>

    <!-- Popup -->
    <div v-if="isPopupOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <h2 class="text-[16px] font-bold text-center text-black">
          การเพิ่มผู้มีสิทธิอนุมัติ
        </h2>
        <div class="w-full mt-4">
          <form>
            <div class="relative">
              <select required
                class="appearance-none w-full h-[40px] bg-white border border-[#d9d9d9] rounded-lg pl-4 pr-8 text-[14px] text-[#bfbfbf] focus:outline-none">
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
        <div class="flex justify-center space-x-4 mt-6">
          <button @click="closePopup"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยกเลิก
          </button>
          <button @click="saveApprover"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยืนยัน
          </button>
        </div>
      </div>
    </div>


    <!-- ถ้าเป็นหน้ารายการแสดงรายการข้อมูล -->
    <div v-if="isEditPage">
      <!-- แถบหัวข้อ -->
      <div class="h-[50px] flex items-center text-[14px] text-black font-bold">
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
          <p class="w-28 pl-2">{{ index + 1 }}</p>
          <Icon :icon="'bin'" class="pl-8" />
        </div>
      </div>

      <!-- แถบเพิ่ม เนื้อหา -->
      <div class="h-[50px] flex items-center text-[14px] text-black border-b border-[#BBBBBB]">
        <p class="w-28 pl-2">{{ approvalStore.approvers.length+1 }}</p>
        <div class="w-full pl-1.5">
          <form class="grid">
            <div class="relative">
              <select required
                class="appearance-none flex justify-between w-[400px] bg-white border-solid border-[#B6B7BA] border rounded py-2.5 p-4 focus:outline-none ">
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
  </div>
</template>

<style scoped>
/* เพิ่มสไตล์ตามความต้องการ */
</style>
