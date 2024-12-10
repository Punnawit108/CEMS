<script setup lang="ts">
/**
* ชื่อไฟล์: Filter.vue
* คำอธิบาย: Component สำหรับแสดงส่วนตัวกรองข้อมูลผู้ใช้และโครงการ
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 8 ธันวาคม 2567
*/

import { computed } from 'vue';
import { User, Project, ExpenseManage } from '../../types';

interface Props {
    loading: boolean;
    users: User[];
    searchTerm: string;
    selectedDepartment: string;
    selectedDivision: string;
    selectedRole: string;
    selectedProjectId: string;
    selectedRequisitionTypeId: string;
    projects: Project[];
    requisitionTypes: ExpenseManage[];
    showSearchFilter?: boolean;
    showDepartmentFilter?: boolean;
    showDivisionFilter?: boolean;
    showRoleFilter?: boolean;
    showProjectFilter?: boolean;
    showRequisitionTypeFilter?: boolean;
}

interface Emits {
    (e: 'update:searchTerm', value: string): void;
    (e: 'update:selectedDepartment', value: string): void;
    (e: 'update:selectedDivision', value: string): void;
    (e: 'update:selectedRole', value: string): void;
    (e: 'update:selectedProjectId', value: string): void;
    (e: 'update:selectedRequisitionTypeId', value: string): void;
}

const props = withDefaults(defineProps<Props>(), {
    projects: () => [],
    showSearchFilter: true,
    showDepartmentFilter: true,
    showDivisionFilter: true,
    showRoleFilter: true,
    showProjectFilter: true,
    showRequisitionTypeFilter: true,
});

const emit = defineEmits<Emits>();

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

const updateSearchTerm = (event: Event) => {
    const target = event.target as HTMLInputElement;
    emit('update:searchTerm', target.value);
};

const updateDepartment = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    emit('update:selectedDepartment', target.value);
};

const updateDivision = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    emit('update:selectedDivision', target.value);
};

const updateRole = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    emit('update:selectedRole', target.value);
};

const updateProject = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    emit('update:selectedProjectId', target.value);
};

const updateRequisitionType = (event: Event) => {
    const target = event.target as HTMLSelectElement;
    emit('update:selectedRequisitionTypeId', target.value);
};
</script>

<template>
    <div class="flex flex-wrap justify-start items-center gap-5 mb-12">
        <!-- Search Filter -->
        <div v-if="showSearchFilter" class="h-[32px] w-[266px]">
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
                    <input type="text" id="SearchBar" :value="searchTerm" @input="updateSearchTerm"
                        class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-9"
                        placeholder="รหัสพนักงาน, ชื่อ-นามสกุล" :disabled="loading" />
                </div>
            </form>
        </div>

        <!-- Department Filter -->
        <div v-if="showDepartmentFilter" class="h-[32px] w-[266px]">
            <form class="grid">
                <label for="SelectDepartment" class="py-0.5 text-[14px] text-black text-start">แผนก</label>
                <div class="relative h-[32px] w-[266px] justify-center items-center">
                    <select :value="selectedDepartment" @change="updateDepartment" id="SelectDepartment"
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
        <div v-if="showDivisionFilter" class="h-[32px] w-[266px]">
            <form class="grid">
                <label for="SelectDivision" class="py-0.5 text-[14px] text-black text-start">ฝ่าย</label>
                <div class="relative h-[32px] w-[266px] justify-center items-center">
                    <select :value="selectedDivision" @change="updateDivision" id="SelectDivision" :disabled="loading"
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
        <div v-if="showRoleFilter" class="h-[32px] w-[266px]">
            <form class="grid">
                <label for="SelectRole" class="py-0.5 text-[14px] text-black text-start">บทบาท</label>
                <div class="relative h-[32px] w-[266px] justify-center items-center">
                    <select :value="selectedRole" @change="updateRole" id="SelectRole" :disabled="loading"
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
        <div v-if="showProjectFilter" class="h-[32px] w-[266px]">
            <form class="grid">
                <label for="SelectProject" class="py-0.5 text-[14px] text-black text-start">โครงการ</label>
                <div class="relative h-[32px] w-[266px] justify-center items-center">
                    <select :value="selectedProjectId" @change="updateProject" id="SelectProject" :disabled="loading"
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
        <div v-if="showRequisitionTypeFilter" class="h-[32px] w-[266px]">
            <form class="grid">
                <label for="SelectRequisitionType" class="py-0.5 text-[14px] text-black text-start">
                    ประเภทค่าใช้จ่าย
                </label>
                <div class="relative h-[32px] w-[266px] justify-center items-center">
                    <select :value="selectedRequisitionTypeId" @change="updateRequisitionType"
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