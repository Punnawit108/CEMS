<script setup lang="ts">
/**
* ชื่อไฟล์: UserSetting.vue
* คำอธิบาย: ไฟล์นี้แสดงหน้าจอจัดการผู้ใช้ ซึ่งแสดงตารางผู้ใช้ภายในระบบ พร้อมฟังก์ชั่นค้นหาและกรองข้อมูล
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 4 มีนาคม 2568
*/

import Icon from '../../components/Icon/CIcon.vue'
import { useRouter } from 'vue-router'
import { ref, onMounted, computed, watch } from 'vue'
import Pagination from '../../components/Pagination.vue'
import { useUserStore } from '../../store/user'
import { storeToRefs } from 'pinia'
import type { User } from '../../types'

// Import filters
import UserSearchInput from '../../components/filters/UserSearchInput.vue'
import DepartmentFilter from '../../components/filters/DepartmentFilter.vue'
import DivisionFilter from '../../components/filters/DivisionFilter.vue'
import RoleFilter from '../../components/filters/RoleFilter.vue'
import FilterButtons from '../../components/filters/FilterButtons.vue'

const router = useRouter()
const store = useUserStore()
const { users } = storeToRefs(store)
const loading = ref(false)

// Pagination state
const currentPage = ref(1)  
const itemsPerPage = ref(10)
const paginatedUsers = ref<User[]>([])

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
  if (!users.value) return []

  return users.value.filter(user => {
    const matchesSearch = lastSearchedFilters.value.searchTerm === '' ||
      user.usrFirstName.toLowerCase().includes(lastSearchedFilters.value.searchTerm.toLowerCase()) ||
      user.usrLastName.toLowerCase().includes(lastSearchedFilters.value.searchTerm.toLowerCase()) ||
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
  } catch (error) {
    console.error('Error in mounted:', error)
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <div class="flex flex-col text-center">
    <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-12 w-full">
      <UserSearchInput v-model="filters.searchTerm" :loading="loading" />

      <DepartmentFilter v-model="filters.department" :users="users" :loading="loading" />

      <DivisionFilter v-model="filters.division" :users="users" :loading="loading" />

      <RoleFilter v-model="filters.role" :users="users" :loading="loading" />

      <FilterButtons :loading="loading" @reset="handleReset" @search="handleSearch" />
    </div>
    <div class="w-full h-fit border-[2px] flex flex-col items-start">
      <table class="table-auto w-full text-center text-black">
        <thead class="bg-[#F2F4F8]">
          <tr class="text-[16px] border-b-2 border-[#BBBBBB]">
            <th class="py-[11px] px-2 w-12 font-bold h-[46px]">ลำดับ</th>
            <th class="py-[11px] px-2 text-center w-24 font-bold">รหัสพนักงาน</th>
            <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อ-นามสกุล</th>
            <th class="py-[11px] px-2 text-start w-20 font-bold">แผนก</th>
            <th class="py-[11px] px-2 text-start w-24 font-bold">ฝ่าย</th>
            <th class="py-[11px] px-2 text-start w-20 font-bold">บทบาท</th>
            <th class="py-[11px] px-2 text-start w-24 font-bold">สถานะ</th>
            <th class="py-[11px] px-2 text-center w-24 font-bold">ดูรายงาน</th>
            <th class="py-[11px] px-2 text-center w-24 font-bold">จัดการ</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="loading">
            <td colspan="9" class="py-4">
              <div class="flex justify-center items-center">
                <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
                <span class="ml-2">กำลังโหลดข้อมูล...</span>
              </div>
            </td>
          </tr>

          <tr v-else-if="!users?.length">
            <td colspan="9" class="py-4">ไม่มีข้อมูลผู้ใช้</td>
          </tr>

          <tr v-else-if="filteredUsers.length === 0">
            <td colspan="9" class="py-4">ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา</td>
          </tr>

          <tr v-else v-for="(user, index) in paginatedUsers" :key="user.usrId"
            class="text-[14px] border-b-2 border-[#BBBBBB] hover:bg-gray-50">
            <th class="py-[12px] px-2 w-12 h-[46px]">{{ ((currentPage - 1) * itemsPerPage) + index + 1 }}</th>
            <th class="py-[12px] px-2 w-24">{{ user.usrEmployeeId }}</th>
            <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
              style="max-width: 208px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
              :title="`${user.usrFirstName} ${user.usrLastName}`">
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
        <Pagination :items="filteredUsers" :itemsPerPage="itemsPerPage" v-model:currentPage="currentPage"
          v-model:paginatedItems="paginatedUsers" :showEmptyRows="true" />
      </table>
    </div>
  </div>
</template>