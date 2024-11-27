<script setup lang="ts">
/**
 * ชื่อไฟล์ : CreateExpenseForm.vue
 * คำอธิบาย : ไฟล์นี้แสดงฟอร์มเบิกค่าใช้จ่าย
 * Input : ข้อมูลฟอร์มเบิกค่าใช้จ่าย
 * Output : -
 * ชื่อผู้เขียน / แก้ไข : อังคณา อุ่นเสียม
 * วันที่จัดทำ / วัยที่แก้ไข : 11 พฤศจิกายน 2567
 */
import { ref } from "vue";
import VueDatePicker from "@vuepic/vue-datepicker";
import Button from "../../components/template/Button.vue";
import "@vuepic/vue-datepicker/dist/main.css";

const date = ref();

const rqRqtName = ref("ค่าเดินทาง");

const formData = ref({
  rqId: "",
  rqName: "",
  rqUsrName: "",
  rqPjName: "1",
  rqRqtName: "",
  rqVhName: "",
  rqDatePay: "",
  rqDateWithdraw: "",
  rqCode: "",
  rqInsteadEmail: "",
  rqExpenses: "",
  rqLocation: "",
  rqStartLocation: "",
  rqEndLocation: "",
  rqDistance: "",
  rqPurpose: "",
  rqReason: "",
  rqProof: "",
  rqStatus: "",
  rqProgress: "",
  preview: null,
});

const fileInput = ref<HTMLInputElement | null>(null); // อ้างอิงถึง input element ที่ใช้เลือกไฟล์
const selectedFile = ref<File | null>(null); // เก็บไฟล์ที่ผู้ใช้เลือก
const previewUrl = ref<string | null>(null); // URL สำหรับแสดงตัวอย่างรูปภาพ

// ค่าคงที่สำหรับกำหนดขนาดสูงสุดของรูปภาพ
const MAX_WIDTH = 800; // ความกว้างสูงสุดที่ยอมรับ (พิกเซล)
const MAX_HEIGHT = 800; // ความสูงสูงสุดที่ยอมรับ (พิกเซล)

// ฟังก์ชันสำหรับเปิด file input dialog
const triggerFileInput = () => {
  fileInput.value?.click(); // จำลองการคลิกที่ input file เมื่อผู้ใช้คลิกที่พื้นที่ drop zone
};

// ฟังก์ชันจัดการเมื่อมีการเลือกไฟล์ผ่าน file input
const handleFileChange = (event: Event) => {
  const target = event.target as HTMLInputElement;
  if (target.files && target.files.length > 0) {
    const file = target.files[0]; // เลือกไฟล์แรกที่ผู้ใช้เลือก
    uploadFile(file); // ส่งไฟล์ไปประมวลผล
  }
};

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
      URL.revokeObjectURL(img.src); // คืน URL object เพื่อเป็นการจัดการหน่วยความจำ
      resolve(img.width <= MAX_WIDTH && img.height <= MAX_HEIGHT); // ตรวจสอบว่าขนาดไม่เกินที่กำหนด
    };
    img.src = URL.createObjectURL(file); // สร้าง URL สำหรับรูปภาพเพื่อตรวจสอบขนาด
  });
};

// ฟังก์ชันหลักในการจัดการไฟล์ที่อัปโหลด
const uploadFile = async (file: File) => {
  // ตรวจสอบประเภทไฟล์ว่าเป็น SVG, PNG หรือ JPG
  if (!["image/svg+xml", "image/png", "image/jpeg"].includes(file.type)) {
    alert("กรุณาอัปโหลดไฟล์ SVG, PNG หรือ JPG เท่านั้น");
    return;
  }

  // ตรวจสอบขนาดรูปภาพ
  const isValidSize = await checkImageDimensions(file);
  if (isValidSize) {
    selectedFile.value = file; // เก็บไฟล์ที่ผ่านการตรวจสอบ
    previewUrl.value = URL.createObjectURL(file); // สร้าง URL สำหรับแสดงตัวอย่าง
  } else {
    alert(
      `กรุณาอัปโหลดรูปภาพที่มีขนาดไม่เกิน ${MAX_WIDTH} x ${MAX_HEIGHT} พิกเซล`
    );
    // รีเซ็ตค่าเมื่อไฟล์ไม่ถูกต้อง
    selectedFile.value = null;
    previewUrl.value = null;
  }
};
</script>

