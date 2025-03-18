<script setup lang="ts">
/*
* ชื่อไฟล์: PaymentList.vue
* คำอธิบาย: ไฟล์นี้แสดงรายการรอนำจ่าย
* ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
* วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
*/
import { useRouter } from 'vue-router';
import Icon from '../../components/Icon/CIcon.vue';
import Ctable from '../../components/Table/CTable.vue';
import { usePayment } from '../../store/paymentStore';
import { onMounted, ref, computed, watch } from 'vue';
import Decimal from 'decimal.js';
import { storeToRefs } from 'pinia';
import Pagination from '../../components/Pagination.vue';

const currentPage = ref(1);
const itemsPerPage = ref(10);
const columnNumber = ref(6);
const totalPages = computed(() => {
    return Math.ceil(filteredPayments.value.length / itemsPerPage.value);
});

const paginated = computed(() => {
    const start = (currentPage.value - 1) * itemsPerPage.value;
    const end = start + itemsPerPage.value;
    const pageItems = filteredPayments.value.slice(start, end);

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

const paymentStore = usePayment();
const { expense } = storeToRefs(paymentStore);
const router = useRouter();
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

// ฟังก์ชันตรวจสอบว่าวันที่เป็นปีพุทธศักราชหรือไม่ (โดยการเช็คว่าปีมากกว่า 2400 หรือไม่)
const isBuddhistYear = (date: Date | string): boolean => {
    if (!date) return false;

    try {
        let year: number;

        if (date instanceof Date) {
            // ถ้าเป็นวัตถุ Date
            year = date.getFullYear();
        } else {
            // ถ้าเป็นสตริงในรูปแบบ YYYY-MM-DD
            year = parseInt(date.split('-')[0], 10);
        }

        // ถ้าปีมากกว่า 2500 มักจะเป็นปีพุทธศักราช (เนื่องจาก ค.ศ. 2000 = พ.ศ. 2543)
        return year > 2500;
    } catch (e) {
        console.error("Error parsing date:", e);
        return false;
    }
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

// ฟังก์ชันแปลงรูปแบบวันที่เป็นคริสต์ศักราชในรูปแบบ YYYY-MM-DD
const formatToChristianYYYYMMDD = (date: Date): string => {
    if (!date) return '';

    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');

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

const filteredPayments = computed(() => {
    if (!expense.value) return [];

    // Log การกรองข้อมูล
    console.log('Filtering payments with filters:', JSON.stringify(lastSearchedFilters.value));

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

    return expense.value.filter((item) => {
        // กรองตามชื่อผู้ใช้
        const matchesSearch = !lastSearchedFilters.value.searchQuery ||
            (item.rqUsrName && item.rqUsrName.toLowerCase().includes(lastSearchedFilters.value.searchQuery.toLowerCase()));

        // กรองตามโครงการ
        const matchesProject = !lastSearchedFilters.value.project ||
            (item.rqPjName && item.rqPjName === lastSearchedFilters.value.project);

        // กรองตามประเภทค่าใช้จ่าย
        const matchesRequisitionType = !lastSearchedFilters.value.requisitionType ||
            (item.rqRqtName && item.rqRqtName === lastSearchedFilters.value.requisitionType);

        // ตรวจสอบวันที่ด้วยการเปรียบเทียบสตริง YYYY-MM-DD แบบพุทธศักราช
        let matchesStartDate = true;
        let matchesEndDate = true;

        if (lastSearchedFilters.value.startDate && item.rqWithdrawDate) {
            // ตรวจสอบว่าวันที่ในข้อมูลเป็นรูปแบบ พ.ศ. หรือ ค.ศ.
            const isItemDateBuddhist = isBuddhistYear(item.rqWithdrawDate);

            // แปลงวันที่จาก DatePicker เป็นรูปแบบที่ตรงกับวันที่ในข้อมูล
            let startDateStr;
            if (isItemDateBuddhist) {
                // ถ้าวันที่ในข้อมูลเป็น พ.ศ. ให้แปลงเป็น พ.ศ.
                startDateStr = formatToBuddhistYYYYMMDD(lastSearchedFilters.value.startDate);
            } else {
                // ถ้าวันที่ในข้อมูลเป็น ค.ศ. ให้แปลงเป็น ค.ศ.
                startDateStr = formatToChristianYYYYMMDD(lastSearchedFilters.value.startDate);
            }

            // เปรียบเทียบวันที่
            if (item.rqWithdrawDate instanceof Date) {
                // ถ้า rqWithdrawDate เป็น Date object
                const compareDate = new Date(startDateStr);
                matchesStartDate = item.rqWithdrawDate >= compareDate;
            } else {
                // ถ้า rqWithdrawDate เป็น string
                matchesStartDate = String(item.rqWithdrawDate).localeCompare(startDateStr) >= 0;
            }

            // Debug
            console.log(`เปรียบเทียบ "${item.rqWithdrawDate}" (${isItemDateBuddhist ? 'พ.ศ.' : 'ค.ศ.'}) >= "${startDateStr}" = ${matchesStartDate}`);
        }

        if (lastSearchedFilters.value.endDate && item.rqWithdrawDate) {
            // ตรวจสอบว่าวันที่ในข้อมูลเป็นรูปแบบ พ.ศ. หรือ ค.ศ.
            const isItemDateBuddhist = isBuddhistYear(item.rqWithdrawDate);

            // แปลงวันที่จาก DatePicker เป็นรูปแบบที่ตรงกับวันที่ในข้อมูล
            let endDateStr;
            if (isItemDateBuddhist) {
                // ถ้าวันที่ในข้อมูลเป็น พ.ศ. ให้แปลงเป็น พ.ศ.
                endDateStr = formatToBuddhistYYYYMMDD(lastSearchedFilters.value.endDate);
            } else {
                // ถ้าวันที่ในข้อมูลเป็น ค.ศ. ให้แปลงเป็น ค.ศ.
                endDateStr = formatToChristianYYYYMMDD(lastSearchedFilters.value.endDate);
            }

            // เปรียบเทียบวันที่
            if (item.rqWithdrawDate instanceof Date) {
                // ถ้า rqWithdrawDate เป็น Date object
                const compareDate = new Date(endDateStr);
                matchesEndDate = item.rqWithdrawDate <= compareDate;
            } else {
                // ถ้า rqWithdrawDate เป็น string
                matchesEndDate = String(item.rqWithdrawDate).localeCompare(endDateStr) <= 0;
            }

            // Debug
            console.log(`เปรียบเทียบ "${item.rqWithdrawDate}" (${isItemDateBuddhist ? 'พ.ศ.' : 'ค.ศ.'}) <= "${endDateStr}" = ${matchesEndDate}`);
        }

        return matchesSearch && matchesProject && matchesRequisitionType && matchesStartDate && matchesEndDate;
    });
});

// สร้าง computed properties สำหรับดึงข้อมูลโครงการและประเภทการเบิกที่มีอยู่
const extractedProjects = computed(() => {
    if (!expense.value) return [];

    // ดึงชื่อโครงการที่ไม่ซ้ำกัน
    const uniqueProjects = new Map();

    expense.value.forEach(item => {
        if (item.rqPjName) {
            uniqueProjects.set(item.rqPjName, { pjId: item.rqPjName, pjName: item.rqPjName });
        }
    });

    return Array.from(uniqueProjects.values());
});

const extractedRequisitionTypes = computed(() => {
    if (!expense.value) return [];

    // ดึงประเภทการเบิกที่ไม่ซ้ำกัน
    const uniqueTypes = new Map();

    expense.value.forEach(item => {
        if (item.rqRqtName) {
            uniqueTypes.set(item.rqRqtName, { rqtId: item.rqRqtName, rqtName: item.rqRqtName });
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
    console.log('Start date converted to คริสต์ศักราช (YYYY-MM-DD):', formatToChristianYYYYMMDD(date));
    console.log('Start date converted to พุทธศักราช (YYYY-MM-DD):', formatToBuddhistYYYYMMDD(date));
};

const confirmEndDate = (date: Date) => {
    filters.value.endDate = date;
    console.log('End date confirmed (คริสต์ศักราช):', date);
    console.log('End date converted to คริสต์ศักราช (YYYY-MM-DD):', formatToChristianYYYYMMDD(date));
    console.log('End date converted to พุทธศักราช (YYYY-MM-DD):', formatToBuddhistYYYYMMDD(date));
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

// เมื่อ Component ถูก Mounted ให้ดึงข้อมูลรายการรอนำจ่าย
onMounted(async () => {
    loading.value = true;

    try {
        await paymentStore.getAllPaymentList();

        // อัปเดตข้อมูลสำหรับตัวกรอง
        projects.value = extractedProjects.value;
        requisitionTypes.value = extractedRequisitionTypes.value;
    } catch (error) {
        console.error("Error fetching payment list:", error);
    } finally {
        // รอสักครู่ก่อนปิด loading เพื่อให้มั่นใจว่า UI ได้ render แล้ว
        setTimeout(() => {
            loading.value = false;
        }, 500);
    }
});

const toDetails = (id: string) => {
    router.push(`/payment/list/detail/${id}`);
}
</script>
<!-- path for test = /payment/List -->
<template>
    <!-- content -->
    <div>
        <!-- Filter -->
        <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 mb-8">
            <!-- ค้นหา -->
            <UserSearchInput v-model="filters.searchQuery" :loading="loading" label="ค้นหา" />

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

        <!-- Table -->
        <div class="w-full border-t-[2px] border-r-[2px] border-l-[2px] mt-5 border-grayNormal">
            <!-- ตาราง -->
            <div>
                <Ctable :table="'Table7-head'" />
            </div>
            <div>
                <table class="w-full ">
                    <tbody>
                        <tr v-if="loading">
                            <td colspan="8" class="py-4 text-center">
                                <div class="flex justify-center items-center">
                                    <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-[#B67D12]"></div>
                                    <span class="ml-2">กำลังโหลดข้อมูล...</span>
                                </div>
                            </td>
                        </tr>

                        <tr v-else-if="!expense?.length">
                            <td colspan="8" class="py-4 text-center">ไม่มีข้อมูลรายการรอนำจ่าย</td>
                        </tr>

                        <tr v-else-if="filteredPayments.length === 0">
                            <td colspan="8" class="py-4 text-center">ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา</td>
                        </tr>

                        <tr v-else v-for="(paymentItem, index) in paginated"
                            :key="paymentItem ? paymentItem.rqId : `empty-${index}`"
                            :class="paymentItem ? 'text-[14px] text-black border-b-2 border-[#BBBBBB]' : ''">
                            <template v-if="paymentItem">
                                <th class="py-3 px-2 w-14 h-[46px]">{{ index + 1 + (currentPage - 1) * itemsPerPage }}
                                </th>
                                <th class="py-3 px-2 text-start truncate overflow-hidden"
                                    style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">
                                    {{ paymentItem.rqUsrName }}
                                </th>
                                <th class="py-3 px-3 w-44 text-start truncate overflow-hidden"
                                    style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">
                                    {{ paymentItem.rqName }}
                                </th>
                                <th class="py-3 px-2 w-44 text-start truncate overflow-hidden"
                                    style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">
                                    {{ paymentItem.rqPjName }}
                                </th>
                                <th class="py-3 px-5 w-44 text-start ">{{ paymentItem.rqRqtName }}</th>
                                <th class="py-3 px-2 w-32 text-start ">{{ paymentItem.rqWithdrawDate }}</th>
                                <th class="py-3 px-2 w-40 text-end ">{{ new Decimal(paymentItem.rqExpenses ??
                                    0).toNumber().toLocaleString("en-US", {
                                    minimumFractionDigits: 2,
                                    maximumFractionDigits: 2,
                                    }) }}</th>
                                <th class="py-3 px-2 w-20 text-center ">
                                    <span class="flex justify-center cursor-pointer hover:text-[#B67D12]"
                                        v-on:click="toDetails(paymentItem.rqId)">
                                        <Icon :icon="'viewDetails'" />
                                    </span>
                                </th>
                            </template>
                            <template v-else>
                                <td class="py-3">&nbsp;</td>
                            </template>
                        </tr>
                    </tbody>
                    <Pagination
                    :currentPage="currentPage"
                    :totalPages="totalPages"
                    :columnNumber="columnNumber"
                    @update:currentPage="(page) => (currentPage = page)"
                  />
                </table>
            </div>
        </div>
    </div>
    <!-- content -->
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
