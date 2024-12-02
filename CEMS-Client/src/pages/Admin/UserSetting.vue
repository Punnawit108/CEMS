<script setup lang="ts">
/**
* ชื่อไฟล์: UserSetting.vue
* คำอธิบาย: ไฟล์นี้แสดงหน้าจอจัดการผู้ใช้ ซึ่งแสดงตารางผู้ใช้ภายในระบบ พร้อมฟังก์ชั่นค้นหาและกรองข้อมูล
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567  
*/

import Icon from '../../components/template/CIcon.vue';
import { useRouter } from 'vue-router';
import { ref, onMounted, computed } from 'vue';
import Ctable from '../../components/template/CTable.vue';
import { useUserStore } from '../../store/user';
import { storeToRefs } from 'pinia';

const router = useRouter();
const store = useUserStore();
const { users } = storeToRefs(store);
const loading = ref(false);

// Pagination states
const currentPage = ref(1);
const itemsPerPage = 10;

// Filter states
const searchTerm = ref('');
const selectedDepartment = ref(''); // สำหรับแผนก
const selectedDivision = ref('');   // สำหรับฝ่าย
const selectedStatus = ref('');
const selectedRole = ref('');

// Get unique filter options
const departments = computed(() => {
  const depts = new Set(users.value.map(user => user.usrDptName));
  return Array.from(depts).sort();
});

const divisions = computed(() => {
  const divs = new Set(users.value.map(user => user.usrStName));
  return Array.from(divs).sort();
});

const roles = computed(() => {
  const roles = new Set(users.value.map(user => user.usrRolName));
  return Array.from(roles).sort();
});

// Filtered users
const filteredUsers = computed(() => {
  return users.value
    .filter(user => {
      const matchesSearch = searchTerm.value === '' ||
        user.usrFirstName.toLowerCase().includes(searchTerm.value.toLowerCase()) ||
        user.usrLastName.toLowerCase().includes(searchTerm.value.toLowerCase()) ||
        user.usrEmployeeId.toLowerCase().includes(searchTerm.value.toLowerCase());

      const matchesDepartment = selectedDepartment.value === '' ||
        user.usrDptName === selectedDepartment.value;

      const matchesDivision = selectedDivision.value === '' ||
        user.usrStName === selectedDivision.value;

      const matchesStatus = selectedStatus.value === '' ||
        (selectedStatus.value === 'active' && user.usrIsActive) ||
        (selectedStatus.value === 'inactive' && !user.usrIsActive);

      const matchesRole = selectedRole.value === '' ||
        user.usrRolName === selectedRole.value;

      return matchesSearch && matchesDepartment && matchesDivision && matchesStatus && matchesRole;
    })
    .sort((a, b) => {
      // เรียงตามรหัสพนักงาน
      return a.usrEmployeeId.localeCompare(b.usrEmployeeId);
    });
});

// Paginated users
const paginatedUsers = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage;
  const end = start + itemsPerPage;
  return filteredUsers.value.slice(start, end);
});


// Navigation and initialization
const navigateToDetail = (userId: string) => {
  router.push(`/systemSettings/user/detail/${userId}`);
};

onMounted(async () => {
  loading.value = true;
  try {
    await store.getAllUsers();
  } finally {
    loading.value = false;
  }
});
</script>

