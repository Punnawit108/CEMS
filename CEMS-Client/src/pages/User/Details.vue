<script setup lang="ts">
/*
* ชื่อไฟล์: Details.vue
* คำอธิบาย: ไฟล์นี้ใช้สำหรับแสดงรายละเอียดคำขอเบิก
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ, นายพงศธร บุญญามา
* วันที่จัดทำ/แก้ไข: 22 ตุลาคม 2567
*/
import { ref , computed , onMounted} from "vue";
import Progress from "../../components/template/Progress.vue";
import Button from "../../components/template/Button.vue";
import { useRoute } from "vue-router";
import { Expense } from '../../types';
import { useDetailStore } from "../../store/detail";

const statusDetail = ref("edit"); // 'reject' is the initial value
const route = useRoute();

const detailStore = useDetailStore();
const id = Number(route.params.id);

const expenseData = ref<any>(null);
const progressData = ref<any>(null);

onMounted(async () => {
  progressData.value = await detailStore.getApprover(id);
  expenseData.value = await detailStore.getRequisition(id);  
})

console.log(progressData)
// FN ตรวจสอบว่ามีคำว่า 'approval' และ list ใน path หรือไม่
const isApprovalPath = computed(() => {
  return route.path.includes('approval') && route.path.includes('list');
});

const isEditPath = computed(() => {
  
});

const colorStatus: { [key: string]: string } = {
  reject: "#E1032B",
  edit: "#FFBE40",
  accept: "#12B669",
  waiting: "#1976D2",
  sketch: "#B6B7BA",
  paying: "#1976D2",
  complete: "#12B669",
};

// แนบตรง disbursement เพิ่ม


</script>

<!-- path for test = /disbursement/listWithdraw/detailsExpenseForm/:id -->
<!-- path for test = /disbursement/historyWithdraw/detail/:id -->
<!-- path for test = /approval/history/detail/:id -->
<!-- path for test = /payment/List/detail/:id -->
<!-- path for test = /payment/history/detail/:id -->

