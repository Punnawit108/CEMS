<script setup lang="ts">
/*
 * ชื่อไฟล์: EditExpenseForm.vue
 * คำอธิบาย: ไฟล์นี้แสดงฟอร์มเบิกค่าใช้จ่าย
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ, อังคณา อุ่นเสียม, พงศธร บุญญามา
 * วันที่จัดทำ/แก้ไข: 22 มีนาคม 2568
 */

import { onMounted, ref, computed, watch } from "vue";
import Button from "../../components/Buttons/Button.vue";
import { useRequisitionStore } from "../../store/requisition";
import router from "../../router";
import { createRequisition, TravelManage } from "../../types";
import SingleDatePicker from "../../components/SingleDatePicker.vue";
import FileDisplay from "../../components/FileDisplay.vue";
import { useRoute } from "vue-router";


const requisitionStore = useRequisitionStore();
const user = ref<any>(null);
const vehicleType = ref<any>(null);
const selectedTravelType = ref<string | null>("");
const fileInput = ref<HTMLInputElement | null>(null);

const route = useRoute();
const id = route.params.id.toString();

const rqtName = ref('')
const vhId = ref(0);
const currentDate = ref(new Date());
const selectedDate = ref(new Date());
const rqCode = ref('')
const selectedFiles = ref<{ file: any; fId: number | null; fileName: string | null }[]>([]);

const isDatePickerOpen = ref(false);
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
  rqRqtId: 0,
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

const parseDate = (dateStr: string): Date => {
  if (!dateStr) return new Date();

  const [year, month, day] = dateStr.split("-").map(Number);
  return new Date(year - 543, month - 1, day); // แปลง พ.ศ. เป็น ค.ศ.
};

const isMounted = ref(false);

onMounted(async () => {
  await requisitionStore.getAllProject();
  await requisitionStore.getAllRequisitionType();
  vehicleType.value = await requisitionStore.getAllVehicleType();
  const storedUser = localStorage.getItem("user");
  console.log(vehicleType)
  if (storedUser) {
    try {
      user.value = await JSON.parse(storedUser);
      await requisitionStore.getUserEmail(user.value.usrId)
      const data = await requisitionStore.getExpenseById(id);
      if (data) {
        Object.assign(formData.value, data);
        vhId.value = data.rqVhId; //rqVhName : "รถเมย์"
        const selectedType = requisitionStore.requisitionType.find(
          (type) => type.rqtId === data.rqRqtId
        );
        rqtName.value = selectedType ? selectedType.rqtName : '';

        const selectedVehicle = vehicleType.value.find(
          (vehicle: TravelManage) => vehicle.vhVehicle === data.rqVhName
        );
        if (selectedVehicle) {
          vhId.value = selectedVehicle.vhId;
        }
        selectedTravelType.value = data.rqVhType || "";
        rqCode.value = data.rqCode
        displayRqExpenses.value = data.rqExpenses ? parseFloat(data.rqExpenses).toFixed(2) : "";
        const user = requisitionStore.UserInstead.find((u) => u.usrName === data.rqInsteadEmail);
        if (user) {
          formData.value.rqInsteadEmail = user.usrEmail; // ตั้งค่าเป็น email
        }
        currentDate.value = parseDate(data.rqWithdrawDate);
        selectedDate.value = parseDate(data.rqPayDate);

        selectedFiles.value = data.files.map((file: any) => {
          const fileUrl = `http://localhost:5247${file.fPath}`;
          return {
            file: fileUrl,
            fId: file.fId,
            fileName: file.fName,
          };
        });
        isMounted.value = true;
      }
    } catch (error) {
      console.log("Error loading user:", error);
    }
  }
});

const formatRqExpenses = () => {
  if (displayRqExpenses.value !== "") {
    displayRqExpenses.value = parseFloat(displayRqExpenses.value).toFixed(2);
    formData.value.rqExpenses = Number(displayRqExpenses.value);
  }
};

// กรองข้อมูลที่ vhType เป็นประเภทที่เลือกและ vhVisible == 0
const filteredVehicleType = computed(() => {
  if (!selectedTravelType.value) {
    return vehicleType.value; // ถ้าไม่ได้เลือกประเภทการเดินทางให้แสดงทั้งหมด
  }
  return vehicleType.value.filter((vehicle: TravelManage) => vehicle.vhType === selectedTravelType.value);
});

// กรองประเภทการเดินทางที่ไม่ซ้ำ
const uniqueTravelTypes = computed(() => {
  return vehicleType.value && vehicleType.value.length > 0
    ? [...new Set(vehicleType.value.map((vehicle: TravelManage) => vehicle.vhType))]
    : [];
});

