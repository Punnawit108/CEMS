<script setup lang="ts">
/*
 * ชื่อไฟล์: Pagination.vue
 * คำอธิบาย: ไฟล์นี้ Component Pagination
 * ชื่อผู้เขียน/แก้ไข: นายธีรวัฒ์ นิระมล
 * วันที่จัดทำ/แก้ไข: 21 มีนาคม 2568
 */
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
  <tfoot class="border-t-2 border-solid border-[#B6B7BA]">
    <tr class="text-sm ">
      <td colspan="100%">
        <div class="flex justify-end items-center h-8">
          <span class="text-gray-600 mr-10">
            {{ currentPage }} of {{ totalPages }}
          </span>
          <button @click="prevPage" :disabled="currentPage === 1"
            class="rounded-full mr-10 flex items-center justify-center">
            <svg width="30" height="30" viewBox="0 0 30 30" fill="none" xmlns="http://www.w3.org/2000/svg"
              :class="currentPage === 1 ? 'fill-[#BBBBBB]' : 'fill-grayDark'">
              <path
                d="M19.4219 11.9062L14.8281 16.5L19.4219 21.0938L18.0156 22.5L12.0156 16.5L18.0156 10.5L19.4219 11.9062Z" />
            </svg>
          </button>
          <button @click="nextPage" :disabled="currentPage === totalPages"
            class="rounded-full flex items-center justify-center">
            <svg width="30" height="30" viewBox="0 0 30 30" fill="none" xmlns="http://www.w3.org/2000/svg"
              :class="currentPage === totalPages ? 'fill-[#BBBBBB]' : ' fill-grayDark'">
              <path
                d="M13.9844 10.5L19.9844 16.5L13.9844 22.5L12.5781 21.0938L17.1719 16.5L12.5781 11.9062L13.9844 10.5Z" />
            </svg>
          </button>
        </div>
      </td>
    </tr>
  </tfoot>
</template>
