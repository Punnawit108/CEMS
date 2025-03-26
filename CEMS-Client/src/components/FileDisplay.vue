<script setup lang="ts">
/*
 * ชื่อไฟล์: FileDisplay.vue
 * คำอธิบาย: ไฟล์นี้ใช้สำหรับแสดงข้อมูลไฟล์
 * ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
 * วันที่จัดทำ/แก้ไข: 18 มีนาคม 2568
 */
import { defineProps, computed, defineEmits } from "vue";
import { useRoute } from "vue-router";

const props = defineProps(["file", "fileName"]);
const emit = defineEmits(["remove", "preview"]);

const route = useRoute();
const showRemoveIcon = computed(() => route.path.includes("editExpenseForm"));

const fileIcon = computed(() => {
  let extension = "";
  if (typeof props.file === "string") {
    // ถ้า file เป็น string (URL)
    extension = props.file.split(".").pop()?.toLowerCase() || "";
  } else {
    // ถ้า file เป็น File object
    extension = props.file.name.split(".").pop()?.toLowerCase() || "";
  }

  switch (extension) {
    case "pdf":
      return "/pdfIcon.svg";
    case "docx":
      return "/docIcon.svg";
    case "jpeg":
      return "/jpegIcon.svg";
  }
});
</script>

<template>
  <div
    class="mt-4 bg-[#F7F7F7] border rounded-[5px] border-[#B8B8B8] w-full h-[66px] flex justify-between"
    @click="$emit('preview', file)"
  >
    <div class="flex flex-row ml-4 my-2">
      <div class="w-[50px] h-[50px] bg-white rounded-[5px]">
        <img :src="fileIcon" alt="File Icon" class="w-full h-full" />
      </div>
      <p class="ml-4 flex items-center">{{ fileName }}</p>
      <!-- แสดงชื่อไฟล์จริง -->
    </div>
    <div class="mr-5 flex items-center">
      <svg
        v-if="showRemoveIcon"
        xmlns="http://www.w3.org/2000/svg"
        width="16"
        height="17"
        viewBox="0 0 16 17"
        fill="none"
        @click="$emit('remove')"
      >
        <path
          d="M1.78433 0.839844L8.00684 7.06235L14.2293 0.839844L16.0068 2.61734L9.78433 8.83984L16.0068 15.0623L14.2281 16.8398L8.00558 10.6173L1.78433 16.8398L0.00683594 15.0623L6.22934 8.83984L0.00683594 2.61734L1.78433 0.839844Z"
          fill="black"
        />
      </svg>
    </div>
  </div>
</template>
