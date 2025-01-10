<script setup lang="ts">
/*
 * ชื่อไฟล์: CreateExpenseForm.vue
 * คำอธิบาย: ไฟล์นี้แสดงฟอร์มเบิกค่าใช้จ่าย
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 4 มกราคม 2568
 */

import { onMounted, ref } from "vue";
import Button from "../../components/template/Button.vue";
import { useRequisitionStore } from "../../store/requisition";
import router from "../../router";
import { createRequisition } from "../../types";

const user = ref<any>(null);
const requisitionStore = useRequisitionStore();

const rqtId = ref(0);
// const startPickerOpen = ref(false);
// const rqtName = ref("");
// const customExpenseType = ref("");
// const isOtherSelected = ref(false);
// const isCustomExpenseTypeAdded = ref(false);
const isPopupSaveOpen = ref(false);
const isPopupCancleOpen = ref(false);
const isPopupSubmitOpen = ref(false);
const isAlertSaveOpen = ref(false);
const isAlertCancleOpen = ref(false);
const isAlertSubmitOpen = ref(false);



const formData = ref<createRequisition>({
  rqName: "",
  rqUsrId: "",
  rqPjId: "",
  rqRqtId: rqtId.value,
  rqVhId: 0,
  rqPayDate: "",
  rqWithdrawDate: "",
  rqCode: "",
  rqInsteadEmail: "",
  rqExpenses: 0,
  rqStartLocation: "",
  rqEndLocation: "",
  rqDistance: "",
  rqPurpose: "",
  rqProof: "",
  rqStatus: "",
  rqProgress: "accepting",
  rqAny: "",
});


// const selectedExpenseTypeId = ref("");
const fileInput = ref<HTMLInputElement | null>(null);
const selectedFile = ref<File | null>(null);
const previewUrl = ref<string | null>(null);

const maxWidth = 800;
const maxHeight = 800;

onMounted(async () => {
  await requisitionStore.getAllProject();
  await requisitionStore.getAllRequisitionType();
  await requisitionStore.getAllvehicleType();
  const storedUser = localStorage.getItem("user");
  if (storedUser) {
    try {
      user.value = await JSON.parse(storedUser);
    } catch (error) {
      console.log("Error loading user:", error);
    }
  }
  console.log("user", formData.value);
});

// watch(
//   () => formData.value.rqRqtId,
//   (newValue) => {
//     if (newValue === "อื่นๆ") {
//       selectedExpenseTypeId.value =
//         customExpenseType.value || "กรุณาระบุประเภทค่าใช้จ่าย";
//     } else {
//       const selectedType = requisitionStore.requisitionType.find(
//         (type) => type.rqtId === newValue
//       );
//       selectedExpenseTypeId.value = selectedType ? selectedType.rqtName : "";
//     }
//   }
// );



// const handleSelectChange = () => {
//   if (formData.value.rqRqtId !== "อื่นๆ") {
//     formData.value.additionalInfo = "";
//   }
// };

const rqtName = ref('')

function updateRqtName(event: Event) {
  const selectedId = (event.target as HTMLSelectElement).value;
  const selectedType = requisitionStore.requisitionType.find(
    (type) => type.rqtId === Number(selectedId)
  );
  rqtName.value = selectedType ? selectedType.rqtName : '';
}



const triggerFileInput = () => {
  fileInput.value?.click();
};

const handleFileChange = (event: Event) => {
  const target = event.target as HTMLInputElement;
  if (target.files && target.files.length > 0) {
    const file = target.files[0];
    uploadFile(file);
  }
};

const handleDrop = (event: DragEvent) => {
  if (event.dataTransfer?.files.length) {
    uploadFile(event.dataTransfer.files[0]);
  }
};

const checkImageDimensions = (file: File): Promise<boolean> => {
  return new Promise((resolve) => {
    const img = new Image();
    img.onload = () => {
      URL.revokeObjectURL(img.src);
      resolve(img.width <= maxWidth && img.height <= maxHeight);
    };
    img.src = URL.createObjectURL(file);
  });
};

