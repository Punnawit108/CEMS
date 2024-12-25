<script setup lang="ts">
/*
* ชื่อไฟล์: Details.vue
* คำอธิบาย: ไฟล์นี้ใช้สำหรับแสดงรายละเอียดคำขอเบิก
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ, นายพงศธร บุญญามา
* วันที่จัดทำ/แก้ไข: 22 ตุลาคม 2567
*/

import { ref, computed, onMounted, reactive } from "vue";
import Progress from "../../components/template/Progress.vue";
import Button from "../../components/template/Button.vue";
import { useRoute, useRouter } from "vue-router";
import { useDetailStore } from "../../store/detail";

const route = useRoute();
const router = useRouter();
const detailStore = useDetailStore();

const isPopupPrintOpen = ref(false); // สำหรับเปิด/ปิด Popup  ส่งออก
const isAlertPrintOpen = ref(false); // ควบคุมการแสดง Alert ส่งออก
const id = route.params.id.toString();
const expenseData = ref<any>(null);
const progressData = ref<any>(null);


onMounted(async () => {
  progressData.value = await detailStore.getApprover(id);
  expenseData.value = await detailStore.getRequisition(id);
})


// FN ตรวจสอบว่ามีคำว่า 'approval' และ list ใน path หรือไม่  
// ถ้ารายการคำขอเบิกนั้นๆ เป็นของ ผู้ใช้ปัจจุบัน และ AprStatus นั้นเป็น waiting จะดึงข้อมูล
const isApprovalPath = computed(() => {
  return route.path.includes('approval') && route.path.includes('list');
});

// ถ้ารายการคำขอเบิกนั้นๆ มีสถานะเป็น rqStatus = accept , rqProgress = paying
const isPaymentPath = computed(() => {
  return route.path.includes('payment') && route.path.includes('list');
});


// FN ตรวจสอบว่ามีคำว่า 'payment' และ list ใน path หรือไม่
const isPaymentOrHistoryPath = computed(() => {
  return route.path.includes('payment') && route.path.includes('list') || route.path.includes('history') && !route.path.includes('approval');
});

const colorStatus: { [key: string]: string } = {
  reject: "#E1032B",
  edit: "#FFBE40",
  accept: "#12B669",
  waiting: "#1976D2",
  sketch: "#B6B7BA",
  paying: "#1976D2",
  complete: "#12B669",
  accepting: "#B6B7BA",
  null: "#B6B7BA"
};

const loadUser = async () => {
  const storedUser = localStorage.getItem("user");
  if (storedUser) {
    try {
      return JSON.parse(storedUser);
    } catch (error) {
      console.log("Error loading user:", error);
    }
  }
  return null;
};

const initializeCurrentUser = async () => {
  const userData = await loadUser();
  const currentUser = {
    usrId: userData.usrId,
    usrFirstName: userData.usrFirstName,
    usrLastName: userData.usrLastName,
  };
  return currentUser;
}

function findAprIdByFirstName(progressData: { disbursement: any[]; acceptor: any[] }, user: { usrFirstName: string }) {
  const match = progressData.acceptor.find(
    (acceptor) => acceptor.usrFirstName === user.usrFirstName
  );
  if (!match) {
    return null;
  }
  return match; // ส่งข้อมูล match และ status
}


// ประเภท popup เช่น 'reject', 'edit', 'approve'
const isApproverPopup = ref("null");

const handleApproverPopup = (status: string) => {
  isApproverPopup.value = status
};

const handleHideApproverPopup = () => {
  isApproverPopup.value = "null";
};

const formData = reactive<any>({
  rqReason: null
})

const handleSummit = async (status: string) => {
  const currentUser = await initializeCurrentUser();
  const matchedAprId = findAprIdByFirstName(progressData.value, currentUser);
  if (matchedAprId != null) {
    const data = {
      aprId: matchedAprId.aprId,
      aprApId: matchedAprId.aprApId,
      aprName: currentUser.usrFirstName + " " + currentUser.usrLastName,
      aprStatus: status,
      rqReason: formData.rqReason
    };
    console.log(data);
    detailStore.updateApprove(data);
    handleHideApproverPopup();
    //router.push(`/approval/list/`)
  }
};

