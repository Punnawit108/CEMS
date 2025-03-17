<script lang="ts" setup>
/**
 * ชื่อไฟล์: DynamicTable.vue
 * คำอธิบาย: ไฟล์นี้เป็น Component Code table
 * ชื่อผู้เขียน/แก้ไข: ธีรวัฒน์ นิระมล
 * วันที่จัดทำ/แก้ไข: 27 กุมภาพันธ์ 2568
 */
import { getCoreRowModel, useVueTable, FlexRender } from "@tanstack/vue-table";
import { computed, defineProps } from "vue";

interface TableColumn {
  id: string;
  header: () => string;
  cell: (info: any) => any;
}

interface TableStyles {
  [key: string]: string;
}

// Define Props
const props = defineProps<{
  columns: TableColumn[];
  tableData: any[];
  tableStyles: TableStyles;
}>();

const table = useVueTable({
  columns: props.columns,
  data: computed(() => props.tableData),
  getCoreRowModel: getCoreRowModel(),
});
</script>

<template>
  <div class="w-full h-fit items-start">
    <table
      class="table-auto w-full text-center text-black border border-[#BBBBBB]"
    >
      <thead class="bg-[#F2F4F8]">
        <tr
          v-for="headerGroup in table.getHeaderGroups()"
          :key="headerGroup.id"
          class="text-base border-b border-[#BBBBBB]"
        >
          <th
            v-for="header in headerGroup.headers"
            :key="header.id"
            :colspan="header.colSpan"
            :class="props.tableStyles[header.column.id] || ''"
            class="font-bold"
          >
            <FlexRender
              :render="header.column.columnDef.header"
              :props="header.getContext()"
            />
          </th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="row in table.getRowModel().rows"
          :key="row.id"
          class="text-base border-b border-[#BBBBBB]"
        >
          <td
            v-for="(cell, index) in row.getVisibleCells()"
            :key="cell.id"
            :class="props.tableStyles[cell.column.id] || ''"
          >
            <slot :name="'td-' + index">
              <FlexRender
                :render="cell.column.columnDef.cell"
                :props="cell.getContext()"
              />
            </slot>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
