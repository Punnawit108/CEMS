<script setup lang="ts">
import { defineProps, computed } from "vue";

const props = defineProps(["file"]);

const fileName = props.file.name ;
const fileType = props.file.type.split("/")[1] ;
console.log(fileType)
const fileIcon = computed(() => {

    switch (fileType) {
        case "pdf":
            return "/pdfIcon.svg";
        case "docs":
            return "/docIcon.svg";
        case "jpeg":
            return "/jpegIcon.svg";
        default:
            return "/defaultIcon.svg"; // ไอคอนเริ่มต้น
    }

});
</script>

<template>
    <div class="mt-4 bg-[#F7F7F7] border rounded-[5px] border-[#B8B8B8] w-full h-[66px] flex justify-between">
        <div class="flex flex-row ml-4 my-2">
            <div class="w-[50px] h-[50px] bg-white rounded-[5px]">
                <img :src="fileIcon" alt="File Icon" class="w-full h-full" />
            </div>
            <p class="ml-4 flex items-center">{{ fileName }}</p>
        </div>
        <div class="mr-5 flex items-center">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="17" viewBox="0 0 16 17" fill="none">
                <path
                    d="M1.78433 0.839844L8.00684 7.06235L14.2293 0.839844L16.0068 2.61734L9.78433 8.83984L16.0068 15.0623L14.2281 16.8398L8.00558 10.6173L1.78433 16.8398L0.00683594 15.0623L6.22934 8.83984L0.00683594 2.61734L1.78433 0.839844Z"
                    fill="black" />
            </svg>
        </div>
    </div>
</template>