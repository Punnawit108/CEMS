<script setup lang="ts">
/*
* ชื่อไฟล์: ExpenseReimbursementHistory.vue
* คำอธิบาย: ไฟล์นี้แสดงประวัติการเบิกค่าใช้จ่าย
* ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
*/

import { useRouter } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
import StatusBudge from '../../components/template/StatusBudge.vue';
import Ctable from '../../components/template/CTable.vue';
import Filter from '../../components/template/Filter.vue';

import { onMounted, ref, watch } from 'vue';
import { useExpenseReimbursement } from '../../store/expenseReimbursement';
import { useProjectStore } from '../../store/project';
import { useRequisitionTypeStore } from '../../store/requisitionType';

// Interfaces
interface ExpenseHistory {
    rqId: number;
    rqUsrName: string;  
    rqName: string;
    rqPjId?: number;    
    rqPjName: string;
    rqRqtId?: number;  
    rqRqtName: string;
    rqWithDrawDate: string;
    rqExpenses: string; 
    rqStatus: number; 
}

interface FilterState {
    searchRqName: string;
    selectedProjectId: string;
    selectedRequisitionTypeId: string;
}

interface DateRangeState {
    start: Date | null;
    end: Date | null;
}

// Store initialization
const expenseReimbursementStore = useExpenseReimbursement();
const projectStore = useProjectStore();
const requisitionTypeStore = useRequisitionTypeStore();
const router = useRouter();

// Refs
const user = ref<any>(null);
const originalData = ref<ExpenseHistory[]>([]);
const loading = ref(false);

// Filter states
const filters = ref<FilterState>({
    searchRqName: '',
    selectedProjectId: '',
    selectedRequisitionTypeId: ''
});

// Date range state
const dateRange = ref<DateRangeState>({
    start: null,
    end: null
});

// Filtering function
const filterData = () => {
    console.log('Starting filter with:', filters.value);
    let filtered: ExpenseHistory[] = [...originalData.value];

    // Filter by expense name (rqName)
    if (filters.value.searchRqName.trim()) {
        filtered = filtered.filter(item => 
            item.rqName.toLowerCase().includes(filters.value.searchRqName.toLowerCase())
        );
    }

    // Filter by project
    if (filters.value.selectedProjectId) {
        const projectId = parseInt(filters.value.selectedProjectId);
        if (!isNaN(projectId)) {
            // Find project name and filter by it
            const project = projectStore.projects.find(p => p.pjId === projectId);
            if (project) {
                filtered = filtered.filter(item => item.rqPjName === project.pjName);
            }
        }
    }

    // Filter by requisition type
    if (filters.value.selectedRequisitionTypeId) {
        const reqTypeId = parseInt(filters.value.selectedRequisitionTypeId);
        if (!isNaN(reqTypeId)) {
            // Find requisition type name and filter by it
            const reqType = requisitionTypeStore.requisitionTypes.find(rt => rt.rqtId === reqTypeId);
            if (reqType) {
                filtered = filtered.filter(item => item.rqRqtName === reqType.rqtName);
            }
        }
    }

    // Filter by date range
    if (dateRange.value.start && dateRange.value.end) {
        filtered = filtered.filter(item => {
            const itemDate = new Date(item.rqWithDrawDate);
            const start = dateRange.value.start!;
            const end = dateRange.value.end!;
            
            // Set hours to make date comparison more accurate
            start.setHours(0, 0, 0, 0);
            end.setHours(23, 59, 59, 999);
            
            return itemDate >= start && itemDate <= end && !isNaN(itemDate.getTime());
        });
    }

    console.log('Filtered results:', filtered);
    expenseReimbursementStore.expenseReimbursementHistory = filtered as any;
};

// Event handlers
const handleDateFilter = (start: Date, end: Date) => {
    dateRange.value.start = start;
    dateRange.value.end = end;
    filterData();
};

const toDetails = (id: number) => {
    router.push(`/disbursement/historyWithdraw/detail/${id}`);
};

// Watch for filter changes
watch(filters, () => {
    filterData();
}, { deep: true });

