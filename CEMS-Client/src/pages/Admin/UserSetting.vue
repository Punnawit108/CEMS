<script setup lang="ts">
/**
* ชื่อไฟล์: UserSetting.vue
* คำอธิบาย: ไฟล์นี้แสดงหน้าจอจัดการผู้ใช้ ซึ่งแสดงตารางผู้ใช้ภายในระบบ พร้อมฟังก์ชั่นค้นหาและกรองข้อมูล
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 4 มีนาคม 2568
*/

import Icon from '../../components/Icon/CIcon.vue'
import { useRouter } from 'vue-router'
import { ref, onMounted, computed, watch, nextTick } from 'vue'
import Pagination from '../../components/Pagination.vue'
import { useUserStore } from '../../store/user'
import { storeToRefs } from 'pinia'


const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalPages = computed(() => {
  return Math.ceil(filteredUsers.value.length / itemsPerPage.value);
});

const paginated = computed(() => {
  if (loading.value) return [];
  
  const start = (currentPage.value - 1) * itemsPerPage.value;
  const end = start + itemsPerPage.value;
  const pageItems = filteredUsers.value.slice(start, end);

  while (pageItems.length < itemsPerPage.value) {
    pageItems.push(null);
  }

  return pageItems;
});


// Import filters
import UserSearchInput from '../../components/filters/UserSearchInput.vue'
import DepartmentFilter from '../../components/filters/DepartmentFilter.vue'
import DivisionFilter from '../../components/filters/DivisionFilter.vue'
import RoleFilter from '../../components/filters/RoleFilter.vue'
import FilterButtons from '../../components/filters/FilterButtons.vue'

const router = useRouter()
const store = useUserStore()
const { users } = storeToRefs(store)
const loading = ref(true)



// Filters
const filters = ref({
  searchTerm: '',
  department: '',
  division: '',
  role: '',
})

// Last searched filters
const lastSearchedFilters = ref({
  searchTerm: '',
  department: '',
  division: '',
  role: '',
})

// Reset pagination when filters change
watch(lastSearchedFilters, () => {
  currentPage.value = 1
})

// Filtered users computation
const filteredUsers = computed(() => {
  if (loading.value || !users.value) return []

  return users.value.filter(user => {
    const fullName = `${user.usrFirstName} ${user.usrLastName}`.toLowerCase()

    const matchesSearch = lastSearchedFilters.value.searchTerm === '' ||
      user.usrFirstName.toLowerCase().includes(lastSearchedFilters.value.searchTerm.toLowerCase()) ||
      user.usrLastName.toLowerCase().includes(lastSearchedFilters.value.searchTerm.toLowerCase()) ||
      fullName.includes(lastSearchedFilters.value.searchTerm.toLowerCase()) ||
      user.usrEmployeeId.toLowerCase().includes(lastSearchedFilters.value.searchTerm.toLowerCase())

    const matchesDepartment = lastSearchedFilters.value.department === '' ||
      user.usrDptName === lastSearchedFilters.value.department

    const matchesDivision = lastSearchedFilters.value.division === '' ||
      user.usrStName === lastSearchedFilters.value.division

    const matchesRole = lastSearchedFilters.value.role === '' ||
      user.usrRolName === lastSearchedFilters.value.role

    return matchesSearch && matchesDepartment && matchesDivision && matchesRole
  }).sort((a, b) => a.usrEmployeeId.localeCompare(b.usrEmployeeId))
})

// Navigation
const navigateToDetail = (userId: string) => {
  router.push(`/systemSettings/user/detail/${userId}`)
}

// Filter handlers
const handleSearch = () => {
  lastSearchedFilters.value = {
    searchTerm: filters.value.searchTerm,
    department: filters.value.department,
    division: filters.value.division,
    role: filters.value.role,
  }
}

const handleReset = () => {
  // Reset current filters
  filters.value = {
    searchTerm: '',
    department: '',
    division: '',
    role: '',
  }

  // Reset last searched filters
  lastSearchedFilters.value = {
    searchTerm: '',
    department: '',
    division: '',
    role: '',
  }
}

