<script setup lang="ts">
/*
 * ชื่อไฟล์: SideBar.vue
 * คำอธิบาย: ไฟล์นี้เป็น Component สำหรับใช้ในทุกๆหน้า
 * ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
 * วันที่จัดทำ/แก้ไข: 27 มีนาคม 2568
 */
import { ref, onMounted, watch } from "vue";
import Icon from "./Icon/CIcon.vue";
import { useNotificationStore } from "../store/notification";

const notificationStore = useNotificationStore();

const userInfo = ref<any>(null);

console.log(notificationStore.unreadCount);

onMounted(async () => {
  const userData = localStorage.getItem("user");
  userInfo.value = userData ? await JSON.parse(userData) : null;
  //role.value = parsedData && parsedData.usrRolName ? parsedData.usrRolName : null;
  await notificationStore.loadNotifications(userInfo.value.usrId);
  notificationStore.initSignalR(userInfo.value.usrId);
  if (window.location.pathname === "/login") {
    localStorage.removeItem("selectMenu"); // ลบ selectMenu จาก localStorage
    updateSelectMenu("clickDashboard");
  } else {
    const storedMenu = localStorage.getItem("selectMenu");
    if (storedMenu) {
      if (storedMenu === "clickDashboard") {
        clickDashboard.value = true;
      } else if (storedMenu === "clickNotification") {
        clickNotification.value = true;
      } else if (storedMenu === "clickListWithdraw") {
        toggleWithdrawDropdown();
        clickListWithdraw.value = true;
      } else if (storedMenu === "clickHistoryWithdraw") {
        toggleWithdrawDropdown();
        clickHistoryWithdraw.value = true;
      } else if (storedMenu === "clickReportProject") {
        toggleReportDropdown();
        clickReportProject.value = true;
      } else if (storedMenu === "clickReportWithdraw") {
        toggleReportDropdown();
        clickReportWithdraw.value = true;
      } else if (storedMenu === "clickListApproval") {
        toggleApprovalDropdown();
        clickListApproval.value = true;
      } else if (storedMenu === "clickHistoryApproval") {
        toggleApprovalDropdown();
        clickHistoryApproval.value = true;
      } else if (storedMenu === "clickListDeliverWithdraw") {
        toggleDeliverDropdown();
        clickListDeliverWithdraw.value = true;
      } else if (storedMenu === "clickHistoryDeliverWithdraw") {
        toggleDeliverDropdown();
        clickHistoryDeliverWithdraw.value = true;
      } else if (storedMenu === "clickSettingUser") {
        toggleSettingDropdown();
        clickSettingUser.value = true;
      } else if (storedMenu === "clickSettingApprover") {
        toggleSettingDropdown();
        clickSettingApprover.value = true;
      } else if (storedMenu === "clickSettingTypeWithdraw") {
        toggleSettingDropdown();
        clickSettingTypeWithdraw.value = true;
      }
    }
  }
});

const updateSelectMenu = (menu: string) => {
  localStorage.setItem("selectMenu", menu);
};

const clickDashboard = ref(false);
const clickNotification = ref(false);

const clickListWithdraw = ref(false); //ปุ่มใน dropdown เบิกค่าใช้จ่าย
const clickHistoryWithdraw = ref(false);
const clickReportProject = ref(false); //ปุ่มใน dropdown รายงาน
const clickReportWithdraw = ref(false);
const clickListApproval = ref(false); //ปุ่มใน dropdown การอนุมัติ
const clickHistoryApproval = ref(false);
const clickListDeliverWithdraw = ref(false); //ปุ่มใน dropdown การนำจ่าย
const clickHistoryDeliverWithdraw = ref(false);
const clickSettingUser = ref(false); //ปุ่มใน dropdown ตั้งค่าระบบ
const clickSettingApprover = ref(false);
const clickSettingTypeWithdraw = ref(false);

const isWithdrawDropdownOpen = ref(false);
const isReportDropdownOpen = ref(false);
const isApprovalDropdownOpen = ref(false);
const isDeliverDropdownOpen = ref(false);
const isSettingDropdownOpen = ref(false);

const resetAllDropdown = () => {
  isWithdrawDropdownOpen.value = false;
  isReportDropdownOpen.value = false;
  isApprovalDropdownOpen.value = false;
  isDeliverDropdownOpen.value = false;
  isSettingDropdownOpen.value = false;
};

const toggleWithdrawDropdown = () => {
  if (isWithdrawDropdownOpen.value == true) {
    isWithdrawDropdownOpen.value = !isWithdrawDropdownOpen.value;
  } else {
    resetAllDropdown();
    isWithdrawDropdownOpen.value = true;
  }
};

