<script setup lang="ts">
/*
* ชื่อไฟล์: StatusBudge.vue
* คำอธิบาย: ไฟล์นี้เป็นคอมโพเนนต์สำหรับแสดงตราสถานะ (Status Badge)
* ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/

import { defineProps, onMounted, shallowRef } from "vue"; // นำเข้า defineProps เพื่อจัดการพร็อพของคอมโพเนนต์
import StatusAccept from "./StatusAccept.vue";
import StatusWaiting from "./StatusWaiting.vue";
import StatusReject from "./StatusReject.vue";
import StatusEdit from "./StatusEdit.vue";
import StatusSketch from "./StatusSketch.vue";


// กำหนดพร็อพที่คอมโพเนนต์นี้รับเข้ามา

const currentComponent = shallowRef();

const props = defineProps<{
    status: String // รับค่า status ซึ่งใช้กำหนดสถานะที่จะถูกแสดง
}>();

onMounted (() => {
    switch(props.status){
    //อนุมัติ
        case "sts-accept" :currentComponent.value =StatusAccept;
        break;
    //รออนุมัติ
        case "sts-waiting" :currentComponent.value =StatusWaiting;
        break;
    //ไม่อนุมัติ
        case "sts-reject" :currentComponent.value =StatusReject;
        break;
    //แก้ไข
        case "sts-edit" :currentComponent.value =StatusEdit;
        break;
    //แก้ไข
        case "sts-sketch" :currentComponent.value =StatusSketch;
        break;
    }
}) 
</script>

<template>
    <div class="flex flex-col items-center text-center ">

        <!-- content -->
        <div
            class="flex gap-4 justify-center items-center px-2 text-xs leading-snug text-center text-white whitespace-nowrap bg-emerald-500 rounded-3xl">
            <component :is="currentComponent"></component>
        </div>
        <!-- content -->
    </div>
</template>