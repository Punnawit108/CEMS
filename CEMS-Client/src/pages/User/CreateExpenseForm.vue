<script setup lang="ts">
/*
 * ชื่อไฟล์: CreateExpenseForm.vue
 * คำอธิบาย: ไฟล์นี้แสดงฟอร์มเบิกค่าใช้จ่าย
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 4 มกราคม 2568
 */

import { onMounted, ref, computed, watch } from "vue";
import Button from "../../components/template/Button.vue";
import { useRequisitionStore } from "../../store/requisition";
import router from "../../router";
import { createRequisition, TravelManage } from "../../types";
import SingleDatePicker from "../../components/template/SingleDatePicker.vue";
import FileDisplay from "../../components/template/FileDisplay.vue";

const requisitionStore = useRequisitionStore();
const user = ref<any>(null);
const vehicleType = ref<any>(null);
const selectedTravelType = ref<string>('');
const rqtId = ref(0);
const fileInput = ref<HTMLInputElement | null>(null);
const previewUrl = ref<string | null>(null);
const maxWidth = 800;
const maxHeight = 800;
const rqtName = ref('')
const vhId = ref(0);
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
  rqPayDate: null,
  rqWithdrawDate: null,
  rqInsteadEmail: "",
  rqExpenses: 0,
  rqStartLocation: "",
  rqEndLocation: "",
  rqDistance: "",
  rqPurpose: "",
  rqStatus: "",
  rqProgress: "",
  rqAny: "",
});

// กรองข้อมูลที่ vhType เป็นประเภทที่เลือกและ vhVisible == 0
const filteredVehicleType = computed(() => {
  return vehicleType.value
    ? vehicleType.value.filter((vehicle: TravelManage) =>
      vehicle.vhType === selectedTravelType.value && vehicle.vhVisible === 0
    )
    : [];
})

// กรองประเภทการเดินทางที่ไม่ซ้ำ
const uniqueTravelTypes = computed(() => {
  return vehicleType.value && vehicleType.value.length > 0
    ? [...new Set(vehicleType.value.map((vehicle: TravelManage) => vehicle.vhType))]
    : [];
});