// Lifecycle hooks
onMounted(async () => {
  loading.value = true
  try {
    await Promise.all([
      store.getAllUsers()
    ])
    await nextTick()
  } catch (error) {
    console.error('Error in mounted:', error)
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <div class="flex flex-col text-center">
    <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-4 w-full">
      <UserSearchInput v-model="filters.searchTerm" :loading="loading" />

      <DepartmentFilter v-model="filters.department" :users="users" :loading="loading" />

      <DivisionFilter v-model="filters.division" :users="users" :loading="loading" />

      <RoleFilter v-model="filters.role" :users="users" :loading="loading" />
      <div class="flex flex-col">
        <!-- ข้อความเพื่อให้ปุ่มอยู่ในระดับเดียวกับฟิลเตอร์ -->
        <div class="py-0.5 text-[14px] text-transparent">การดำเนินการ</div>
        <FilterButtons :loading="loading" @reset="handleReset" @search="handleSearch" />
      </div>

    </div>
    <div class="w-full h-fit border-[2px] flex flex-col items-start border-[#BBBBBB]">
      <table class="table-auto w-full text-center text-black">
        <thead class="bg-[#F2F4F8]">
          <tr class="text-[16px] h-[46px] border-b-2 border-[#BBBBBB]">
            <th class="py-3 px-2 w-12 font-bold">ลำดับ</th>
            <th class="py-3 px-2 text-center w-24 font-bold">รหัสพนักงาน</th>
            <th class="py-3 px-2 text-start w-52 font-bold">ชื่อ-นามสกุล</th>
            <th class="py-3 px-2 text-start w-20 font-bold">แผนก</th>
            <th class="py-3 px-2 text-start w-24 font-bold">ฝ่าย</th>
            <th class="py-3 px-2 text-start w-24 font-bold">บทบาท</th>
            <th class="py-3 px-2 text-start w-24 font-bold">สถานะ</th>
            <th class="py-3 px-2 text-center w-24 font-bold">ดูรายงาน</th>
            <th class="py-3 px-2 text-center w-24 font-bold">จัดการ</th>
          </tr>
        </thead>
        <!-- แยก tbody สำหรับสถานะโหลด -->
        <tbody v-if="loading">
          <tr>
            <td colspan="100%" class="py-4">
              <div class="flex justify-center items-center">
                <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
                <span class="ml-2">กำลังโหลดข้อมูล...</span>
              </div>
            </td>
          </tr>
        </tbody>

        <!-- tbody สำหรับแสดงข้อมูลหลังโหลดเสร็จ -->
        <tbody v-else>

          <tr v-if="!users?.length || filteredUsers.length === 0" v-for="n in 10" :key="n" class="h-[50px]">
            <td colspan="100%" class="py-4 text-center">
              <span v-if="n === 5">
                {{ !users?.length ? 'ไม่มีข้อมูลผู้ใช้' : 'ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา' }}
              </span>
            </td>
          </tr>

          <tr v-for="(user, index) in paginated" :key="user?.usrId ?? `empty-${index}`"
            :class="user ? 'text-[14px] h-[46px] border-b-2 border-[#BBBBBB] hover:bg-gray-50' : 'h-[46px]'">
            <template v-if="user">
              <th class="py-3 px-2 w-12">{{ ((currentPage - 1) * itemsPerPage) + index + 1 }}</th>
              <th class="py-3 px-2 w-24">{{ user.usrEmployeeId }}</th>
              <th class="py-3 px-2 x text-start "
                style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                :title="`${user.usrFirstName} ${user.usrLastName}`">
                {{ user.usrFirstName }} {{ user.usrLastName }}
              </th>
              <th class="py-3 px-2 w-20 text-start font-[100]">{{ user.usrDptName }}</th>
              <th class="py-3 px-2 w-24 text-start">{{ user.usrStName }}</th>
              <th class="py-3 px-2 w-24 text-start">{{ user.usrRolName }}</th>
              <th class="py-3 px-2 w-24 text-start">
                <span :class="user.usrIsActive
                  ? 'bg-[#12B669] text-white px-3 py-1 rounded-full text-sm font-normal'
                  : 'bg-[#E1032B] text-white px-3 py-1 rounded-full text-sm font-normal'">
                  {{ user.usrIsActive ? 'อยู่ในระบบ' : 'ไม่อยู่ในระบบ' }}
                </span>
              </th>
              <th class="w-24">
                <span class="flex justify-center">
                  <svg v-if="user.usrIsSeeReport === 1" xmlns="http://www.w3.org/2000/svg" width="20" height="20"
                    viewBox="0 0 24 24" fill="none" stroke="#999999" stroke-width="2" stroke-linecap="round"
                    stroke-linejoin="round">
                    <rect x="3" y="3" width="18" height="18" rx="4" ry="4"></rect>
                    <path d="M7 13l3 3 7-7"></path>
                  </svg>
                  <svg v-else xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none"
                    stroke="#999999" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <rect x="3" y="3" width="18" height="18" rx="4" ry="4"></rect>
                  </svg>
                </span>
              </th>
              <th class="py-[10px] px-2 w-24 text-center">
                <span class="flex justify-center">
                  <div @click="() => navigateToDetail(user.usrId.toString())"
                    class="cursor-pointer hover:text-blue-500">
                    <Icon :icon="'viewDetails'" />
                  </div>
                </span>
              </th>
            </template>
            <template v-else>
              <td>&nbsp;</td>
            </template>
          </tr>
        </tbody>
        <Pagination :currentPage="currentPage" :totalPages="totalPages"
          @update:currentPage="(page) => (currentPage = page)" />
      </table>
    </div>
  </div>
</template>