<template>
  <div class="flex flex-col text-center">
    <!-- Filter Section -->
    <div class="flex flex-wrap justify-start items-center gap-5 mb-12">
      <!-- Search Filter -->
      <div class="h-[32px] w-[266px]">
        <form class="grid" @submit.prevent>
          <label for="SearchBar" class="py-0.5 text-[14px] text-black text-start">ค้นหา</label>
          <div class="relative h-[32px] w-[266px] justify-center items-center">
            <div class="absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
              <svg width="19" height="20" viewBox="0 0 19 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path
                  d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z"
                  stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
              </svg>
            </div>
            <input type="text" id="SearchBar" v-model="searchTerm"
              class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-9"
              placeholder="รหัสพนักงาน, ชื่อ-นามสกุล" :disabled="loading" />
          </div>
        </form>
      </div>

      <!-- Department Filter (แผนก) -->
      <div class="h-[32px] w-[266px]">
        <form class="grid">
          <label for="SelectDepartment" class="py-0.5 text-[14px] text-black text-start">แผนก</label>
          <div class="relative h-[32px] w-[266px] justify-center items-center">
            <select v-model="selectedDepartment" id="SelectDepartment" :disabled="loading"
              class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8">
              <option value="">ทั้งหมด</option>
              <option v-for="dept in departments" :key="dept" :value="dept">
                {{ dept }}
              </option>
            </select>
            <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
              <svg width="13" height="8" viewBox="0 0 13 8" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path fill-rule="evenodd" clip-rule="evenodd"
                  d="M7.2071 7.2071C6.8166 7.5976 6.1834 7.5976 5.7929 7.2071L0.79289 2.20711C0.40237 1.81658 0.40237 1.18342 0.79289 0.79289C1.18342 0.40237 1.81658 0.40237 2.20711 0.79289L6.5 5.0858L10.7929 0.79289C11.1834 0.40237 11.8166 0.40237 12.2071 0.79289C12.5976 1.18342 12.5976 1.81658 12.2071 2.20711L7.2071 7.2071Z"
                  fill="black" />
              </svg>
            </div>
          </div>
        </form>
      </div>

      <!-- Division Filter (ฝ่าย) -->
      <div class="h-[32px] w-[266px]">
        <form class="grid">
          <label for="SelectDivision" class="py-0.5 text-[14px] text-black text-start">ฝ่าย</label>
          <div class="relative h-[32px] w-[266px] justify-center items-center">
            <select v-model="selectedDivision" id="SelectDivision" :disabled="loading"
              class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8">
              <option value="">ทั้งหมด</option>
              <option v-for="div in divisions" :key="div" :value="div">
                {{ div }}
              </option>
            </select>
            <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
              <svg width="13" height="8" viewBox="0 0 13 8" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path fill-rule="evenodd" clip-rule="evenodd"
                  d="M7.2071 7.2071C6.8166 7.5976 6.1834 7.5976 5.7929 7.2071L0.79289 2.20711C0.40237 1.81658 0.40237 1.18342 0.79289 0.79289C1.18342 0.40237 1.81658 0.40237 2.20711 0.79289L6.5 5.0858L10.7929 0.79289C11.1834 0.40237 11.8166 0.40237 12.2071 0.79289C12.5976 1.18342 12.5976 1.81658 12.2071 2.20711L7.2071 7.2071Z"
                  fill="black" />
              </svg>
            </div>
          </div>
        </form>
      </div>

      <!-- Role Filter -->
      <div class="h-[32px] w-[266px]">
        <form class="grid">
          <label for="SelectRole" class="py-0.5 text-[14px] text-black text-start">บทบาท</label>
          <div class="relative h-[32px] w-[266px] justify-center items-center">
            <select v-model="selectedRole" id="SelectRole" :disabled="loading"
              class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8">
              <option value="">ทั้งหมด</option>
              <option v-for="role in roles" :key="role" :value="role">
                {{ role }}
              </option>
            </select>
            <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
              <svg width="13" height="8" viewBox="0 0 13 8" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path fill-rule="evenodd" clip-rule="evenodd"
                  d="M7.2071 7.2071C6.8166 7.5976 6.1834 7.5976 5.7929 7.2071L0.79289 2.20711C0.40237 1.81658 0.40237 1.18342 0.79289 0.79289C1.18342 0.40237 1.81658 0.40237 2.20711 0.79289L6.5 5.0858L10.7929 0.79289C11.1834 0.40237 11.8166 0.40237 12.2071 0.79289C12.5976 1.18342 12.5976 1.81658 12.2071 2.20711L7.2071 7.2071Z"
                  fill="black" />
              </svg>
            </div>
          </div>
        </form>
      </div>
    </div>

    <!-- Table Section -->
    <div class="w-full h-fit border-[2px] flex flex-col items-start">
      <Ctable :table="'Table5-head'" />
      <table class="table-auto w-full text-center text-black">
        <tbody>
          <!-- Loading State -->
          <tr v-if="loading">
            <td colspan="9" class="py-4">
              <div class="flex justify-center items-center">
                <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
                <span class="ml-2">กำลังโหลดข้อมูล...</span>
              </div>
            </td>
          </tr>

          <!-- No Data State -->
          <tr v-else-if="filteredUsers.length === 0">
            <td colspan="9" class="py-4">ไม่พบข้อมูล</td>
          </tr>

          <!-- Data Display -->
          <tr v-else v-for="(user, index) in paginatedUsers" :key="user.usrId"
            class="text-[14px] border-b-2 border-[#BBBBBB] hover:bg-gray-50">
            <th class="py-[12px] px-2 w-12 h-[46px]">{{ ((currentPage - 1) * itemsPerPage) + index + 1 }}</th>
            <th class="py-[12px] px-2 w-24">{{ user.usrEmployeeId }}</th>
            <th class="py-[12px] px-2 w-52 text-start truncate">
              {{ user.usrFirstName }} {{ user.usrLastName }}
            </th>
            <th class="py-[12px] px-2 w-20 text-start font-[100]">{{ user.usrDptName }}</th>
            <th class="py-[12px] px-2 w-24 text-start">{{ user.usrStName }}</th>
            <th class="py-[12px] px-2 w-20 text-start">{{ user.usrRolName }}</th>
            <th class="py-[12px] px-2 w-24 text-start">{{ user.usrIsActive ? 'อยู่ในระบบ' : 'ไม่อยู่ในระบบ' }}</th>
            <th class="w-24">
              <span class="flex justify-center">
                <input type="checkbox" :checked="user.usrIsSeeReport === 1" disabled
                  class="w-4 h-4 border-2 border-[#BBBBBB] rounded cursor-not-allowed opacity-70">
              </span>
            </th>
            <th class="py-[10px] px-2 w-24 text-center">
              <span class="flex justify-center">
                <div @click="() => navigateToDetail(user.usrId.toString())" class="cursor-pointer hover:text-blue-500">
                  <Icon :icon="'viewDetails'" />
                </div>
              </span>
            </th>
          </tr>
        </tbody>
      </table>
      <Ctable :table="'Table5-footer'" />
    </div>
  </div>
</template>


<style scoped>
.custom-select {
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  background-image: none;
}

.custom-select::-ms-expand {
  display: none;
}

select,
select option {
  background-color: white;
  color: #000000;
}

select:invalid,
select option[value=""] {
  color: #999999;
}

[hidden] {
  display: none;
}

@media screen and (-webkit-min-device-pixel-ratio:0) {
  .custom-select {
    background-image: url("data:image/svg+xml;utf8,<svg fill='transparent' height='24' viewBox='0 0 24 24' width='24' xmlns='http://www.w3.org/2000/svg'><path d='M7 10l5 5 5-5z'/></svg>");
    background-repeat: no-repeat;
    background-position-x: 100%;
    background-position-y: 5px;
  }
}
</style>