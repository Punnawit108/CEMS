 <script setup lang="ts">
/**
* ชื่อไฟล์ : EditExpenseForm.vue
* คำอธิบาย : ไฟล์นี้แสดงแก้ไขฟอร์มเบิกค่าใช้จ่าย
* Input : ข้อมูลฟอร์มเบิกค่าใช้จ่าย
* Output : ข้อมูลแก้ไขฟอร์มเบิกค่าใช้จ่าย
* ชื่อผู้เขียน / แก้ไข : อังคณา อุ่นเสียม
* วันที่จัดทำ / วัยที่แก้ไข : 11 พฤศจิกายน 2567
*/

import { useRouter } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
const router = useRouter();

const toDetails = (id: string) => {
    router.push(`/disbursement/listWithdraw/detail/:${id}/editExpenseForm`);
} 
</script> 
<!-- path for test = /disbursement/listWithdraw/detail/:id/editExpenseForm -->
<template>
  <div class="">
    <!-- Content -->
    <main class="m-5 bg-white-500 text-black p-5 rounded-[10px] grid gap-y-2">
      <!-- เลือกประเภทค่าใช้จ่าย -->
      <div
        class="content-center px-5 flex justify-between flex flex-col md:flex-row"
      >
        <div class="content-center">
          <label for="projectName" class="self-start text-sm">โครงการ</label>
          <div class="text-xs">
            <select
              id="projectName"
              class="px-3 py-3 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
            >
              <option>เลือกโครงการ</option>
              <option>ระบบจัดการงานอัตโนมัติ</option>
              <option>อบรมการบริหาร</option>
            </select>
          </div>
        </div>
      </div>

      <div class="px-5">
        <label for="selectExpenseType" class="text-sm">ประเภทค่าใช้จ่าย</label>
        <div class="relative">
          <select
            id="selectExpenseType"
            v-model="selectedExpenseType"
            @change="handleSelectChange"
            class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
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
        <h1 class="font-bold text-lg">รายละเอียดคำขอเบิก</h1>
      </div>
      <form @submit.prevent="handleSubmit" class="space-y-4">
        <!-- Fromประเภทค่าเดินทาง-->
        <div class="px-5" v-show="selectedExpenseType !== 'ค่าอาหาร'">
          <!-- แบ่งเป็น 2 คอลัมน์ -->
          <div class="flex flex-col md:flex-row justify-between gap-5">
            <!-- Form Left -->
            <div class="w-1/2 rounded-[10px]">
              <!-- ช่อง "ชื่อผู้เบิก" -->
              <div>
                <label for="name" class="block text-sm font-medium py-1"
                  >ชื่อผู้ขอเบิก</label
                >
                <input
                  type="text"
                  id="name"
                  v-model="formData.name"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>
              <!-- ช่อง "ประเภทการเดินทาง" -->
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
                <label for="vehicleType" class="self-start text-sm"
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
              <!-- ช่อง "วันที่เกิดค่าใช้จ่าย *" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >วันที่เกิดค่าใช้จ่าย *</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>
              <!-- ช่อง "วันที่ทำรายการเบิกค่าใช้จ่าย *" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >วันที่ทำรายการเบิกค่าใช้จ่าย *</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>
              <!-- ช่อง "รหัสรายการเบิก *" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >รหัสรายการเบิก*</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>

              <!-- ช่อง "อีเมลผู้ขอเบิกแทน *" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >อีเมลผู้ขอเบิกแทน *</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>
              <!-- ช่อง "จำนวนเงิน (บาท)" -->
              <div>
                <label for="amount" class="block text-sm font-medium py-1"
                  >จำนวนเงิน (บาท)</label
                >
                <input
                  type="text"
                  id="amount"
                  v-model="formData.amount"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>

              <!-- ช่อง "สถานที่เริ่มต้น" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >สถานที่เริ่มต้น</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>

              <div>
                <!-- ช่อง "สถานที่สิ้นสุด" -->
                <div>
                  <label
                    for="endLocation"
                    class="block text-sm font-medium py-1"
                    >สถานที่สิ้นสุด</label
                  >
                  <input
                    type="text"
                    id="endLocation"
                    v-model="formData.endLocation"
                    class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                  />
                </div>
              </div>
              <div class="text-sm">
                <label>วัตถุประสงค์</label>
                <div class="md:w-[400px]">
                  <textarea
                    class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:h-[120px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                  ></textarea>
                </div>
              </div>
            </div>

            <!-- Form Right -->
            <div class="w-1/2 rounded-[10px] grid content-between">
              <div class="sm:w-1/2 rounded-[10px]">
                <div class="upload-container">
                  <label for="file-upload" class="custom-file-upload w-full">
                    <div class="upload-box w-full">
                      <img
                        v-if="formData.preview"
                        :src="formData.preview"
                        alt="Preview Image"
                        class="object-contain"
                      />
                      <div v-else class="icon-upload text-gray-500">
                        <i class="fas fa-upload text-4xl"></i>
                        <p>อัปโหลดไฟล์</p>
                      </div>
                    </div>
                  </label>
                  <input
                    type="file"
                    id="file-upload"
                    @change="onFileChange"
                    class="hidden"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Fromประเภทอื่นๆ-->
        <div v-show="selectedExpenseType == 'ค่าอาหาร'" class="px-5">
          <!-- แบ่งเป็น 2 คอลัมน์ -->
          <div class="flex flex-col md:flex-row justify-between gap-5">
            <!-- Form Left -->
            <div class="w-1/2 rounded-[10px]">
              <!-- ช่อง "ชื่อผู้เบิก" -->
              <div>
                <label for="name" class="block text-sm font-medium py-1"
                  >ชื่อผู้ขอเบิก</label
                >
                <input
                  type="text"
                  id="name"
                  v-model="formData.name"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>
              <!-- ช่อง "วันที่เกิดค่าใช้จ่าย *" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >วันที่เกิดค่าใช้จ่าย *</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>
              <!-- ช่อง "วันที่ทำรายการเบิกค่าใช้จ่าย *" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >วันที่ทำรายการเบิกค่าใช้จ่าย *</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>
              <!-- ช่อง "รหัสรายการเบิก *" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >รหัสรายการเบิก*</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>

              <!-- ช่อง "อีเมลผู้ขอเบิกแทน *" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >อีเมลผู้ขอเบิกแทน *</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>
              <!-- ช่อง "จำนวนเงิน (บาท)" -->
              <div class="">
                <label for="amount" class="block text-sm font-medium py-1"
                  >จำนวนเงิน (บาท)</label
                >
                <input
                  type="text"
                  id="amount"
                  v-model="formData.amount"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>

              <!-- ช่อง "สถานที่" -->
              <div>
                <label
                  for="startLocation"
                  class="block text-sm font-medium py-1"
                  >สถานที่</label
                >
                <input
                  type="text"
                  id="startLocation"
                  v-model="formData.startLocation"
                  class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
                />
              </div>
            </div>

            <!-- Form Right -->
            <div class="w-1/2 rounded-[10px]">
              <div class="sm:w-1/2 rounded-[10px]">
                <div class="upload-container">
                  <label for="file-upload" class="custom-file-upload w-full">
                    <div class="upload-box w-full">
                      <img
                        v-if="formData.preview"
                        :src="formData.preview"
                        alt="Preview Image"
                        class="object-contain"
                      />
                      <div v-else class="icon-upload text-gray-500">
                        <i class="fas fa-upload text-4xl"></i>
                        <p>อัปโหลดไฟล์</p>
                      </div>
                    </div>
                  </label>
                  <input
                    type="file"
                    id="file-upload"
                    @change="onFileChange"
                    class="hidden"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="flex justify-center px-5">
          <button
            type="submit"
            class="px-4 py-2 m-5 w-[180px] bg-[#B6B7BA] text-black rounded-[10px]"
          >
            บันทึก
          </button>
          <button
            type="submit"
            class="px-4 py-2 m-5 w-[180px] bg-[#FF0000] text-white rounded-[10px]"
          >
            ยกเลิก
          </button>
          <button
            type="submit"
            class="px-4 py-2 m-5 w-[180px] bg-[#15CC1C] text-white rounded-[10px]"
          >
            ยืนยัน
          </button>
        </div>
      </form>
    </main>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from "vue";
const selectedExpenseType = ref("ค่าเดินทาง");

const formData = ref({
  name: "",
  startLocation: "",
  endLocation: "",
  amount: "",
  preview: null,
});

const handleSubmit = () => {
  console.log("Form submitted:", formData.value);
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

const expenseOptions = ref(["ค่าเดินทาง", "ค่าอาหาร"]);
const customExpenseType = ref("");
const isCustomExpenseTypeAdded = ref(false);
const isEditing = ref(false);

// Computed property to check if "อื่นๆ" is selected
const isOtherSelected = computed(
  () => selectedExpenseType.value === "อื่นๆ" || customExpenseType.value !== ""
);

function handleSelectChange() {
  if (!isOtherSelected.value) {
    customExpenseType.value = ""; // Reset the input when other option is not selected
  }
  isEditing.value = false;
}

function startEditing() {
  isEditing.value = true;
  customExpenseType.value = selectedExpenseType.value;
}

function addCustomExpense() {
  if (
    customExpenseType.value &&
    !expenseOptions.value.includes(customExpenseType.value)
  ) {
    expenseOptions.value.push(customExpenseType.value); // Add custom option
    selectedExpenseType.value = customExpenseType.value; // Set as selected
    isCustomExpenseTypeAdded.value = true; // Set flag to hide "อื่นๆ"
  } else if (customExpenseType.value) {
    selectedExpenseType.value = customExpenseType.value; // Just set the selected value
  }
  isEditing.value = false;
  customExpenseType.value = ""; // Reset the input
}
</script>

<style scoped>
.upload-container {
  text-align: center;
  width: 300px;
  margin: auto;
}

.custom-file-upload {
  display: block;
  width: 100%;
  height: 200px;
  border: 2px dashed #ccc;
  border-radius: 10px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}

.custom-file-upload:hover {
  background-color: #f9f9f9;
}

.upload-box {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
}

.icon-upload {
  text-align: center;
  color: #999;
}

.icon-upload i {
  font-size: 48px;
  margin-bottom: 10px;
}

img {
  max-width: 100%;
  max-height: 100%;
  object-fit: cover;
  border-radius: 10px;
}

input[type="file"] {
  display: none;
}

.selectExpenseType {
  position: relative;
  top: 0;
}
</style>
