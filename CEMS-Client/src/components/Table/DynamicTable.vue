<script setup lang="ts">
import { computed, defineProps, defineEmits, watch } from "vue";
import { useVueTable, getCoreRowModel, FlexRender } from "@tanstack/vue-table";
import Pagination from "../../components/Pagination.vue";

// Interfaces
interface TableColumn {
  id: string;
  header: () => string;
  cell: (info: any) => any;
}

interface TableStyles {
  [key: string]: string;
}

const props = defineProps<{
  columns: TableColumn[];
  tableData: any[];
  tableStyles: TableStyles;
  itemsPerPage: number;
  currentPage: number;
  loading: boolean; // ✅ Added loading prop
}>();

const emit = defineEmits(["update:currentPage"]);

const totalPages = computed(() => Math.ceil(props.tableData.length / props.itemsPerPage));

// ✅ Filtered list computed property (if needed)
const filteredList = computed(() => props.tableData ?? []);

// Paginated Data Computation with empty rows handling
const paginatedData = computed(() => {
  const start = (props.currentPage - 1) * props.itemsPerPage;
  const pageItems = filteredList.value.slice(start, start + props.itemsPerPage);

  // Fill the page with empty rows if needed
  while (pageItems.length < props.itemsPerPage) {
    pageItems.push(null);
  }

  return pageItems;
});

// Watch data changes to reset page to 1
watch(
  () => props.tableData,
  () => emit("update:currentPage", 1),
  { deep: true }
);

// Setup TanStack Table
const table = useVueTable({
  columns: props.columns,
  data: paginatedData,
  getCoreRowModel: getCoreRowModel(),
});
</script>

<template>
  <div class="w-full h-fit items-start">
    <table class="table-auto w-full text-center text-black border border-[#BBBBBB]">
      <thead class="bg-[#F2F4F8]">
        <tr v-for="headerGroup in table.getHeaderGroups()" :key="headerGroup.id"
          class="text-base border-b border-[#BBBBBB]">
          <th v-for="header in headerGroup.headers" :key="header.id" :colspan="header.colSpan"
            :class="tableStyles[header.column.id] || ''" class="font-bold py-2 px-3">
            <FlexRender :render="header.column.columnDef.header" :props="header.getContext()" />
          </th>
        </tr>
      </thead>
      <tbody>
        <!-- ✅ Added loading state -->
        <tr v-if="props.loading">
          <td :colspan="props.columns.length" class="py-4">
            <div class="flex justify-center items-center">
              <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-[#B67D12]"></div>
              <span class="ml-2">กำลังโหลดข้อมูล...</span>
            </div>
          </td>
        </tr>

        <!-- ✅ No data found message -->
        <tr v-else-if="filteredList.length === 0">
          <td :colspan="props.columns.length" class="py-4">
            ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา
          </td>
        </tr>

        <!-- ✅ Table rows -->
        <tr v-for="(row, index) in table.getRowModel().rows" :key="row.original?.id ?? `empty-${index}`"
          :class="row.original ? 'text-base border-b border-[#BBBBBB] hover:bg-gray-50' : ''">
          <template v-if="row.original">
            <td v-for="cell in row.getVisibleCells()" :key="cell.id" :class="props.tableStyles[cell.column.id] || ''"
              class="py-2 px-3">
              <slot :name="'cell-' + cell.column.id" v-bind="{ row: row.original }">
                <FlexRender :render="cell.column.columnDef.cell" :props="cell.getContext()" />
              </slot>
            </td>
          </template>
          <template v-else>
            <td :colspan="props.columns.length" class="py-[14px]">&nbsp;</td>
          </template>
        </tr>
      </tbody>
      <Pagination :current-page="props.currentPage" :total-pages="totalPages"
        @update:currentPage="emit('update:currentPage', $event)" />
    </table>
  </div>
</template>
