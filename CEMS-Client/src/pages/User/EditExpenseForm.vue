<script setup lang="ts">
import { ref, onMounted, watch } from "vue";
import { useRoute } from "vue-router";
import Button from "../../components/template/Button.vue";
import { useRequisitionStore } from "../../store/requisition";

const route = useRoute();
const requisitionStore = useRequisitionStore();
const id = String(route.params.id);
console.log("Requisition ID:", id); // เช็คว่า id ถูกต้องหรือไม่

const formData: any = ref({
  rqName: "",
  rqUsrId: "9999",
  rqPjId: "1",
  rqRqtId: "2", // กำหนดค่าเริ่มต้น
  rqVhId: "",
  rqDatePay: "",
  rqDateWithdraw: "",
  rqCode: "",
  rqInsteadEmail: "",
  rqExpenses: "",
  rqStartLocation: "",
  rqEndLocation: "",
  rqDistance: "",
  rqPurpose: "",
  rqReason: "",
  rqProof: "",
  rqStatus: "accept",
  rqProgress: "accepting",
  preview: null,
});
onMounted(async () => {
  try {
    await requisitionStore.getAllProject();
    await requisitionStore.getAllRequisitionType();
    await requisitionStore.getAllvehicleType();

    // ดึงข้อมูลค่าใช้จ่ายที่มีอยู่จาก API
    const expenseData = await requisitionStore.getExpenseById(id);

    formData.value = expenseData; // กำหนดค่าให้กับ formData

    console.log(formData.value);
    console.log(expenseData);
  } catch (error) {
    alert("เกิดข้อผิดพลาดในการดึงข้อมูล");
  }
});
const handleSubmit = async () => {
  await requisitionStore.updateExpense(id, formData.value);
};
</script>

<template>
  <form @submit.prevent="handleSubmit" class="text-black text-sm">
    <!-- btn -->
    <div class="flex justify-end gap-4">
      <Button :type="'btn-save'" @click="handleSave"></Button>
      <Button :type="'btn-cancleBorderGray'" @click="handleCancel"></Button>
      <Button :type="'btn-summit'"></Button>
    </div>
    <!-- ช่อง "ชื่อรายการเบิก" -->
    <div class="m-4">
      <label for="rqName" class="block text-sm font-medium py-1"
        >ชื่อรายการเบิก *</label
      >
      <input
        type="text"
        id="rqName"
        v-model="formData.rqName"
        class="px-3 py-2 border border-gray-400 bg-white rounded-md md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
      />
    </div>
  </form>
</template>
