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
const columnNumber = ref(6);
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
import UserSearchInput from '../../components/Filter/UserSearchInput.vue';
import ProjectFilter from '../../components/Filter/ProjectFilter.vue';
import RequisitionTypeFilter from '../../components/Filter/RequisitionTypeFilter.vue';
import DateFilter from '../../components/Filter/DateFilter.vue';
import FilterButtons from '../../components/Filter/FilterButtons.vue';



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

// ฟังก์ชันแปลงวันที่จากรูปแบบสตริง DD/MM/YYYY เป็น Date object
const parseDateString = (dateStr: string): Date | null => {
    if (!dateStr) return null;

    try {
        // รองรับรูปแบบ DD/MM/YYYY
        const parts = dateStr.split('/');
        if (parts.length === 3) {
            const day = parseInt(parts[0], 10);
            const month = parseInt(parts[1], 10) - 1; // เดือนใน JavaScript เริ่มที่ 0
            let year = parseInt(parts[2], 10);

            // ถ้าเป็นปีพุทธศักราช ให้แปลงเป็นคริสต์ศักราช
            if (year > 2500) {
                year = year - 543;
            }

            const date = new Date(year, month, day);
            date.setHours(0, 0, 0, 0);

            // ตรวจสอบว่าวันที่ถูกต้อง
            if (
                date.getDate() === day &&
                date.getMonth() === month &&
                date.getFullYear() === year
            ) {
                return date;
            }
        }
    } catch (e) {
        console.error("Error parsing date string:", dateStr, e);
    }

    return null;
};

// ฟังก์ชันเปรียบเทียบวันที่ ไม่ว่าจะอยู่ในรูปแบบใด
const compareDates = (date1: string | Date | null | undefined, date2: string | Date | null | undefined): number => {
    if (!date1 && !date2) return 0;
    if (!date1) return -1;
    if (!date2) return 1;

    // แปลงวันที่ให้เป็น Date objects
    let d1: Date | null = null;
    let d2: Date | null = null;

    if (date1 instanceof Date) {
        d1 = new Date(date1);
        d1.setHours(0, 0, 0, 0);
    } else if (typeof date1 === 'string') {
        // ตรวจสอบรูปแบบของสตริง
        if (date1.includes('/')) {
            // รูปแบบ DD/MM/YYYY
            d1 = parseDateString(date1);
        } else if (date1.includes('-')) {
            // รูปแบบ YYYY-MM-DD
            const parts = date1.split('-');
            let year = parseInt(parts[0], 10);
            const month = parseInt(parts[1], 10) - 1;
            const day = parseInt(parts[2], 10);

            // ตรวจสอบปีพุทธศักราช
            if (year > 2500) {
                year = year - 543;
            }

            d1 = new Date(year, month, day);
            d1.setHours(0, 0, 0, 0);
        }
    }

    if (date2 instanceof Date) {
        d2 = new Date(date2);
        d2.setHours(0, 0, 0, 0);
    } else if (typeof date2 === 'string') {
        // ตรวจสอบรูปแบบของสตริง
        if (date2.includes('/')) {
            // รูปแบบ DD/MM/YYYY
            d2 = parseDateString(date2);
        } else if (date2.includes('-')) {
            // รูปแบบ YYYY-MM-DD
            const parts = date2.split('-');
            let year = parseInt(parts[0], 10);
            const month = parseInt(parts[1], 10) - 1;
            const day = parseInt(parts[2], 10);

            // ตรวจสอบปีพุทธศักราช
            if (year > 2500) {
                year = year - 543;
            }

            d2 = new Date(year, month, day);
            d2.setHours(0, 0, 0, 0);
        }
    }

    // ถ้าไม่สามารถแปลงเป็น Date ได้
    if (!d1 && !d2) return 0;
    if (!d1) return -1;
    if (!d2) return 1;

    // เปรียบเทียบวันที่
    if (d1 < d2) return -1;
    if (d1 > d2) return 1;
    return 0;
};

// เพิ่มตัวแปรเก็บสถานะข้อผิดพลาด
const startDateError = ref(false);

// เพิ่มฟังก์ชันสำหรับจัดการการเปิด DatePicker ของวันที่สิ้นสุด
const handleEndDatePickerOpen = (isOpen: boolean) => {
    if (isOpen && !filters.value.startDate) {
        // ถ้าผู้ใช้พยายามเปิด datepicker ของวันที่สิ้นสุดโดยที่ยังไม่ได้เลือกวันที่เริ่มต้น
        startDateError.value = true; // แสดงข้อผิดพลาดที่วันที่เริ่มต้น
        isEndDatePickerOpen.value = false; // ไม่เปิด datepicker ของวันที่สิ้นสุด
    } else {
        isEndDatePickerOpen.value = isOpen; // เปิดหรือปิด datepicker ตามปกติ
    }
};