const uploadFile = async (file: File) => {
  if (!["image/svg+xml", "image/png", "image/jpeg"].includes(file.type)) {
    alert("กรุณาอัปโหลดไฟล์ SVG, PNG หรือ JPG เท่านั้น");
    return;
  }

  const isValidSize = await checkImageDimensions(file);
  if (isValidSize) {
    selectedFile.value = file;
    previewUrl.value = URL.createObjectURL(file);
    formData.value.rqProof = await convertToBase64(file);
  } else {
    alert(
      `กรุณาอัปโหลดรูปภาพที่มีขนาดไม่เกิน ${maxWidth} x ${maxHeight} พิกเซล`
    );
    selectedFile.value = null;
    previewUrl.value = null;
  }
};

const convertToBase64 = (file: File): Promise<string> => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result as string);
    reader.onerror = (error) => reject(error);
  });
};

const handleSubmit = async () => {
  openPopupSubmit();
};

const handleSave = async () => {
  openPopupSave();
};

const handleCancel = () => {
  openPopupCancle();
};

const openPopupSave = () => {
  isPopupSaveOpen.value = true;
};

const closePopupSave = () => {
  isPopupSaveOpen.value = false;
};

const openPopupCancle = () => {
  isPopupCancleOpen.value = true;
};

const closePopupCancle = () => {
  isPopupCancleOpen.value = false;
};

const openPopupSubmit = () => {
  isPopupSubmitOpen.value = true;
};

const closePopupSubmit = () => {
  isPopupSubmitOpen.value = false;
};

function updateFormData() {
  if (rqtName.value != 'ค่าเดินทาง') {
    formData.value.rqVhId = null;
    formData.value.rqStartLocation = null;
    formData.value.rqEndLocation = null;
    formData.value.rqDistance = null;
  }
  if (rqtName.value != 'อื่นๆ') {
    formData.value.rqAny = null;
  }
}

const confirmSave = async (event: Event) => {
  event.preventDefault();
  isAlertSaveOpen.value = true;
  formData.value.rqStatus = "sketch";
  formData.value.rqUsrId = user.value.usrId;
  await updateFormData()
  await requisitionStore.createExpense(formData.value);

  setTimeout(() => {
    isAlertSaveOpen.value = false;
    closePopupSave();
    router.push("/disbursement/listWithdraw");
  }, 1500);
};

const confirmSubmit = async (event: Event) => {
  event.preventDefault();
  isAlertSubmitOpen.value = true;
  formData.value.rqStatus = "waiting";
  formData.value.rqUsrId = user.value.usrId;
  await updateFormData()
  isAlertSubmitOpen.value = false;
  await requisitionStore.createExpense(formData.value);

  setTimeout(() => {
    isAlertSubmitOpen.value = false;
    closePopupSubmit();
    router.push("/disbursement/listWithdraw");
  }, 1500);
};

const confirmCancle = async (event: Event) => {
  event.preventDefault();
  isAlertCancleOpen.value = true;
  setTimeout(() => {
    isAlertCancleOpen.value = false;
    closePopupCancle();
    router.push("/disbursement/listWithdraw");
  }, 1500);
};

// const handleDateConfirm = (type: "start" | "end", confirmedDate: Date) => {
//   if (type === "start") {
//     startPickerOpen.value = false;
//   } else {
//     startPickerOpen.value = false;
//   }
// };

// const handleDateCancel = (type: "start" | "end") => {
//   if (type === "start") {
//     startPickerOpen.value = false;
//   }
// };
</script>

