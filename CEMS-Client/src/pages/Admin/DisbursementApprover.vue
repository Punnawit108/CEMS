<script setup lang="ts">
/*
* ชื่อไฟล์: DisbursementApprover.vue
* คำอธิบาย: ไฟล์นี้แสดงรายชื่อผู้มีสิทธิ์ในการอนุมัติ
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒ์ นิระมล
* วันที่จัดทำ/แก้ไข: 29 ธันวาคม 2567
*/
import { ref, onMounted, reactive, computed, watch } from 'vue';
import { useRoute } from 'vue-router';
import Icon from '../../components/Icon/CIcon.vue';
import Button from '../../components/Buttons/Button.vue';
import { useApprovalStore } from '../../store/approval';
import { useUserStore } from '../../store/user';
import { User } from '../../types';
import { useCheckExpenseStore } from '../../store/checkExpense';
import { useLockStore } from '../../store/lockSystem';

// Import filters
import UserSearchInput from '../../components/filters/UserSearchInput.vue'
import FilterButtons from '../../components/filters/FilterButtons.vue'

const approvalStore = useApprovalStore();
const userStore = useUserStore();
const lockStore = useLockStore();
const checkExpenseStore = useCheckExpenseStore();

// กำหนดตัวแปรควบคุมการแสดงผล
const isEditPage = ref(false);
const newApproverName = ref(""); // เก็บค่าชื่อที่เพิ่มใหม่
const isPopupAddOpen = ref(false); // สำหรับเปิด/ปิด Popup Add
const isPopupEditOpen = ref(false); // สำหรับเปิด/ปิด Popup Edit
const isPopupDeleteOpen = ref(false); // สำหรับเปิด/ปิด Popup Delete
const isPopupConfirmAddOpen = ref(false); // สำหรับเปิด/ปิด Popup ConfirmAdd
const isPopupConfirmEditOpen = ref(false); // สำหรับเปิด/ปิด Popup ConfirmEdit
const isAddAlertOpen = ref(false); // ควบคุมการแสดง Alert Add
const isEditAlertOpen = ref(false); // ควบคุมการแสดง Alert Edit
const isDeleteAlertOpen = ref(false); // ควบคุมการแสดง Alert delete
const isPopupConfirmLockOpen = ref(false); // สำหรับเปิด/ปิด Popup ยืนยันการล็อคระบบ
const isLockAlertOpen = ref(false); // สำหรับเปิด/ปิด Alert การล็อคระบบ
let userNotRepeatWithApprovers = ref<User[]>();
const selectUserId = ref<string>("");
const approverSequence = reactive({
  apId: 0,
  apSequence: 0
})
const selectedApproverId = ref<number>(0);
const loading = ref(false);

// เพิ่ม filters สำหรับการค้นหา
const filters = ref({
  searchTerm: '',
});

// Last searched filters
const lastSearchedFilters = ref({
  searchTerm: '',
});

// กรองข้อมูลผู้อนุมัติตามคำค้นหา
const filteredApprovers = computed(() => {
  if (!approvalStore.approvers) return [];

  return approvalStore.approvers.filter(approver => {
    const matchesSearch = lastSearchedFilters.value.searchTerm === '' ||
      approver.usrFirstName.toLowerCase().includes(lastSearchedFilters.value.searchTerm.toLowerCase()) ||
      approver.usrLastName.toLowerCase().includes(lastSearchedFilters.value.searchTerm.toLowerCase()) ||
      approver.usrEmployeeId.toLowerCase().includes(lastSearchedFilters.value.searchTerm.toLowerCase());

    return matchesSearch;
  });
});

const isFreeEdit = ref(false);
const isFreeAdd = ref(false);
const isFreeDelete = ref(false);

// ใช้ Vue Router
const route = useRoute();

// เปิด Popup Add ผู้อนุมัติ
const openPopupAdd = () => {
  if (!checkExpenseStore.checkExpense) {
    isFreeAdd.value = true;
    setTimeout(() => {
      isFreeAdd.value = false;
    }, 1500);
  } else {
    isPopupAddOpen.value = true;
  }
};

const closePopupAdd = () => {
  isPopupAddOpen.value = false;
  newApproverName.value = ""; // รีเซ็ตค่าเมื่อปิด
  selectUserId.value = "";
};

// เปิด Popup  Edit ผู้อนุมัติ
const openPopupEdit = () => {
  if (!checkExpenseStore.checkExpense) {
    isFreeEdit.value = true;
    setTimeout(() => {
      isFreeEdit.value = false;
    }, 1500);
  } else {
    isPopupEditOpen.value = true;
  }
};

