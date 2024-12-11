<script setup lang="ts">
/**
* ชื่อไฟล์: UserSetting.vue
* คำอธิบาย: ไฟล์นี้แสดงหน้าจอจัดการผู้ใช้ ซึ่งแสดงตารางผู้ใช้ภายในระบบ พร้อมฟังก์ชั่นค้นหาและกรองข้อมูล
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 8 ธันวาคม 2567
*/

import Icon from '../../components/template/CIcon.vue';
import { useRouter } from 'vue-router';
import { ref, onMounted, computed, watch } from 'vue';
import Ctable from '../../components/template/CTable.vue';
import { useUserStore } from '../../store/user';
import { storeToRefs } from 'pinia';
import Filter from '../../components/template/Filter.vue';
import { useProjectStore } from '../../store/project';
import { useRequisitionTypeStore } from '../../store/requisitionType';

const projectStore = useProjectStore();
const requisitionTypeStore = useRequisitionTypeStore();

const router = useRouter();
const store = useUserStore();
const { users } = storeToRefs(store);
const loading = ref(false);

const currentPage = ref(1);
const itemsPerPage = ref(10);

const filters = ref({
  searchTerm: '',
  department: '',
  division: '',
  status: '',
  role: '',
  projectId: '',
  selectedRequisitionTypeId: ''
});

watch(filters, () => {
  currentPage.value = 1;
});

const filteredUsers = computed(() => {
  return users.value.filter(user => {
    const matchesSearch = filters.value.searchTerm === '' ||
      user.usrFirstName.toLowerCase().includes(filters.value.searchTerm.toLowerCase()) ||
      user.usrLastName.toLowerCase().includes(filters.value.searchTerm.toLowerCase()) ||
      user.usrEmployeeId.toLowerCase().includes(filters.value.searchTerm.toLowerCase());

    const matchesDepartment = filters.value.department === '' ||
      user.usrDptName === filters.value.department;

    const matchesDivision = filters.value.division === '' ||
      user.usrStName === filters.value.division;

    const matchesStatus = filters.value.status === '' ||
      (filters.value.status === 'active' && user.usrIsActive) ||
      (filters.value.status === 'inactive' && !user.usrIsActive);

    const matchesRole = filters.value.role === '' ||
      user.usrRolName === filters.value.role;

    return matchesSearch && matchesDepartment && matchesDivision && matchesStatus && matchesRole;
  }).sort((a, b) => a.usrEmployeeId.localeCompare(b.usrEmployeeId));
});

const totalPages = computed(() => Math.ceil(filteredUsers.value.length / itemsPerPage.value));

const paginatedUsers = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage.value;
  const end = start + itemsPerPage.value;
  return filteredUsers.value.slice(start, end);
});

const remainingRows = computed(() => {
  const currentRows = paginatedUsers.value.length;
  return currentRows < itemsPerPage.value ? itemsPerPage.value - currentRows : 0;
});

const nextPage = () => {
  if (currentPage.value < totalPages.value) {
    currentPage.value++;
  }
};

const prevPage = () => {
  if (currentPage.value > 1) {
    currentPage.value--;
  }
};

const navigateToDetail = (userId: string) => {
  router.push(`/systemSettings/user/detail/${userId}`);
};

onMounted(async () => {
  loading.value = true;
  try {
    await Promise.all([
      store.getAllUsers(),
      projectStore.getAllProjects(),
    ]);
  } finally {
    loading.value = false;
  }
});
</script>

<template>
  <div class="flex flex-col text-center">
    <!-- เรียกใช้ตามต้องการ -->
    <Filter 
      :loading="loading"
      :users="users"
      :projects="projectStore.projects"
      :requisitionTypes="requisitionTypeStore.requisitionTypes"
      v-model:searchTerm="filters.searchTerm"
      v-model:selectedProjectId="filters.projectId"
      v-model:selectedRequisitionTypeId="filters.selectedRequisitionTypeId"
      :showSearchFilter="true"
      :showDepartmentFilter="false" 
      :showDivisionFilter="false"
      :showRoleFilter="false"
      :showProjectFilter="true"
      :showRequisitionTypeFilter="true"
      selectedDepartment=""
      selectedDivision=""
      selectedRole=""
    />
    <div class="w-full h-fit border-[2px] flex flex-col items-start">
      <Ctable :table="'Table5-head'" />
      <table class="table-auto w-full text-center text-black">
        <tbody>
          <tr v-if="loading">
            <td colspan="9" class="py-4">
              <div class="flex justify-center items-center">
                <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
                <span class="ml-2">กำลังโหลดข้อมูล...</span>
              </div>
            </td>
          </tr>

          <tr v-else-if="filteredUsers.length === 0">
            <td colspan="9" class="py-4">ไม่พบข้อมูล</td>
          </tr>

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

          <tr v-if="remainingRows > 0" v-for="index in remainingRows" :key="'empty-row' + index">
            <td v-for="i in 9" :key="'empty-cell' + i" class="py-[12px] px-2 h-[46px]">&nbsp;</td>
          </tr>
        </tbody>

        <tfoot class="border-t">
          <tr class="text-[14px] border-b-2 border-[#BBBBBB]">
            <th colspan="7"></th>
            <th class="py-[5px] text-center">
              {{ currentPage }} of {{ totalPages }}
            </th>
            <th class="py-[5px] flex justify-between text-[14px] font-bold">
              <span class="text-[#A0A0A0]">
                <button @click="prevPage"
                  class="p-2 rounded-md justify-start transition-colors duration-200 hover:bg-gray-100"
                  :class="{ 'text-[#BBBBBB] cursor-not-allowed hover:bg-transparent': currentPage === 1, 'text-black': currentPage !== 1 }"
                  :disabled="currentPage === 1">
                  <span class="text-sm">&lt;</span>
                </button>
              </span>
              <span class="text-[#A0A0A0]">
                <button @click="nextPage" class="p-2 rounded-md transition-colors duration-200 hover:bg-gray-100"
                  :class="{ 'text-[#BBBBBB] cursor-not-allowed hover:bg-transparent': currentPage === totalPages, 'text-black': currentPage !== totalPages }"
                  :disabled="currentPage === totalPages">
                  <span class="text-sm">&gt;</span>
                </button>
              </span>
            </th>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
</template>
