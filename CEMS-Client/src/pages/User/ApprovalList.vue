<script setup lang="ts">
/*
* ชื่อไฟล์: ApprovalList
* คำอธิบาย: ไฟล์นี้แสดงหน้า รายการอนุมัติ
* ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/

import { useRouter } from 'vue-router';
import Icon from '../../components/Icon/CIcon.vue';
import Ctable from '../../components/Table/CTable.vue';
import { useApprovalStore } from '../../store/approvalList';
import { onMounted, ref, computed, watch } from 'vue';
import { Expense } from '../../types';
import Decimal from 'decimal.js';
import { storeToRefs } from 'pinia';
import Pagination from '../../components/Pagination.vue';

const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalPages = computed(() => {
    return Math.ceil(filteredApprovals.value.length / itemsPerPage.value);
});

const paginated = computed(() => {
    const start = (currentPage.value - 1) * itemsPerPage.value;
    const end = start + itemsPerPage.value;
    const pageItems = filteredApprovals.value.slice(start, end);

    // Add empty rows if fewer than 10 items
    while (pageItems.length < itemsPerPage.value) {
        pageItems.push(null);
    }

    return pageItems;
});

// Import filters
import UserSearchInput from '../../components/filters/UserSearchInput.vue';
import ProjectFilter from '../../components/Filters/ProjectFilter.vue';
import RequisitionTypeFilter from '../../components/Filters/RequisitionTypeFilter.vue';
import DateFilter from '../../components/Filters/DateFilter.vue';
import FilterButtons from '../../components/Filters/FilterButtons.vue';



// เรียกใช้ ApprovalStore
const approvalStore = useApprovalStore();
// ใช้ storeToRefs เพื่อดึงข้อมูลจาก store อย่างมีปฏิกิริยา
const { approvalList } = storeToRefs(approvalStore);

// เรียกใช้ Router
const router = useRouter();

// สร้างตัวแปร ref สำหรับเก็บข้อมูลผู้ใช้
const user = ref<any>(null);
const loading = ref(false);

// สำหรับข้อมูลโครงการและประเภทการเบิก
const projects = ref<any[]>([]);
const requisitionTypes = ref<any[]>([]);

// Filters
const filters = ref({
    searchQuery: '',
    project: '',
    requisitionType: '',
    startDate: undefined as Date | undefined,
    endDate: undefined as Date | undefined,
});

// Last searched filters (สำหรับการกรองข้อมูลจริงๆ)
const lastSearchedFilters = ref({
    searchQuery: '',
    project: '',
    requisitionType: '',
    startDate: undefined as Date | undefined,
    endDate: undefined as Date | undefined,
});

// Date picker state
const startDateTemp = ref(new Date());
const endDateTemp = ref(new Date());
const isStartDatePickerOpen = ref(false);
const isEndDatePickerOpen = ref(false);

// Reset pagination when filters change
watch(lastSearchedFilters, () => {
    // TODO: เพิ่ม pagination หากจำเป็น
}, { deep: true });

// ฟังก์ชันแปลงปี คริสต์ศักราช เป็น พุทธศักราช (บวก 543)
const toBuddhistYear = (date: Date): Date => {
    if (!date) return date;
    const newDate = new Date(date);
    newDate.setFullYear(newDate.getFullYear() + 543);
    return newDate;
};

// ฟังก์ชันแปลงรูปแบบวันที่เป็นพุทธศักราชในรูปแบบ YYYY-MM-DD
const formatToBuddhistYYYYMMDD = (date: Date): string => {
    if (!date) return '';

    const buddhistDate = toBuddhistYear(date);
    const year = buddhistDate.getFullYear();
    const month = (buddhistDate.getMonth() + 1).toString().padStart(2, '0');
    const day = buddhistDate.getDate().toString().padStart(2, '0');

    return `${year}-${month}-${day}`;
};

// ฟังก์ชันแปลงรูปแบบวันที่สำหรับการกรอง (สำหรับแสดงในคอนโซล)
const formatDateForDisplay = (date: Date): string => {
    if (!date) return '';
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();
    const buddhistYear = year + 543;
    return `${day}/${month}/${buddhistYear}`;
};

const filteredApprovals = computed(() => {
    if (!approvalList.value) return [];

    // Log การกรองข้อมูล
    console.log('Filtering approvals with filters:', JSON.stringify(lastSearchedFilters.value));

    if (lastSearchedFilters.value.startDate) {
        console.log('Start date for filtering (แบบคริสต์ศักราช):', lastSearchedFilters.value.startDate);
        console.log('Start date for filtering (แบบพุทธศักราช):', formatDateForDisplay(lastSearchedFilters.value.startDate));
        console.log('Start date formatted (YYYY-MM-DD พุทธศักราช):', formatToBuddhistYYYYMMDD(lastSearchedFilters.value.startDate));
    }

    if (lastSearchedFilters.value.endDate) {
        console.log('End date for filtering (แบบคริสต์ศักราช):', lastSearchedFilters.value.endDate);
        console.log('End date for filtering (แบบพุทธศักราช):', formatDateForDisplay(lastSearchedFilters.value.endDate));
        console.log('End date formatted (YYYY-MM-DD พุทธศักราช):', formatToBuddhistYYYYMMDD(lastSearchedFilters.value.endDate));
    }

    return approvalList.value.filter((item) => {
        // กรองตามชื่อผู้ใช้
        const matchesSearch = !lastSearchedFilters.value.searchQuery ||
            (item.usrName && item.usrName.toLowerCase().includes(lastSearchedFilters.value.searchQuery.toLowerCase()));

        // กรองตามโครงการ
        const matchesProject = !lastSearchedFilters.value.project ||
            (item.pjName && item.pjName === lastSearchedFilters.value.project);

        // กรองตามประเภทค่าใช้จ่าย
        const matchesRequisitionType = !lastSearchedFilters.value.requisitionType ||
            (item.rqtName && item.rqtName === lastSearchedFilters.value.requisitionType);

        // ตรวจสอบวันที่ด้วยการเปรียบเทียบสตริง YYYY-MM-DD แบบพุทธศักราช
        let matchesStartDate = true;
        let matchesEndDate = true;

        // สมมติว่ามีฟิลด์วันที่ชื่อ rqWithDrawDate (ปรับตามฟิลด์จริงในข้อมูล)
        if (lastSearchedFilters.value.startDate && item.rqWithdrawDate) {
            // แปลงวันที่จาก DatePicker (คริสต์ศักราช) เป็นรูปแบบ YYYY-MM-DD แบบพุทธศักราช
            const startDateStr = formatToBuddhistYYYYMMDD(lastSearchedFilters.value.startDate);

            // เปรียบเทียบกับวันที่ในฐานข้อมูล (ซึ่งเป็นพุทธศักราช)
            matchesStartDate = item.rqWithdrawDate >= startDateStr;

            // Debug
            console.log(`เปรียบเทียบ "${item.rqWithdrawDate}" >= "${startDateStr}" = ${matchesStartDate}`);
        }

        if (lastSearchedFilters.value.endDate && item.rqWithdrawDate) {
            // แปลงวันที่จาก DatePicker (คริสต์ศักราช) เป็นรูปแบบ YYYY-MM-DD แบบพุทธศักราช
            const endDateStr = formatToBuddhistYYYYMMDD(lastSearchedFilters.value.endDate);

            // เปรียบเทียบกับวันที่ในฐานข้อมูล (ซึ่งเป็นพุทธศักราช)
            matchesEndDate = item.rqWithdrawDate <= endDateStr;

            // Debug
            console.log(`เปรียบเทียบ "${item.rqWithdrawDate}" <= "${endDateStr}" = ${matchesEndDate}`);
        }

        return matchesSearch && matchesProject && matchesRequisitionType && matchesStartDate && matchesEndDate;
    });
});

// สร้าง computed properties สำหรับดึงข้อมูลโครงการและประเภทการเบิกที่มีอยู่
const extractedProjects = computed(() => {
    if (!approvalList.value) return [];

    // ดึงชื่อโครงการที่ไม่ซ้ำกัน
    const uniqueProjects = new Map();

    approvalList.value.forEach(item => {
        if (item.pjName) {
            // สมมติว่า id เป็นชื่อโครงการเพราะไม่มีข้อมูลชัดเจนเกี่ยวกับโครงสร้าง id โครงการ
            uniqueProjects.set(item.pjName, { pjId: item.pjName, pjName: item.pjName });
        }
    });

    return Array.from(uniqueProjects.values());
});

const extractedRequisitionTypes = computed(() => {
    if (!approvalList.value) return [];

    // ดึงประเภทการเบิกที่ไม่ซ้ำกัน
    const uniqueTypes = new Map();

    approvalList.value.forEach(item => {
        if (item.rqtName) {
            uniqueTypes.set(item.rqtName, { rqtId: item.rqtName, rqtName: item.rqtName });
        }
    });

    return Array.from(uniqueTypes.values());
});

// Filter handlers
const handleSearch = () => {
    lastSearchedFilters.value = {
        searchQuery: filters.value.searchQuery,
        project: filters.value.project,
        requisitionType: filters.value.requisitionType,
        startDate: filters.value.startDate,
        endDate: filters.value.endDate,
    };

    console.log('Search with filters:', JSON.stringify(lastSearchedFilters.value));
};

const handleReset = () => {
    // รีเซ็ตค่าปัจจุบัน
    filters.value = {
        searchQuery: '',
        project: '',
        requisitionType: '',
        startDate: undefined,
        endDate: undefined,
    };

    // รีเซ็ตค่าที่ใช้ในการค้นหาล่าสุด
    lastSearchedFilters.value = {
        searchQuery: '',
        project: '',
        requisitionType: '',
        startDate: undefined,
        endDate: undefined,
    };

    // รีเซ็ตวันที่
    startDateTemp.value = new Date();
    endDateTemp.value = new Date();
};

// Date handlers
const confirmStartDate = (date: Date) => {
    filters.value.startDate = date;
    console.log('Start date confirmed (คริสต์ศักราช):', date);
    console.log('Start date confirmed (พุทธศักราช):', formatToBuddhistYYYYMMDD(date));
};

const confirmEndDate = (date: Date) => {
    filters.value.endDate = date;
    console.log('End date confirmed (คริสต์ศักราช):', date);
    console.log('End date confirmed (พุทธศักราช):', formatToBuddhistYYYYMMDD(date));
};

const cancelStartDate = () => {
    if (!filters.value.startDate) {
        startDateTemp.value = new Date();
    } else {
        startDateTemp.value = filters.value.startDate;
    }
};

const cancelEndDate = () => {
    if (!filters.value.endDate) {
        endDateTemp.value = new Date();
    } else {
        endDateTemp.value = filters.value.endDate;
    }
};

// เมื่อ Component ถูก Mounted ให้ดึงข้อมูลประวัติการอนุมัติสำหรับผู้ใช้ที่ระบุ
onMounted(async () => {
    loading.value = true;

    try {
        const storedUser = localStorage.getItem("user"); // ดึงข้อมูลผู้ใช้จาก localStorage
        if (storedUser) {
            try {
                user.value = await JSON.parse(storedUser); // แปลงข้อมูลที่ได้จาก JSON String เป็น Object
            } catch (error) {
                console.log("Error loading user:", error); // ถ้าล้มเหลวแสดงข้อความ Error 
            }
        }
        if (user.value) {
            console.log(user.value.usrId);
            await approvalStore.getApprovalList(user.value.usrId); // เรียกใช้ฟังก์ชันดึงข้อมูลรายการอนุมัติ

            // อัปเดตข้อมูลสำหรับตัวกรอง
            projects.value = extractedProjects.value;
            requisitionTypes.value = extractedRequisitionTypes.value;
        }
    } catch (error) {
        console.error("Error fetching approval list:", error);
    } finally {
        // รอสักครู่ก่อนปิด loading เพื่อให้มั่นใจว่า UI ได้ render แล้ว
        setTimeout(() => {
            loading.value = false;
        }, 500);
    }
});

// ฟังก์ชันสำหรับเปลี่ยนเส้นทางไปยังหน้ารายละเอียดของรายการที่เลือก
const toDetails = async (data: Expense) => {
    router.push(`/approval/list/detail/${data.rqId}`); // นำไปที่ URL: /approval/list/detail/:rqId
};
</script>
<!-- path for test = /approval/list -->
<template>
    <!-- content -->
    <div>
        <!-- Filter -->
        <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-8">
            <!-- ค้นหาชื่อผู้ใช้ -->
            <UserSearchInput v-model="filters.searchQuery" :loading="loading" label="ค้นหาชื่อผู้ใช้" />

            <!-- โครงการ -->
            <ProjectFilter v-model="filters.project" :projects="projects" :loading="loading" />

            <!-- ประเภทค่าใช้จ่าย -->
            <RequisitionTypeFilter v-model="filters.requisitionType" :requisition-types="requisitionTypes"
                :loading="loading" />

            <!-- วันที่เริ่มต้นขอเบิก -->
            <DateFilter v-model="startDateTemp" :loading="loading" label="วันที่เริ่มต้นขอเบิก"
                :is-open="isStartDatePickerOpen" @update:is-open="isStartDatePickerOpen = $event"
                :confirmed-date="filters.startDate" @confirm="confirmStartDate" @cancel="cancelStartDate" />

            <!-- วันที่สิ้นสุดขอเบิก -->
            <div class="flex flex-col">
                <DateFilter v-model="endDateTemp" :loading="loading" label="วันที่สิ้นสุดขอเบิก"
                    :is-open="isEndDatePickerOpen" @update:is-open="isEndDatePickerOpen = $event"
                    :confirmed-date="filters.endDate" @confirm="confirmEndDate" @cancel="cancelEndDate" class="mb-2" />

                <!-- ปุ่มค้นหาและรีเซ็ต -->
                <FilterButtons :loading="loading" @reset="handleReset" @search="handleSearch" />
            </div>
        </div>

        <!-- ตาราง -->
        <div class="w-full border-r-[2px] border-l-[2px] border-t-[2px] mt-12 border-grayNormal">
            <Ctable :table="'Table2-head'" />
            <table class="table-auto w-full text-center text-black">
                <tbody>
                    <tr v-if="loading">
                        <td colspan="8" class="py-4">
                            <div class="flex justify-center items-center">
                                <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-[#B67D12]"></div>
                                <span class="ml-2">กำลังโหลดข้อมูล...</span>
                            </div>
                        </td>
                    </tr>

                    <tr v-else-if="!approvalList?.length">
                        <td colspan="8" class="py-4">ไม่มีข้อมูลรายการอนุมัติ</td>
                    </tr>

                    <tr v-else-if="filteredApprovals.length === 0">
                        <td colspan="8" class="py-4">ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา</td>
                    </tr>

                    <tr v-else v-for="(item, index) in paginated" :key="item ? item.rqId : `empty-${index}`"
                        :class="item ? 'text-[14px] border-b-2 border-[#BBBBBB] hover:bg-gray-50' : ''">
                        <template v-if="item">
                            <th class="py-3 px-2 w-14 h-[46px]">{{ index + 1 + (currentPage - 1) * itemsPerPage }}</th>
                            <th class="py-3 px-2 text-start truncate overflow-hidden"
                                style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                                :title="item.usrName">
                                {{ item.usrName }}
                            </th>
                            <th class="py-3 px-2 text-start w-44 truncate overflow-hidden"
                                style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                                :title="item.rqName">
                                {{ item.rqName }}
                            </th>
                            <th class="py-3 px-2 text-start w-44">{{ item.pjName }}</th>
                            <th class="py-3 px-5 text-start w-44">{{ item.rqtName }}</th>
                            <th class="py-3 px-2 text-start w-32">{{ item.rqWithdrawDate }}</th>
                            <th class="py-3 px-2 text-end w-40">
                                {{ new Decimal(item.rqExpenses ?? 0).toNumber().toLocaleString("en-US", {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2,
                                }) }}</th>
                            <th @click="toDetails(item)" class="py-3 px-2 text-center w-20 hover:text-[#B67D12]">
                                <span class="flex justify-center">
                                    <Icon :icon="'viewDetails'" />
                                </span>
                            </th>
                        </template>
                        <template v-else>
                            <td class="py-3">&nbsp;</td>
                        </template>
                    </tr>
                </tbody>
                <Pagination :currentPage="currentPage" :totalPages="totalPages"
                    @update:currentPage="(page) => (currentPage = page)" />
            </table>
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

/* Additional styles to ensure the dropdown arrow is hidden in WebKit browsers */
@media screen and (-webkit-min-device-pixel-ratio:0) {
    .custom-select {
        background-image: url("data:image/svg+xml;utf8,<svg fill='transparent' height='24' viewBox='0 0 24 24' width='24' xmlns='http://www.w3.org/2000/svg'><path d='M7 10l5 5 5-5z'/></svg>");
        background-repeat: no-repeat;
        background-position-x: 100%;
        background-position-y: 5px;
    }
}
</style>