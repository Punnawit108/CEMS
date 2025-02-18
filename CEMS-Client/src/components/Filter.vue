<script setup lang="ts">
/**
* ชื่อไฟล์: Filter.vue
* คำอธิบาย: Component สำหรับแสดงส่วนตัวกรองข้อมูลผู้ใช้และโครงการ
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 11 มกราคม 2568
*/

import { computed, ref, watch } from 'vue';
import { User, Project, ExpenseManage } from '../types';
import SingleDatePicker from '../components/SingleDatePicker.vue';

interface Props {
    loading: boolean;
    users: User[];
    searchTerm: string;
    searchRqName: string;
    selectedDepartment: string;
    selectedDivision: string;
    selectedRole: string;
    selectedProjectId: string | number | undefined;
    selectedRequisitionTypeId: string | number | undefined;
    projects: Project[];
    requisitionTypes: ExpenseManage[];
    showSearchFilter?: boolean;
    showRqNameFilter?: boolean;
    showDepartmentFilter?: boolean;
    showDivisionFilter?: boolean;
    showRoleFilter?: boolean;
    showProjectFilter?: boolean;
    showRequisitionTypeFilter?: boolean;
    showDateFilter?: boolean;
}

interface Emits {
    (e: 'update:searchTerm', value: string): void;
    (e: 'update:searchRqName', value: string): void;
    (e: 'update:selectedDepartment', value: string): void;
    (e: 'update:selectedDivision', value: string): void;
    (e: 'update:selectedRole', value: string): void;
    (e: 'update:selectedProjectId', value: string): void;
    (e: 'update:selectedRequisitionTypeId', value: string): void;
    (e: 'dateFilter', start: Date, end: Date): void;
}

const props = withDefaults(defineProps<Props>(), {
    projects: () => [],
    showSearchFilter: true,
    showDepartmentFilter: true,
    showDivisionFilter: true,
    showRoleFilter: true,
    showProjectFilter: true,
    showRequisitionTypeFilter: true,
    showDateFilter: true,
});

const emit = defineEmits<Emits>();

// Local state for filters
const localFilters = ref({
    searchTerm: props.searchTerm,
    searchRqName: props.searchRqName,
    selectedDepartment: props.selectedDepartment,
    selectedDivision: props.selectedDivision,
    selectedRole: props.selectedRole,
    selectedProjectId: props.selectedProjectId,
    selectedRequisitionTypeId: props.selectedRequisitionTypeId,
});

// Watch for props changes to update local state
watch(() => props.searchTerm, (newVal) => { localFilters.value.searchTerm = newVal });
watch(() => props.searchRqName, (newVal) => { localFilters.value.searchRqName = newVal });
watch(() => props.selectedDepartment, (newVal) => { localFilters.value.selectedDepartment = newVal });
watch(() => props.selectedDivision, (newVal) => { localFilters.value.selectedDivision = newVal });
watch(() => props.selectedRole, (newVal) => { localFilters.value.selectedRole = newVal });
watch(() => props.selectedProjectId, (newVal) => { localFilters.value.selectedProjectId = newVal });
watch(() => props.selectedRequisitionTypeId, (newVal) => { localFilters.value.selectedRequisitionTypeId = newVal });

// Filter computed properties
const departments = computed(() => {
    const depts = new Set(props.users.map(user => user.usrDptName));
    return Array.from(depts).sort();
});

const divisions = computed(() => {
    const divs = new Set(props.users.map(user => user.usrStName));
    return Array.from(divs).sort();
});

const roles = computed(() => {
    const roles = new Set(props.users.map(user => user.usrRolName));
    return Array.from(roles).sort();
});

// Date filter state
const startDate = ref(new Date());
const endDate = ref(new Date());
const tempStartDate = ref(new Date());
const tempEndDate = ref(new Date());

// Date picker visibility states
const startPickerOpen = ref(false);
const endPickerOpen = ref(false);

// Date handlers
const handleDateConfirm = (type: 'start' | 'end', confirmedDate: Date) => {
    if (type === 'start') {
        startDate.value = confirmedDate;
        startPickerOpen.value = false;
        endPickerOpen.value = false;
    } else {
        endDate.value = confirmedDate;
        endPickerOpen.value = false;
        startPickerOpen.value = false;
    }
};

const handleDateCancel = (type: 'start' | 'end') => {
    if (type === 'start') {
        tempStartDate.value = startDate.value;
        startPickerOpen.value = false;
    } else {
        tempEndDate.value = endDate.value;
        endPickerOpen.value = false;
    }
};

