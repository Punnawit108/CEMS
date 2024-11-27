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
import { createTodo } from "../../store/CreateExpenseForm"; // Import createTodo function
import "@vuepic/vue-datepicker/dist/main.css";

const date = ref();
const expenseOptions = ref(["ค่าเดินทาง", "ค่าอาหาร"]);
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

const onFileChange = (event) => {
  const file = event.target.files[0];
  if (file && file.type.startsWith("image/")) {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (e) => {
      formData.value.preview = e.target.result;
    };
  }
};

const handleSubmit = async () => {
  // ตรวจสอบข้อมูลก่อนส่ง
  if (!formData.value.rqName) {
    alert("กรุณากรอกชื่อรายการเบิก!");
    return;
  }
  if (!formData.value.rqDatePay) {
    alert("กรุณากรอกวันที่เกิดค่าใช้จ่าย!");
    return;
  }
  if (rqRqtName.value !== "ค่าอาหาร" && !formData.value.rqStartLocation) {
    alert("กรุณากรอกสถานที่เริ่มต้น!");
    return;
  }

  try {
    // ส่งข้อมูลไปยัง API
    await createTodo(formData.value);
    alert("ส่งข้อมูลสำเร็จ!");
    resetForm(); // รีเซ็ตฟอร์มหลังจากส่งข้อมูล
  } catch (error: AxiosError) {
    console.error("Error submitting data:", error.response?.data);
    alert(
      `เกิดข้อผิดพลาด: ${
        error.response?.data?.message || "ไม่สามารถส่งข้อมูลได้"
      }`
    );
  }
};

const handleCancel = () => {
  // Reset form data or navigate away
  alert("ยกเลิกการส่งข้อมูล");
  resetForm();
};

const handleSave = () => {
  // Save data to local storage or draft, not submit
  alert("บันทึกข้อมูลไว้แล้ว");
};

const resetForm = () => {
  formData.value = {
    rqId: "",
    rqName: "",
    rqUsrName: "",
    rqPjName: "1",
    rqRqtName: rqRqtName.value,
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
  };
};
</script>
<template>
  <form @submit.prevent="handleSubmit" class="text-black text-sm">
    <!-- btn -->
    <div class="flex justify-end gap-4">
      <Button :type="'btn-save'"></Button>
      <Button :type="'btn-cancleBorderGray'"></Button>
      <Button :type="'btn-summit'"></Button>
    </div>
    <!-- Fromประเภทค่าเดินทาง-->
    <div class="">
      <!-- แบ่งเป็น 2 คอลัมน์ -->
      <div class="flex flex-col md:flex-row justify-between gap-5">
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
            <label for="rqDatePay" class="block text-sm font-medium py-1"
              >วันที่เกิดค่าใช้จ่าย *</label
            >
            <input
              type="text"
              id="rqDatePay"
              v-model="formData.rqDatePay"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
          </div>
          <!-- ช่อง "วันที่ทำรายการเบิกค่าใช้จ่าย *" -->
          <div class="m-4">
            <label for="rqDateWithdraw" class="block text-sm font-medium py-1"
              >วันที่ทำรายการเบิกค่าใช้จ่าย *</label
            >
            <input
              type="text"
              id="rqDateWithdraw"
              v-model="formData.rqDateWithdraw"
              class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            />
          </div>
          <div class="content-center m-4">
            <label for="rqPjName" class="block text-sm font-medium py-1"
              >โครงการ</label
            >
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
        <div class="w-1/2 rounded-[10px] grid justify-items-end">
          <!-- ช่อง "ประเภทการเดินทาง" -->
          <div class="m-4">
            <label
              for="selectExpenseType"
              class="block text-sm font-medium py-1"
              >ประเภทค่าใช้จ่าย</label
            >
            <div class="relative">
              <select
                id="selectExpenseType"
                v-model="rqRqtName"
                @change="handleSelectChange"
                class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
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
                class="absolute top-0 left-0 mt-2 px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                style="width: calc(100% - 16px)"
              />
            </div>
          </div>

          <!-- ช่อง "ประเภทรถ" -->
          <div v-show="rqRqtName !== 'ค่าอาหาร'" class="m-4">
            <label for="vehicleType" class="block text-sm font-medium py-1"
              >ประเภทรถ</label
            >
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
          <!-- ช่อง "สถานที่เริ่มต้น" -->
          <div v-show="rqRqtName !== 'ค่าอาหาร'" class="m-4">
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
          <div v-show="rqRqtName !== 'ค่าอาหาร'" class="m-4">
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

          <!-- ช่อง "สถาน *" -->
          <div v-show="rqRqtName !== 'ค่าเดินทาง'" class="m-4">
            <label for="rqLocation" class="block text-sm font-medium py-1"
              >สถาน *</label
            >
            <input
              type="text"
              id="rqLocation"
              v-model="formData.rqLocation"
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
          class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full focus:border-gray-400 focus:ring-0 focus:outline-none"
        ></textarea>
      </div>
    </div>
    <!-- upload -->
    <div class="upload-container w-2/4 m-5">
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
        <img
          v-else
          :src="previewUrl!"
          alt="Preview"
          class="max-w-full max-h-full object-contain absolute inset-0 m-auto"
        />
      </div>
    </div>
  </form>
</template>