<template>
  <div class="bg-black-500">
    <form @submit.prevent="handleSubmit" class="space-y-4">
      <div class="flex justify-end gap-x-4">
        <Button :type="'btn-save'"></Button>
        <Button :type="'btn-cancleBorderGray'"></Button>
        <Button :type="'btn-approve'"></Button>
      </div>
      <div>
        <div>
          <label for="rqCode" class="block text-sm font-medium py-1"
            >รหัสรายการเบิก</label
          >
          <input
            type="text"
            id="rqCode"
            v-model="formData.rqCode"
            class="px-3 py-2 border border-gray-400 bg-white rounded-md md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
          />
        </div>

        <div>
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

        <div
          class="content-center flex justify-between flex flex-col md:flex-row"
        >
          <div class="content-center">
            <label for="rqPjName" class="self-start text-sm">โครงการ</label>
            <div class="text-xs">
              <select
                v-model="rqPjName"
                id="rqPjName"
                class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
              >
                <option>เลือกโครงการ</option>
                <option>อบรมการเงิน</option>
                <option>อบรมการบริหาร</option>
              </select>
            </div>
          </div>
        </div>
        <div class="">
          <label for="selectExpenseType" class="text-sm"
            >ประเภทค่าใช้จ่าย</label
          >
          <div class="relative">
            <select
              id="selectExpenseType"
              v-model="rqRqtName"
              @change="handleSelectChange"
              class="py-2 border border-gray-400 bg-white rounded-md sm:text-sm text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            >
              <option value="">เลือกประเภทค่าใช้จ่าย</option>
              <option
                v-for="option in expenseOptions"
                :key="option"
                :value="option"
              >
                {{ option }}
              </option>
              <option value="อื่นๆ" v-if="!isCustomExpenseTypeAdded">
                อื่นๆ
              </option>
            </select>
            <input
              v-if="isOtherSelected"
              v-model="customExpenseType"
              @keyup.enter="addCustomExpense"
              placeholder="กรุณาระบุประเภทค่าใช้จ่าย"
              class="absolute top-0 left-0 mt-2 px-3 py-0 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
              style="width: calc(100% - 16px)"
            />
          </div>
        </div>
        <div>
          <label for="travelType" class="self-start text-sm"
            >ประเภทการเดินทาง</label
          >
          <div class="text-xs">
            <select
              id="travelType"
              class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            >
              <option>เลือกประเภทการเดินทาง</option>
              <option>ประเภทส่วนตัว</option>
              <option>ประเภทสาธารณะ</option>
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
        <div>
          <label for="vehicleType" class="self-start text-sm">ประเภทรถ</label>
          <div class="text-xs">
            <select
              id="vehicleType"
              class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            >
              <option>เลือกประเภทรถ</option>
              <option>รถยนต์</option>
              <option>รถจักรยานยนต์</option>
            </select>
            <img
              loading="lazy"
              src="https://cdn.builder.io/api/v1/image/assets/TEMP/f20eda30529a1c8726efb4a2b005d3a5b8c664e952cac725d871bbe2133f6684?placeholderIfAbsent=true&apiKey=e768e888ed824b2ebad298dfac1054a5"
              alt=""
              class="object-contain shrink-0 self-start w-4 aspect-[0.7] pointer-events-none absolute right-4 top-1/2 transform -translate-y-1/2"
            />
          </div>
        </div>

        <!-- ช่อง "วันที่เกิดค่าใช้จ่าย *" -->
        <div>
          <label for="rqStartLocation" class="block text-sm font-medium py-1"
            >วันที่เกิดค่าใช้จ่าย *</label
          >
          <VueDatePicker v-model="rqDateWithdraw" vertical />
        </div>
        <!-- ช่อง "วันที่ทำรายการเบิกค่าใช้จ่าย *" -->
        <div>
          <label for="rqStartLocation" class="block text-sm font-medium py-1"
            >วันที่ทำรายการเบิกค่าใช้จ่าย *</label
          >
          <VueDatePicker v-model="rqDatePay" vertical />
        </div>

        <!-- ช่อง "อีเมลผู้ขอเบิกแทน *" -->
        <div>
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
        <div>
          <label for="rqLocation" class="block text-sm font-medium py-1"
            >สถานที่ *</label
          >
          <input
            type="text"
            id="rqLocation"
            v-model="formData.rqLocation"
            class="px-3 py-2 border border-gray-400 bg-white rounded-md md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
          />
        </div>
        <!-- ช่อง "สถานที่เริ่มต้น" -->
        <div>
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
        <div>
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
        <div>
          <label for="rqDistance" class="block text-sm font-medium py-1"
            >ระยะทาง</label
          >
          <input
            type="text"
            id="rqDistance"
            v-model="formData.rqDistance"
            class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
          />
        </div>

        <div class="text-sm">
          <label>วัตถุประสงค์</label>
          <div class="md:w-[400px]">
            <textarea
              v-model="formData.rqPurpose"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:h-[120px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            ></textarea>
          </div>
        </div>
      </div>
      <div>
        <section class="flex relative flex-col leading-snug max-w-[536px]">
          <label class="z-0 text-base font-bold text-black max-md:max-w-full">
            แนบหลักฐานการเบิก
          </label>
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
            <img
              v-else
              :src="previewUrl!"
              alt="Preview"
              class="max-w-full max-h-full object-contain absolute inset-0 m-auto"
            />
          </div>
        </section>
      </div>
    </form>
  </div>
</template>