<template>
  <!-- content -->
  <div v-if="expenseData" class="ml-[16px] ">

    <div v-if="expenseData.rqReason === 'edit'" class="border border-[#E00000] p-[15px] rounded-[10px] bg-[#FFECEC] mb-[5px]">
      <div class="flex justify-between">
        <p class="!text-[#ED0000] font-bold">เหตุผลส่งกลับ :</p>
        <p class="!text-[#FF0000]">วันที่ส่งกลับ : 11/09/2567</p>
      </div>
      <p class="!text-[#FF0000]">รูปหลักฐานไม่ชัดเจน</p>
    </div>

    <div v-if="expenseData.rqStatus === 'reject'" class="border border-[#E00000] p-[15px] rounded-[10px] bg-[#FFECEC] mb-[24px]">
      <p class="!text-[#ED0000] font-bold">เหตุผลการไม่อนุมัติ :</p>
      <p class="!text-[#FF0000]">{{expenseData.rqReason}}</p>
    </div>

    <div v-if="isApprovalPath" class="flex justify-end">
      <div class="flex mb-[22px]">
          <Button type="btn-unapprove" />
          <span class="mx-[12px]"></span>
          <Button type="btn-editSend" class="mx-[24px]" />
          <span class="mx-[12px]"></span>
          <Button type="btn-approve" />
        </div>
    </div>

    <div class="flex justify-between">
      <div class="left w-[80%]">
        <h3 class="text-base font-bold text-black">
          เบิกค่าใช้จ่าย<span :class="`bg-[${colorStatus[expenseData.rqStatus]}]`"
            class="!text-white px-7 py-[1px] rounded-[10px] text-xs font-thin ml-[15px]">{{expenseData.rqStatus}}</span>
        </h3>
        <div  class="row flex justify-around">
          <div class="col">
            <p class="head">รหัสรายการเบิก</p>
            <p class="item">{{expenseData.rqCode}}</p>
          </div>
          <div class="col">
            <p class="head">โครงการ</p>
            <p class="item">{{expenseData?.rqPjName}}</p>
          </div>
          <div class="col">
            <p class="head">วันที่เกิดค่าใช้จ่าย</p>
            <p class="item">{{expenseData?.rqDatePay}}</p>
          </div>
          <div class="col">
            <p class="head">วันที่ทำรายการเบิกค่าใช้จ่าย</p>
            <p class="item">{{expenseData?.rqDateWithdraw}}</p>
          </div>
        </div>
        
        <div class="row flex justify-around">
          <div class="col">
            <p class="head">ชื่อผู้เบิก</p>
            <p class="item">{{ expenseData?.rqUsrName }}</p>
          </div>
          <div class="col">
            <p class="head">ชื่อผู้เบิกแทน</p>
            <!-- ต้องปรับแก้ให้แสดงข้อมูลเป็น ชื่อแทน getuser -->
            <p class="item">{{ expenseData?.rqInsteadEmail }}</p> 
          </div>
        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">ประเภทค่าใช้จ่าย</p>
            <p class="item">{{expenseData?.rqRqtName}}</p>
          </div>
          <div class="col">
            <p class="head">วันที่อนุมัติ</p>
            <p class="item">13/09/2567</p>
          </div>
          <div class="col">
            <p class="head">จำนวนเงิน(บาท)</p>
            <p class="item">{{ expenseData?.rqExpenses }}</p>
          </div>
          <div class="col">

          </div>
        </div>

        <div class="travel row flex">
          <div class="col">
            <p class="head">ประเภทการเดินทาง</p>
            <!-- แก้ข้อมูลหลังบ้าน เพิ่ม rqVhType -->
            <p class="item">{{expenseData?.rqVhName}}</p>
          </div>
          <div class="col">
            <p class="head">ประเภทรถ</p>
            <p class="item">{{expenseData?.rqVhName}}</p>
          </div>
          <div class="col">
            <p class="head">ระยะทาง</p>
            <p class="item">{{ expenseData?.rqDistance }}</p>
          </div>
          <div class="col">
            <p class="head">อัตราค่าเดินทาง</p>
            <!-- แก้ข้อมูลหลังบ้าน เพิ่ม rqVhPayrate -->
            <p class="item">{{expenseData?.rqVhName}}</p>
          </div>
          
        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">สถานที่เริ่มต้น</p>
            <p class="item">{{expenseData?.rqStartLocation}}</p>
          </div>
          <div class="col">
            <p class="head">สถานที่สิ้นสุด</p>
            <p class="item">{{expenseData?.rqEndLocation}}</p>
          </div>
        </div>

        <div class="row">
          <p class="head">รายละเอียด</p>
          <p class="item">{{expenseData?.rqPurpose}}</p>
        </div>

        <div class="row flex">
          <div class="flex-1">
            <h3 class="mb-[16px] text-base font-bold text-black">รูปหลักฐาน</h3>
            <img src="/evidence.jpg" alt="" class="w-[50%] h-auto cursor-pointer" />
          </div>
          <div class="flex-1"></div>
        </div>
      </div>

      <div class="right">
        <!-- <div class="flex mb-[24px]">
          <Button type="btn-unapprove" />
          <span class="mx-[12px]"></span>
          <Button type="btn-editSend" class="mx-[24px]" />
          <span class="mx-[12px]"></span>
          <Button type="btn-approve" />
        </div> -->
        <div class="flex justify-end">
          <Progress v-if="progressData !== null" :progressInfo="progressData" :colorStatus="colorStatus" class="w-[100%]" />
        </div>
      </div>
    </div>
  </div>
  <!-- content -->
</template>
<style scoped>
p {
  font-size: 14px;
  color: black;
}

.row {
  margin: 16px 0;
}

.head {
  font-weight: 600;
  color: gray; 
}

.item {
  font-weight: bold; 
  color: black; 
}

.col {
  flex: 1;
}
</style>