# Pagination.vue
<script setup lang="ts">
/**
* ชื่อไฟล์: Pagination.vue
* คำอธิบาย: Generic Pagination component สำหรับใช้งานกับข้อมูลทุกประเภท
* วันที่จัดทำ: 24 ธันวาคม 2567
*/
import { computed } from 'vue';

interface Props<T> {
    // รายการข้อมูลทั้งหมด
    items: T[];
    // จำนวนรายการต่อหน้า
    itemsPerPage: number;
    // หน้าปัจจุบัน
    currentPage: number;
    // จำนวนช่องว่างที่ต้องการแสดงเพิ่มเติม (optional)
    showEmptyRows?: boolean;
}

interface Emits {
    (e: 'update:currentPage', value: number): void;
    // Generic type T สำหรับ paginatedItems
    <T>(e: 'update:paginatedItems', value: T[]): void;
}

const props = defineProps<Props<any>>();
const emit = defineEmits<Emits>();

// คำนวณจำนวนหน้าทั้งหมด
const totalPages = computed(() => Math.ceil(props.items.length / props.itemsPerPage));

// คำนวณรายการที่จะแสดงในหน้าปัจจุบัน
const paginatedItems = computed(() => {
    const start = (props.currentPage - 1) * props.itemsPerPage;
    const end = start + props.itemsPerPage;
    const items = props.items.slice(start, end);
    emit('update:paginatedItems', items);
    return items;
});

// คำนวณจำนวนแถวที่เหลือที่ต้องเติมให้เต็มหน้า
const remainingRows = computed(() => {
    if (!props.showEmptyRows) return 0;
    const currentRows = paginatedItems.value.length;
    return currentRows < props.itemsPerPage ? props.itemsPerPage - currentRows : 0;
});

// ฟังก์ชันเปลี่ยนหน้า
const prevPage = () => {
    if (props.currentPage > 1) {
        emit('update:currentPage', props.currentPage - 1);
    }
};

const nextPage = () => {
    if (props.currentPage < totalPages.value) {
        emit('update:currentPage', props.currentPage + 1);
    }
};
</script>

<template>
    <tfoot class="border-t">
        <template v-if="showEmptyRows && remainingRows > 0">
            <tr v-for="index in remainingRows" :key="'empty-row' + index">
                <td v-for="i in 9" :key="'empty-cell' + i" class="py-[12px] px-2 h-[46px]">&nbsp;</td>
            </tr>
        </template>
        <tr class="text-[14px] border-b-2 border-[#BBBBBB]">
            <th colspan="7"></th>
            <th class="py-[5px] text-center">
                {{ currentPage }} of {{ totalPages }}
            </th>
            <th class="py-[5px] flex justify-between text-[14px] font-bold">
                <span class="text-[#A0A0A0]">
                    <button @click="prevPage"
                        class="p-2 rounded-md justify-start transition-colors duration-200 hover:bg-gray-100" :class="{
                            'text-[#BBBBBB] cursor-not-allowed hover:bg-transparent': currentPage === 1,
                            'text-black': currentPage !== 1
                        }" :disabled="currentPage === 1">
                        <span class="text-sm">&lt;</span>
                    </button>
                </span>
                <span class="text-[#A0A0A0]">
                    <button @click="nextPage" class="p-2 rounded-md transition-colors duration-200 hover:bg-gray-100"
                        :class="{
                            'text-[#BBBBBB] cursor-not-allowed hover:bg-transparent': currentPage === totalPages,
                            'text-black': currentPage !== totalPages
                        }" :disabled="currentPage === totalPages">
                        <span class="text-sm">&gt;</span>
                    </button>
                </span>
            </th>
        </tr>
    </tfoot>
</template>