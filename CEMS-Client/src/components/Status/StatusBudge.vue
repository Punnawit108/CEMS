<script setup lang="ts">
/*
* ชื่อไฟล์: StatusBudge.vue
* คำอธิบาย: ไฟล์นี้เป็นคอมโพเนนต์สำหรับแสดงตราสถานะ (Status Badge)
* ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/

import { defineProps, watchEffect, ref, defineAsyncComponent, type Component } from "vue"; // นำเข้า defineProps เพื่อจัดการพร็อพของคอมโพเนนต์

// กำหนดพร็อพที่คอมโพเนนต์นี้รับเข้ามา

const currentComponent = ref<Component | null>(null);

const props = defineProps<{
    status: string // รับค่า status ซึ่งใช้กำหนดสถานะที่จะถูกแสดง
}>();
const componentMap = new Map<string, Component>([
    ['sts-accept', defineAsyncComponent(() => import("./StatusAccept.vue"))],
    ['sts-waiting', defineAsyncComponent(() => import("./StatusWaiting.vue"))],
    ['sts-reject', defineAsyncComponent(() => import("./StatusReject.vue"))],
    ['sts-edit', defineAsyncComponent(() => import("./StatusEdit.vue"))],
    ['sts-sketch', defineAsyncComponent(() => import("./StatusSketch.vue"))],
]);

watchEffect(() => {
    currentComponent.value = componentMap.get(props.status) || null;
});
</script>

<template>
    <component :is="currentComponent"></component>
</template>