// Methods for handling search and reset
const handleSearch = () => {
    emit('update:searchTerm', localFilters.value.searchTerm);
    emit('update:searchRqName', localFilters.value.searchRqName);
    emit('update:selectedDepartment', localFilters.value.selectedDepartment);
    emit('update:selectedDivision', localFilters.value.selectedDivision);
    emit('update:selectedRole', localFilters.value.selectedRole);
    emit('update:selectedProjectId', localFilters.value.selectedProjectId?.toString() || '');
    emit('update:selectedRequisitionTypeId', localFilters.value.selectedRequisitionTypeId?.toString() || '');

    if (props.showDateFilter) {
        emit('dateFilter', startDate.value, endDate.value);
    }
};

const handleReset = () => {
    // Reset local state
    localFilters.value = {
        searchTerm: '',
        searchRqName: '',
        selectedDepartment: '',
        selectedDivision: '',
        selectedRole: '',
        selectedProjectId: '',
        selectedRequisitionTypeId: '',
    };

    // Reset dates if date filter is shown
    if (props.showDateFilter) {
        startDate.value = new Date();
        endDate.value = new Date();
        tempStartDate.value = new Date();
        tempEndDate.value = new Date();
    }

    // Emit reset values
    handleSearch();
};

// Update methods for local state
const updateSearchTerm = (event: Event) => {
    const target = event.target as HTMLInputElement;
    localFilters.value.searchTerm = target.value;
};

const updateSearchRqName = (event: Event) => {
    const target = event.target as HTMLInputElement;
    localFilters.value.searchRqName = target.value;
};

const updateDepartment = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    localFilters.value.selectedDepartment = target.value;
};

const updateDivision = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    localFilters.value.selectedDivision = target.value;
};

const updateRole = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    localFilters.value.selectedRole = target.value;
};

const updateProject = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    localFilters.value.selectedProjectId = target.value;
};

const updateRequisitionType = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    localFilters.value.selectedRequisitionTypeId = target.value;
};
</script>