const handleDisburse = () => {
  detailStore.updateDisburse(expenseData.value.rqId);
  handleHideApproverPopup();
}

// เปิด/ปิด Popup ยืนยัน ผู้อนุมัติ
const openPopupPrint = () => {
  isPopupPrintOpen.value = true;
};
const closePopupPrint = () => {
  isPopupPrintOpen.value = false;
};


// เปิด/ปิด Alert บันทึก
const confirmPrint = async () => {
  // เปิด Popup Alert
  isAlertPrintOpen.value = true;
  setTimeout(() => {
    isAlertPrintOpen.value = false; // ปิด Alert
    closePopupPrint(); // ปิด Popup แก้ไข
  }, 1500); // 1.5 วินาที
};

const approveCompleteDate = computed(() => {
  const target = progressData.value.acceptor.find(
    (item: any) => item.aprApId == 3
  );
  return target ? target.aprDate : null;
})

const editAprDate = computed(() => {
  const target = progressData.value.acceptor.find(
    (item: any) => item.aprStatus == "edit"
  )
  return target ? target.aprDate : "";
})

</script>

<!-- path for test = /disbursement/listWithdraw/detailsExpenseForm/:id -->
<!-- path for test = /disbursement/historyWithdraw/detail/:id -->
<!-- path for test = /approval/history/detail/:id -->
<!-- path for test = /payment/List/detail/:id -->
<!-- path for test = /payment/history/detail/:id -->