const toggleReportDropdown = () => {
  if (isReportDropdownOpen.value == true) {
    isReportDropdownOpen.value = !isReportDropdownOpen.value;
  } else {
    resetAllDropdown();
    isReportDropdownOpen.value = true;
  }
};

const toggleApprovalDropdown = () => {
  if (isApprovalDropdownOpen.value == true) {
    isApprovalDropdownOpen.value = !isApprovalDropdownOpen.value;
  } else {
    resetAllDropdown();
    isApprovalDropdownOpen.value = true;
  }
};

const toggleDeliverDropdown = () => {
  if (isDeliverDropdownOpen.value == true) {
    isDeliverDropdownOpen.value = !isDeliverDropdownOpen.value;
  } else {
    resetAllDropdown();
    isDeliverDropdownOpen.value = true;
  }
};

const toggleSettingDropdown = () => {
  if (isSettingDropdownOpen.value == true) {
    isSettingDropdownOpen.value = !isSettingDropdownOpen.value;
  } else {
    resetAllDropdown();
    isSettingDropdownOpen.value = true;
  }
};

const resetAllToggles = () => {
  clickNotification.value = false;
  clickDashboard.value = false;
  clickListWithdraw.value = false;
  clickHistoryWithdraw.value = false;
  clickReportProject.value = false;
  clickReportWithdraw.value = false;
  clickListApproval.value = false;
  clickHistoryApproval.value = false;
  clickListDeliverWithdraw.value = false;
  clickHistoryDeliverWithdraw.value = false;
  clickSettingUser.value = false;
  clickSettingApprover.value = false;
  clickSettingTypeWithdraw.value = false;
};

const toggleDashboard = () => {
  clickDashboard.value = !clickDashboard.value;
  resetAllToggles();
  clickDashboard.value = true;
};

const toggleNotification = () => {
  clickNotification.value = !clickNotification.value;
  resetAllToggles();
  clickNotification.value = true;
};

const toggleListWithdraw = () => {
  clickListWithdraw.value = !clickListWithdraw.value;
  resetAllToggles();
  clickListWithdraw.value = true;
};

const toggleHistoryWithdraw = () => {
  clickHistoryWithdraw.value = !clickHistoryWithdraw.value;
  resetAllToggles();
  clickHistoryWithdraw.value = true;
};

const toggleReportProject = () => {
  clickReportProject.value = !clickReportProject.value;
  resetAllToggles();
  clickReportProject.value = true;
};

const toggleReportWithdraw = () => {
  clickReportWithdraw.value = !clickReportWithdraw.value;
  resetAllToggles();
  clickReportWithdraw.value = true;
};

const toggleListApproval = () => {
  clickListApproval.value = !clickListApproval.value;
  resetAllToggles();
  clickListApproval.value = true;
};

const toggleHistoryApproval = () => {
  clickHistoryApproval.value = !clickHistoryApproval.value;
  resetAllToggles();
  clickHistoryApproval.value = true;
};

const toggleListDeliverWithdraw = () => {
  clickListDeliverWithdraw.value = !clickListDeliverWithdraw.value;
  resetAllToggles();
  clickListDeliverWithdraw.value = true;
};

const toggleHistoryDeliverWithdraw = () => {
  clickHistoryDeliverWithdraw.value = !clickHistoryDeliverWithdraw.value;
  resetAllToggles();
  clickHistoryDeliverWithdraw.value = true;
};

const toggleSettingUser = () => {
  clickSettingUser.value = !clickSettingUser.value;
  resetAllToggles();
  clickSettingUser.value = true;
};

const toggleSettingApprover = () => {
  clickSettingApprover.value = !clickSettingApprover.value;
  resetAllToggles();
  clickSettingApprover.value = true;
};

const toggleSettingTypeWithdraw = () => {
  clickSettingTypeWithdraw.value = !clickSettingTypeWithdraw.value;
  resetAllToggles();
  clickSettingTypeWithdraw.value = true;
};

