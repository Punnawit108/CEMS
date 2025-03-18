<!-- Pagination.vue -->
<script setup lang="ts">
// Props
const props = defineProps({
  currentPage: {
    type: Number,
    required: true,
  },
  totalPages: {
    type: Number,
    required: true,
  },
  columnNumber: {
    type: Number,
    required: true,
  },
});

// Emits
const emit = defineEmits(['update:currentPage']);

// Methods
const nextPage = () => {
  if (props.currentPage < props.totalPages) {
    emit('update:currentPage', props.currentPage + 1);
  }
};

const prevPage = () => {
  if (props.currentPage > 1) {
    emit('update:currentPage', props.currentPage - 1);
  }
};
</script>

<template>
  <tfoot class="border-t">
    <tr class="text-[14px] border-b-2 border-[#BBBBBB]">
      <!-- สร้าง <th> ตามจำนวน columnNumber -->
      <th v-for="index in columnNumber" :key="index"></th>
      <th class="py-[12px] text-end">
        {{ currentPage }} of {{ totalPages }}
      </th>
      <th class="py-[12px] flex justify-evenly text-[14px] font-bold">
        <span>
          <button @click="prevPage" :disabled="currentPage === 1" class="px-3 py-1 rounded">
            <span class="text-sm">&lt;</span>
          </button>
        </span>
        <span>
          <button @click="nextPage" :disabled="currentPage === totalPages" class="px-3 py-1 rounded">
            <span class="text-sm">&gt;</span>
          </button>
        </span>
      </th>
    </tr>
  </tfoot>
</template>

<style scoped>
button:disabled {
  color: #c7c8cc; 
  cursor: not-allowed; 
}

button:not(:disabled) {
  color: #000000; 
}
</style>