//fn ตรวจสอบการเลือกประเภทค่าใช้จ่าย
function updateRqtName(event: Event) {
  const selectedId = (event.target as HTMLSelectElement).value;
  const selectedType = requisitionStore.requisitionType.find(
    (type) => type.rqtId === Number(selectedId)
  );
  rqtName.value = selectedType ? selectedType.rqtName : '';
}

//fn หา vhPayrate ของพาหนะที่ถูกเลือก
const selectedPayrate = computed(() => {
  if (!vehicleType.value || !vhId.value) {
    return '';
  }

  const selectedVehicle = vehicleType.value.find(
    (vehicle: any) => vehicle.vhId.toString() === vhId.value.toString()
  );
  
  return selectedVehicle ? selectedVehicle.vhPayrate : '';
});

const calculateExpenses = () => {
  if (formData.value.rqDistance && selectedPayrate.value) {
    const expenses = Number(formData.value.rqDistance) * Number(selectedPayrate.value);
    displayRqExpenses.value = expenses.toString();
    formData.value.rqExpenses = expenses;
  }
};

//fn การกดอัพโหลดไฟล์
const triggerFileInput = () => {
  fileInput.value?.click();
};

//fn เมื่อมีการลากไฟล์ทำการอัพโหลด
const handleDrop = async (event: DragEvent) => {
  event.preventDefault();

  if (event.dataTransfer && event.dataTransfer.files) {
    const droppedFiles = Array.from(event.dataTransfer.files);
    await uploadFiles(droppedFiles); // เรียกใช้ uploadFiles แทนการ push ตรงๆ
  }
};

//fn เมื่อมีการเลือกไฟล์
const handleFileChange = async (event: Event) => {
  const target = event.target as HTMLInputElement;
  if (target.files) {
    const newFiles = Array.from(target.files);
    await uploadFiles(newFiles);
  }
};

//fn ลบข้อมูลไฟล์
const removeFile = async (fIdToRemove: number | null, fileNameToRemove?: string) => {
  if (fIdToRemove !== null) {
    await requisitionStore.deleteFile(fIdToRemove);
    selectedFiles.value = selectedFiles.value.filter(item => item.fId !== fIdToRemove);
  } else if (fileNameToRemove) {
    selectedFiles.value = selectedFiles.value.filter(item => item.file.name !== fileNameToRemove);
  }
};

watch(rqtName, (newValue) => {
  displayRqExpenses.value = "";
  if (newValue !== 'ค่าเดินทาง') {
    selectedTravelType.value = null;
  } else {
    selectedTravelType.value = 'public';
  }
});

