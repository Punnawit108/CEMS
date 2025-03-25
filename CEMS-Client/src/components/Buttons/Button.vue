<script setup lang="ts">
/*
 * ชื่อไฟล์: Button.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ปุ่มทั้งระบบ
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
 import { ref, defineAsyncComponent, defineProps, onMounted, type Component } from "vue";

const currentComponent = ref<Component| null>(null);

const props = defineProps<{
  type: string;
}>();

const componentMap = new Map<string, Component>([
  ['btn-approve', defineAsyncComponent(() => import("./ButtonApprove.vue"))],
  ['btn-summit', defineAsyncComponent(() => import("./ButtonSubmit.vue"))],
  ['btn-cancleBorderGray', defineAsyncComponent(() => import("./ButtonCancleBorderGray.vue"))],
  ['btn-cancleGray', defineAsyncComponent(() => import("./ButtonCancleGray.vue"))],
  ['btn-editProject', defineAsyncComponent(() => import("./ButtonEditProject.vue"))],
  ['btn-editRequest', defineAsyncComponent(() => import("./ButtonEditRequest.vue"))],
  ['btn-editSend', defineAsyncComponent(() => import("./ButtonEditSend.vue"))],
  ['btn-editUser', defineAsyncComponent(() => import("./ButtonEditUser.vue"))],
  ['btn-expense', defineAsyncComponent(() => import("./ButtonExpense.vue"))],
  ['btn-expenseType', defineAsyncComponent(() => import("./ButtonExpenseType.vue"))],
  ['btn-expenseTypeGray', defineAsyncComponent(() => import("./ButtonExpenseTypeGray.vue"))],
  ['btn-expenseTypeGrayClick', defineAsyncComponent(() => import("./ButtonExpenseTypeGrayClick.vue"))],
  ['btn-payment1', defineAsyncComponent(() => import("./ButtonPayment1.vue"))],
  ['btn-print2', defineAsyncComponent(() => import("./ButtonPrint2.vue"))],
  ['btn-private', defineAsyncComponent(() => import("./ButtonPrivate.vue"))],
  ['btn-public1', defineAsyncComponent(() => import("./ButtonPublic1.vue"))],
  ['btn-save', defineAsyncComponent(() => import("./ButtonSave.vue"))],
  ['btn-search', defineAsyncComponent(() => import("./ButtonSearch.vue"))],
  ['btn-transport', defineAsyncComponent(() => import("./ButtonTransport.vue"))],
  ['btn-transportClick', defineAsyncComponent(() => import("./ButtonTransportClick.vue"))],
  ['btn-unapprove', defineAsyncComponent(() => import("./ButtonUnApprove.vue"))],
  ['btn-logout', defineAsyncComponent(() => import("./ButtonLogout.vue"))],
]);

onMounted(() => {
  currentComponent.value = componentMap.get(props.type) || null;
});
</script>

<template>
  <component :is="currentComponent"></component>
</template>