onMounted(async () => {
  await requisitionStore.getAllProject();
  await requisitionStore.getAllRequisitionType();
  vehicleType.value = await requisitionStore.getAllvehicleType();
  const storedUser = localStorage.getItem("user");
  if (storedUser) {
    try {
      console.log(vehicleType)
      user.value = await JSON.parse(storedUser);
      await requisitionStore.getUserEmail(user.value.usrId)
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


function updateRqtName(event: Event) {
  const selectedId = (event.target as HTMLSelectElement).value;
  const selectedType = requisitionStore.requisitionType.find(
    (type) => type.rqtId === Number(selectedId)
  );
  rqtName.value = selectedType ? selectedType.rqtName : '';
}



// หา vhPayrate ของพาหนะที่ถูกเลือก
const selectedPayrate = computed(() => {
  const selectedVehicle = vehicleType.value.find(
    (vehicle: any) => vehicle.vhId.toString() === vhId.value.toString()  // ใช้ vhId ที่เลือกเปรียบเทียบ
  );

  return selectedVehicle ? selectedVehicle.vhPayrate : '';  // คืนค่า vhPayrate หรือค่าว่าง
});

const triggerFileInput = () => {
  fileInput.value?.click();
};

const handleDrop = (event: DragEvent) => {
  if (event.dataTransfer && event.dataTransfer.files) {
    const droppedFiles = Array.from(event.dataTransfer.files);
    selectedFiles.value.push(...droppedFiles);
  }
};

const removeFile = (fileToRemove: File) => {
  selectedFiles.value = selectedFiles.value.filter(file => file !== fileToRemove);
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

const selectedFiles = ref<File[]>([]);

const handleFileChange = async (event: Event) => {
  const target = event.target as HTMLInputElement;

  if (target.files) {
    const newFiles = Array.from(target.files);
    await uploadFiles(newFiles);
  }
};

const uploadFiles = async (files: File[]) => {
  const allowedFileTypes = ["application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg"];
  const maxFileSize = 2 * 1024 * 1024; // 2MB
  const validFiles: File[] = [];

  for (const file of files) {
    if (!allowedFileTypes.includes(file.type)) {
      console.log(file.type)
      alert(`ไฟล์ ${file.name} ไม่ได้รับอนุญาต อัปโหลดได้เฉพาะ PDF, DOCS หรือ JPEG เท่านั้น`);
      continue;
    }

    if (file.size > maxFileSize) {
      alert(`ไฟล์ ${file.name} มีขนาดเกิน 2MB`);
      continue;
    }

    validFiles.push(file);
  }

  if (validFiles.length > 0) {
    selectedFiles.value.push(...validFiles);
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
  if (await validateForm()) {
    openPopupSubmit();
  }

};

const handleSave = async () => {
  if (await validateForm()) {
    openPopupSave();
  }
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

// ตัวแปรเก็บ error ของแต่ละฟิลด์
const errors = ref<{ [key: string]: boolean }>({});

// ฟิลด์ที่จำเป็นต้องกรอก
const requiredFields = ['rqName', 'rqPjId', 'rqRqtId'
  , 'rqExpenses', 'rqStartLocation'
  , 'rqEndLocation', 'rqDistance', 'rqPurpose', 'rqAny'];


watch(
  [formData, selectedTravelType, vhId],
  ([newFormData, newSelectedTravelType, newVhId]) => {

    for (const field of requiredFields) {
      if (newFormData[field as keyof createRequisition] !== "" && newFormData[field as keyof createRequisition] !== 0) {
        delete errors.value[field];
      }
    }
    if (newSelectedTravelType !== '') {
      delete errors.value.selectedTravelType;
    }
    if (newVhId !== 0) {
      delete errors.value.vhId;
    }
  },
  { deep: true }
);


const displayRqExpenses = ref('');
watch(displayRqExpenses, (newVal) => {
  formData.value.rqExpenses = newVal === '' ? 0 : Number(newVal);
});


// ฟังก์ชันตรวจสอบฟอร์มแบบ loop
const validateForm = async () => {
  errors.value = {};
  for (const field of requiredFields) {
    const value = formData.value[field as keyof createRequisition];
    if (value === "" || value === undefined || value === 0) {
      if (rqtName.value !== 'ค่าเดินทาง' &&
        ['rqStartLocation', 'rqEndLocation', 'rqDistance', 'rqVhId'].includes(field)) {
        continue;
      }
      if (rqtName.value !== 'อื่นๆ' && field === 'rqAny') {
        continue;
      }

      errors.value[field] = true;
    }
  }
  if (rqtName.value === 'ค่าเดินทาง' && selectedTravelType.value === '') {
    errors.value.selectedTravelType = true;
  }
  if (rqtName.value === 'ค่าเดินทาง' && vhId.value === 0) {
    errors.value.vhId = true;
  }
  if (rqtName.value === 'อื่นๆ' && formData.value.rqAny === '') {
    errors.value.vhId = true;
  }
  return Object.keys(errors.value).length === 0;
};


function updateFormData() {
  if (rqtName.value != 'ค่าเดินทาง') {
    formData.value.rqVhId = null;
    formData.value.rqStartLocation = null;
    formData.value.rqEndLocation = null;
    formData.value.rqDistance = null;
  }
  if (rqtName.value == 'ค่าเดินทาง') {
    formData.value.rqVhId = vhId.value;
  }
  if (rqtName.value != 'อื่นๆ') {
    formData.value.rqAny = null;
  }
  formData.value.rqUsrId = user.value.usrId;
  formData.value.rqPayDate = formatDateToThai(selectedDate.value)
  formData.value.rqWithdrawDate = formatDateToThai(currentDate.value)
}

const confirmSave = async (event: Event) => {
  event.preventDefault();
  formData.value.rqStatus = "sketch";
  formData.value.rqProgress = "accepting"
  await updateFormData()
  const fd = await createFormData(formData.value, selectedFiles.value);
  await requisitionStore.createExpense(fd);
  isAlertSaveOpen.value = true;
  
  setTimeout(() => {
    isAlertSaveOpen.value = false;
    closePopupSave();
    router.push("/disbursement/listWithdraw");
  }, 1500);
};

const formatDateToThai = (date: Date) => {
  if (!date) return null;
  const thaiYear = date.getFullYear() + 543;
  const formattedDate = `${thaiYear}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getDate().toString().padStart(2, '0')}`;
  return formattedDate;
};

const createFormData = (formData: createRequisition, selectedFiles: File[]): FormData => {
  const fd = new FormData();
  Object.entries(formData).forEach(([key, value]) => {
    if (value !== null && value !== undefined) {
      fd.append(key, value.toString());
    }
  });
  selectedFiles.forEach((file) => {
    fd.append("Files", file);
  });
  return fd;
};

const confirmSubmit = async (event: Event) => {
  event.preventDefault();
  formData.value.rqStatus = "waiting";
  formData.value.rqProgress = "accepting"
  await updateFormData()
  const fd = await createFormData(formData.value, selectedFiles.value);
  await requisitionStore.createExpense(fd);
  isAlertSubmitOpen.value = true;
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

const currentDate = ref(new Date());
const selectedDate = ref(new Date());
const isDatePickerOpen = ref(false);



// ฟังก์ชันจัดการเมื่อยืนยันการเลือกวันที่
const handleConfirm = (date: Date) => {
  console.log('Confirmed date:', date);
  // ทำอย่างอื่นเพิ่มเติมตามต้องการ
};

// ฟังก์ชันจัดการเมื่อยกเลิกการเลือกวันที่
const handleDateCancel = () => {
  console.log('Date selection cancelled');
  // ทำอย่างอื่นเพิ่มเติมตามต้องการ
};

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
    <div class="mt-4">
      <!-- แบ่งเป็น 2 คอลัมน์ -->
      <div class="flex flex-col">
        <!-- Form Left -->
        <div class="grid grid-cols-4 gap-4">

          <!-- ช่อง "รหัสรายการเบิก *" -->

          <div>
            <label for="rqCode" class="block text-sm font-medium py-2">รหัสรายการเบิก </label>
            <p class="inputItem bg-[#F7F7F7] text-[#BABBBE]">CN-xxxxxx</p>
          </div>

          <!-- ช่อง "ชื่อรายการเบิก" -->
          <div>
            <label for="rqName" class="block text-sm font-medium py-2">ชื่อรายการเบิก <span
                class="text-red-500">*</span></label>
            <input type="text" id="rqName" v-model="formData.rqName"
              :class="['inputItem', { 'error': errors.rqName }]" />
          </div>

          <!-- ช่อง "วันที่ทำรายการเบิกค่าใช้จ่าย *" -->
          <div class="">
            <label for="rqWithdrawDate" class="block text-sm font-medium py-2">วันที่ทำรายการเบิกค่าใช้จ่าย <span
                class="text-red-500">*</span></label>
            <SingleDatePicker v-model="currentDate" id="rqWithdrawDate" :confirmedDate="currentDate" :disabled="true"
              class="date w-full h-[42px] text-[#BABBBE] " />
          </div>

          <!-- ช่อง "วันที่ทำการเบิกค่าใช้จ่าย *" -->
          <div>
            <label for="rqPayDate" class="block text-sm font-medium py-2">วันที่ทำการเบิกค่าใช้จ่าย <span
                class="text-red-500">*</span></label>
            <SingleDatePicker v-model="selectedDate" id="rqPayDate" v-model:isOpen="isDatePickerOpen"
              :confirmedDate="selectedDate" class="dateInput w-full h-[42px] text-black" placeholder="เลือกวันที่"
              @confirm="handleConfirm" @cancel="handleDateCancel" />
          </div>


          <div>
            <label for="projectName" class="block text-sm font-medium py-2">โครงการ<span
                class="text-red-500">*</span></label>
            <div class="text-xs">
              <select id="projectName" v-model="formData.rqPjId" :class="['inputItem', { 'error': errors.rqPjId }]"
                required>
                <option disabled selected>เลือกโครงการ</option>
                <option v-for="project in requisitionStore.projects" :key="project.pjId" :value="project.pjId">
                  {{ project.pjName }}
                </option>
              </select>
            </div>
          </div>

          <!-- ช่อง "อีเมลผู้ขอเบิกแทน *" -->

          <div>
            <!-- Dropdown -->
            <label for="expenseType" class="block text-sm font-medium py-2">
              ประเภทค่าใช้จ่าย <span class="text-red-500">*</span>
            </label>
            <select id="expenseType" v-model="formData.rqRqtId" @change="updateRqtName"
              :class="['inputItem', { 'error': errors.rqRqtId }]" required>
              <option value="" selected disabled>กรุณาเลือกประเภท</option>
              <option v-for="requisitionTypeData in requisitionStore.requisitionType" :key="requisitionTypeData.rqtId"
                :value="requisitionTypeData.rqtId">
                {{ requisitionTypeData.rqtName }}
              </option>
              <!-- <option value="999">อื่นๆ</option> -->
            </select>
          </div>

          <div v-show="rqtName == 'อื่นๆ'">
            <label for="rqAny" class="block text-sm font-medium py-2">
              ประเภทค่าใช้จ่ายอื่นๆ <span class="text-red-500">*</span>
            </label>
            <input id="rqAny" v-model="formData.rqAny" :class="['inputItem', { 'error': errors.rqAny }]" type="text"
              placeholder="กรุณาระบุข้อมูลเพิ่มเติม" />
          </div>

          <!-- ช่อง "ประเภทการเดินทาง" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <!-- Dropdown เลือกประเภทการเดินทาง -->
            <label for="travelType" class="block text-sm font-medium py-2">
              ประเภทการเดินทาง <span class="text-red-500">*</span>
            </label>
            <div class="text-xs">
              <select id="travelType" v-model="selectedTravelType"
                :class="['inputItem', { 'error': errors.selectedTravelType }]">
                <option value="" disabled>เลือกประเภทการเดินทาง</option>
                <!-- ใช้ uniqueTravelTypes ที่กรองแล้ว -->
                <option v-for="type in uniqueTravelTypes" :value="type">
                  {{ type === 'private' ? 'ประเภทส่วนตัว' : 'ประเภทสาธารณะ' }}
                </option>
              </select>
            </div>
          </div>

          <!-- ช่อง "ประเภทรถ" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <!-- Dropdown เลือกประเภทรถ (กรองตามประเภทการเดินทางที่เลือก) -->
            <label for="vehicleType" class="block text-sm font-medium py-2">
              ประเภทรถ <span class="text-red-500">*</span>
            </label>
            <div class="text-xs">
              <select v-model="vhId" :class="['inputItem', { 'error': errors.vhId }]">
                <option value="" selected disabled>เลือกประเภทรถ</option>
                <!-- ใช้ filteredVehicleType ที่กรองแล้ว -->
                <option v-for="vehicle in filteredVehicleType" :key="vehicle.vhId.toString()"
                  :value="vehicle.vhId.toString()">
                  {{ vehicle.vhVehicle }}
                </option>
              </select>
            </div>
          </div>

          <!-- ช่อง "สถานที่เริ่มต้น" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <label for="rqStartLocation" class="block text-sm font-medium py-2">สถานที่เริ่มต้น <span
                class="text-red-500">*</span></label>
            <input type="text" id="rqStartLocation" v-model="formData.rqStartLocation"
              :class="['inputItem', { 'error': errors.rqStartLocation }]" />
          </div>
          <!-- ช่อง "สถานที่สิ้นสุด" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <label for="rqEndLocation" class="block text-sm font-medium py-2">สถานที่สิ้นสุด <span
                class="text-red-500">*</span></label>
            <input type="text" id="rqEndLocation" v-model="formData.rqEndLocation"
              :class="['inputItem', { 'error': errors.rqEndLocation }]" />
          </div>

          <!-- ช่อง "ระยะทาง" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <label for="rqDistance" class="block text-sm font-medium py-2">ระยะทาง <span
                class="text-red-500">*</span></label>
            <input type="text" id="rqDistance" v-model="formData.rqDistance"
              :class="['inputItem', { 'error': errors.rqDistance }]" />
          </div>

          <div v-if="rqtName === 'ค่าเดินทาง'">
            <label for="rqPayrate" class="block text-sm font-medium py-2">อัตราค่าเดินทาง </label>
            <p class="inputItem bg-[#F7F7F7] text-[#BABBBE]">{{ selectedPayrate }}</p>
          </div>

          <!-- ช่อง "จำนวนเงิน (บาท)" -->
          <div>
            <label for="rqExpenses" class="block text-sm font-medium py-2">จำนวนเงิน (บาท) <span
                class="text-red-500">*</span></label>
            <input type="number" id="rqExpenses" v-model="displayRqExpenses"
              :class="['inputItem', { 'error': errors.rqExpenses }]" />
          </div>


          <div>
            <label for="rqInsteadEmail" class="block text-sm font-medium py-2">ชื่อผู้ขอเบิกแทน </label>
            <select type="text" id="rqInsteadEmail" v-model="formData.rqInsteadEmail" class="inputItem">
              <option :value="null" disabled selected>Select User</option>
              <option :value="user.usrEmail" v-for="user in requisitionStore.UserInstead">{{ user.usrName }}</option>
            </select>
          </div>





        </div>
        <!-- <div class="border border-gray-200"></div> -->

        <!-- Form Right -->
        <!-- <div class="w-2/5 rounded-[10px] place-items-end"> -->




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



      </div>
      <!-- วัตถุประสงค์ -->
      <div class="text-sm my-4">
        <label class="block text-sm font-medium pb-2">รายละเอียด</label>
        <textarea v-model="formData.rqPurpose" class="h-[81px]"
          :class="['inputItem', { 'error': errors.rqPurpose }]"> </textarea>
      </div>
      <!-- upload -->
      <div class="upload-container">
        <label class="z-0 max-md:max-w-full">อัปโหลดไฟล์</label>
        <div
          class="flex z-0 mt-1 h-[278px] w-full bg-white rounded-md border border-solid border-[#B8B8B8] min-h-[395px] max-md:max-w-full cursor-pointer relative"
          @click="triggerFileInput" @dragover.prevent @drop.prevent="handleDrop">
          <!-- Input สำหรับอัปโหลดหลายไฟล์ -->
          <input type="file" ref="fileInput" @change="handleFileChange" accept="image/jpeg,application/pdf,.doc,.docx"
            multiple style="display: none" />

          <div class="flex flex-col items-center justify-center absolute inset-0 text-sm text-[#B8B8B8]">
            <img loading="lazy"
              src="https://cdn.builder.io/api/v1/image/assets/TEMP/5da245b200f054a57a812257a8291e28aacdd77733a878e94699b2587a54360d?placeholderIfAbsent=true&apiKey=963991dcf23f4b60964b821ef12710c5"
              alt="Upload icon" class="object-contain w-16 aspect-[1.1]" />
            <p class="mt-3">คลิก หรือลากไฟล์มาที่นี่ เพื่ออัปโหลด</p>
            <p class="mt-3">DOCS PNG หรือ PDF (MAX 2MB)</p>
          </div>
        </div>
      </div>
    </div>

    <FileDisplay v-for="file in selectedFiles" :key="file.name || file.lastModified" :file="file"
      @remove="removeFile(file)" />


    <!-- <div class="mt-4 bg-[#F7F7F7] border rounded-[5px] border-[#B8B8B8] w-full h-[66px] flex justify-between">
      <div class="flex flex-row ml-4 my-2">
        <div class="w-[50px] h-[50px] bg-white rounded-[5px]">
          <img src="/docIcon.svg" alt="Doc Icon" class="w-full h-full">
        </div>
        <p class="ml-4 flex items-center">file name</p>
      </div>

      <div class="mr-5 flex items-center">
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="17" viewBox="0 0 16 17" fill="none">
          <path
            d="M1.78433 0.839844L8.00684 7.06235L14.2293 0.839844L16.0068 2.61734L9.78433 8.83984L16.0068 15.0623L14.2281 16.8398L8.00558 10.6173L1.78433 16.8398L0.00683594 15.0623L6.22934 8.83984L0.00683594 2.61734L1.78433 0.839844Z"
            fill="black" />
        </svg>
      </div>
    </div> -->

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
<style>
.inputItem {
  padding: 8px 12px;
  border: 1px solid #B8B8B8;
  background-color: white;
  border-radius: 0.375rem;
  font-size: 0.875rem;
  width: 100%;
  height: 40px;
  line-height: 24px;
  transition: border-color 0.3s ease;
}

.date input {
  height: 40px;
  border: 1px solid #B8B8B8;
  background-color: #F7F7F7;
}

.dateInput input {
  height: 40px;
  border: 1px solid #B8B8B8;
}

.inputItem.error,
.date input.error,
.dateInput input.error {
  border-color: red !important;
  /* ใช้ !important เพื่อให้แน่ใจว่าขอบสีแดงถูกใช้ */
  background-color: #FFF5F5;
  /* เพิ่มสีพื้นหลังจาง ๆ เพื่อให้ดูชัดเจน */
}
</style>