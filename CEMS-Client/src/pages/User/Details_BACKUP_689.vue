<script setup lang="ts">
/*
* ชื่อไฟล์: Details.vue
* คำอธิบาย: ไฟล์นี้ใช้สำหรับแสดงรายละเอียดคำขอเบิก
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ, นายพงศธร บุญญามา
* วันที่จัดทำ/แก้ไข: 22 ตุลาคม 2567
*/


import { ref, computed, onMounted, reactive } from "vue";
import Progress from "../../components/Progress.vue";
import Button from "../../components/Buttons/Button.vue";
import { useRoute, useRouter } from "vue-router";
import { useDetailStore } from "../../store/detail";
import { useExportDetailStore } from "../../store/exportDetail";
import FileDisplay from "../../components/FileDisplay.vue";

const route = useRoute();
const router = useRouter();
const detailStore = useDetailStore();
const exportDetailStore = useExportDetailStore();
const isPopupPrintOpen = ref(false); // สำหรับเปิด/ปิด Popup  ส่งออก
const isAlertPrintOpen = ref(false); // ควบคุมการแสดง Alert ส่งออก
const id = route.params.id.toString();
const expenseData = ref<any>(null);
const progressData = ref<any>(null);
const selectedFiles = ref<{ file: File; fId: number | null }[]>([]);


