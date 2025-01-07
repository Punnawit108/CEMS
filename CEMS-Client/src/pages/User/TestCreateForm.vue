<template>
  <div>
    <!-- Dropdown -->
    <label for="expenseType" class="block text-sm font-medium py-1">
      ประเภทค่าใช้จ่าย
    </label>
    <select
      id="expenseType"
      v-model="formData.rqRqtId"
      class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
      @change="handleSelectChange"
    >
      <option value="">กรุณาเลือกประเภท</option>
      <option
        v-for="requisitionTypeData in requisitionStore.requisitionType"
        :key="requisitionTypeData.rqtId"
        :value="requisitionTypeData.rqtId"
      >
        {{ requisitionTypeData.rqtName }}
      </option>
    </select>

    <!-- Input ที่จะแสดงเมื่อเลือก ID = 2 -->
    <div v-show="formData.rqRqtId === '2'">
      <label for="additionalInfo" class="block text-sm font-medium py-1">
        ระบุข้อมูลเพิ่มเติม
      </label>
      <input
        id="additionalInfo"
        v-model="formData.additionalInfo"
        class="px-3 py-2 border border-gray-400 bg-white rounded-md sm:text-sm sm:w-full md:w-[400px] focus:border-gray-400 focus:ring-0 focus:outline-none"
        type="text"
        placeholder="กรุณาระบุข้อมูลเพิ่มเติม"
      />
    </div>

    <!-- Debugging Display -->
    <p class="mt-4 text-gray-600">
      Selected Expense Type ID: {{ formData.rqRqtId }}
    </p>
    <p v-if="formData.rqRqtId === '2'" class="mt-1 text-gray-600">
      Additional Info: {{ formData.additionalInfo }}
    </p>
  </div>
</template>

<script setup lang="ts">
/*
 * ชื่อไฟล์: CreateExpenseForm.vue
 * คำอธิบาย: ไฟล์นี้แสดงฟอร์มเบิกค่าใช้จ่าย
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 4 มกราคม 2568
 */

import { computed, onMounted, ref, watch } from "vue";
import Button from "../../components/template/Button.vue";
import { useRequisitionStore } from "../../store/requisition";
import router from "../../router";

const user = ref<any>(null);
const requisitionStore = useRequisitionStore();

const rqtId = ref(2);
const startPickerOpen = ref(false);
const rqtName = ref("");
const customExpenseType = ref("");
const isOtherSelected = ref(false);
const isCustomExpenseTypeAdded = ref(false);
const isPopupSaveOpen = ref(false);
const isPopupCancleOpen = ref(false);
const isPopupSubmitOpen = ref(false);
const isAlertSaveOpen = ref(false);
const isAlertCancleOpen = ref(false);
const isAlertSubmitOpen = ref(false);

const formData = ref({
  rqName: "",
  rqUsrId: "",
  rqPjId: "",
  rqRqtId: rqtId.value.toString(),
  rqVhId: null,
  rqPayDate: "",
  rqWithdrawDate: null,
  rqCode: null,
  rqInsteadEmail: null,
  rqExpenses: null,
  rqStartLocation: null,
  rqEndLocation: null,
  rqDistance: null,
  rqPurpose: null,
  rqReason: "",
  rqProof: null,
  rqStatus: "",
  rqProgress: "accepting",
  additionalInfo: "",
});

const selectedExpenseTypeId = ref("");
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
});

watch(
  () => formData.value.rqRqtId,
  (newValue) => {
    if (newValue === "อื่นๆ") {
      selectedExpenseTypeId.value =
        customExpenseType.value || "กรุณาระบุประเภทค่าใช้จ่าย";
    } else {
      const selectedType = requisitionStore.requisitionType.find(
        (type) => type.rqtId === newValue
      );
      selectedExpenseTypeId.value = selectedType ? selectedType.rqtName : "";
    }
  }
);

watch(
  () => formData.value.rqRqtId,
  (newValue) => {
    if (newValue === "2") {
      // Handle specific logic when rqRqtId is 2
    }
  }
);

const handleSelectChange = () => {
  if (formData.value.rqRqtId !== "อื่นๆ") {
    formData.value.additionalInfo = "";
  }
};

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

const confirmSave = async (event: Event) => {
  event.preventDefault();
  isAlertSaveOpen.value = true;
  formData.value.rqStatus = "sketch";
  formData.value.rqUsrId = user.value.usrId;
  await requisitionStore.createExpense(formData.value);

  setTimeout(() => {
    isAlertSaveOpen.value = false;
    closePopupSave();
  }, 1500);
};

const confirmSubmit = async (event: Event) => {
  event.preventDefault();
  isAlertSubmitOpen.value = true;
  formData.value.rqStatus = "waiting";
  formData.value.rqUsrId = user.value.usrId;

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

const handleDateConfirm = (type: "start" | "end", confirmedDate: Date) => {
  if (type === "start") {
    startPickerOpen.value = false;
  } else {
    startPickerOpen.value = false;
  }
};

const handleDateCancel = (type: "start" | "end") => {
  if (type === "start") {
    startPickerOpen.value = false;
  }
};
</script>