<template>
    <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-12 w-full [&>*:last-child]:xl:col-start-5">
        <!-- Search Filter -->
        <div v-if="showSearchFilter" class="h-[32px] min-w-[200px] flex-1">
            <form class="grid" @submit.prevent>
                <label for="SearchBar" class="py-0.5 text-[14px] text-black text-start">ค้นหา</label>
                <div class="relative h-[32px] w-full justify-center items-center">
                    <div class="absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                        <svg width="19" height="20" viewBox="0 0 19 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z"
                                stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                        </svg>
                    </div>
                    <input type="text" id="SearchBar" :value="localFilters.searchTerm" @input="updateSearchTerm"
                        class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-9"
                        placeholder="รหัสพนักงาน, ชื่อ-นามสกุล" :disabled="loading" />
                </div>
            </form>
        </div>

        <!-- RqName Search Filter -->
        <div v-if="showRqNameFilter" class="h-[32px] min-w-[200px] flex-1">
            <form class="grid" @submit.prevent>
                <label for="SearchRqName" class="py-0.5 text-[14px] text-black text-start">ค้นหารายการเบิก</label>
                <div class="relative h-[32px] w-full justify-center items-center">
                    <div class="absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                        <svg width="19" height="20" viewBox="0 0 19 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z"
                                stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                        </svg>
                    </div>
                    <input type="text" id="SearchRqName" :value="localFilters.searchRqName" @input="updateSearchRqName"
                        class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-9"
                        placeholder="ชื่อรายการเบิก" :disabled="loading" />
                </div>
            </form>
        </div>

        <!-- Department Filter -->
        <div v-if="showDepartmentFilter" class="h-[32px] min-w-[200px] flex-1">
            <form class="grid">
                <label for="SelectDepartment" class="py-0.5 text-[14px] text-black text-start">แผนก</label>
                <div class="relative h-[32px] w-full justify-center items-center">
                    <select :value="localFilters.selectedDepartment" @change="updateDepartment" id="SelectDepartment"
                        :disabled="loading"
                        class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8">
                        <option value="">ทั้งหมด</option>
                        <option v-for="dept in departments" :key="dept" :value="dept">{{ dept }}</option>
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

        <!-- Division Filter -->
        <div v-if="showDivisionFilter" class="h-[32px] min-w-[200px] flex-1">
            <form class="grid">
                <label for="SelectDivision" class="py-0.5 text-[14px] text-black text-start">ฝ่าย</label>
                <div class="relative h-[32px] w-full justify-center items-center">
                    <select :value="localFilters.selectedDivision" @change="updateDivision" id="SelectDivision"
                        :disabled="loading"
                        class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8">
                        <option value="">ทั้งหมด</option>
                        <option v-for="div in divisions" :key="div" :value="div">{{ div }}</option>
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
        <div v-if="showRoleFilter" class="h-[32px] min-w-[200px] flex-1">
            <form class="grid">
                <label for="SelectRole" class="py-0.5 text-[14px] text-black text-start">บทบาท</label>
                <div class="relative h-[32px] w-full justify-center items-center">
                    <select :value="localFilters.selectedRole" @change="updateRole" id="SelectRole" :disabled="loading"
                        class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8">
                        <option value="">ทั้งหมด</option>
                        <option v-for="role in roles" :key="role" :value="role">{{ role }}</option>
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

        <!-- Project Filter -->
        <div v-if="showProjectFilter" class="h-[32px] min-w-[200px] flex-1">
            <form class="grid">
                <label for="SelectProject" class="py-0.5 text-[14px] text-black text-start">โครงการ</label>
                <div class="relative h-[32px] w-full justify-center items-center">
                    <select :value="localFilters.selectedProjectId" @change="updateProject" id="SelectProject"
                        :disabled="loading"
                        class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8">
                        <option value="">ทั้งหมด</option>
                        <option v-for="project in projects" :key="project.pjId" :value="project.pjId">
                            {{ project.pjName }}
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

        <!-- Requisition Type Filter -->
        <div v-if="showRequisitionTypeFilter" class="h-[32px] min-w-[200px] flex-1">
            <form class="grid">
                <label for="SelectRequisitionType"
                    class="py-0.5 text-[14px] text-black text-start">ประเภทค่าใช้จ่าย</label>
                <div class="relative h-[32px] w-full justify-center items-center">
                    <select :value="localFilters.selectedRequisitionTypeId" @change="updateRequisitionType"
                        id="SelectRequisitionType" :disabled="loading"
                        class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8">
                        <option value="">ทั้งหมด</option>
                        <option v-for="type in requisitionTypes" :key="type.rqtId" :value="type.rqtId">
                            {{ type.rqtName }}
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

        <!-- Date Filter -->
        <template v-if="showDateFilter">
            <!-- Start Date -->
            <div class="h-[32px] min-w-[200px] flex-1">
                <form class="grid">
                    <label class="py-0.5 text-[14px] text-black text-start">วันที่เริ่มต้น</label>
                    <div class="relative h-[32px] w-full date-picker-container">
                        <SingleDatePicker v-model="tempStartDate" placeholder="mm/dd/yyyy" :disabled="loading"
                            class="w-full" @confirm="handleDateConfirm('start', $event)"
                            @cancel="handleDateCancel('start')" :confirmedDate="startDate"
                            v-model:isOpen="startPickerOpen" />
                    </div>
                </form>
            </div>

            <!-- End Date -->
            <div class="h-[32px] min-w-[200px] flex-1">
                <form class="grid">
                    <label class="py-0.5 text-[14px] text-black text-start">วันที่สิ้นสุด</label>
                    <div class="relative h-[32px] w-full date-picker-container">
                        <SingleDatePicker v-model="tempEndDate" placeholder="mm/dd/yyyy" :disabled="loading"
                            class="w-full" @confirm="handleDateConfirm('end', $event)" @cancel="handleDateCancel('end')"
                            :confirmedDate="endDate" v-model:isOpen="endPickerOpen" />
                    </div>
                </form>
            </div>
        </template>

        <!-- Search and Clear Buttons -->
        <div class="h-[32px]">
            <div class="py-0.5 text-[14px] text-transparent">การดำเนินการ</div>
            <div class="grid grid-cols-2 gap-4">
                <button type="button"
                    class="bg-transparent hover:bg-gray-50 text-[#B67D12] rounded-md text-sm border border-[#B67D12] transition-colors h-[32px]"
                    :disabled="loading" @click="handleReset">
                    ล้าง
                </button>
                <button type="button"
                    class="bg-[#B67D12] hover:bg-[#9e6c0f] text-white rounded-md text-sm transition-colors h-[32px]"
                    :disabled="loading" @click="handleSearch">
                    ค้นหา
                </button>
            </div>
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

/* DatePicker Container Styles */
.date-picker-container :deep(.dp__input) {
    width: 100%;
    height: 32px;
    padding: 0 8px;
    border: 1px solid black;
    border-radius: 6px;
    background-color: white;
}

.date-picker-container :deep(.dp__input:focus) {
    outline: none;
    border-color: #3b82f6;
}

.date-picker-container :deep(.dp__input_icon) {
    right: 8px;
}
</style>