<template>
  <form class="text-black text-sm m-4">
    <!-- btn -->
    <div class="flex justify-end gap-4">
      <Button :type="'btn-save'" @click="handleSave">บันทึก</Button>
      <Button :type="'btn-cancleBorderGray'" @click="handleCancel">ยกเลิก</Button>
      <Button :type="'btn-summit'" @click="handleSubmit">ยืนยัน</Button>
    </div>
    <!-- Fromประเภทค่าเดินทาง-->
    <div class="">
      <!-- แบ่งเป็น 2 คอลัมน์ -->
      <div class="flex flex-col md:flex-row justify-around">
        <!-- Form Left -->
        <div class="w-2/5 rounded-[10px]">
          <!-- ช่อง "รหัสรายการเบิก *" -->
          <div>
            <label for="rqCode" class="block text-sm font-medium py-1">รหัสรายการเบิก *</label>
            <input type="text" id="rqCode" v-model="formData.rqCode"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none" />
          </div>
          <!-- ช่อง "ชื่อรายการเบิก" -->
          <div>
            <label for="rqName" class="block text-sm font-medium py-1">ชื่อรายการเบิก *</label>
            <input type="text" id="rqName" v-model="formData.rqName"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none" />
          </div>

          <!-- ช่อง "วันที่เกิดค่าใช้จ่าย *" -->
          <div>
            <label for="rqPayDate" class="block text-sm font-medium py-1">วันที่เกิดค่าใช้จ่าย *</label>
            <input type="text" id="rqPayDate" v-model="formData.rqPayDate" placeholder="YYYY-MM-DD"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none" />
            <div class="relative h-[32px] w-[208px] date-picker-container">
              <SingleDatePicker v-model="formData.rqPayDate" placeholder="yyyy-mm-dd" :disabled="loading" class="w-full"
                :confirmedDate="startDate" :isOpen="startPickerOpen" @update:isOpen="startPickerOpen = $event" />
            </div>
          </div>
          <!-- ช่อง "วันที่ทำรายการเบิกค่าใช้จ่าย *" -->
          <div>
            <label for="rqWithdrawDate" class="block text-sm font-medium py-1">วันที่ทำรายการเบิกค่าใช้จ่าย *</label>
            <input type="text" id="rqWithdrawDate" placeholder="YYYY-MM-DD" v-model="formData.rqWithdrawDate"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none" />
          </div>
          <div class="content-center">
            <label for="projectName" class="block text-sm font-medium py-1">โครงการ</label>
            <div class="text-xs">
              <select id="projectName" v-model="formData.rqPjId"
                class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none">
                <option disabled selected>เลือกโครงการ</option>
                <option v-for="project in requisitionStore.projects" :key="project.pjId" :value="project.pjId">
                  {{ project.pjName }}
                </option>
              </select>
            </div>
          </div>

          <!-- ช่อง "อีเมลผู้ขอเบิกแทน *" -->
          <div>
            <label for="rqInsteadEmail" class="block text-sm font-medium py-1">อีเมลผู้ขอเบิกแทน *</label>
            <input type="text" id="rqInsteadEmail" v-model="formData.rqInsteadEmail"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none" />
          </div>
          <div>
            <div>
              <!-- Dropdown -->
              <label for="expenseType" class="block text-sm font-medium py-1">
                ประเภทค่าใช้จ่าย
              </label>
              <select id="expenseType" v-model="formData.rqRqtId" @change="updateRqtName"
                class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none">
                <option value="">กรุณาเลือกประเภท</option>
                <option v-for="requisitionTypeData in requisitionStore.requisitionType" :key="requisitionTypeData.rqtId"
                  :value="requisitionTypeData.rqtId">
                  {{ requisitionTypeData.rqtName }}
                </option>
                <!-- <option value="999">อื่นๆ</option> -->
              </select>

              <!-- Input ที่จะแสดงเมื่อเลือก ID = 2 -->
              <div v-show="rqtName == 'อื่นๆ'">
                <label for="rqAny" class="block text-sm font-medium py-1">
                  ระบุข้อมูลเพิ่มเติม
                </label>
                <input id="rqAny" v-model="formData.rqAny"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                  type="text" placeholder="กรุณาระบุข้อมูลเพิ่มเติม" />
              </div>
            </div>
          </div>
        </div>
        <div class="border border-gray-200"></div>

        <!-- Form Right -->
        <div class="w-2/5 rounded-[10px] place-items-end">
          <!-- ช่อง "ประเภทการเดินทาง" -->
          <div v-show="formData.rqRqtId === 2">
            <label for="travelType" class="block text-sm font-medium py-1">
              ประเภทการเดินทาง
            </label>
            <div class="text-xs">
              <select id="travelType"
                class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                v-model="requisitionStore.selectedTravelType">
                <option value="null" disabled selected>
                  เลือกประเภทการเดินทาง
                </option>
                <option value="private">ประเภทส่วนตัว</option>
                <option value="public">ประเภทสาธารณะ</option>
              </select>
              <img loading="lazy"
                src="https://cdn.builder.io/api/v1/image/assets/TEMP/f20eda30529a1c8726efb4a2b005d3a5b8c664e952cac725d871bbe2133f6684?placeholderIfAbsent=true&apiKey=e768e888ed824b2ebad298dfac1054a5"
                alt=""
                class="object-contain shrink-0 self-start w-4 aspect-[0.7] pointer-events-none absolute right-4 top-1/2 transform -translate-y-1/2" />
            </div>
          </div>

          <!-- ช่อง "ประเภทรถ" -->
          <div v-show="formData.rqRqtId === 2">
            <label for="vehicleType" class="block text-sm font-medium py-1">
              ประเภทรถ
            </label>
            <div class="text-xs">
              <select v-model="formData.rqVhId"
                class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none">
                <option value="null" selected disabled>เลือกประเภทรถ</option>
                <option v-for="vehicle in requisitionStore.filteredVehicleType" :key="vehicle.vhId.toString()"
                  :value="vehicle.vhId.toString()">
                  {{ vehicle.vhVehicle }}
                </option>
              </select>
              <img loading="lazy"
                src="https://cdn.builder.io/api/v1/image/assets/TEMP/f20eda30529a1c8726efb4a2b005d3a5b8c664e952cac725d871bbe2133f6684?placeholderIfAbsent=true&apiKey=e768e888ed824b2ebad298dfac1054a5"
                alt=""
                class="object-contain shrink-0 self-start w-4 aspect-[0.7] pointer-events-none absolute right-4 top-1/2 transform -translate-y-1/2" />
            </div>
          </div>
          <!-- ช่อง "สถานที่เริ่มต้น" -->
          <div v-show="rqtName === 'ค่าเดินทาง'">
            <label for="rqStartLocation" class="block text-sm font-medium py-1">สถานที่เริ่มต้น</label>
            <input type="text" id="rqStartLocation" v-model="formData.rqStartLocation"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none" />
          </div>

          <!-- ช่อง "สถานที่สิ้นสุด" -->
          <div v-show="rqtName === 'ค่าเดินทาง'">
            <label for="rqEndLocation" class="block text-sm font-medium py-1">สถานที่สิ้นสุด</label>
            <input type="text" id="rqEndLocation" v-model="formData.rqEndLocation"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none" />
          </div>

          <!-- ช่อง "ระยะทาง" -->
          <div v-show="rqtName === 'ค่าเดินทาง'">
            <label for="rqEndLocation" class="block text-sm font-medium py-1">ระยะทาง</label>
            <input type="text" id="rqEndLocation" v-model="formData.rqDistance"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none" />
          </div>

          <!-- ช่อง "สถาน *" -->
          <!-- <div v-if="rqtId !== 'ค่าเดินทาง'" class="m-4">
            <label for="rqLocation" class="block text-sm font-medium py-1"
              >สถาน *</label
            >
            <input
              type="text"
              id="rqLocation"
              v-model="formData.rqLocation"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
          </div> -->

          <div>
            <!-- ช่อง "จำนวนเงิน (บาท)" -->
            <div>
              <label for="rqExpenses" class="block text-sm font-medium py-1">จำนวนเงิน (บาท)</label>
              <input type="number" id="rqExpenses" v-model="formData.rqExpenses"
                class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none" />
            </div>
          </div>
        </div>
      </div>
      <!-- วัตถุประสงค์ -->
      <div class="text-sm m-[38px]">
        <label class="block text-sm font-medium py-1">วัตถุประสงค์</label>
        <div class="">
          <textarea v-model="formData.rqPurpose"
            class="py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full focus:border-gray-400 focus:ring-0 focus:outline-none"></textarea>
        </div>
      </div>
      <!-- upload -->
      <div class="upload-container w-2/6 m-[38px]">
        <label class="z-0 max-md:max-w-full"> อัปโหลดไฟล์ </label>
        <div
          class="flex z-0 mt-1 w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[395px] max-md:max-w-full cursor-pointer relative"
          @click="triggerFileInput" @dragover.prevent @drop.prevent="handleDrop">
          <input type="file" ref="fileInput" @change="handleFileChange" accept="image/" style="display: none" />
          <div v-if="!selectedFile"
            class="flex flex-col items-center justify-center absolute inset-0 text-sm text-[color:var(--,#B8B8B8)]">
            <img loading="lazy"
              src="https://cdn.builder.io/api/v1/image/assets/TEMP/5da245b200f054a57a812257a8291e28aacdd77733a878e94699b2587a54360d?placeholderIfAbsent=true&apiKey=963991dcf23f4b60964b821ef12710c5"
              alt="Upload icon" class="object-contain w-16 aspect-[1.1]" />
            <p class="mt-3">อัปโหลดไฟล์ที่นี่</p>
            <p class="mt-3">SVG, PNG หรือ JPG (MAX 800 800 px)</p>
          </div>
          <img v-else :src="previewUrl!" alt="Preview"
            class="max-w-full max-h-full object-contain absolute inset-0 m-auto" />
        </div>
      </div>
    </div>

    <!-- Popup บันทึก -->
    <div v-if="isPopupSaveOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
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
          ยืนยันการบันทึกคำขอเบิกค่าใช้จ่าย
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยืนยันการบันทึกคำขอเบิกค่าใช้จ่ายหรือไม่ ?
        </h2>
        <div class="flex justify-center space-x-4">
          <button @click="closePopupSave"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยกเลิก
          </button>
          <button @click="confirmSave"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยืนยัน
          </button>
        </div>
      </div>
    </div>

    <!-- Popup ยกเลิก -->
    <div v-if="isPopupCancleOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
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
          ยกเลิกการทำรายการเบิกค่าใช้จ่าย
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยกเลิกการทำรายการเบิกค่าใช้จ่ายหรือไม่ ?
        </h2>
        <div class="flex justify-center space-x-4">
          <button @click="closePopupCancle"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยกเลิก
          </button>
          <button @click="confirmCancle"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยืนยัน
          </button>
        </div>
      </div>
    </div>

    <!-- Popup ยืนยัน -->
    <div v-if="isPopupSubmitOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
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
          ยืนยันการทำรายการเบิกค่าใช้จ่าย
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยืนยันการทำรายการเบิกค่าใช้จ่ายหรือไม่ ?
        </h2>
        <div class="flex justify-center space-x-4">
          <button @click="closePopupSubmit"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยกเลิก
          </button>
          <button @click="confirmSubmit"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
            ยืนยัน
          </button>
        </div>
      </div>
    </div>

    <!-- Alert -->
    <div v-if="isAlertSaveOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
        <div class="mb-4">
          <svg :class="`w-[96px] h-[96px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mt-3">
          บันทึกการทำรายการเบิกค่าใช้จ่ายสำเร็จ
        </h2>
      </div>
    </div>

    <div v-if="isAlertCancleOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
        <div class="mb-4">
          <svg :class="`w-[96px] h-[96px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-3">
          ยกเลิกการทำรายการเบิกค่าใช้จ่ายสำเร็จ
        </h2>
      </div>
    </div>

    <div v-if="isAlertSubmitOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
        <div class="mb-4">
          <svg :class="`w-[96px] h-[96px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-3">
          ยืนยันการทำรายการเบิกค่าใช้จ่ายสำเร็จ
        </h2>
      </div>
    </div>
  </form>
</template>