// แก้ไขฟังก์ชัน confirmStartDate เพื่อล้างข้อผิดพลาด
const confirmStartDate = (date: Date) => {
    filters.value.startDate = date;
    startDateError.value = false; // ล้างข้อผิดพลาดเมื่อเลือกวันที่เริ่มต้นแล้ว
    console.log('Start date confirmed (คริสต์ศักราช):', date);
    console.log('Start date confirmed (พุทธศักราช):', formatToBuddhistYYYYMMDD(date));
};

const filteredApprovals = computed(() => {
    if (!approvalList.value) return [];

    // Log การกรองข้อมูล
    console.log('Filtering approvals with filters:', JSON.stringify(lastSearchedFilters.value));

    if (lastSearchedFilters.value.startDate) {
        console.log('Start date for filtering:', lastSearchedFilters.value.startDate);
    }

    if (lastSearchedFilters.value.endDate) {
        console.log('End date for filtering:', lastSearchedFilters.value.endDate);
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

        // ตรวจสอบวันที่ด้วยการใช้ฟังก์ชัน compareDates
        let matchesStartDate = true;
        let matchesEndDate = true;

        if (lastSearchedFilters.value.startDate && item.rqWithdrawDate) {
            // เปรียบเทียบวันที่เริ่มต้นกับวันที่ในข้อมูล
            matchesStartDate = compareDates(item.rqWithdrawDate, lastSearchedFilters.value.startDate) >= 0;
            console.log(`Start date check: "${item.rqWithdrawDate}" >= "${lastSearchedFilters.value.startDate}" = ${matchesStartDate}`);
        }

        if (lastSearchedFilters.value.endDate && item.rqWithdrawDate) {
            // เปรียบเทียบวันที่สิ้นสุดกับวันที่ในข้อมูล
            matchesEndDate = compareDates(item.rqWithdrawDate, lastSearchedFilters.value.endDate) <= 0;
            console.log(`End date check: "${item.rqWithdrawDate}" <= "${lastSearchedFilters.value.endDate}" = ${matchesEndDate}`);
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
        <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-4">
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
                :confirmed-date="filters.startDate" @confirm="confirmStartDate" @cancel="cancelStartDate"
                :error="startDateError" />

            <!-- วันที่สิ้นสุดขอเบิก -->
            <div class="flex flex-col">
                <DateFilter v-model="endDateTemp" :loading="loading" label="วันที่สิ้นสุดขอเบิก"
                    :is-open="isEndDatePickerOpen" @update:is-open="handleEndDatePickerOpen"
                    :confirmed-date="filters.endDate" @confirm="confirmEndDate" @cancel="cancelEndDate"
                    :min-date="filters.startDate" class="mb-4" />

                <!-- ปุ่มค้นหาและรีเซ็ต -->
                <FilterButtons :loading="loading" @reset="handleReset" @search="handleSearch" />
            </div>
        </div>

        <!-- ตาราง -->
        <div class="w-full h-fit border-[2px] flex flex-col items-start border-[#BBBBBB]">
            <Ctable :table="'Table2-head'" />
            <table class="w-full text-center text-black table-auto">
                <tbody>
                    <tr v-if="loading">
                        <td colspan="100%" class="py-4">
                            <div class="flex justify-center items-center">
                                <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-[#B67D12]"></div>
                                <span class="ml-2">กำลังโหลดข้อมูล...</span>
                            </div>
                        </td>
                    </tr>
                    <tr v-else-if="!approvalList?.length || filteredApprovals.length === 0" v-for="n in 10" :key="n"
                        class="h-[50px]">
                        <td colspan="100%" class="py-4 text-center">
                            <span v-if="n === 5">
                                {{ !approvalList?.length ? 'ไม่มีข้อมูลรายการอนุมัติ' :
                                    'ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา' }}
                            </span>
                        </td>
                    </tr>

                    <tr v-else v-for="(item, index) in paginated" :key="item ? item.rqId : `empty-${index}`"
                        :class="item ? 'text-[14px] h-[46px] border-b-2 border-[#BBBBBB] hover:bg-gray-50' : 'h-[50px]'">
                        <template v-if="item">
                            <th class="py-3 px-2 w-12">{{ index + 1 + (currentPage - 1) * itemsPerPage }}
                            </th>
                            <th class="py-3 px-2 w-1/4 text-start "
                                style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                                :title="item.usrName">
                                {{ item.usrName }}
                            </th>
                            <th class="py-3 px-2 text-start w-44 "
                                style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                                :title="item.rqName">
                                {{ item.rqName }}
                            </th>
                            <th class="py-3 px-2 text-start w-44">{{ item.pjName }}</th>
                            <th class="py-3 px-2 text-start w-32">{{ item.rqtName }}</th>
                            <th class="py-3 px-2 text-start w-24">{{ item.rqWithdrawDate }}</th>
                            <th class="py-3 px-2 text-end w-36">
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