// watch สถานะของแต่ละปุ่ม
watch(clickDashboard, (newVal) => {
  if (newVal) updateSelectMenu("clickDashboard");
});
watch(clickNotification, (newVal) => {
  if (newVal) updateSelectMenu("clickNotification");
});
watch(clickListWithdraw, (newVal) => {
  if (newVal) updateSelectMenu("clickListWithdraw");
});
watch(clickHistoryWithdraw, (newVal) => {
  if (newVal) updateSelectMenu("clickHistoryWithdraw");
});
watch(clickReportProject, (newVal) => {
  if (newVal) updateSelectMenu("clickReportProject");
});
watch(clickReportWithdraw, (newVal) => {
  if (newVal) updateSelectMenu("clickReportWithdraw");
});
watch(clickListApproval, (newVal) => {
  if (newVal) updateSelectMenu("clickListApproval");
});
watch(clickHistoryApproval, (newVal) => {
  if (newVal) updateSelectMenu("clickHistoryApproval");
});
watch(clickListDeliverWithdraw, (newVal) => {
  if (newVal) updateSelectMenu("clickListDeliverWithdraw");
});
watch(clickHistoryDeliverWithdraw, (newVal) => {
  if (newVal) updateSelectMenu("clickHistoryDeliverWithdraw");
});
watch(clickSettingUser, (newVal) => {
  if (newVal) updateSelectMenu("clickSettingUser");
});
watch(clickSettingApprover, (newVal) => {
  if (newVal) updateSelectMenu("clickSettingApprover");
});
watch(clickSettingTypeWithdraw, (newVal) => {
  if (newVal) updateSelectMenu("clickSettingTypeWithdraw");
});
</script>