<template>
  <!-- content -->
  <div v-if="expenseData" class="ml-[16px] ">

    <div v-if="expenseData.rqStatus === 'edit'"
      class="border border-[#E00000] p-[15px] rounded-[10px] bg-[#FFECEC] mb-[24px]">
      <div class="flex justify-between">
        <p class="!text-[#ED0000] font-bold">เหตุผลส่งกลับ :</p>
        <p class="!text-[#FF0000]">{{ editAprDate }}</p>
      </div>
      <p class="!text-[#FF0000]">{{ expenseData.rqReason }}</p>
    </div>

    <div v-if="expenseData.rqStatus === 'reject'"
      class="border border-[#E00000] p-[15px] rounded-[10px] bg-[#FFECEC] mb-[24px]">
      <p class="!text-[#ED0000] font-bold">เหตุผลการไม่อนุมัติ :</p>
      <p class="!text-[#FF0000]">{{ expenseData.rqReason }}</p>
    </div>

    <div v-if="isApprovalPath" class="flex justify-end">
      <div class="flex mb-[22px]">
        <Button type="btn-unapprove" @click="handleApproverPopup('reject')" />
        <span class="mx-[12px]"></span>
        <Button type="btn-editSend" class="mx-[24px]" @click="handleApproverPopup('edit')" />
        <span class="mx-[12px]"></span>
        <Button type="btn-approve" @click="handleApproverPopup('approve')" />
      </div>
    </div>

    <div v-if="isPaymentPath" class="flex justify-end">
      <div class="flex mb-[22px]">
        <Button :type="'btn-payment1'" @click="handleApproverPopup('pay')"></Button>
      </div>
    </div>

    <div class="flex justify-between">
      <div class="left w-[85%]">
        <div class="flex items-center align-middle justify-between">
          <h3 class="text-base font-bold text-black ">
            เบิกค่าใช้จ่าย<span :class="`bg-[${colorStatus[expenseData.rqStatus]}]`"
              class="!text-white px-7 py-[1px] rounded-[10px] text-xs font-thin ml-[15px]">{{
                expenseData.rqStatus }}</span>
          </h3>
          <div class="pr-5">
            <Button :type="'btn-print2'" @click="openPopupPrint"></Button>
          </div>
        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">รหัสรายการเบิก</p>
            <p class="item">{{ expenseData.rqCode }}</p>
          </div>
          <div class="col">
            <p class="head">โครงการ</p>
            <p class="item">{{ expenseData?.rqPjName || '-'}}</p>
          </div>
          <div class="col">
            <p class="head">วันที่เกิดค่าใช้จ่าย</p>
            <p class="item">{{ expenseData?.rqPayDate }}</p>
          </div>
          <div class="col">
            <p class="head">วันที่ทำรายการเบิกค่าใช้จ่าย</p>
            <p class="item">{{ expenseData?.rqWithDrawDate }}</p>
          </div>
        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">ชื่อผู้เบิก</p>
            <p class="item">{{ expenseData?.rqUsrName }}</p>
          </div>
          <div class="col">
            <p class="head">ชื่อผู้เบิกแทน</p>
            <p class="item">{{ expenseData?.rqInsteadName || '-' }}</p>
          </div>
        </div>

        <div class="flex flex-row">
          <div class="col">
            <p class="head">ประเภทค่าใช้จ่าย</p>
            <p class="item">{{ expenseData?.rqRqtName }}</p>
          </div>
          <div v-if="isPaymentOrHistoryPath" class="col">
            <p class="head">วันที่อนุมัติ</p>
            <p class="item">{{ approveCompleteDate }}</p>
          </div>
          <div class="col">
            <p class="head">จำนวนเงิน(บาท)</p>
            <p class="item">{{ expenseData?.rqExpenses }}</p>
          </div>
          <div class="col"></div>
          <div v-if="!isPaymentOrHistoryPath" class="col"></div>
        </div>

        <div class="travel row flex">
          <div class="col">
            <p class="head">ประเภทการเดินทาง</p>
            <p class="item">{{ expenseData?.rqVhType }}</p>
          </div>
          <div class="col">
            <p class="head">ประเภทรถ</p>
            <p class="item">{{ expenseData?.rqVhName }}</p>
          </div>
          <div class="col">
            <p class="head">ระยะทาง</p>
            <p class="item">{{ expenseData?.rqDistance }}</p>
          </div>
          <div class="col">
            <p class="head">อัตราค่าเดินทาง</p>
            <p class="item">{{ expenseData?.rqVhPayrate || '-'}}</p>
          </div>

        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">สถานที่เริ่มต้น</p>
            <p class="item">{{ expenseData?.rqStartLocation }}</p>
          </div>
          <div class="col">
            <p class="head">สถานที่สิ้นสุด</p>
            <p class="item">{{ expenseData?.rqEndLocation }}</p>
          </div>
        </div>

        <div class="row">
          <p class="head">รายละเอียด</p>
          <p class="item">{{ expenseData?.rqPurpose }}</p>
        </div>

        <div class="row flex">
          <div class="flex-1">
            <h3 class="mb-[16px] text-base font-bold text-black">รูปหลักฐาน</h3>
            <div>
            </div>
            <img :src="(expenseData?.rqProof)" alt="" class="w-[50%] h-auto cursor-pointer" />
          </div>
          <div class="flex-1"></div>
        </div>
      </div>

      <div class="right">
        <div class="flex justify-end">
          <Progress v-if="progressData !== null" :progressInfo="progressData" :colorStatus="colorStatus"
            class="w-[100%]" />
        </div>
      </div>
    </div>
  </div>

  <!-- popup-approver -->
  <div v-if="isApproverPopup === 'approve'" class="fixed inset-0 bg-black/50 flex justify-center items-center z-50">
    <div class="bg-white rounded-lg shadow-lg p-6 w-[460px] h-[417px]">
      <div class="flex justify-center mb-2 mt-12">
        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="61" viewBox="0 0 60 61" fill="none">
          <path
            d="M29.9999 0C37.9187 0 45.5131 3.19263 51.1126 8.87555C56.712 14.5585 59.8577 22.2662 59.8577 30.303C59.8577 38.3399 56.712 46.0476 51.1126 51.7305C45.5131 57.4134 37.9187 60.6061 29.9999 60.6061C22.0811 60.6061 14.4867 57.4134 8.88724 51.7305C3.28782 46.0476 0.14209 38.3399 0.14209 30.303C0.14209 22.2662 3.28782 14.5585 8.88724 8.87555C14.4867 3.19263 22.0811 0 29.9999 0ZM29.9999 12.987C29.4584 12.9865 28.9227 13.1007 28.4271 13.3222C27.9315 13.5437 27.4868 13.8677 27.1216 14.2735C26.7563 14.6792 26.4784 15.1578 26.3057 15.6787C26.133 16.1996 26.0692 16.7514 26.1184 17.2987L27.6753 34.6407C27.7356 35.2237 28.0066 35.7634 28.4358 36.1557C28.8651 36.548 29.4223 36.7651 29.9999 36.7651C30.5776 36.7651 31.1347 36.548 31.564 36.1557C31.9933 35.7634 32.2642 35.2237 32.3246 34.6407L33.8772 17.2987C33.9264 16.7517 33.8627 16.2004 33.6902 15.6797C33.5177 15.1591 33.2402 14.6807 32.8754 14.275C32.5106 13.8694 32.0665 13.5453 31.5714 13.3235C31.0763 13.1018 30.5411 12.9871 29.9999 12.987ZM29.9999 47.619C30.9049 47.619 31.7729 47.2542 32.4128 46.6047C33.0527 45.9552 33.4122 45.0743 33.4122 44.1558C33.4122 43.2373 33.0527 42.3565 32.4128 41.707C31.7729 41.0575 30.9049 40.6926 29.9999 40.6926C29.0949 40.6926 28.227 41.0575 27.587 41.707C26.9471 42.3565 26.5876 43.2373 26.5876 44.1558C26.5876 45.0743 26.9471 45.9552 27.587 46.6047C28.227 47.2542 29.0949 47.619 29.9999 47.619Z"
            fill="#FFBE40" />
        </svg>
      </div>
      <h2 class="text-center text-[24px] text-black mb-4">
        ยืนยันการอนุมัติค่าใช้จ่าย
      </h2>
      <p class="text-center text-black text-[18px] mb-4">
        {{ expenseData.rqUsrName }}
        <br />
        วันที่ขอเบิก {{ expenseData.rqDatePay }}
      </p>
      <p class="text-center text-gray-400 text-[18px] mb-8">
        คุณยืนยันการอนุมัติค่าใช้จ่ายหรือไม่ ?
      </p>
      <div class="flex justify-center gap-5">
        <Button :type="'btn-cancleGray'" @click="handleHideApproverPopup();"></Button>
        <Button :type="'btn-summit'" @click="handleSummit('accept')"></Button>
      </div>
    </div>
  </div>
  <!-- popup-reject -->
  <div v-if="isApproverPopup === 'reject'" class="fixed inset-0 bg-black/50 flex justify-center items-center z-50 ">
    <div class="bg-white rounded-lg shadow-lg w-[460px] h-[417px] px-[75px] py-[69px] ">
      <div class="flex justify-center mb-4  items-center align-middle gap-3">
        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 60 60" fill="none">
          <path
            d="M33.6873 7.875H29.9998H20.1665C16.0934 7.875 12.7915 11.1769 12.7915 15.25V44.75C12.7915 48.8232 16.0934 52.125 20.1665 52.125H27.5415M33.6873 7.875L47.2082 21.7031M33.6873 7.875V19.2448C33.6873 20.6025 34.7879 21.7031 36.1457 21.7031H47.2082M47.2082 21.7031V29.5391"
            stroke="#E63C3C" stroke-width="5" stroke-linecap="round" stroke-linejoin="round" />
          <path
            d="M37.375 39.833L43.5208 45.9788M43.5208 45.9788L49.6667 52.1247M43.5208 45.9788L49.6667 39.833M43.5208 45.9788L37.375 52.1247"
            stroke="#E63C3C" stroke-width="5" stroke-linecap="round" stroke-linejoin="round" />
        </svg>
        <h2 class="text-center text-[24px] text-black ">
          ยืนยันการปฏิเสธคำขอ
        </h2>
      </div>
      <p class="text-center text-gray-400 text-[18px] mb-4">
        คุณยืนยันการปฏิเสธคำขอหรือไม่ ?
      </p>
      <p class="text-start text-gray-400 text-[18px] mb-4">ระบุเหตุผลการปฏิเสธ <span class="text-red-500">*</span></p>
      <textarea id="rqReason" v-model="formData.rqReason" required
        class="flex overflow-hidden gap-1.5 items-start mb-4 px-2.5 pt-1.5 pb-7 w-full text-sm text-gray-500 bg-white rounded-md border border-solid border-slate-200 min-h-[70px]"
        aria-label="ระบุเหตุผลการปฏิเสธ" />
      <div class="flex justify-center gap-5">
        <Button :type="'btn-cancleGray'" @click="handleHideApproverPopup()"></Button>
        <Button :type="'btn-summit'" @click="handleSummit('reject')"></Button>
      </div>
    </div>
  </div>

  <!-- popup-edit -->
  <div v-if="isApproverPopup === 'edit'" class="fixed inset-0 bg-black/50 flex justify-center items-center z-50 ">
    <div class="bg-white rounded-lg shadow-lg w-[460px] h-[417px] px-[75px] py-[69px] ">
      <div class="flex justify-center mb-4  items-center align-middle gap-3">
        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 60 60" fill="none">
          <path
            d="M33.6873 7.875H29.9998H20.1665C16.0934 7.875 12.7915 11.1769 12.7915 15.25V44.75C12.7915 48.8232 16.0934 52.125 20.1665 52.125H27.5415M33.6873 7.875L47.2082 21.7031M33.6873 7.875V19.2448C33.6873 20.6025 34.7879 21.7031 36.1457 21.7031H47.2082M47.2082 21.7031V29.5391"
            stroke="#E63C3C" stroke-width="5" stroke-linecap="round" stroke-linejoin="round" />
          <path
            d="M37.375 39.833L43.5208 45.9788M43.5208 45.9788L49.6667 52.1247M43.5208 45.9788L49.6667 39.833M43.5208 45.9788L37.375 52.1247"
            stroke="#E63C3C" stroke-width="5" stroke-linecap="round" stroke-linejoin="round" />
        </svg>
        <h2 class="text-center text-[24px] text-black ">
          ยืนยันการส่งกลับคำขอ
        </h2>
      </div>
      <p class="text-center text-gray-400 text-[18px] mb-4">
        คุณยืนยันการส่งกลับคำขอหรือไม่ ?
      </p>
      <p class="text-start text-gray-400 text-[18px] mb-4">ระบุเหตุผล <span class="text-red-500">*</span></p>
      <textarea id="rqReason" v-model="formData.rqReason" required
        class="flex overflow-hidden gap-1.5 items-start mb-4 px-2.5 pt-1.5 pb-7 w-full text-sm text-gray-500 bg-white rounded-md border border-solid border-slate-200 min-h-[70px]"
        aria-label="ระบุเหตุผล" />
      <div class="flex justify-center gap-5">
        <Button :type="'btn-cancleGray'" @click="handleHideApproverPopup()"></Button>
        <Button :type="'btn-summit'" @click="handleSummit('edit')"></Button>
      </div>
    </div>
  </div>

  <!-- Popup นำจ่าย -->
  <div v-if="isApproverPopup === 'pay'"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <div class="flex justify-center mb-4">
        <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mb-4">
        ยืนยันการอัปเดตสถานะคำขอเบิก
      </h2>
      <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
        คุณยืนยันการอัปเดตสถานะคำขอเบิกหรือไม่ ?
      </h2>
      <div class="flex justify-center space-x-4">
        <button @click="handleHideApproverPopup()"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="handleDisburse()"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>

  <!-- Popup ส่งออก -->
  <div v-if="isPopupPrintOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <div class="flex justify-center mb-4">
        <svg :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mb-4">
        ยืนยันส่งออกคำขอเบิกค่าใช้จ่าย
      </h2>
      <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
        คุณยืนยันส่งออกคำขอเบิกค่าใช้จ่ายหรือไม่ ?
      </h2>
      <div class="flex justify-center space-x-4">
        <button @click="closePopupPrint"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmPrint"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>


  <!-- Alert ส่งออก-->
  <div v-if="isAlertPrintOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="mb-4">
        <svg :class="`w-[96px] h-[96px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">ยืนยันส่งออกคำขอเบิกค่าใช้จ่ายสำเร็จ</h2>
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

.cols {
  width: 207.8px;
}
</style>