<script lang="ts" setup>
// import lib
import { ref, onMounted, computed, watch } from "vue";
import { useRouter } from "vue-router";
import { createColumnHelper } from "@tanstack/vue-table";
import { storeToRefs } from "pinia";

// import data
import { useProjectsStore } from "../../store/projectsReport";
import { useUserStore } from "../../store/user";
import type { ProjectReport, User } from "../../types";

// import components
import Icon from "../../components/Icon/CIcon.vue";
import Pagination from "../../components/Pagination.vue";
import BaseTable from "../../components/Table/DynamicTable.vue";

// lib variable
const router = useRouter();
const loading = ref(false);

// store variable
const userStore = useUserStore();
const projectsStore = useProjectsStore();
const { users } = storeToRefs(userStore);

// Pagination state

const columnHelper = createColumnHelper<ProjectReport>();

// Reset pagination when filters change
// watch(lastSearchedFilters, () => {
//   currentPage.value = 1
// })

// Navigation
const navigateToDetail = (userId: string) => {
    router.push(`/systemSettings/user/detail/${userId}`);
};

// Define columns
const tableHeader1 = [
    columnHelper.display({
        id: "order",
        header: () => "ลำดับ",
        cell: (props: any) => props.row.index + 1,
    }),
    columnHelper.accessor("pjName", {
        id: "pjName",
        header: () => "โครงการ",
        cell: (info: any) => info.getValue(),
    }),
    columnHelper.accessor("pjSumAmountExpenses", {
        id: "pjSumAmountExpenses",
        header: () => "ยอดค่าใช้จ่าย (บาท)",
        cell: (info: any) => {
            const value = info.getValue();
            const numberVal = parseFloat(value);
            if (!isNaN(numberVal)) {
                return numberVal.toLocaleString("en-US", {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2,
                });
            }
            return value;
        },
    }),
];

const tableHeader2 = [
    columnHelper.display({
        id: "order",
        header: () => "ลำดับ",
        cell: (props: any) => props.row.index + 1,
    }),
    columnHelper.accessor("usrEmployeeId", {
        id: "usrEmployeeId",
        header: () => "รหัสพนักงาน",
        cell: (info: any) => info.getValue(),
    }),
    columnHelper.accessor("usrFullName", {
        id: "usrFullName",
        header: () => "ชื่อ-นามสกุล",
        cell: (info: any) => info.getValue(),
    }),
    columnHelper.accessor("usrDptName", {
        id: "usrDptName",
        header: () => "แผนก",
        cell: (info: any) => info.getValue(),
    }),
    columnHelper.accessor("usrStName", {
        id: "usrStName",
        header: () => "ฝ่าย",
        cell: (info: any) => info.getValue(),
    }),
    columnHelper.accessor("usrRolName", {
        id: "usrRolName",
        header: () => "บทบาท",
        cell: (info: any) => info.getValue(),
    }),
    columnHelper.accessor("usrIsActive", {
        id: "usrIsActive",
        header: () => "สถานะ",
        cell: (info: any) => info.getValue(),
    }),
    columnHelper.accessor("usrIsSeeReport", {
        id: "usrIsSeeReport",
        header: () => "ดูรายงาน",
        cell: (info: any) => info.getValue(),
    }),
    columnHelper.accessor("usrManage", {
        id: "usrManage",
        header: () => "จัดการ",
        cell: (info: any) => info.getValue(),
    }),
];



const tableStyles = {
    order: "py-3 px-2 w-14",
    pjName: "py-3 px-2 w-auto text-start",
    pjSumAmountExpenses: "py-3 px-2 w-60 text-end",
};

onMounted(async () => {
    try {
        await Promise.all([
            userStore.getAllUsers(),
            projectsStore.getAllProjects(),
        ]);
    } catch (error) {
        console.error("Error in mounted:", error);
    } finally {
        loading.value = false;
    }
});



const tableData1 = computed(() => projectsStore.projects);
// ✅ **Pagination State**
const currentPage = ref(1);
const itemsPerPage = ref(10);
const paginatedUsers = ref<User[]>([]);

// ✅ **Compute Paginated Data**
const tableData2 = computed(() => {
    return userStore.users.map((user) => ({
        ...user,
        usrFullName: `${user.usrFirstName} ${user.usrLastName}`,
    }));
});
</script>

<template>
    <div>
        <!-- Project Table -->
        <BaseTable :columns="tableHeader1" :tableData="tableData1" :tableStyles="tableStyles"></BaseTable>

        <!-- Users Table -->
        <BaseTable :columns="tableHeader2" :tableData="tableData2" :tableStyles="tableStyles">
            <!-- ✅ Custom Rendering for usrIsActive -->
            <template #cell-usrIsActive="{ row }">
                <span :class="row.usrIsActive
                    ? 'bg-[#12B669] text-white px-3 py-1 rounded-full text-sm font-normal'
                    : 'bg-[#E1032B] text-white px-3 py-1 rounded-full text-sm font-normal'">
                    {{ row.usrIsActive ? 'อยู่ในระบบ' : 'ไม่อยู่ในระบบ' }}
                </span>
            </template>

            <!-- ✅ Custom Rendering for usrIsSeeReport -->
            <template #cell-usrIsSeeReport="{ row }">
                <span class="flex justify-center">
                    <input type="checkbox" :checked="row.usrIsSeeReport === 1" disabled
                        class="w-4 h-4 border-2 border-[#BBBBBB] rounded cursor-not-allowed opacity-70">
                </span>
            </template>

            <!-- ✅ Custom Action Button for User Management -->
            <template #cell-usrManage="{ row }">
                <span class="flex justify-center">
                    <div @click="navigateToDetail(row.usrId.toString())" class="cursor-pointer hover:text-blue-500">
                        <Icon :icon="'viewDetails'" />
                    </div>
                </span>
            </template>
        </BaseTable>
    </div>
</template>
