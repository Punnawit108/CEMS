<script setup lang="ts">
/*
 * ชื่อไฟล์: CreateExpenseForm.vue
 * คำอธิบาย: ไฟล์นี้แสดงฟอร์มเบิกค่าใช้จ่าย
 * ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข: 28 พฤศจิกายน 2567
 */

import { onMounted, ref, computed } from "vue";
import Button from "../../components/Buttons/Button.vue";
import { useRequisitionStore } from "../../store/requisition";
import router from "../../router";
import { useRoute } from "vue-router";

const requisitionStore = useRequisitionStore();
const route = useRoute();
const id = String(route.params.id);

const rqtName = ref(""); // ค่าเริ่มต้นสำหรับประเภทค่าใช้จ่าย
const customExpenseType = ref(""); // ค่าเริ่มต้นสำหรับประเภทที่กำหนดเอง
const isOtherSelected = ref(false); // เช็คว่าเลือก 'อื่นๆ' หรือไม่
const isCustomExpenseTypeAdded = ref(false); // เช็คว่าได้เพิ่มประเภทใหม่หรือยัง
const isPopupSaveOpen = ref(false); // สำหรับเปิด/ปิด Popup  บันทึก
const isPopupSubmitOpen = ref(false); // สำหรับเปิด/ปิด Popup  ยืนยัน
const isAlertSaveOpen = ref(false); // ควบคุมการแสดง Alert บันทึก
const isAlertSubmitOpen = ref(false); // ควบคุมการแสดง Alert ยืนยัน

let formData: any = ref({});

onMounted(async () => {
  await requisitionStore.getAllProject();
  await requisitionStore.getAllRequisitionType();
  await requisitionStore.getAllvehicleType();
  formData.value = await requisitionStore.getExpenseById(id);
});

const fileInput = ref<HTMLInputElement | null>(null); // อ้างอิงถึง input element
const selectedFile = ref<File | null>(null); // เก็บไฟล์ที่ผู้ใช้เลือก
const previewUrl = ref<string | null>(null); // URL สำหรับแสดงตัวอย่างรูปภาพ

// ค่าคงที่สำหรับกำหนดขนาดสูงสุดของรูปภาพ
const maxWidth = 800; // ความกว้างสูงสุดที่ยอมรับ (พิกเซล)
const maxHeight = 800; // ความสูงสูงสุดที่ยอมรับ (พิกเซล)

// ฟังก์ชันสำหรับเปิด file input dialog
const triggerFileInput = () => {
  fileInput.value?.click();
};

// ฟังก์ชันจัดการเมื่อมีการเลือกไฟล์ผ่าน file input
const handleFileChange = async (event: Event) => {
  const target = event.target as HTMLInputElement;
  if (target.files && target.files.length > 0) {
    const file = target.files[0]; // เลือกไฟล์แรก
    await uploadFile(file); // ส่งไฟล์ไปประมวลผล
  }
};

// ฟังก์ชันจัดการเมื่อมีการลากไฟล์มาวาง (drag & drop)
// const handleDrop = async (event: DragEvent) => {
//   if (event.dataTransfer?.files.length) {
//     const file = event.dataTransfer.files[0];
//     await uploadFile(file);
//   }
// };
// ฟังก์ชันจัดการเมื่อมีการลากไฟล์มาวาง (drag & drop)
const handleDrop = (event: DragEvent) => {
  if (event.dataTransfer?.files.length) {
    uploadFile(event.dataTransfer.files[0]); // ส่งไฟล์แรกที่ถูกลากมาวางไปประมวลผล
  }
};

// ฟังก์ชันตรวจสอบขนาดของรูปภาพ
const checkImageDimensions = (file: File): Promise<boolean> => {
  return new Promise((resolve) => {
    const img = new Image();
    img.onload = () => {
      URL.revokeObjectURL(img.src); // จัดการหน่วยความจำ
      resolve(img.width <= maxWidth && img.height <= maxHeight);
    };
    img.src = URL.createObjectURL(file); // สร้าง URL สำหรับตรวจสอบขนาด
  });
};

const resetFile = () => {
  if (formData.rqProof) {
    URL.revokeObjectURL(formData.rqProof.value); // ลบ URL เก่า
  }
  formData.rqProof = null; // รีเซ็ตตัวแปรเป็นค่าว่าง
};

