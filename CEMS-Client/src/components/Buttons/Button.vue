<script setup lang="ts">
/*
 * ชื่อไฟล์: Button.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ปุ่มทั้งระบบ
 * ชื่อผู้เขียน/แก้ไข: พรชัย
 * วันที่จัดทำ/แก้ไข: 12 กุมภาพันธ์ 2568
 */
import { defineProps, onMounted, shallowRef } from "vue";
import ButtonApprove from "./ButtonApprove.vue";
import ButtonSubmit from "./ButtonSubmit.vue";
import ButtonCancleBorderGray from "./ButtonCancleBorderGray.vue";
import ButtonCancleGray from "./ButtonCancleGray.vue";
import ButtonEditProject from "./ButtonEditProject.vue";
import ButtonEditRequest from "./ButtonEditRequest.vue";
import ButtonEditSend from "./ButtonEditSend.vue";
import ButtonEditUser from "./ButtonEditUser.vue";
import ButtonExpense from "./ButtonExpense.vue";
import ButtonExpenseType from "./ButtonExpenseType.vue";
import ButtonExpenseTypeGray from "./ButtonExpenseTypeGray.vue";
import ButtonExpenseTypeGrayClick from "./ButtonExpenseTypeGrayClick.vue";
import ButtonPayment1 from "./ButtonPayment1.vue";
import ButtonPrint2 from "./ButtonPrint2.vue";
import ButtonPrivate from "./ButtonPrivate.vue";
import ButtonPublic1 from "./ButtonPublic1.vue";
import ButtonSave from "./ButtonSave.vue";
import ButtonSearch from "./ButtonSearch.vue";
import ButtonTransport from "./ButtonTransport.vue";
import ButtonTransportClick from "./ButtonTransportClick.vue";
import ButtonUnApprove from "./ButtonUnApprove.vue";


const currentComponent = shallowRef();

const props = defineProps({
  type: {
    type: String,
    required: true,
  },
});

onMounted(() => {
  switch (props.type) {
    case 'btn-approve':
      currentComponent.value = ButtonApprove;
      break;
    case 'btn-summit':
      currentComponent.value = ButtonSubmit;
      break;
    case 'btn-cancleBorderGray':
      currentComponent.value = ButtonCancleBorderGray;
      break;
    case 'btn-cancleGray':
      currentComponent.value = ButtonCancleGray;
      break;
    // ตั้งแต่ตรง Set แค่ Case ก่อน
    case 'btn-editProject':
      currentComponent.value = ButtonEditProject;
      break;
    case 'btn-editRequest':
      currentComponent.value = ButtonEditRequest;
      break;
    case 'btn-editSend':
      currentComponent.value = ButtonEditSend;
      break;
    case 'btn-editUser':
      currentComponent.value = ButtonEditUser;
      break;
    case 'btn-expense':
      currentComponent.value = ButtonExpense;
      break;
    case 'btn-expenseType':
      currentComponent.value = ButtonExpenseType;
      break;
    case 'btn-expenseTypeGray':
      currentComponent.value = ButtonExpenseTypeGray;
      break;
    case 'btn-expenseTypeGrayClick':
      currentComponent.value = ButtonExpenseTypeGrayClick;
      break;
    case 'btn-payment1':
      currentComponent.value = ButtonPayment1;
      break;
    case 'btn-print2':
      currentComponent.value = ButtonPrint2;
      break;
    case 'btn-private':
      currentComponent.value = ButtonPrivate;
      break;
    case 'btn-public1':
      currentComponent.value = ButtonPublic1;
      break;
    case 'btn-save':
      currentComponent.value = ButtonSave;
      break;
    case 'btn-search':
      currentComponent.value = ButtonSearch;
      break;
    case 'btn-transport':
      currentComponent.value = ButtonTransport;
      break;
    case 'btn-transportClick':
      currentComponent.value = ButtonTransportClick;
      break;
    case 'btn-unapprove':
      currentComponent.value = ButtonUnApprove;
      break;
  }
});
</script>

<template>
  <component :is="currentComponent"></component>
</template>
