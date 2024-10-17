<script setup lang="ts">
import { useRoute } from 'vue-router';
import { computed } from 'vue';
import Icon from './CIcon.vue';


// ใช้ route เพื่อดึงข้อมูลเส้นทางปัจจุบัน
const route = useRoute();

// คำนวณข้อความของ navbar ตามเส้นทางปัจจุบัน
let name_navbar: string = '';
const navbarTitle = computed(() => {
    name_navbar = route.name as string


    return route.meta.breadcrumb
});

</script>

<template>
    <!-- navbar -->
    <div class="w-full inline-flex justify-between items-center pt-6">
        <div class="ml-6 inline-flex items-center">
            <div>
                <Icon :icon="name_navbar" :size="32" />
            </div>
            <div class=" ml-4 ">
                <H1 class=" text-[24px] text-[#000000]">
                    {{ navbarTitle }}
                </H1>
                <!-- ถ้าเป็นหน้า expense จะแสดง -->
            </div>
        </div>
        <div class="mr-6 inline-flex h-9 ">
            <div class=" mr-6 items-end " v-if="route.name === 'listWithdraw'">
                <button class=" flex justify-center items-center px-4 py-1 bg-emerald-500 rounded-md border-2
            border-emerald-500 border-solid min-h-[30px]">
                    <img loading="lazy"
                        src="https://cdn.builder.io/api/v1/image/assets/TEMP/2d66bb937008d0de5fc7f3175afcb11d55f1df5aec556599f71c92ceef192be1?placeholderIfAbsent=true&apiKey=9804936447ee44d9b9e8514cc347c2f4"
                        class="object-contain shrink-0 self-stretch my-auto w-6 aspect-square" alt="Icon" />
                    <span class=" flex justify-center items-center gap-2.5 self-stretch pr-1.5 pl-2 text-white">
                        สร้างใบเบิกค่าใช้จ่าย
                    </span>
                </button>
            </div>
            <div class="inline-flex justify-center items-center">
                <Icon :icon="'profile'" :size="32" />
            </div>
        </div>
    </div>

</template>