// สมมุติว่า userStore มี currentUser
const currentUser = userStore.currentUser;

const filteredApproversForEdit = computed(() => {
  if (!approvalStore.approvers) return [];
  return approvalStore.approvers.filter(approver => {
    // คัดกรองไม่ให้แสดงผู้ใช้งานตัวเอง
    return approver.usrId !== currentUser?.usrId;
  });
});


const closePopupEdit = () => {
  isPopupEditOpen.value = false;
  newApproverName.value = ""; // รีเซ็ตค่าเมื่อปิด
};

// เปิด Popup Delete ผู้อนุมัติ
const openPopupDelete = (approverId: number) => {
  if (!checkExpenseStore.checkExpense) {
    isFreeDelete.value = true;
    setTimeout(() => {
      isFreeDelete.value = false;
    }, 1500);
  } else {
    selectedApproverId.value = approverId;
    isPopupDeleteOpen.value = true;
  }
};

const closePopupDelete = () => {
  isPopupDeleteOpen.value = false;
  newApproverName.value = ""; // รีเซ็ตค่าเมื่อปิด
  selectUserId.value = "";
};

// เปิด PopupConfirmAdd ผู้อนุมัติ
const openPopupConfirmAdd = () => {
  isPopupConfirmAddOpen.value = true;
};
const closePopupConfirmAdd = () => {
  isPopupConfirmAddOpen.value = false;
  newApproverName.value = ""; // รีเซ็ตค่าเมื่อปิด
};

// เปิด PopupConfirmEdit ผู้อนุมัติ
const openPopupConfirmEdit = () => {
  isPopupConfirmEditOpen.value = true;
};
const closePopupConfirmEdit = () => {
  isPopupConfirmEditOpen.value = false;
  newApproverName.value = ""; // รีเซ็ตค่าเมื่อปิด
};

const confirmAdd = async () => {
  await approvalStore.addApprovers(selectUserId.value);
  closePopupConfirmAdd();
  isAddAlertOpen.value = true;
  fatchApproval();

  setTimeout(() => {
    isAddAlertOpen.value = false;
    closePopupAdd();
  }, 1500);
};

const confirmEdit = async () => {
  await approvalStore.changeSequence(approverSequence);
  closePopupConfirmEdit();
  isEditAlertOpen.value = true;

  setTimeout(() => {
    isEditAlertOpen.value = false;
    closePopupEdit();
    // รีเซ็ตค่าใน dropdown แก้ไขลำดับ
    approverSequence.apId = 0;
    approverSequence.apSequence = 0;
  }, 1500);
};

const confirmDelete = async () => {
  isPopupDeleteOpen.value = false;
  await approvalStore.deleteApprover(selectedApproverId.value);
  isDeleteAlertOpen.value = true;
  fatchApproval();

  setTimeout(() => {
    isDeleteAlertOpen.value = false;
    closePopupDelete();
  }, 1500);
};

const openPopupConfirmLock = () => {
  isPopupConfirmLockOpen.value = true;
};

const closePopupConfirmLock = () => {
  isPopupConfirmLockOpen.value = false;
};

const confirmLock = async () => {
  await lockStore.toggleLock();
  closePopupConfirmLock();
  isLockAlertOpen.value = true;

  setTimeout(() => {
    isLockAlertOpen.value = false;
  }, 1500);
};

// ฟังก์ชันล็อคระบบ
const lockSystem = () => {
  lockStore.toggleLock();
};

// ฟังก์ชันสำหรับการค้นหา
const handleSearch = () => {
  lastSearchedFilters.value = {
    searchTerm: filters.value.searchTerm,
  };
};

// ฟังก์ชันสำหรับการรีเซ็ต
const handleReset = () => {
  // รีเซ็ตค่าในฟิลเตอร์
  filters.value = {
    searchTerm: '',
  };

  // รีเซ็ตค่าล่าสุดที่ใช้ค้นหา
  lastSearchedFilters.value = {
    searchTerm: '',
  };
};

const fatchApproval = async () => {
  userNotRepeatWithApprovers.value = userStore.users.filter((user: any) => {
    return !approvalStore.approvers.map((approver) => approver.usrId).includes(user.usrId)
  });
};

// ตรวจสอบ path เมื่อ component โหลด
onMounted(async () => {
  loading.value = true;
  try {
    await lockStore.fetchLockStatus();
    await approvalStore.getApprovers();
    await userStore.getAllUsers();
    await checkExpenseStore.fetchCheck();
    await fatchApproval();

    if (route.path === "/systemSettings/disbursementApprover/edit") {
      isEditPage.value = true;
    } else {
      isEditPage.value = false;
    }
  } catch (error) {
    console.error('Error in mounted:', error);
  } finally {
    loading.value = false;
  }
});
</script>

