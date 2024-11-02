<script setup lang="ts">
import { ref } from "vue";
import Progress from "../../components/template/Progress.vue";
import Button from "../../components/template/Button.vue";

const statusDetail = ref("edit"); // 'reject' is the initial value

const colorStatus: { [key: string]: string } = {
  reject: "#E1032B",
  edit: "#FFBE40",
  accept: "#12B669",
  waiting: "#1976D2",
  sketch: "#B6B7BA",
};

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
    <div
      class="border border-[#E00000] p-[15px] rounded-[10px] bg-[#FFECEC] mb-[5px]"
    >
      <div class="flex justify-between">
        <p class="!text-[#ED0000] font-bold">เหตุผลส่งกลับ :</p>
        <p class="!text-[#FF0000]">วันที่ส่งกลับ : 11/09/2567</p>
      </div>
      <p class="!text-[#FF0000]">รูปหลักฐานไม่ชัดเจน</p>
    </div>
    <div
      class="border border-[#E00000] p-[15px] rounded-[10px] bg-[#FFECEC] mb-[24px]"
    >
      <p class="!text-[#ED0000] font-bold">เหตุผลการไม่อนุมัติ :</p>
      <p class="!text-[#FF0000]">รูปหลักฐานไม่ชัดเจน</p>
    </div>
    <div class="flex justify-between">
      <div class="left w-[80%]">
        <h3 class="text-base font-bold text-black">
          รายละเอียดคำขอเบิก<span
            :class="`bg-[${colorStatus[statusDetail]}]`"
            class="!text-white px-7 py-[1px] rounded-[10px] text-xs font-thin ml-[15px]"
            >แก้ไข</span
          >
        </h3>
        <div class="row">
          <p>โครงการ</p>
          <p>อบรมการบริหาร</p>
        </div>
        <div class="row flex justify-around">
          <div class="col">
            <p>ชื่อผู้เบิก</p>
            <p>นางสาวอลิสา ปะกังพลัง</p>
          </div>
          <div class="col">
            <p>ชื่อผู้เบิกแทน</p>
            <p>นายปุณณวิชน์ วิเชียร์มาร์น</p>
          </div>
        </div>
        <div class="row flex justify-around">
          <div class="col">
            <p>ประเภทค่าใช้จ่าย</p>
            <p>ค่าเดินทาง</p>
          </div>
          <div class="col">
            <p>วันที่ขอเบิก</p>
            <p>11/09/2567</p>
          </div>
          <div class="col">
            <p>วันที่อนุมัติ</p>
            <p>13/09/2567</p>
          </div>
          <div class="col">
            <p>จำนวนเงิน(บาท)</p>
            <p>315.00</p>
          </div>
        </div>
        <div class="travel row flex">
          <div class="col">
            <p>ประเภทการเดินทาง</p>
            <p>รถสาธารณะ</p>
          </div>
          <div class="col">
            <p>ประเภทรถ</p>
            <p>รถไฟฟ้า</p>
          </div>
          <div class="col">
            <p>ระยะทาง</p>
            <p>40 กิโลเมตร</p>
          </div>
          <div class="col">
            <p>อัตราค่าเดินทาง</p>
            <p>7 บาท/กิโลเมตร</p>
          </div>
        </div>
        <div class="row">
          <p>สถานที่เริ่มต้น</p>
          <p>Clicknext กรุงเทพฯ</p>
        </div>
        <div class="row">
          <p>วัตถุประสงค์</p>
          <p>เพื่อติดต่อสหกิจศึกษา</p>
        </div>
        <div class="row flex">
          <div class="flex-1">
            <h3 class="mb-[16px] text-base font-bold text-black">รูปหลักฐาน</h3>
            <img
              src="/evidence.jpg"
              alt=""
              class="w-[50%] h-auto cursor-pointer"
            />
          </div>
          <div class="flex-1"></div>
        </div>
      </div>
      <div class="right">
        <div class="flex mb-[24px]">
          <Button type="btn-unapprove" />
          <span class="mx-[12px]"></span>
          <Button type="btn-editSend" class="mx-[24px]" />
          <span class="mx-[12px]"></span>
          <Button type="btn-approve" />
        </div>
        <div class="flex justify-end">
          <Progress
            :progressInfo="progressInfo"
            :colorStatus="colorStatus"
            class="w-[80%]"
          />
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

.col {
  flex: 1;
}
</style>
