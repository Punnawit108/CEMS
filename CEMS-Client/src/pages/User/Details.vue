<script setup lang="ts">
/**
 * ชื่อไฟล์ : Details.vue
 * คำอธิบาย : ไฟล์นี้ใช้สำหรับแสดงรายละเอียดคำขอเบิก
 * Input : -
 * Output : แสดงรายละเอียดคำขอเบิก
 * ชื่อผู้เขียน/แก้ไข : นายพรชัย เพิ่มพูลกิจ, นายพงศธร บุญญามา
 * วันที่จัดทำแก้ไข : 22 ตุลาคม 2567
 */
import { ref } from "vue";
import Progress from "../../components/template/Progress.vue";
import Button from "../../components/template/Button.vue";
import { useRoute } from "vue-router";

const statusDetail = ref("edit"); // 'reject' is the initial value

const route = useRoute();

// ตรวจสอบว่ามีคำว่า 'approval' ใน path หรือไม่
const isApprovalPath = computed(() => {
  return route.path.includes('approval');
});

const colorStatus: { [key: string]: string } = {
  reject: "#E1032B",
  edit: "#FFBE40",
  accept: "#12B669",
  waiting: "#1976D2",
  sketch: "#B6B7BA",
};


const requestInfo = [
  {
    rqId: 1001,
    rqUsrName: "Pongsatorn Boonyama",
    rqPjName: "งานเลี้ยง",
    rqRqtName: "ค่าเดินทาง",
    rqVhName: "รถยนต์ส่วนตัว",
    rqDatePay: "2024-11-01",
    rqDateWithdraw: "2024-11-02",
    rqCode: "CN-1001",
    rqInsteadEmail: "",
    rqExpenses: 100,
    rqLocation: "หอประชุมปิยพัฒน์",
    rqStartLocation: "Bangkok",
    rqEndLocation: "Nonthaburi",
    rqDistance: "15 KM",
    rqPurpose: "เดินทาง",
    rqReason: null,
    rqProof: "เพื่อเดินทางไปงานเลี้ยงรุ่นน้องของบริษัท",
    rqStatus: "waiting",
    rqProgress: "accepting",
  },
];



const progressInfo = {
  disbursement: {
    status: "accept",
    datetime: "10/02/67 10:52",
  },
  acceptor: [
    {
      name: "นายพรชัย เพิ่มพูลกิจ",
      status: "accept",
      datetime: "10/02/67 10:52",
    },
    {
      name: "นายจักรวาล ร่วมนิคม",
      status: "waiting",
      datetime: null,
    },
    {
      name: "นายพงศธร บุญญามา",
      status: "edit",
      datetime: "10/02/67 10:52",
    },
    {
      name: "นายจักวรรดิ หงวนเจริญ",
      status: "reject",
      datetime: "10/02/67 10:52",
    },
  ],
};


</script>

<!-- path for test = /disbursement/listWithdraw/detailsExpenseForm/:id -->
<!-- path for test = /disbursement/historyWithdraw/detail/:id -->
<!-- path for test = /approval/history/detail/:id -->
<!-- path for test = /payment/List/detail/:id -->
<!-- path for test = /payment/history/detail/:id -->

<template>
  <!-- content -->
  <div class="ml-[16px]">
    <div class="border border-[#E00000] p-[15px] rounded-[10px] bg-[#FFECEC] mb-[5px]">
      <div class="flex justify-between">
        <p class="!text-[#ED0000] font-bold">เหตุผลส่งกลับ :</p>
        <p class="!text-[#FF0000]">วันที่ส่งกลับ : 11/09/2567</p>
      </div>
      <p class="!text-[#FF0000]">รูปหลักฐานไม่ชัดเจน</p>
    </div>

    <div class="border border-[#E00000] p-[15px] rounded-[10px] bg-[#FFECEC] mb-[24px]">
      <p class="!text-[#ED0000] font-bold">เหตุผลการไม่อนุมัติ :</p>
      <p class="!text-[#FF0000]">รูปหลักฐานไม่ชัดเจน</p>
    </div>

    <div class="flex justify-end">
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
          เบิกค่าใช้จ่าย<span :class="`bg-[${colorStatus[statusDetail]}]`"
            class="!text-white px-7 py-[1px] rounded-[10px] text-xs font-thin ml-[15px]">แก้ไข</span>
        </h3>
        <div class="row flex justify-around">
          <div class="col">
            <p class="head">รหัสรายการเบิก</p>
            <p class="item">CN-1998</p>
          </div>
          <div class="col">
            <p class="head">โครงการ</p>
            <p class="item">อบรมการบริหาร</p>
          </div>
          <div class="col">
            <p class="head">วันที่เกิดค่าใช้จ่าย</p>
            <p class="item">13/09/2567</p>
          </div>
          <div class="col">
            <p class="head">วันที่ทำรายการเบิกค่าใช้จ่าย</p>
            <p class="item">13/09/2567</p>
          </div>
        </div>
        
        <div class="row flex justify-around">
          <div class="col">
            <p class="head">ชื่อผู้เบิก</p>
            <p class="item">นางสาวอลิสา ปะกังพลัง</p>
          </div>
          <div class="col">
            <p class="head">ชื่อผู้เบิกแทน</p>
            <p class="item">นายปุณณวิชน์ วิเชียร์มาร์น</p>
          </div>
        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">ประเภทค่าใช้จ่าย</p>
            <p class="item">ค่าเดินทาง</p>
          </div>
          <div class="col">
            <p class="head">วันที่อนุมัติ</p>
            <p class="item">13/09/2567</p>
          </div>
          <div class="col">
            <p class="head">จำนวนเงิน(บาท)</p>
            <p class="item">315.00</p>
          </div>
          <div class="col">

          </div>
        </div>

        <div class="travel row flex">
          <div class="col">
            <p class="head">ประเภทการเดินทาง</p>
            <p class="item">รถสาธารณะ</p>
          </div>
          <div class="col">
            <p class="head">ประเภทรถ</p>
            <p class="item">รถไฟฟ้า</p>
          </div>
          <div class="col">
            <p class="head">ระยะทาง</p>
            <p class="item">40 กิโลเมตร</p>
          </div>
          <div class="col">
            <p class="head">อัตราค่าเดินทาง</p>
            <p class="item">7 บาท/กิโลเมตร</p>
          </div>
          
        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">สถานที่เริ่มต้น</p>
            <p class="item">Clicknext กรุงเทพฯ</p>
          </div>
          <div class="col">
            <p class="head">สถานที่สิ้นสุด</p>
            <p class="item">Clicknext กรุงเทพฯ</p>
          </div>
        </div>

        <div class="row">
          <p class="head">รายละเอียด</p>
          <p class="item">เพื่อติดต่อสหกิจศึกษา</p>
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
          <Progress :progressInfo="progressInfo" :colorStatus="colorStatus" class="w-[100%]" />
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