//fn ตัวตรวจสอบไฟล์
const uploadFiles = async (files: File[]) => {
  const allowedFileTypes = new Set([
    "application/pdf",
    "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
    "image/jpeg",
  ]);
  const maxFileSize = 2 * 1024 * 1024; // 2MB
  const validFiles: { file: File; fId: number | null; fileName: string }[] = [];
  const errors: string[] = [];

  const existingFiles = new Set(
    selectedFiles.value.map((item) => `${item.file.name}-${item.file.size}`)
  );

  for (const file of files) {
    const fileKey = `${file.name}-${file.size}`;

    // ตรวจสอบว่าไฟล์ซ้ำหรือไม่
    if (existingFiles.has(fileKey)) {
      errors.push(`ไฟล์ ${file.name} ถูกเพิ่มไปแล้ว`);
      continue;
    }

    // ตรวจสอบประเภทไฟล์
    if (!allowedFileTypes.has(file.type)) {
      errors.push(
        `ไฟล์ ${file.name} ไม่ได้รับอนุญาต (อัปโหลดได้เฉพาะ PDF, DOCX หรือ JPEG)`
      );
      continue;
    }

    // ตรวจสอบว่าไฟล์เป็น .JPG หรือไม่
    if (file.type === "image/jpeg" && file.name.toLowerCase().endsWith(".jpg")) {
      errors.push(`ไฟล์ ${file.name} เป็นไฟล์ .JPG ไม่อนุญาตให้อัปโหลด`);
      continue;
    }

    // ตรวจสอบขนาดไฟล์
    if (file.size > maxFileSize) {
      errors.push(`ไฟล์ ${file.name} มีขนาดเกิน 2MB`);
      continue;
    }

    // เพิ่มไฟล์ที่ผ่านการตรวจสอบลงใน validFiles
    validFiles.push({ file, fId: null, fileName: file.name });
    existingFiles.add(fileKey);
  }

  // เพิ่มไฟล์ที่ผ่านการตรวจสอบลงใน selectedFiles
  if (validFiles.length > 0) {
    selectedFiles.value.push(...validFiles);
  }

  // แสดงข้อผิดพลาด (ถ้ามี)
  if (errors.length > 0) {
    alert(errors.join("\n"));
  }
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

const displayRqExpenses = ref('');

//ตรวจสอบสถานะของ rqExpense มีการแก้ไขหรือไม่ และ ให้แสดงค่าว่าง
watch(displayRqExpenses, (newVal) => {
  formData.value.rqExpenses = newVal === "" ? 0 : Number(newVal);
});

// ตัวแปรเก็บ error ของแต่ละฟิลด์
const errors = ref<{ [key: string]: boolean }>({});

// ฟิลด์ที่จำเป็นต้องกรอก
const requiredFields = ['rqName', 'rqPjId', 'rqRqtId'
  , 'rqExpenses', 'rqStartLocation'
  , 'rqEndLocation', 'rqDistance', 'rqPurpose', 'rqAny'];

//ตรวจสอบสถานะของแต่ละฟิวว่ามีการแก้ไขหรือไม่ 
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


// ฟังก์ชันตรวจสอบฟอร์มเมื่อมีการกดส่ง
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

// ปรับเปลี่ยนค่าของข้อมูล เมื่อเป็นค่าใช้จ่ายทั่วไป ค่าเดินทาง และค่าใช้จ่ายอื่นๆ
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

// ปรับรูปแบบวันเดือนปี
const formatDateToThai = (date: Date) => {
  if (!date) return null;
  const localDate = new Date(date.getTime() + date.getTimezoneOffset() * 60000);
  const options: Intl.DateTimeFormatOptions = { year: "numeric", month: "2-digit", day: "2-digit" };
  const thaiDate = new Intl.DateTimeFormat("th-TH", options).format(localDate);
  const [day, month, year] = thaiDate.split("/");
  return `${year}-${month}-${day}`;
};

// ปรับรูปแบบค่าที่ส่งเข้า db
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

//เมื่อกดปุ่มบันทึก
const confirmSave = async (event: Event) => {
  event.preventDefault();
  formData.value.rqStatus = "sketch";
  formData.value.rqProgress = "accepting"
  await updateFormData()
  const filesOnly = selectedFiles.value.filter(item => item.fId === null).map(item => item.file);
  const fd = await createFormData(formData.value, filesOnly);
  await requisitionStore.updateExpense(id, fd);
  isAlertSaveOpen.value = true;
  setTimeout(() => {
    isAlertSaveOpen.value = false;
    closePopupSave();
    router.push("/disbursement/listWithdraw");
  }, 1500);
};

//เมื่อกดปุ่มยืนยัน
const confirmSubmit = async (event: Event) => {
  event.preventDefault();
  formData.value.rqStatus = "waiting";
  formData.value.rqProgress = "accepting"
  await updateFormData()
  const filesOnly = selectedFiles.value.filter(item => item.fId === null).map(item => item.file);
  const fd = await createFormData(formData.value, filesOnly);
  await requisitionStore.updateExpense(id, fd);
  isAlertSubmitOpen.value = true;

  setTimeout(() => {
    isAlertSubmitOpen.value = false;
    closePopupSubmit();
    router.push("/disbursement/listWithdraw");
  }, 1500);
};

//เมื่อกดปุ่มยกเลิก
const confirmCancle = async (event: Event) => {
  event.preventDefault();
  isAlertCancleOpen.value = true;
  setTimeout(() => {
    isAlertCancleOpen.value = false;
    closePopupCancle();
    router.push("/disbursement/listWithdraw");
  }, 1500);
};

//ดูข้อมูลใน file
const previewFile = (file: string | File) => {
  if (typeof file === 'string') {
    // กรณี file เป็น string (URL)
    if (file.endsWith('.pdf') || file.match(/\.(jpeg|jpg|png)$/i)) {
      window.open(file, '_blank'); // เปิดไฟล์ PDF หรือรูปภาพในแท็บใหม่
    } else if (file.endsWith('.docx')) {
      const link = document.createElement('a');
      link.href = file;
      link.download = file.substring(file.lastIndexOf('/') + 1); // ดาวน์โหลดไฟล์ Word
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    } else {
      console.log('ไม่สามารถแสดงผลไฟล์ประเภทนี้ได้');
    }
  } else if (file instanceof File) {
    // กรณี file เป็น File object (ไฟล์ที่อัพโหลดใหม่)
    const fileUrl = URL.createObjectURL(file); // สร้าง URL ชั่วคราวสำหรับไฟล์
    if (file.type === 'application/pdf' || file.type.match(/image\/(jpeg|jpg|png)/i)) {
      window.open(fileUrl, '_blank'); // เปิดไฟล์ PDF หรือรูปภาพในแท็บใหม่
    } else if (file.type === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document') {
      const link = document.createElement('a');
      link.href = fileUrl;
      link.download = file.name; // ดาวน์โหลดไฟล์ Word
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    } else {
      console.log('ไม่สามารถแสดงผลไฟล์ประเภทนี้ได้');
    }
    URL.revokeObjectURL(fileUrl); // ลบ URL object หลังจากใช้งานเสร็จ
  }
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

          <!-- ช่อง "รหัสรายการเบิก " -->

          <div>
            <label for="rqCode" class="block text-sm font-medium py-2">รหัสรายการเบิก </label>
            <p class="inputItem bg-[#F7F7F7] text-[#BABBBE]">{{ rqCode }}</p>
          </div>

          <!-- ช่อง "ชื่อรายการเบิก *" -->
          <div>
            <label for="rqName" class="block text-sm font-medium py-2"
              :class="{ 'text-red-500': errors.rqName }">ชื่อรายการเบิก <span class="text-red-500">*</span></label>
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
            <label for="rqPayDate" class="block text-sm font-medium py-2"
              :class="{ 'text-red-500': errors.selectedDate }">วันที่ทำการเบิกค่าใช้จ่าย <span
                class="text-red-500">*</span></label>
            <SingleDatePicker v-model="selectedDate" id="rqPayDate" v-model:isOpen="isDatePickerOpen"
              :confirmedDate="selectedDate" class="dateInput w-full h-[42px] text-black" placeholder="เลือกวันที่"
              :class="['dateInput', { 'error': errors.selectedDate }]" />
          </div>

          <!-- ช่อง "โครงการ *" -->
          <div>
            <label for="projectName" class="block text-sm font-medium py-2"
              :class="{ 'text-red-500': errors.rqPjId }">โครงการ<span class="text-red-500">*</span></label>
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

          <!-- ช่อง "ประเภทค่าใช้จ่าย *" -->

          <div>
            <!-- Dropdown -->
            <label for="expenseType" class="block text-sm font-medium py-2" :class="{ 'text-red-500': errors.rqRqtId }">
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

          <!-- ช่อง "ประเภทค่าใช้จ่ายอื่นๆ *" -->
          <div v-show="rqtName == 'อื่นๆ'">
            <label for="rqAny" class="block text-sm font-medium py-2" :class="{ 'text-red-500': errors.rqAny }">
              ประเภทค่าใช้จ่ายอื่นๆ <span class="text-red-500">*</span>
            </label>
            <input id="rqAny" v-model="formData.rqAny" :class="['inputItem', { 'error': errors.rqAny }]" type="text"
              placeholder="กรุณาระบุข้อมูลเพิ่มเติม" />
          </div>

          <!-- ช่อง "ประเภทการเดินทาง *" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <!-- Dropdown เลือกประเภทการเดินทาง -->
            <label for="travelType" class="block text-sm font-medium py-2"
              :class="{ 'text-red-500': errors.selectedTravelType }">
              ประเภทการเดินทาง <span class="text-red-500">*</span>
            </label>
            <div class="text-xs">
              <select id="travelType" v-model="selectedTravelType"
                :class="['inputItem', { 'error': errors.selectedTravelType }]">
                <option value="" disabled>เลือกประเภทการเดินทาง</option>
                <option v-for="type in uniqueTravelTypes" :value="type">
                  {{ type === 'private' ? 'ประเภทส่วนตัว' : 'ประเภทสาธารณะ' }}
                </option>
              </select>
            </div>
          </div>

          <!-- ช่อง "ประเภทรถ *" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <!-- Dropdown เลือกประเภทรถ (กรองตามประเภทการเดินทางที่เลือก) -->
            <label for="vehicleType" class="block text-sm font-medium py-2" :class="{ 'text-red-500': errors.vhId }">
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

          <!-- ช่อง "สถานที่เริ่มต้น *" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <label for="rqStartLocation" class="block text-sm font-medium py-2"
              :class="{ 'text-red-500': errors.rqStartLocation }">สถานที่เริ่มต้น <span
                class="text-red-500">*</span></label>
            <input type="text" id="rqStartLocation" v-model="formData.rqStartLocation"
              :class="['inputItem', { 'error': errors.rqStartLocation }]" />
          </div>
          <!-- ช่อง "สถานที่สิ้นสุด *" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <label for="rqEndLocation" class="block text-sm font-medium py-2"
              :class="{ 'text-red-500': errors.rqEndLocation }">สถานที่สิ้นสุด <span
                class="text-red-500">*</span></label>
            <input type="text" id="rqEndLocation" v-model="formData.rqEndLocation"
              :class="['inputItem', { 'error': errors.rqEndLocation }]" />
          </div>

          <!-- ช่อง "ระยะทาง *" -->
          <div v-if="rqtName === 'ค่าเดินทาง'">
            <label for="rqDistance" class="block text-sm font-medium py-2"
              :class="{ 'text-red-500': errors.rqDistance }">ระยะทาง <span class="text-red-500">*</span></label>
            <input type="text" id="rqDistance" v-model="formData.rqDistance" @input="calculateExpenses"
              :class="['inputItem', { 'error': errors.rqDistance }]" />
          </div>

          <!-- ช่อง "อัตราค่าเดินทาง" -->
          <div v-if="rqtName === 'ค่าเดินทาง' && selectedTravelType === 'private'">
            <label for="rqPayrate" class="block text-sm font-medium py-2">อัตราค่าเดินทาง </label>
            <p class="inputItem bg-[#F7F7F7] text-[#BABBBE]">{{ selectedPayrate }}</p>
          </div>

          <!-- ช่อง "จำนวนเงิน (บาท) *" -->
          <div>
            <label for="rqExpenses" class="block text-sm font-medium py-2"
              :class="{ 'text-red-500': errors.rqExpenses }">จำนวนเงิน (บาท) <span class="text-red-500">*</span></label>
            <input type="number" id="rqExpenses" v-model="displayRqExpenses" @blur="formatRqExpenses" :class="[
              'inputItem ',
              {
                error: errors.rqExpenses,
                'bg-gray-200 text-gray-500 cursor-not-allowed  bg-[#F7F7F7] text-[#BABBBE]':
                  rqtName === 'ค่าเดินทาง' &&
                  selectedTravelType === 'private',
              },
            ]" :disabled="rqtName === 'ค่าเดินทาง' && selectedTravelType === 'private'
              " />
          </div>

          <!-- ช่อง "ชื่อผู้ขอเบิกแทน" -->
          <div>
            <label for="rqInsteadEmail" class="block text-sm font-medium py-2">ชื่อผู้ขอเบิกแทน </label>
            <select type="text" id="rqInsteadEmail" v-model="formData.rqInsteadEmail" class="inputItem">
              <option :value="null" disabled selected>Select User</option>
              <option :value="user.usrEmail" v-for="user in requisitionStore.UserInstead" :key="user.usrEmail">
                {{ user.usrName }}
              </option>
            </select>
            <button v-if="formData.rqInsteadEmail" @click="formData.rqInsteadEmail = null"
              class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 text-sm bg-gray-200 p-1 rounded-full">
              X
            </button>
          </div>
        </div>

      </div>
      <!-- ช่อง "รายละเอียด *" -->
      <div class="text-sm my-4">
        <label class="block text-sm font-medium pb-2" :class="{ 'text-red-500': errors.rqPurpose }">รายละเอียด <span
            class="text-red-500">*</span></label>
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
            <p class="mt-3">DOCS JPEG หรือ PDF (MAX 2MB)</p>
          </div>
        </div>
      </div>
    </div>

    <FileDisplay v-for="fileObj in selectedFiles" :key="fileObj.file.name || fileObj.file.lastModified"
      :file="fileObj.file" :fileName="fileObj.fileName" @remove="removeFile(fileObj.fId, fileObj.file.name)"
      @preview="previewFile(fileObj.file)" />

    <!-- Popup บันทึก -->
    <div v-if="isPopupSaveOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
        <div class="flex justify-center mb-4">
          <svg :class="`w-[90px] h-[90px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
            <path fill-rule="evenodd"
              d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
              clip-rule="evenodd" />
          </svg>
        </div>
        <h2 class="text-[24px] font-bold text-center text-black mb-4">
          ยืนยันการบันทึกรายการเบิกค่าใช้จ่าย
        </h2>
        <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
          คุณยืนยันการบันทึกรายการเบิกค่าใช้จ่ายหรือไม่ ?
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
          <svg :class="`w-[90px] h-[90px] text-gray-800 dark:text-white`" aria-hidden="true"
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
          <svg :class="`w-[90px] h-[90px] text-gray-800 dark:text-white`" aria-hidden="true"
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
          ยืนยันการบันทึกรายการเบิกค่าใช้จ่ายสำเร็จ
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

}
</style>