onMounted(async () => {
  progressData.value = await detailStore.getApprover(id);
  expenseData.value = await detailStore.getRequisition(id);
  console.log(expenseData.value)

  if (expenseData) {
    selectedFiles.value = expenseData.value.files.map((file: any) => {
      const blob = base64ToBlob(file.fFile, file.fFileType);
      const fileObject = new File([blob], file.fName, { type: file.fFileType });
      return {
        file: fileObject,
        fId: file.fId,
      };
    });
  }
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

const statusMapping = [
  {
    condition: (data: any) => data.rqProgress === 'accepting' && data.rqStatus === 'waiting',
    label: 'รออนุมัติ',
    color: '#1976D2',
  },
  {
    condition: (data: any) => data.rqStatus === 'edit',
    label: 'แก้ไข',
    color: '#FFBE40',
  },
  {
    condition: (data: any) => data.rqStatus === 'reject',
    label: 'ไม่อนุมัติ',
    color: '#E1032B',
  },
  {
    condition: (data: any) => data.rqStatus === 'sketch',
    label: 'แบบร่าง',
    color: '#B6B7BA',
  },
  {
    condition: (data: any) => data.rqStatus === 'accept' && data.rqProgress === 'paying',
    label: 'รอนำจ่าย',
    color: '#FFBE40',
  },
  {
    condition: (data: any) => data.rqStatus === 'accept' && data.rqProgress === 'complete',
    label: 'นำจ่ายสำเร็จ',
    color: '#12B669',
  },

];

const base64ToBlob = (base64: string, mimeType: string): Blob => {
  const byteCharacters = atob(base64); // แปลง Base64 ให้เป็น string ของ byte
  const byteArrays = [];

  for (let offset = 0; offset < byteCharacters.length; offset += 1024) {
    const slice = byteCharacters.slice(offset, offset + 1024);
    const byteNumbers = new Array(slice.length);
    for (let i = 0; i < slice.length; i++) {
      byteNumbers[i] = slice.charCodeAt(i);
    }
    byteArrays.push(new Uint8Array(byteNumbers));
  }

  return new Blob(byteArrays, { type: mimeType });
};


const statusInfo = computed(() => {
  if (expenseData.value) {
    const match = statusMapping.find((item) => item.condition(expenseData.value));
    return match || { label: 'ไม่ทราบสถานะ', color: '#000000' };
  }
  return { label: 'กำลังโหลดข้อมูล...', color: '##B6B7BA' }; // กรณีที่ข้อมูลยังไม่โหลด
});

const colorStatus: { [key: string]: string } = {
  reject: "#E1032B",
  edit: "#FFBE40",
  accept: "#12B669",
  waiting: "#1976D2",
  sketch: "#B6B7BA",
  paying: "#FFBE40",
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
    detailStore.updateApprove(data);
    handleHideApproverPopup();
    confirmPrint(status)
    isAlertPrintOpen.value = true;
    setTimeout(() => {
      isAlertPrintOpen.value = false;
      closePopupPrint();
      router.push(`/approval/list/`)
    }, 1500);

  }
};

const handleDisburse = async () => {
  const currentUser = await initializeCurrentUser();
  if (currentUser) {
    const data = {
      usrId: currentUser.usrId,
      rqId: expenseData.value.rqId,
    };
    detailStore.updateDisburse(data);
    confirmPrint("pay")
    isAlertPrintOpen.value = true;
    setTimeout(() => {
      isAlertPrintOpen.value = false;
      closePopupPrint();
      router.push(`/payment/List/`)
    }, 1500);
    handleHideApproverPopup();
  }
}

// เปิด/ปิด Popup ยืนยัน ผู้อนุมัติ
const openPopupPrint = () => {
  isPopupPrintOpen.value = true;
};
const closePopupPrint = () => {
  isPopupPrintOpen.value = false;
};

const statusMessage = {
  accept: "ยืนยันการการอนุมัติรายการเบิกค่าใช้จ่ายสำเร็จ",
  reject: "ยืนยันการไม่อนุมัติรายการเบิกค่าใช้จ่ายสำเร็จ",
  edit: "ยืนยันการส่งกลับรายการเบิกค่าใช้จ่ายสำเร็จ",
  pay: "ยืนยันการนำจ่ายรายการเบิกค่าใช้จ่ายสำเร็จ",
};

const alertMessage = ref("")
const confirmPrint = async (status: string) => {
  const message = statusMessage[status as keyof typeof statusMessage] || "สถานะไม่ถูกต้อง";
  alertMessage.value = message;
  isAlertPrintOpen.value = true;

  setTimeout(() => {
    isAlertPrintOpen.value = false;
    closePopupPrint();
  }, 1500);
};


const handleExportFile = () => {
  // Logic for handling export
  exportDetailStore.exportFile(expenseData.value.rqId);
  isPopupPrintOpen.value = false;  // ปิด popup การยืนยัน
  isAlertPrintOpen.value = true;   // แสดง popup ที่แสดงผลลัพธ์
  alertMessage.value = 'ส่งออกใบเบิกค่าใช้จ่ายสำเร็จ';  // กำหนดข้อความที่จะแสดง
  setTimeout(() => {
    isAlertPrintOpen.value = false; // ปิด popup หลังจาก 3 วินาที
  }, 3000);
};
const approveCompleteDate = computed(() => {
  const lastAccepter = progressData.value.acceptor.slice().reverse().find((item: any) => item.aprDate);
  return lastAccepter ? lastAccepter.aprDate.split(' ')[0] : null;
});

const editAprDate = computed(() => {
  const target = progressData.value.acceptor.find(
    (item: any) => item.aprStatus == "edit"
  )
  return target ? target.aprDate : "";
});

//ดูข้อมูลใน file
const previewFile = (file: File) => {
  const fileURL = URL.createObjectURL(file);

  if (file.type === 'application/pdf') {
    window.open(fileURL, '_blank');
  }
  else if (file.type.startsWith('image/')) {
    window.open(fileURL, '_blank');
  }
  else if (file.type === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document') {
    const link = document.createElement('a');
    link.href = fileURL;
    link.download = file.name;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
  else {
    console.log('ไม่สามารถแสดงผลไฟล์ประเภทนี้ได้');
  }
};
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
      <div class="flex gap-[42px] mb-[24px]">
        <Button type="btn-unapprove" class="w-[95px] h-[40px]" @click="handleApproverPopup('reject')" />
        <Button type="btn-editSend" class="w-[95px] h-[40px]" @click="handleApproverPopup('edit')" />
        <Button type="btn-approve" class="w-[95px] h-[40px]" @click="handleApproverPopup('approve')" />
      </div>
    </div>

    <div v-if="isPaymentPath" class="flex justify-end">
      <div class="flex mb-[24px]">
        <Button :type="'btn-payment1'" @click="handleApproverPopup('pay')" class="w-[95px] h-[40px]"></Button>
      </div>
    </div>

    <div class="flex justify-between">
      <div class="left w-[85%]">
        <div class="flex items-center align-middle justify-between">
          <h3 class="text-base font-bold text-black ">
            {{ expenseData.rqName }}<span :class="`bg-[${statusInfo.color}]`"
              class="!text-white px-4 py-[1px] rounded-[10px] text-xs font-thin ml-[15px]">{{
                statusInfo.label }}</span>
          </h3>
<<<<<<< HEAD
          <div class="pr-5 flex">
            <button class="bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin mr-4">แก้ไขคำขอเบิก</button>
            <Button :type="'btn-print2'" @click="openPopupPrint"></Button>
=======
          <div class="flex flex-row pr-8 gap-4">
            <RouterLink v-if="expenseData.rqStatus == 'edit' && route.name === 'listWithdrawDetail'" :to="'/disbursement/listWithdraw/detail/' + route.params.id + '/editExpenseForm'">
              <Button :type="'btn-editRequest'"></Button>
            </RouterLink>

            <Button :type="'btn-print2'" class="w-[95px] h-[40px]" @click="openPopupPrint"></Button>
>>>>>>> develop
          </div>
        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">รหัสรายการเบิก</p>
            <p class="item">{{ expenseData.rqCode || '-' }}</p>
          </div>
          <div class="col">
            <p class="head">โครงการ</p>
            <p class="item">{{ expenseData?.rqPjName || '-' }}</p>
          </div>
          <div class="col">
            <p class="head">วันที่เกิดค่าใช้จ่าย</p>
            <p class="item">{{ expenseData?.rqPayDate || '-' }}</p>
          </div>
          <div class="col">
            <p class="head">วันที่ทำรายการเบิกค่าใช้จ่าย</p>
            <p class="item">{{ expenseData?.rqWithDrawDate || '-' }}</p>
          </div>
        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">ชื่อผู้เบิก</p>
            <p class="item">{{ expenseData?.rqUsrName || '-' }}</p>
          </div>
          <div class="col">
            <p class="head">ชื่อผู้เบิกแทน</p>
            <p class="item">{{ expenseData?.rqInsteadName || '-' }}</p>
          </div>
        </div>

        <div class="flex flex-row">
          <div class="col">
            <p class="head">ประเภทค่าใช้จ่าย</p>
            <p class="item">{{ expenseData?.rqRqtName || '-' }}</p>
          </div>
          <div v-if="isPaymentOrHistoryPath" class="col">
            <p class="head">วันที่อนุมัติ</p>
            <p class="item">{{ approveCompleteDate }}</p>
          </div>
          <div class="col">
            <p class="head">จำนวนเงิน(บาท)</p>
            <p class="item">{{ expenseData?.rqExpenses || '-' }}</p>
          </div>
          <div class="col"></div>
          <div v-if="!isPaymentOrHistoryPath" class="col"></div>
        </div>

        <div class="travel row flex">
          <div class="col">
            <p class="head">ประเภทการเดินทาง</p>
            <p class="item">{{ expenseData?.rqVhType || '-' }}</p>
          </div>
          <div class="col">
            <p class="head">ประเภทรถ</p>
            <p class="item">{{ expenseData?.rqVhName || '-' }}</p>
          </div>
          <div class="col">
            <p class="head">ระยะทาง</p>
            <p class="item">{{ expenseData?.rqDistance || '-' }}</p>
          </div>
          <div class="col">
            <p class="head">อัตราค่าเดินทาง</p>
            <p class="item">{{ expenseData?.rqVhPayrate || '-' }}</p>
          </div>
        </div>

        <div class="row flex justify-around">
          <div class="col">
            <p class="head">สถานที่เริ่มต้น</p>
            <p class="item">{{ expenseData?.rqStartLocation || '-' }}</p>
          </div>
          <div class="col">
            <p class="head">สถานที่สิ้นสุด</p>
            <p class="item">{{ expenseData?.rqEndLocation || '-' }}</p>
          </div>
        </div>

        <div class="row">
          <p class="head">รายละเอียด</p>
          <p class="item">{{ expenseData?.rqPurpose || '-' }}</p>
        </div>

        <div class="row flex">
          <div class="flex-1">
            <p class="head">อัปโหลดไฟล์</p>
            <FileDisplay v-for="fileObj in selectedFiles" :key="fileObj.file.name || fileObj.file.lastModified"
              :file="fileObj.file" @preview="previewFile(fileObj.file)" class="text-[14px] text-black" />
            <p v-if="selectedFiles == null" class="item">-</p>
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
    <div class="bg-white rounded-lg shadow-lg p-6 w-[460px] h-[295px]">
      <div class="flex flex-col justify-center m-[23px] gap-4">
        <h2 class="text-[24px] font-bold text-center text-black">ยืนยันการอนุมัติรายการเบิกค่าใช้จ่าย</h2>
        <div class="flex flex-col gap-2">
          <h1 class="text-[16px] text-center text-black">{{ expenseData.rqUsrName }}</h1>
          <h1 class="text-[16px] text-center text-black">วันที่ขอเบิก {{ expenseData.rqWithDrawDate }}</h1>
        </div>
        <h1 class="text-[18px] text-center text-[#7E7E7E]">คุณยืนยันการอนุมัติรายการเบิกค่าใช้จ่ายหรือไม่ ?</h1>
        <div class="flex justify-center gap-5">
          <Button :type="'btn-cancleGray'" @click="handleHideApproverPopup();" class="w-[95px] h-[40px]"></Button>
          <Button :type="'btn-summit'" @click="handleSummit('accept')" class="w-[95px] h-[40px]"></Button>
        </div>
      </div>
    </div>
  </div>
  <!-- popup-reject -->
  <div v-if="isApproverPopup === 'reject'" class="fixed inset-0 bg-black/50 flex justify-center items-center z-50 ">
    <div class="bg-white rounded-lg shadow-lg w-[460px] h-[295px]">
      <div class="flex flex-col justify-center my-[23px] gap-4 mx-[40px]">
        <h2 class="text-center text-[24px] text-black">ยืนยันการไม่อนุมัติรายการเบิกค่าใช้จ่าย</h2>
        <h1 class="text-center text-[#7E7E7E] text-[18px] ">คุณยืนยันการไม่อนุมัติรายการเบิกค่าใช้จ่ายหรือไม่ ?</h1>
        <div class="flex flex-col gap-[5px]">
          <p class="text-start text-black text-[16px]">ระบุเหตุผล <span class="text-red-500">*</span></p>
          <textarea id="rqReason" v-model="formData.rqReason" required
            class="flex overflow-hidden gap-1.5 items-start  px-2.5 py-1.5  w-full text-sm text-gray-500 bg-white rounded-md border-2 border-solid border-gray-200 min-h-[70px] focus:outline-none focus:border-gray-500"
            aria-label="ระบุเหตุผลการปฏิเสธ" placeholder="ระบุเหตุผล"></textarea>
        </div>
        <div class="flex justify-center gap-5">
          <Button :type="'btn-cancleGray'" @click="handleHideApproverPopup()"></Button>
          <Button :type="'btn-summit'" @click="handleSummit('reject')"></Button>
        </div>
      </div>
    </div>
  </div>

  <!-- popup-edit -->
  <div v-if="isApproverPopup === 'edit'" class="fixed inset-0 bg-black/50 flex justify-center items-center z-50 ">
    <div class="bg-white rounded-lg shadow-lg w-[460px] h-[295px]">
      <div class="flex flex-col justify-center my-[23px] gap-4 mx-[40px]">
        <h2 class="text-center text-[24px] text-black">ยืนยันการส่งกลับรายการเบิกค่าใช้จ่าย</h2>
        <h1 class="text-center text-[#7E7E7E] text-[18px] ">คุณยืนยันการส่งกลับรายการเบิกค่าใช้จ่ายหรือไม่ ?</h1>
        <div class="flex flex-col gap-[5px]">
          <p class="text-start text-black text-[16px]">ระบุเหตุผล <span class="text-red-500">*</span></p>
          <textarea id="rqReason" v-model="formData.rqReason" required
            class="flex overflow-hidden gap-1.5 items-start px-2.5 py-1.5 w-full text-sm text-gray-500 bg-white rounded-md border-2 border-solid border-gray-200 min-h-[70px] focus:outline-none focus:border-gray-500"
            aria-label="ระบุเหตุผล" placeholder="ระบุเหตุผล">
        </textarea>
        </div>
        <div class="flex justify-center gap-5">
          <Button :type="'btn-cancleGray'" @click="handleHideApproverPopup()"></Button>
          <Button :type="'btn-summit'" @click="handleSummit('edit')"></Button>
        </div>
      </div>
    </div>
  </div>

  <!-- Popup นำจ่าย -->
  <div v-if="isApproverPopup === 'pay'"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <div class="flex justify-center mb-4">
        <svg :class="`w-[80px] h-[80px] text-gray-800 dark:text-white`" aria-hidden="true"
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


  <!-- Popup สำหรับยืนยันการส่งออกคำขอเบิกค่าใช้จ่าย -->
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

        <button @click="handleExportFile"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>

  <!-- Popup สำหรับแสดงผลลัพธ์ -->
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
      <h2 class="text-[24px] font-bold text-center text-black mt-3">{{ alertMessage }}</h2>
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
  color: rgba(0, 0, 0, 0.50);
  font-size: 14px;
  font-style: normal;
  font-weight: 400;
}

.item {
  font-family: Sarabun;
  font-size: 14px;
  font-style: normal;
  font-weight: 400;

}

.col {
  display: flex;
  flex-direction: column;
  flex: 1;
  gap: 8px;
}


.cols {
  width: 207.8px;
}
</style>