<template>
  <div>
    <!-- ปุ่มเปลี่ยนเส้นทาง -->
    <div class="items-center">
      <div>
        <div class="flex justify-between items-center">
          <!-- แทนที่ช่องค้นหาเดิมด้วยฟิลเตอร์ จาก UserSetting.vue -->
          <div class="flex flex-wrap gap-4 mb-4 lg:mb-0">
            <div class="min-w-[200px] flex-1 lg:max-w-[300px]">
              <UserSearchInput v-model="filters.searchTerm" :loading="loading" />
            </div>

            <div class="min-w-[200px] flex-1 lg:max-w-[300px]">
              <FilterButtons :loading="loading" @reset="handleReset" @search="handleSearch" />
            </div>
          </div>

          <div class="flex space-x-4 my-5 justify-end">
            <Button :type="'btn-editProject'" @click="openPopupEdit" class="my-5 px-5">แก้ไขลำดับ</Button>
            <button
              class="bg-[#B6B7BA] text-white rounded-[6px] h-[40px] px-8 flex items-center text-[14px] font-thin mt-5"
              @click="openPopupConfirmLock">
              {{ lockStore.isLocked ? 'เปิดรับคำขอ' : 'ปิดรับคำขอ' }}
            </button>
            <button @click="openPopupAdd"
              class="my-5 bg-green text-white rounded-[6px] h-[40px] px-8 flex items-center text-[14px] font-thin justify-center">เพิ่มผู้มีสิทธิ์อนุมัติ</button>
          </div>
        </div>
        <!-- ปุ่มแก้ไขลำดับ และผู้มีสิทธิอนุมัติ -->
      </div>
    </div>

    <!-- หน้าหลัก รายการแสดงรายการข้อมูล /-->
    <div>
      <!-- แถบหัวข้อ -->
      <div class="h-[50px] flex items-center justify-between text-[14px] text-black font-bold">
        <p class="w-20 text-center">ลำดับ</p>
        <p class="w-36 text-center">รหัสพนักงาน</p>
        <p class="w-4/5 pl-2">ชื่อ-นามสกุล</p>
        <p class="w-56 text-end pr-2">จัดการ</p>
      </div>
      <!-- แถบเนื้อหา -->
      <div v-if="loading" class="flex justify-center items-center py-4">
        <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
        <span class="ml-2">กำลังโหลดข้อมูล...</span>
      </div>
      <div v-else-if="filteredApprovers.length === 0" class="py-4 text-center">
        ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา
      </div>
      <div v-else v-for="(approver, index) in filteredApprovers" :key="approver.usrId"
        class="h-[50px] flex items-center justify-between text-[14px] text-black border-b border-[#BBBBBB]">
        <p class="w-20 text-center">{{ index + 1 }}</p>
        <p class="w-36 text-center">{{ approver.usrEmployeeId }}</p>
        <p class="w-4/5 pl-2">{{ approver.usrFirstName }} {{ approver.usrLastName }}</p>
        <div class="ml-5 w-52 text-center flex items-center justify-between">
          <div class="">

          </div>

          <Icon :icon="'bin'" @click="openPopupDelete(approver.apId)" class="mx-3" />
        </div>
      </div>
    </div>

    <!-- Popup Delete -->
    <div v-if="isPopupDeleteOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-4">
          ยืนยันการลบผู้มีสิทธิ์อนุมัติ
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยืนยันการลบผู้มีสิทธิ์อนุมัติหรือไม่ ?
        </h2>
        <div class="flex justify-center space-x-4">
          <button @click="closePopupDelete"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยกเลิก
          </button>
          <button @click="confirmDelete"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยืนยัน
          </button>
        </div>
      </div>
    </div>

    <!-- Popup ยืนยันการล็อคระบบ -->
    <div v-if="isPopupConfirmLockOpen"
      class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-4">
          ยืนยันการ{{ lockStore.isLocked ? 'เปิดรับ' : 'ปิดรับ' }}รายการเบิกค่าใช้จ่าย
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยืนยันการ{{ lockStore.isLocked ? 'เปิดรับ' : 'ปิดรับ' }}รายการเบิกค่าใช้จ่ายหรือไม่ ?
        </h2>
        <div class="flex justify-center space-x-4">
          <button @click="closePopupConfirmLock"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยกเลิก
          </button>
          <button @click="confirmLock"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยืนยัน
          </button>
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
                <option class="text-black" :value="user.usrId" v-for="user in userNotRepeatWithApprovers">
                  {{ user.usrFirstName }} {{ user.usrLastName }}</option>
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
          <button @click="openPopupConfirmAdd"
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
              <select required v-model="approverSequence.apId"
                class="appearance-none w-[350px] h-[40px] bg-white border-2 border-[#d9d9d9] rounded-lg pl-4 pr-8 text-[14px] text-black focus:outline-none">
                <option value=0 disabled selected hidden>
                  เลือกชื่อ-นามสกุล
                </option>
                <option class="text-black" :value="approver.apId" v-for="approver in filteredApproversForEdit"
                  :key="approver.usrId">
                  {{ approver.usrFirstName }} {{ approver.usrLastName }}
                </option>
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
          <select v-model="approverSequence.apSequence"
            class="appearance-none w-[350px] h-[40px] bg-white border-2 border-[#d9d9d9] rounded-lg pl-4 pr-8 text-[14px] text-black focus:outline-none">
            <option value=0 disabled selected hidden>
              ลำดับผู้อนุมัติ
            </option>
            <option v-for="i in approvalStore.approvers.length" :key="i" :value="i" class="text-black">
              {{ "ลำดับที่ " + i }}
            </option>
          </select>
        </div>

        <div class="flex justify-center space-x-4 mt-3">
          <button @click="closePopupEdit"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยกเลิก
          </button>
          <button @click="openPopupConfirmEdit"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยืนยัน
          </button>
        </div>
      </div>
    </div>

    <!-- Popup ยืนยัน Add -->
    <div v-if="isPopupConfirmAddOpen"
      class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-4">
          ยืนยันการเพิ่มผู้มีสิทธิ์อนุมัติ
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยืนยันการเพิ่มผู้มีสิทธิ์อนุมัติหรือไม่ ?
        </h2>
        <div class="flex justify-center space-x-4">
          <button @click="closePopupConfirmAdd"
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
    <!-- Popup ยืนยัน Edit -->
    <div v-if="isPopupConfirmEditOpen"
      class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-4">
          ยืนยันการแก้ไขผู้มีสิทธิ์อนุมัติ
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยืนยันการแก้ไขผู้มีสิทธิ์อนุมัติหรือไม่ ?
        </h2>
        <div class="flex justify-center space-x-4">
          <button @click="closePopupConfirmEdit"
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

    <!-- Popup แก้ไขไม่ได้ -->
    <div v-if="isFreeEdit" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mt-3">
          รายการเบิกค่าใช้จ่ายค้างอยู่ในระบบ<br>
          กรุณาตรวจสอบ หรือปิดรับรายการ<br>
          ก่อนทำการแก้ไขลำดับผู้อนุมัติการเบิกจ่าย
        </h2>
      </div>
    </div>

    <!-- Popup เพิ่มไม่ได้ -->
    <div v-if="isFreeAdd" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mt-3">
          รายการเบิกค่าใช้จ่ายค้างอยู่ในระบบ<br>
          กรุณาตรวจสอบ หรือปิดรับรายการ<br>
          ก่อนทำการเพิ่มผู้อนุมัติการเบิกจ่าย
        </h2>
      </div>
    </div>

    <!-- Popup แก้ไขไม่ได้ -->
    <div v-if="isFreeDelete" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mt-3">
          รายการเบิกค่าใช้จ่ายค้างอยู่ในระบบ<br>
          กรุณาตรวจสอบ หรือปิดรับรายการ<br>
          ก่อนทำการลบผู้อนุมัติการเบิกจ่าย
        </h2>
      </div>
    </div>

    <!-- Alert -->
    <div v-if="isAddAlertOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[96px] h-[96px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mt-3">ยืนยันการเพิ่มผู้มีสิทธิ์อนุมัติสำเร็จ</h2>
      </div>
    </div>

    <!-- Alert ยืนยันการล็อคระบบ -->
    <div v-if="isLockAlertOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[96px] h-[96px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mt-3">
          ยืนยันการ{{ lockStore.isLocked ? 'ปิดรับ' : 'เปิดรับ' }}รายการเบิกค่าใช้จ่ายสำเร็จ
        </h2>
      </div>
    </div>

    <div v-if="isEditAlertOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[96px] h-[96px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mt-3">ยืนยันการแก้ไขผู้มีสิทธิ์อนุมัติสำเร็จ</h2>
      </div>
    </div>

    <div v-if="isDeleteAlertOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mt-3">ยืนยันการลบผู้มีสิทธิ์อนุมัติสำเร็จ</h2>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* เพิ่มสไตล์ตามความต้องการ */
</style>