// Initialize data
onMounted(async () => {
    loading.value = true;
    try {
        const storedUser = localStorage.getItem("user");
        if (!storedUser) {
            throw new Error("No user found in localStorage");
        }

        user.value = JSON.parse(storedUser);
        
        // Load all required data
        await Promise.all([
            expenseReimbursementStore.getAllExpenseReimbursementHistory(user.value.usrId),
            projectStore.getAllProjects(),
            requisitionTypeStore.getAllRequisitionTypes()
        ]);

        // Store original data after all data is loaded
        originalData.value = [...expenseReimbursementStore.expenseReimbursementHistory] as ExpenseHistory[];
        
    } catch (error) {
        console.error("Error loading data:", error);
    } finally {
        loading.value = false;
    }
});
</script>

<template>
    <div class="content">
        <!-- Filter Component -->
        <Filter
            :loading="loading"
            :users="[]"
            v-model:searchRqName="filters.searchRqName"
            v-model:selectedProjectId="filters.selectedProjectId"
            v-model:selectedRequisitionTypeId="filters.selectedRequisitionTypeId"
            :projects="projectStore.projects"
            :requisitionTypes="requisitionTypeStore.requisitionTypes"
            :showSearchFilter="false"
            :showRqNameFilter="true"
            :showDepartmentFilter="false"
            :showDivisionFilter="false"
            :showRoleFilter="false"
            :showProjectFilter="true"
            :showRequisitionTypeFilter="true"
            :showDateFilter="true"
            @dateFilter="handleDateFilter"
            searchTerm=""
            selectedDepartment=""
            selectedDivision=""
            selectedRole=""
        />

        <div class="w-full border-r-[2px] border-l-[2px] border-t-[2px] mt-12">
            <!-- ตาราง -->
            <div>
                <Ctable :table="'Table9-head-New'" />
            </div>

            <!-- Table Content -->
            <table class="table-auto w-full text-center text-black">
                <tbody>
                    <!-- Loading State -->
                    <tr v-if="loading">
                        <td colspan="8" class="py-4">
                            <div class="flex justify-center items-center">
                                <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
                                <span class="ml-2">กำลังโหลดข้อมูล...</span>
                            </div>
                        </td>
                    </tr>

                    <!-- No Data State -->
                    <tr v-else-if="expenseReimbursementStore.expenseReimbursementHistory.length === 0">
                        <td colspan="8" class="py-4 text-center">ไม่พบข้อมูล</td>
                    </tr>

                    <!-- Data Rows -->
                    <tr v-else v-for="(item, index) in expenseReimbursementStore.expenseReimbursementHistory"
                        :key="item.rqId" 
                        class="text-[14px] border-b-2 border-[#BBBBBB]">
                        <th class="py-[12px] px-2 w-14">{{ index + 1 }}</th>
                        <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                            style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            :title="item.rqName">
                            {{ item.rqName }}
                        </th>
                        <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                            style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            :title="item.rqPjName">
                            {{ item.rqPjName }}
                        </th>
                        <th class="py-[12px] px-5 w-32 text-start font-[100]">
                            {{ item.rqRqtName }}
                        </th>
                        <th class="py-[12px] px-2 w-20 text-end">
                            {{ item.rqWithDrawDate }}
                        </th>
                        <th class="py-[12px] px-2 w-40 text-end">
                            {{ item.rqExpenses }}
                        </th>
                        <th class="py-[12px] px-2 w-32 text-center">
                            <span>
                                <StatusBudge :status="'sts-'+item.rqStatus" />
                            </span>
                        </th>
                        <th class="py-[10px] px-2 w-24 text-center">
                            <span @click="toDetails(item.rqId)" 
                                  class="flex justify-center cursor-pointer">
                                <Icon :icon="'viewDetails'" />
                            </span>
                        </th>
                    </tr>
                </tbody>
            </table>

            <!-- Table Footer -->
            <div>
                <Ctable :table="'Table9-footer'" />
            </div>
        </div>
    </div>
</template>