// ฟังก์ชันหลักในการจัดการไฟล์ที่อัปโหลด
const uploadFile = async (file: File) => {
  // ตรวจสอบประเภทไฟล์
  if (!["image/svg+xml", "image/png", "image/jpeg"].includes(file.type)) {
    alert("กรุณาอัปโหลดไฟล์ SVG, PNG หรือ JPG เท่านั้น");
    return;
  }
  // ตรวจสอบขนาดรูปภาพ
  const isValidSize = await checkImageDimensions(file);
  if (isValidSize) {
    selectedFile.value = file; // เก็บไฟล์ที่ผ่านการตรวจสอบ
    previewUrl.value = URL.createObjectURL(file); // สร้าง URL สำหรับแสดงตัวอย่าง
    formData.value.rqProof = await convertToBase64(file);
  } else {
    alert(
      `กรุณาอัปโหลดรูปภาพที่มีขนาดไม่เกิน ${maxWidth} x ${maxHeight} พิกเซล`
    );
    // รีเซ็ตค่าเมื่อไฟล์ไม่ถูกต้อง
    selectedFile.value = null;
    previewUrl.value = null;
  }
};
// const isValidSize = await checkImageDimensions(file);
// if (!isValidSize) {
//   alert(
//     `กรุณาอัปโหลดรูปภาพที่มีขนาดไม่เกิน ${maxWidth} x ${maxHeight} พิกเซล`
//   );
//   resetFile(); // ล้างข้อมูลเก่า
//   return;
// }

// รีเซ็ต URL เก่าหากมี
//   if (formData.rqProof) {
//     URL.revokeObjectURL(formData.rqProof.value); // ปลดปล่อย URL เดิม
//   }

