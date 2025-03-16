<script lang="ts" setup>
import { onMounted, computed } from "vue";
import { createColumnHelper } from "@tanstack/vue-table";
import { useProjectsStore } from "../../store/projectsReport";
import BaseTable from "../../components/Table/DynamicTable.vue";
import ProjectReport from "../../types/index";

const projectsStore = useProjectsStore();
const columnHelper = createColumnHelper<ProjectReport>();

// Define columns
const tableHeader = [
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

const tableStyles = {
    order: "py-3 px-2 w-14",
    pjName: "py-3 px-2 w-auto text-start",
    pjSumAmountExpenses: "py-3 px-2 w-60 text-end",
};

onMounted(async () => {
    await projectsStore.getAllProjects();
});

const check = (() => {
    alert('');
});

const tableData = computed(() => projectsStore.projects);
</script>

<template>
    <BaseTable :columns="tableHeader" :tableData="tableData" :tableStyles="tableStyles">
        <template #td-2>
            <div @click="check">check</div>
        </template>
    </BaseTable>
</template>
