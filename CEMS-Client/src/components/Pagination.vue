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
        <th></th>
        <th></th>
        <th></th>
        <th></th>
        <th></th>
        <th></th>
  
        <th class="py-[12px] text-end">
          {{ currentPage }} of {{ totalPages }}
        </th>
        <th class="py-[12px] flex justify-evenly text-[14px] font-bold">
          <span >
            <button
              @click="prevPage"
              :disabled="currentPage === 1"
              class="px-3 py-1 rounded"
            >
              <span class="text-sm">&lt;</span>
            </button>
          </span>
          <span class="mr-6 text-[#000000]">
            <button
              @click="nextPage"
              :disabled="currentPage === totalPages"
              class="px-3 py-1 rounded"
            >
              <span class="text-sm">&gt;</span>
            </button>
          </span>
        </th>
      </tr>
    </tfoot>
  </template>
  

  