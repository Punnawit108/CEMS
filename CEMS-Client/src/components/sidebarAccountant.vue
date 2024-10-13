<template>
    <nav class="flex flex-col bg-white max-w-[256px] pb-[564px]">
      <header class="flex overflow-hidden flex-col justify-center py-6 w-full">
        <div class="flex flex-col px-5 w-full">
          <img loading="lazy"
            src="https://cdn.builder.io/api/v1/image/assets/TEMP/7351532a9124e6dbb90e0396fe615c9c0fbd984007bb32abc87e5f05ddeefc8a?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
            class="object-contain z-10 -mb-2 aspect-[2.7] w-[216px]" alt="Logo" />
        </div>
      </header>
  
      <ul class="flex flex-col w-full">
        <li
          class="flex overflow-hidden flex-col justify-center px-4 py-2.5 w-full text-sm leading-snug text-black whitespace-nowrap ">
          <RouterLink to="/dashboard">
            <button @click="toggleDashboard" :class="{ 'bg-red-100 rounded-xl': clickDashboard }"
              class="flex relative gap-2.5 items-center w-56 max-w-full min-h-[40px] hover:bg-red-100  rounded-xl"
              tabindex="0">
              <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
              <img loading="lazy"
                src="https://cdn.builder.io/api/v1/image/assets/TEMP/b4675fb7b7b61dd2f3d546d6038dfb20306e4726504bcab26f727202704f03ee?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
                class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
              <span class="self-stretch py-2.5 my-auto w-[174px] text-left">แดชบอร์ด</span>
            </button>
          </RouterLink>
        </li>
  
        <!-- การเบิกจ่าย dropdown -->
        <li
          class="flex overflow-hidden flex-col justify-center px-4 py-2.5 w-full text-sm leading-snug text-black whitespace-nowrap">
          <button @click="toggleWithdrawDropdown" :class="{ 'bg-neutral-100 rounded-xl': isWithdrawDropdownOpen }"
            class="flex relative gap-2.5 items-center w-56 max-w-full hover:bg-neutral-100 rounded-xl  " tabindex="0">
            <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
            <img loading="lazy"
              src="https://cdn.builder.io/api/v1/image/assets/TEMP/03778099500b2f98224640a7c0d0c308da4cbfc93c511866614b728b299bef10?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
              class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
            <span class="self-stretch py-2.5 my-auto w-[134px] text-left">การเบิกจ่าย</span>
            <img loading="lazy"
              src="https://cdn.builder.io/api/v1/image/assets/TEMP/6dbdcd13be406ba0564e7ceae089fdddfd52a9bc3536460ba87e6e13c22a5f4c?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
              class="object-contain z-0 shrink-0 self-stretch my-auto aspect-[0.75] w-[30px]" alt="" />
          </button>
  
          <!-- Dropdown content for การเบิกจ่าย -->
          <ul v-if="isWithdrawDropdownOpen" class="ml-8 mt-2 space-y-2">
            <li @click="toggleListWithdraw" :class="{ 'bg-red-100 rounded-xl': clickListWithdraw }"
              class="hover:bg-red-100  rounded-xl">
              <RouterLink to="/listWithdraw">
                <a href="#"
                  class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px] ">
                  <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                  <div class="flex z-0 shrink-0 self-stretch my-auto "></div>
                  <img loading="lazy"
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/78773019f1878802996c7647863089b1029e5a2cedd37f5bb689ab1a3b1c02bd?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
                    class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
                  <span class="self-stretch py-2.5 my-auto w-[124px]">รายการเบิกค่าใช้จ่าย</span>
                </a>
              </RouterLink>
            </li>
            <li @click="toggleHistoryWithdraw" :class="{ 'bg-red-100 rounded-xl': clickHistoryWithdraw }"
              class="hover:bg-red-100  rounded-xl">
              <RouterLink to="/historyWithdraw">
                <a href="#"
                  class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                  <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                  <div class="flex z-0 shrink-0 self-stretch my-auto "></div>
                  <img loading="lazy"
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/a887bf583dd45e6fcab5ada6177f5df5e1879444be88e86158b6178d7191bf4d?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
                    class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
                  <span class="self-stretch py-2.5 my-auto w-[124px]">ประวัติเบิกค่าใช้จ่าย</span>
                </a>
              </RouterLink>
  
            </li>
          </ul>
        </li>
  
        <!-- รายงาน dropdown -->
        <li class="flex overflow-hidden flex-col px-4 py-2.5 w-full">
          <button @click="toggleReportDropdown" :class="{ 'bg-neutral-100 rounded-xl': isReportDropdownOpen }"
            class="flex relative gap-2.5 items-center w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px] hover:bg-neutral-100 rounded-xl"
            tabindex="0">
            <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
            <img loading="lazy"
              src="https://cdn.builder.io/api/v1/image/assets/TEMP/c02761727dcf26d682e23bb827b9ace5885b3a9c2ee9d6205240096ecfa13e1d?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
              class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
            <span class="self-stretch py-2.5 my-auto w-[134px] text-left">รายงาน</span>
            <img loading="lazy"
              src="https://cdn.builder.io/api/v1/image/assets/TEMP/6dbdcd13be406ba0564e7ceae089fdddfd52a9bc3536460ba87e6e13c22a5f4c?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
              class="object-contain z-0 shrink-0 self-stretch my-auto aspect-[0.75] w-[30px]" alt="" />
          </button>
  
          <!-- Dropdown content for รายงาน -->
          <ul v-if="isReportDropdownOpen" class="ml-8 mt-2 space-y-2">
            <li @click="toggleReportProject" :class="{ 'bg-red-100 rounded-xl': clickReportProject }"
              class="hover:bg-red-100  rounded-xl">
              <RouterLink to="/reportProject">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto "></div>
                <img loading="lazy"
                  src="https://cdn.builder.io/api/v1/image/assets/TEMP/031bd0f136b68469d0809d7c1fea60fd88d63a5b5e82179d1772844aa325c962?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
                  class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
                <span class="self-stretch py-2.5 my-auto w-[124px]">รายงานโครงการ</span>
              </a>
            </RouterLink>
  
            </li>
            <li @click="toggleReportWithdraw" :class="{ 'bg-red-100 rounded-xl': clickReportWithdraw }"
              class="hover:bg-red-100  rounded-xl">
  
              <RouterLink to="/reportWithdraw">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto "></div>
                <img loading="lazy"
                  src="https://cdn.builder.io/api/v1/image/assets/TEMP/78773019f1878802996c7647863089b1029e5a2cedd37f5bb689ab1a3b1c02bd?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
                  class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
                <span class="self-stretch py-2.5 my-auto w-[124px]">รายงานเบิกค่าใช้จ่าย</span>
              </a>
            </RouterLink>
  
            </li>
          </ul>
  
        </li>
  
        <!-- การอนุมัติ dropdown -->
        <li
          class="flex overflow-hidden flex-col justify-center px-4 py-2.5 w-full text-sm leading-snug text-black whitespace-nowrap">
          <button @click="toggleDeliverDropdown" :class="{ 'bg-neutral-100 rounded-xl': isDeliverDropdownOpen }"
            class="flex relative gap-2.5 items-center w-56 max-w-full min-h-[40px] hover:bg-neutral-100 rounded-xl"
            tabindex="0">
            <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
            <img loading="lazy"
              src="https://cdn.builder.io/api/v1/image/assets/TEMP/5d27f4d04d18e2ade2269d8c1ebe454ffdb3f4b1f6b065a61ae156ed18d72715?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
              class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
            <span class="self-stretch py-2.5 my-auto w-[134px] text-left">การนำจ่าย</span>
            <img loading="lazy"
              src="https://cdn.builder.io/api/v1/image/assets/TEMP/6dbdcd13be406ba0564e7ceae089fdddfd52a9bc3536460ba87e6e13c22a5f4c?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
              class="object-contain z-0 shrink-0 self-stretch my-auto aspect-[0.75] w-[30px]" alt="" />
          </button>
          <!-- Dropdown content for  การอนุมัติ -->
  
          <ul v-if="isDeliverDropdownOpen" class="ml-8 mt-2 space-y-2">
            <li @click="toggleListDeliverWithdraw" :class="{ 'bg-red-100 rounded-xl': clickListDeliverWithdraw }"
              class="hover:bg-red-100  rounded-xl">
  
              <RouterLink to="/listDeliverWithdraw">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto "></div>
                <img loading="lazy"
                  src="https://cdn.builder.io/api/v1/image/assets/TEMP/46185c3d2e7c068668b6199bff591760cb9b2634efff560dcf20f8de07b98edb?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
                  class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
                <span class="self-stretch py-2.5 my-auto w-[124px]">รายการรอนำจ่าย</span>
              </a>
            </RouterLink>
  
            </li>
            <li @click="toggleHistoryDeliverWithdraw" :class="{ 'bg-red-100 rounded-xl': clickHistoryDeliverWithdraw }"
              class="hover:bg-red-100  rounded-xl">
              <RouterLink to="/historyDeliverWithdraw">
              <a href="#"
                class="flex relative gap-2.5 items-center mt-2.5 w-56 max-w-full text-sm leading-snug text-black whitespace-nowrap min-h-[40px]">
                <div class="flex absolute right-0 bottom-0 z-0 shrink-0 self-start w-56 h-10 rounded-xl"></div>
                <div class="flex z-0 shrink-0 self-stretch my-auto "></div>
                <img loading="lazy"
                  src="https://cdn.builder.io/api/v1/image/assets/TEMP/a887bf583dd45e6fcab5ada6177f5df5e1879444be88e86158b6178d7191bf4d?placeholderIfAbsent=true&apiKey=b075c04e5be74b53b5cb51cf80fcda46"
                  class="object-contain z-0 shrink-0 self-stretch my-auto w-10 aspect-square" alt="" />
                <span class="self-stretch py-2.5 my-auto w-[124px]">ประวัติการนำจ่าย</span>
              </a>
            </RouterLink>
  
            </li>
            
          </ul>
  
        </li>
      </ul>
    </nav>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { RouterLink } from 'vue-router';
  
  
  
  export default defineComponent({
    name: 'Sidebar',
    setup() {
      const clickDashboard = ref(false);
      const clickListWithdraw = ref(false);     //ปุ่มใน dropdown เบิกค่าใช้จ่าย
      const clickHistoryWithdraw = ref(false);
      const clickReportProject = ref(false);    //ปุ่มใน dropdown รายงาน
      const clickReportWithdraw = ref(false);
      const clickListDeliverWithdraw = ref(false);    //ปุ่มใน dropdown ตั้งค่าระบบ
      const clickHistoryDeliverWithdraw = ref(false);
      const clickSettingTypeWithdraw = ref(false);
  
  
      const isWithdrawDropdownOpen = ref(false);
      const isReportDropdownOpen = ref(false);
      const isDeliverDropdownOpen = ref(false);
  
      const toggleWithdrawDropdown = () => { //เมื่อกด เบิกจ่าย dropdownตัวอื่นที่แสดงอยู่จะถูกซ่อน
        isWithdrawDropdownOpen.value = !isWithdrawDropdownOpen.value;
  
      };
  
      const toggleReportDropdown = () => { //เมื่อกด รายงาน dropdownตัวอื่นที่แสดงอยู่จะถูกซ่อน
        isReportDropdownOpen.value = !isReportDropdownOpen.value;
  
      };
      const toggleDeliverDropdown = () => { //เมื่อกด ตั้งค่าระบบ dropdownตัวอื่นที่แสดงอยู่จะถูกซ่อน
        isDeliverDropdownOpen.value = !isDeliverDropdownOpen.value;
  
      };
  
  
      const toggleDashboard = () => {
        clickDashboard.value = !clickDashboard.value;
        clickListWithdraw.value = false;
        clickHistoryWithdraw.value = false;
        clickReportProject.value = false;
        clickReportWithdraw.value = false;
        clickListDeliverWithdraw.value = false;
        clickHistoryDeliverWithdraw.value = false;
        clickSettingTypeWithdraw.value = false;
  
      };
      const toggleListWithdraw = () => {
        clickListWithdraw.value = !clickListWithdraw.value;
        clickDashboard.value = false;
        clickHistoryWithdraw.value = false;
        clickReportProject.value = false;
        clickReportWithdraw.value = false;
        clickListDeliverWithdraw.value = false;
        clickHistoryDeliverWithdraw.value = false;
        clickSettingTypeWithdraw.value = false;
  
      };
      const toggleHistoryWithdraw = () => {
        clickHistoryWithdraw.value = !clickHistoryWithdraw.value;
        clickDashboard.value = false;
        clickListWithdraw.value = false;
        clickReportProject.value = false;
        clickReportWithdraw.value = false;
        clickListDeliverWithdraw.value = false;
        clickHistoryDeliverWithdraw.value = false;
        clickSettingTypeWithdraw.value = false;
  
      };
      const toggleReportProject = () => {
        clickReportProject.value = !clickReportProject.value;
        clickDashboard.value = false;
        clickListWithdraw.value = false;
        clickHistoryWithdraw.value = false;
        clickReportWithdraw.value = false;
        clickListDeliverWithdraw.value = false;
        clickHistoryDeliverWithdraw.value = false;
        clickSettingTypeWithdraw.value = false;
      };
      const toggleReportWithdraw = () => {
        clickReportWithdraw.value = !clickReportWithdraw.value;
        clickDashboard.value = false;
        clickListWithdraw.value = false;
        clickHistoryWithdraw.value = false;
        clickReportProject.value = false;
        clickListDeliverWithdraw.value = false;
        clickHistoryDeliverWithdraw.value = false;
        clickSettingTypeWithdraw.value = false;
      };
      const toggleListDeliverWithdraw = () => {
        clickListDeliverWithdraw.value = !clickListDeliverWithdraw.value;
        clickDashboard.value = false;
        clickListWithdraw.value = false;
        clickHistoryWithdraw.value = false;
        clickReportProject.value = false;
        clickReportWithdraw.value = false;
        clickHistoryDeliverWithdraw.value = false;
        clickSettingTypeWithdraw.value = false;
      };
      const toggleHistoryDeliverWithdraw = () => {
        clickHistoryDeliverWithdraw.value = !clickHistoryDeliverWithdraw.value;
        clickDashboard.value = false;
        clickListWithdraw.value = false;
        clickHistoryWithdraw.value = false;
        clickReportProject.value = false;
        clickReportWithdraw.value = false;
        clickListDeliverWithdraw.value = false;
        clickSettingTypeWithdraw.value = false;
      };
      const toggleSettingTypeWithdraw = () => {
        clickSettingTypeWithdraw.value = !clickSettingTypeWithdraw.value;
        clickDashboard.value = false;
        clickListWithdraw.value = false;
        clickHistoryWithdraw.value = false;
        clickReportProject.value = false;
        clickReportWithdraw.value = false;
        clickListDeliverWithdraw.value = false;
        clickHistoryDeliverWithdraw.value = false;
      };
  
      return {
        isWithdrawDropdownOpen,
        isReportDropdownOpen,
        isDeliverDropdownOpen,
        clickDashboard,
        clickListWithdraw,
        clickHistoryWithdraw,
        clickReportProject,
        clickReportWithdraw,
        clickListDeliverWithdraw,
        clickHistoryDeliverWithdraw,
        clickSettingTypeWithdraw,
        toggleWithdrawDropdown,
        toggleReportDropdown,
        toggleDeliverDropdown,
        toggleDashboard,
        toggleListWithdraw,
        toggleHistoryWithdraw,
        toggleReportProject,
        toggleReportWithdraw,
        toggleListDeliverWithdraw,
        toggleHistoryDeliverWithdraw,
        toggleSettingTypeWithdraw
  
      };
    },
  });
  </script>
  