<template>
  <nav class="flex flex-col bg-white max-w-[256px] relative drop-shadow-md">
    <header class="flex flex-col justify-center py-6 w-full">
      <div class="flex flex-col px-5 w-full">
        <img loading="lazy"
          src="https://cdn.builder.io/api/v1/image/assets/TEMP/7351532a9124e6dbb90e0396fe615c9c0fbd984007bb32abc87e5f05ddeefc8a?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
          class="object-contain z-10 -mb-2 aspect-[2.7] w-[216px]" alt="Logo" />
      </div>
    </header>

    <ul class="flex flex-col w-full" v-if="userInfo != null">
      <!-- ปุ่มแดชบอร์ด -->
      <li
        class="flex overflow-hidden flex-col justify-center px-4 py-2.5 w-full text-sm leading-snug text-black whitespace-nowrap">
        <RouterLink to="/dashboard">
          <button @click="toggleDashboard" :class="{ 'bg-red-100 rounded-xl': clickDashboard }"
            class="flex relative gap-2.5 items-center w-56 max-w-full min-h-[40px] hover:bg-red-100 rounded-xl"
            tabindex="0">
            <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
            <Icon :icon="'dashboard'" class="ml-2" />

            <span class="self-stretch py-2.5 my-auto w-[174px] text-left">แดชบอร์ด</span>
          </button>
        </RouterLink>
      </li>

      <!-- การเบิกจ่าย dropdown -->
      <li
        class="flex overflow-hidden flex-col justify-center px-4 py-2.5 w-full text-sm leading-snug text-black whitespace-nowrap">
        <button @click="toggleWithdrawDropdown" :class="{ 'bg-neutral-100 rounded-xl': isWithdrawDropdownOpen }"
          class="flex relative gap-2.5 items-center w-56 max-w-full hover:bg-neutral-100 rounded-xl" tabindex="0">
          <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
          <Icon :icon="'disbursement'" class="ml-2" />
          <span class="self-stretch py-2.5 my-auto w-[134px] text-left">การเบิกค่าใช้จ่าย</span>
          <img loading="lazy"
            src="https://cdn.builder.io/api/v1/image/assets/TEMP/6dbdcd13be406ba0564e7ceae089fdddfd52a9bc3536460ba87e6e13c22a5f4c?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
            class="object-contain z-0 shrink-0 self-stretch my-auto aspect-[0.75] w-[30px]" alt="" />
        </button>

        <!-- Dropdown content for การเบิกจ่าย -->
        <ul v-if="isWithdrawDropdownOpen" class="ml-8 mt-2 space-y-2">
          <li @click="toggleListWithdraw" :class="{ 'bg-red-100 rounded-xl': clickListWithdraw }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/disbursement/listWithdraw">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'listWithdraw'" />
                <span class="self-stretch py-2.5 my-auto w-[124px]">รายการเบิกค่าใช้จ่าย</span>
              </a>
            </RouterLink>
          </li>
          <li @click="toggleHistoryWithdraw" :class="{ 'bg-red-100 rounded-xl': clickHistoryWithdraw }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/disbursement/historyWithdraw">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'historyWithdraw'" />
                <span class="self-stretch py-2.5 my-auto w-[124px]">ประวัติเบิกค่าใช้จ่าย</span>
              </a>
            </RouterLink>
          </li>
        </ul>
      </li>
      <!-- รายงาน dropdown -->
      <li v-if="userInfo.usrIsSeeReport == 1" class="flex overflow-hidden flex-col px-4 py-2.5 w-full">
        <button @click="toggleReportDropdown" :class="{ 'bg-neutral-100 rounded-xl': isReportDropdownOpen }"
          class="flex relative gap-2.5 items-center w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px] hover:bg-neutral-100 rounded-xl"
          tabindex="0">
          <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
          <Icon :icon="'report'" class="ml-2" />
          <span class="self-stretch py-2.5 my-auto w-[134px] text-left">รายงาน</span>
          <img loading="lazy"
            src="https://cdn.builder.io/api/v1/image/assets/TEMP/6dbdcd13be406ba0564e7ceae089fdddfd52a9bc3536460ba87e6e13c22a5f4c?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
            class="object-contain z-0 shrink-0 self-stretch my-auto aspect-[0.75] w-[30px]" alt="" />
        </button>

        <!-- Dropdown content for รายงาน -->
        <ul v-if="isReportDropdownOpen" class="ml-8 mt-2 space-y-2">
          <li @click="toggleReportProject" :class="{ 'bg-red-100 rounded-xl': clickReportProject }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/report/project">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'reportProject'" />

                <span class="self-stretch py-2.5 my-auto w-[124px]">รายงานโครงการ</span>
              </a>
            </RouterLink>
          </li>
          <li @click="toggleReportWithdraw" :class="{ 'bg-red-100 rounded-xl': clickReportWithdraw }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/report/expense">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'reportExpense'" />
                <span class="self-stretch py-2.5 my-auto w-[124px]">รายงานเบิกค่าใช้จ่าย</span>
              </a>
            </RouterLink>
          </li>
        </ul>
      </li>

      <!-- การอนุมัติ dropdown -->
      <li v-if="userInfo.usrIsApprover == 1"
        class="flex overflow-hidden flex-col justify-center px-4 py-2.5 w-full text-sm leading-snug text-black whitespace-nowrap">
        <button @click="toggleApprovalDropdown" :class="{ 'bg-neutral-100 rounded-xl': isApprovalDropdownOpen }"
          class="flex relative gap-2.5 items-center w-56 max-w-full min-h-[40px] hover:bg-neutral-100 rounded-xl"
          tabindex="0">
          <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
          <Icon :icon="'approval'" class="ml-2" />
          <span class="self-stretch py-2.5 my-auto w-[134px] text-left">การอนุมัติ</span>
          <img loading="lazy"
            src="https://cdn.builder.io/api/v1/image/assets/TEMP/6dbdcd13be406ba0564e7ceae089fdddfd52a9bc3536460ba87e6e13c22a5f4c?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
            class="object-contain z-0 shrink-0 self-stretch my-auto aspect-[0.75] w-[30px]" alt="" />
        </button>
        <!-- Dropdown content for  การอนุมัติ -->

        <ul v-if="isApprovalDropdownOpen" class="ml-8 mt-2 space-y-2">
          <li @click="toggleListApproval" :class="{ 'bg-red-100 rounded-xl': clickListApproval }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/approval/list">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'approvalList'" />
                <span class="self-stretch py-2.5 my-auto w-[124px]">รายการอนุมัติ</span>
              </a>
            </RouterLink>
          </li>
          <li @click="toggleHistoryApproval" :class="{ 'bg-red-100 rounded-xl': clickHistoryApproval }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/approval/history">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'historyWithdraw'" />

                <span class="self-stretch py-2.5 my-auto w-[124px]">ประวัติการอนุมัติ</span>
              </a>
            </RouterLink>
          </li>
        </ul>
      </li>

      <!-- การนำจ่าย dropdown -->
      <li v-if="userInfo.usrRolName == 'Accountant'"
        class="flex overflow-hidden flex-col justify-center px-4 py-2.5 w-full text-sm leading-snug text-black whitespace-nowrap">
        <button @click="toggleDeliverDropdown" :class="{ 'bg-neutral-100 rounded-xl': isDeliverDropdownOpen }"
          class="flex relative gap-2.5 items-center w-56 max-w-full min-h-[40px] hover:bg-neutral-100 rounded-xl"
          tabindex="0">
          <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
          <Icon :icon="'systemSettingsDisbursementTypeExpense'" class="ml-2" />

          <span class="self-stretch py-2.5 my-auto w-[134px] text-left">การนำจ่าย</span>
          <img loading="lazy"
            src="https://cdn.builder.io/api/v1/image/assets/TEMP/6dbdcd13be406ba0564e7ceae089fdddfd52a9bc3536460ba87e6e13c22a5f4c?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
            class="object-contain z-0 shrink-0 self-stretch my-auto aspect-[0.75] w-[30px]" alt="" />
        </button>
        <!-- Dropdown content for  การนำจ่าย -->

        <ul v-if="isDeliverDropdownOpen" class="ml-8 mt-2 space-y-2">
          <li @click="toggleListDeliverWithdraw" :class="{ 'bg-red-100 rounded-xl': clickListDeliverWithdraw }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/payment/list">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'listWithdraw'" />

                <span class="self-stretch py-2.5 my-auto w-[124px]">รายการรอนำจ่าย</span>
              </a>
            </RouterLink>
          </li>
          <li @click="toggleHistoryDeliverWithdraw" :class="{ 'bg-red-100 rounded-xl': clickHistoryDeliverWithdraw }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/payment/history">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'historyWithdraw'" />

                <span class="self-stretch py-2.5 my-auto w-[124px]">ประวัติการนำจ่าย</span>
              </a>
            </RouterLink>
          </li>
        </ul>
      </li>

      <!-- ตั้งค่าระบบ dropdown -->
      <li v-if="userInfo.usrRolName == 'Admin'"
        class="flex overflow-hidden flex-col justify-center px-4 py-2.5 w-full text-sm leading-snug text-black whitespace-nowrap">
        <button @click="toggleSettingDropdown" :class="{ 'bg-neutral-100 rounded-xl': isSettingDropdownOpen }"
          class="flex relative gap-2.5 items-center w-56 max-w-full min-h-[40px] hover:bg-neutral-100 rounded-xl"
          tabindex="0">
          <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
          <Icon :icon="'systemSettings'" class="ml-2" />

          <span class="self-stretch py-2.5 my-auto w-[134px] text-left">ตั้งค่าระบบ</span>
          <img loading="lazy"
            src="https://cdn.builder.io/api/v1/image/assets/TEMP/6dbdcd13be406ba0564e7ceae089fdddfd52a9bc3536460ba87e6e13c22a5f4c?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
            class="object-contain z-0 shrink-0 self-stretch my-auto aspect-[0.75] w-[30px]" alt="" />
        </button>
        <!-- Dropdown content for ตั้งค่าระบบ -->

        <ul v-if="isSettingDropdownOpen" class="ml-8 mt-2 space-y-2">
          <li @click="toggleSettingUser" :class="{ 'bg-red-100 rounded-xl': clickSettingUser }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/systemSettings/user">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'systemSettingsUser'" />

                <span class="self-stretch py-2.5 my-auto w-[124px]">ผู้ใช้งาน</span>
              </a>
            </RouterLink>
          </li>
          <li @click="toggleSettingApprover" :class="{ 'bg-red-100 rounded-xl': clickSettingApprover }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/systemSettings/disbursementApprover">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'systemSettingsDisbursementApprover'" />

                <span class="self-stretch py-2.5 my-auto w-[124px]">ผู้อนุมัติการเบิกจ่าย</span>
              </a>
            </RouterLink>
          </li>
          <li @click="toggleSettingTypeWithdraw" :class="{ 'bg-red-100 rounded-xl': clickSettingTypeWithdraw }"
            class="hover:bg-red-100 rounded-xl">
            <RouterLink to="/systemSettings/disbursementType">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto"></div>
                <Icon :icon="'systemSettingsDisbursementType'" />

                <span class="self-stretch py-2.5 my-auto w-[124px]">ประเภทเบิกจ่าย</span>
              </a>
            </RouterLink>
          </li>
        </ul>
      </li>

      <!-- ปุ่มแจ้งเตือน -->
      <li
        class="flex overflow-hidden flex-col justify-center px-4 py-2.5 w-full text-sm leading-snug text-black whitespace-nowrap">
        <RouterLink to="/notification">
          <button @click="toggleNotification" :class="{ 'bg-red-100 rounded-xl': clickNotification }"
            class="flex relative gap-2.5 items-center w-56 max-w-full min-h-[40px] hover:bg-red-100 rounded-xl"
            tabindex="0">
            <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
            <Icon :icon="'notification'" class="ml-2" />
            <!-- ตัวเลข -->
            <span class="self-stretch py-2.5 my-auto w-[174px] text-left">การแจ้งเตือน</span>
            <div v-if="notificationStore.unreadCount > 0"
              class="absolute right-2 flex items-center justify-center w-6 h-6 text-white bg-red-500 rounded-full text-xs">
              {{ notificationStore.unreadCount }}
            </div>
          </button>
        </RouterLink>
      </li>
    </ul>
  </nav>
</template>