//   // อัปเดต URL ใหม่
//   formData.rqProof = URL.createObjectURL(file); // สร้าง URL ใหม่สำหรับรูปภาพ
// };
const handleSelectChange = () => {
  if (rqtName.value === "อื่นๆ") {
    isOtherSelected.value = true; // แสดงช่องกรอกข้อมูลเมื่อเลือก "อื่นๆ"
  } else {
    isOtherSelected.value = false; // ซ่อนช่องกรอกข้อมูลเมื่อเลือกประเภทอื่น
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
  formData.value.rqStatus = "waiting";
  try {
    // เรียกใช้งานฟังก์ชัน updateExpense เพื่ออัปเดตข้อมูล
    const data = await requisitionStore.updateExpense(id, formData.value);

    // ตรวจสอบว่า data ที่ได้รับจาก API ไม่เป็น null หรือ undefined

    // router.push("/disbursement/listWithdraw");
  } catch (error) {
    console.log("Error occurred during form submission:", error); // แสดงข้อผิดพลาด
    alert("There was an error while submitting the form.");
  }
};

const handleSave = async () => {
  formData.value.rqStatus = "sketch";
  const data = await requisitionStore.updateExpense(id, formData.value);
  if (data) {
    router.push("/disbursement/listWithdraw");
  } else {
    alert("Something went wrong");
  }
};

const handleCancel = () => {
  // Reset form data or navigate away
  router.push("/disbursement/listWithdraw");
};

const filteredVehicleType = computed(() => {
  if (formData.value.rqVht === "private") {
    return requisitionStore.vehicleType.filter(
      (vehicle) => vehicle.vhType === "private"
    );
  } else if (formData.value.rqVht === "public") {
    return requisitionStore.vehicleType.filter(
      (vehicle) => vehicle.vhType === "public"
    );
  }
  return [];
});

// เปิด/ปิด Popup บันทึก ผู้อนุมัติ
const openPopupSave = () => {
  isPopupSaveOpen.value = true;
};
const closePopupSave = () => {
  isPopupSaveOpen.value = false;
};

// เปิด/ปิด Popup ยืนยัน ผู้อนุมัติ
const openPopupSubmit = () => {
  isPopupSubmitOpen.value = true;
};
const closePopupSubmit = () => {
  isPopupSubmitOpen.value = false;
};

// เปิด/ปิด Alert บันทึก
const confirmSave = async () => {
  // เปิด Popup Alert
  isAlertSaveOpen.value = true;
  setTimeout(() => {
    isAlertSaveOpen.value = false; // ปิด Alert
    closePopupSave(); // ปิด Popup แก้ไข
  }, 1500); // 1.5 วินาที
};

// เปิด/ปิด Alert ยืนยัน
const confirmSubmit = async () => {
  // เปิด Popup Alert
  isAlertSubmitOpen.value = true;
  setTimeout(() => {
    isAlertSubmitOpen.value = false; // ปิด Alert
    closePopupSubmit(); // ปิด Popup แก้ไข
  }, 1500); // 1.5 วินาที
};
</script>
<template>
  <form @submit.prevent="handleSubmit" class="text-black text-sm">
    <!-- btn -->
    <div class="flex justify-end gap-4">
      <Button :type="'btn-save'" @click="openPopupSave"></Button>
      <Button :type="'btn-cancleBorderGray'" @click="handleCancel"></Button>
      <Button :type="'btn-summit'" @click="openPopupSubmit"></Button>
    </div>
    <!-- Fromประเภทค่าเดินทาง-->
    <div class="">
      <!-- แบ่งเป็น 2 คอลัมน์ -->
      <div class="flex flex-col md:flex-row justify-around gap-5">
        <!-- Form Left -->
        <div class="w-1/2 rounded-[10px]">
          <!-- ช่อง "รหัสรายการเบิก *" -->
          <div class="m-4">
            <label for="rqCode" class="block text-sm font-medium py-1"
              >รหัสรายการเบิก *</label
            >
            <input
              type="text"
              id="rqCode"
              v-model="formData.rqCode"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
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

          <!-- ช่อง "วันที่เกิดค่าใช้จ่าย *" -->
          <div class="m-4">
            <label for="rqPayDate" class="block text-sm font-medium py-1"
              >วันที่เกิดค่าใช้จ่าย *</label
            >
            <input
              type="text"
              id="rqPayDate"
              v-model="formData.rqPayDate"
              placeholder="YYYY-MM-DD"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
          </div>
          <!-- ช่อง "วันที่ทำรายการเบิกค่าใช้จ่าย *" -->
          <div class="m-4">
            <label for="rqWithdrawDate" class="block text-sm font-medium py-1"
              >วันที่ทำรายการเบิกค่าใช้จ่าย *</label
            >
            <input
              type="text"
              id="rqWithDrawDate"
              placeholder="YYYY-MM-DD"
              v-model="formData.rqWithDrawDate"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
          </div>
          <div class="content-center m-4">
            <label for="projectName" class="self-start text-sm">โครงการ</label>
            <div class="text-xs">
              <select
                id="projectName"
                v-model="formData.rqPjId"
                class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
              >
                <option disabled selected>เลือกโครงการ</option>
                <option
                  v-for="project in requisitionStore.projects"
                  :key="project.rqPjName"
                  :value="project.PjId"
                >
                  {{ project.pjName }}
                </option>
              </select>+
            </div>
          </div>

          <!-- ช่อง "อีเมลผู้ขอเบิกแทน *" -->
          <div class="m-4">
            <label for="rqInsteadEmail" class="block text-sm font-medium py-1"
              >อีเมลผู้ขอเบิกแทน *</label
            >
            <input
              type="text"
              id="rqInsteadEmail"
              v-model="formData.rqInsteadEmail"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
          </div>
        </div>
        <div class="border border-gray-200"></div>

        <!-- Form Right -->
        <div class="w-1/2 rounded-[10px] place-items-end">
          <div class="m-4">
            <label
              for="selectExpenseType"
              class="block text-sm font-medium py-1"
              >ประเภทค่าใช้จ่าย</label
            >
            <div class="relative">
              <select
                id="selectExpenseType"
                v-model="formData.rqRqtId"
                @change="handleSelectChange"
                class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
              >
                <option disabled selected>เลือกประเภทค่าใช้จ่าย</option>
                <option
                  v-for="requisitionTypeData in requisitionStore.requisitionType"
                  :key="requisitionTypeData.rqtId"
                  :value="requisitionTypeData.rqRqtName"
                >
                  {{ requisitionTypeData.rqtName }}
                </option>

                <!-- Option for custom expense type -->
                <option value="อื่นๆ" v-if="!isCustomExpenseTypeAdded">
                  อื่นๆ
                </option>
              </select>

              <!-- Input for custom expense type when "อื่นๆ" is selected -->
              <input
                v-if="isOtherSelected"
                v-model="customExpenseType"
                placeholder="กรุณาระบุประเภทค่าใช้จ่าย"
                class="absolute top-0 left-1 mt-[1.5px] px-3 py-3 border-1 border-grayDark bg-white rounded-md sm:text-sm text-sm focus:border-gray-400 focus:ring-0 focus:outline-none"
                style="width: calc(50% - 16px)"
              />
            </div>
          </div>
          <!-- ช่อง "ประเภทการเดินทาง" -->
          <div class="m-4" v-if="formData.rqRqtId === 2">
            <label for="travelType" class="block text-sm font-medium py-1">
              ประเภทการเดินทาง
            </label>
            <div class="text-xs">
              <select
                id="travelType"
                class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                v-model="formData.rqVht"
              >
                <option value="null" disabled selected>
                  เลือกประเภทการเดินทาง
                </option>
                <option value="private">ประเภทส่วนตัว</option>
                <option value="public">ประเภทสาธารณะ</option>
              </select>
              <img
                loading="lazy"
                src="https://cdn.builder.io/api/v1/image/assets/TEMP/f20eda30529a1c8726efb4a2b005d3a5b8c664e952cac725d871bbe2133f6684?placeholderIfAbsent=true&apiKey=e768e888ed824b2ebad298dfac1054a5"
                alt=""
                class="object-contain shrink-0 self-start w-4 aspect-[0.7] pointer-events-none absolute right-4 top-1/2 transform -translate-y-1/2"
              />
            </div>
          </div>

          <!-- ช่อง "ประเภทรถ" -->
          <div class="m-4" v-if="formData.rqRqtId === 2">
            <label for="travelType" class="block text-sm font-medium py-1">
              ประเภทรถ
            </label>
            <div class="text-xs">
              <select
                class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                v-model="formData.rqVhId"
              >
                <option value="null" selected disabled>เลือกประเภทรถ</option>
                <option
                  v-for="vehicle in filteredVehicleType"
                  :key="vehicle.vhId"
                  :value="vehicle.vhId"
                >
                  {{ vehicle.vhVehicle }}
                </option>
              </select>
              <img
                loading="lazy"
                src="https://cdn.builder.io/api/v1/image/assets/TEMP/f20eda30529a1c8726efb4a2b005d3a5b8c664e952cac725d871bbe2133f6684?placeholderIfAbsent=true&apiKey=e768e888ed824b2ebad298dfac1054a5"
                alt=""
                class="object-contain shrink-0 self-start w-4 aspect-[0.7] pointer-events-none absolute right-4 top-1/2 transform -translate-y-1/2"
              />
            </div>
          </div>

          <!-- ช่อง "สถานที่เริ่มต้น" -->
          <div v-show="formData.rqRqtId === 2" class="m-4">
            <label for="rqStartLocation" class="block text-sm font-medium py-1"
              >สถานที่เริ่มต้น</label
            >
            <input
              type="text"
              id="rqStartLocation"
              v-model="formData.rqStartLocation"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
          </div>

          <!-- ช่อง "สถานที่สิ้นสุด" -->
          <div v-show="formData.rqRqtId === 2" class="m-4">
            <label for="rqEndLocation" class="block text-sm font-medium py-1"
              >สถานที่สิ้นสุด</label
            >
            <input
              type="text"
              id="rqEndLocation"
              v-model="formData.rqEndLocation"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
          </div>

          <!-- ช่อง "ระยะทาง" -->
          <div v-show="formData.rqRqtId === 2" class="m-4">
            <label for="rqEndLocation" class="block text-sm font-medium py-1"
              >ระยะทาง</label
            >
            <input
              type="text"
              id="rqEndLocation"
              v-model="formData.rqDistance"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
          </div>

          <div class="m-4">
            <!-- ช่อง "จำนวนเงิน (บาท)" -->
            <div>
              <label for="rqExpenses" class="block text-sm font-medium py-1"
                >จำนวนเงิน (บาท)</label
              >
              <input
                type="text"
                id="rqExpenses"
                v-model="formData.rqExpenses"
                class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- วัตถุประสงค์ -->
    <div class="text-sm m-5">
      <label class="block text-sm font-medium py-1">วัตถุประสงค์</label>
      <div class="">
        <textarea
          v-model="formData.rqPurpose"
          class="py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full focus:border-gray-400 focus:ring-0 focus:outline-none"
        ></textarea>
      </div>
    </div>

    <!-- upload -->
    <div class="upload-container w-2/6 m-5">
      <label class="z-0 max-md:max-w-full"> อัปโหลดไฟล์ </label>
      <div
        class="flex z-0 mt-1 w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[395px] max-md:max-w-full cursor-pointer relative"
        @click="triggerFileInput"
        @dragover.prevent
        @drop.prevent="handleDrop"
      >
        <input
          type="file"
          ref="fileInput"
          @change="handleFileChange"
          accept="image/"
          style="display: none"
        />
        <div
          v-if="!selectedFile"
          class="flex flex-col items-center justify-center absolute inset-0 text-sm text-[color:var(--,#B8B8B8)]"
        >
          <img
            loading="lazy"
            src="https://cdn.builder.io/api/v1/image/assets/TEMP/5da245b200f054a57a812257a8291e28aacdd77733a878e94699b2587a54360d?placeholderIfAbsent=true&apiKey=963991dcf23f4b60964b821ef12710c5"
            alt="Upload icon"
            class="object-contain w-16 aspect-[1.1]"
          />
          <p class="mt-3">อัปโหลดไฟล์ที่นี่</p>
          <p class="mt-3">SVG, PNG หรือ JPG (MAX 800 800 px)</p>
        </div>
        <div
          v-if="selectedFile"
          class="flex flex-col items-center justify-center absolute inset-0 text-sm text-[color:var(--,#B8B8B8)]"
        >
          <img :src="formData.rqProof" />
        </div>
        <img
          v-else
          :src="formData.rqProof!"
          alt="Preview"
          class="max-w-full max-h-full object-contain absolute inset-0 m-auto"
        />
      </div>
    </div>
    <!-- Popup บันทึก -->
    <div
      v-if="isPopupSaveOpen"
      class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
    >
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center"
      >
        <div class="flex justify-center mb-4">
          <svg
            :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            width="20"
            height="20"
            fill="#FFBE40"
            viewBox="0 0 24 24"
          >
            <path
              fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd"
            />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-4">
          ยืนยันการบันทึกคำขอเบิกค่าใช้จ่าย
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยืนยันการบันทึกคำขอเบิกค่าใช้จ่ายหรือไม่ ?
        </h2>
        <div class="flex justify-center space-x-4">
          <button
            @click="closePopupSave"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
          >
            ยกเลิก
          </button>
          <button
            @click="confirmSave"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
          >
            ยืนยัน
          </button>
        </div>
      </div>
    </div>

    <!-- Popup ยืนยัน -->
    <div
      v-if="isPopupSubmitOpen"
      class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
    >
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center"
      >
        <div class="flex justify-center mb-4">
          <svg
            :class="`w-[72px] h-[72px] text-gray-800 dark:text-white`"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            width="20"
            height="20"
            fill="#FFBE40"
            viewBox="0 0 24 24"
          >
            <path
              fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd"
            />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-4">
          ยืนยันการแก้ไขคำขอเบิกค่าใช้จ่าย
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยืนยันการแก้ไขคำขอเบิกค่าใช้จ่ายหรือไม่ ?
        </h2>
        <div class="flex justify-center space-x-4">
          <button
            @click="closePopupSubmit"
            class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
          >
            ยกเลิก
          </button>
          <button
            @click="confirmSubmit"
            class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
          >
            ยืนยัน
          </button>
        </div>
      </div>
    </div>

    <!-- Alert -->
    <div
      v-if="isAlertSaveOpen"
      class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
    >
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center"
      >
        <div class="mb-4">
          <svg
            :class="`w-[96px] h-[96px] text-gray-800 dark:text-white`"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            width="24"
            height="24"
            fill="green"
            viewBox="0 0 24 24"
          >
            <path
              fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
              clip-rule="evenodd"
            />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mt-3">
          ยืนยันการบันทึกคำขอเบิกค่าใช้จ่ายสำเร็จ
        </h2>
      </div>
    </div>

    <div
      v-if="isAlertSubmitOpen"
      class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
    >
      <div
        class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center"
      >
        <div class="mb-4">
          <svg
            :class="`w-[96px] h-[96px] text-gray-800 dark:text-white`"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            width="24"
            height="24"
            fill="green"
            viewBox="0 0 24 24"
          >
            <path
              fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
              clip-rule="evenodd"
            />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-3">
          ยืนยันการแก้ไขคำขอเบิกค่าใช้จ่ายสำเร็จ
        </h2>
      </div>
    </div>
  </form>
</template>
