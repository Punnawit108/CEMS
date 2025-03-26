<script setup lang="ts">
/*
 * ชื่อไฟล์: ExpenseManage.vue
 * คำอธิบาย: ไฟล์นี้สามารถตั้งค่าประเภทการเดินทางต่างๆ ที่มีอยู่ในระบบ เพิ่ม แก้ไข หรือปิดการใช้งานได้
 * ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
 * วันที่จัดทำ/แก้ไข: 22 มีนาคม 2568
 */
import { onMounted, ref, reactive } from "vue";
import Button from "../../components/Buttons/Button.vue";
import { useExpenseManageStore } from "../../store/expenseManageStore";
import Icon from "../../components/Icon/CIcon.vue";

const expenseManageType = useExpenseManageStore();

const expenseType = ref<any>(null);
const vehiclePrivate = ref<any>(null);
const vehiclePublic = ref<any>(null);

onMounted(async () => {
  expenseType.value = await expenseManageType.getRequisitionType();
  vehiclePrivate.value = await expenseManageType.getVehiclePrivate();
  vehiclePublic.value = await expenseManageType.getVehiclePublic();
});

const formData = reactive<any>({
  vhType: "",
  vhVehicle: "",
  vhPayrate: null,
});

const formRequisitiontype = reactive<any>({
  rqtName: "",
});

// State สำหรับแถวส่วนตัว
const privateRows = ref<
  {
    vehicleType: string;
    fareRate: number | null;
    isSubmitted: boolean;
    isDisabled: boolean;
    isIconChanged: boolean;
  }[]
>([]);
const publicRows = ref<
  {
    vehicleType: string;
    fareRate: number | null;
    isSubmitted: boolean;
    isDisabled: boolean;
    isIconChanged: boolean;
  }[]
>([]);
const expenseRows = ref<
  {
    expenseType: string;
    fareRate: number | null;
    isSubmitted: boolean;
    isDisabled: boolean;
    isIconChanged: boolean;
  }[]
>([]);

const isHiddenPrivate = ref(false);
const isHiddenPublic = ref(false);
const isHiddenExpense = ref(false);
const isHiddenType = ref(false);
const isHiddenTypeVehical = ref(true);

const isPopupAddExpenseOpen = ref(false); // สำหรับเปิด/ปิด Popup Add ประเภทค่าใช้จ่าย
const isPopupConfirmAddExpenseOpen = ref(false); // สำหรับเปิด/ปิด Popup ConfirmAdd ประเภทค่าใช้จ่าย
const isAddExpenseAlertOpen = ref(false); // ควบคุมการแสดง Alert Add

const isPopupAddPrivatecarOpen = ref(false); // สำหรับเปิด/ปิด Popup Add ประเภทรถส่วนตัว
const isPopupConfirmAddPrivatecarOpen = ref(false); // สำหรับเปิด/ปิด Popup ConfirmAdd ประเภทรถส่วนตัว

const isPopupAddPublictravelOpen = ref(false); // สำหรับเปิด/ปิด Popup Add ประเภทรถสาธารณะ
const isPopupConfirmAddPublictravelOpen = ref(false); // สำหรับเปิด/ปิด Popup ConfirmAdd ประเภทรถสาธารณะ
const isPublictravelAlertOpen = ref(false); // ควบคุมการแสดง Alert ประเภทรถสาธารณะ

const handleClickTypeVehicl = () => {
  toggleDivsTypeVehicle();
};

const toggleDivsTypeVehicle = () => {
  isHiddenTypeVehical.value = true;
  isHiddenType.value = false;
};

const toggleDivsType = () => {
  isHiddenType.value = true;
  isHiddenTypeVehical.value = false;
};

const toggleDivsPrivate = () => {
  isHiddenPublic.value = !isHiddenPublic.value;
};
const toggleDivsExpense = () => {
  isHiddenExpense.value = !isHiddenExpense.value;
};

const toggleDivs = () => {
  isHiddenPrivate.value = !isHiddenPrivate.value;
};
const handleClickPublic1 = () => {
  startAddPublic(); // เรียกฟังก์ชันแรก
  toggleDivsPrivate(); // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickPublic2 = () => {
  cancelAddPublic(); // เรียกฟังก์ชันแรก
  toggleDivsPrivate();
  // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickPublic3 = () => {
  submitPublicRow(); // เรียกฟังก์ชันแรก
  toggleDivsPrivate();
  // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickPrivate1 = () => {
  startAddPrivate(); // เรียกฟังก์ชันแรก
  toggleDivs(); // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickPrivate2 = () => {
  cancelAddPrivate(); // เรียกฟังก์ชันแรก
  toggleDivs();
  // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickPrivate3 = () => {
  submitPrivateRow(); // เรียกฟังก์ชันแรก
  toggleDivs();
  // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickExpense1 = () => {
  startAddExpense(); // เรียกฟังก์ชันแรก
  toggleDivsExpense();
  // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickExpense2 = () => {
  submitExpenseRow(); // เรียกฟังก์ชันแรก
  toggleDivsExpense();
  // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickExpense3 = () => {
  cancelAddExpense(); // เรียกฟังก์ชันแรก
  toggleDivsExpense();
  // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};

// State สำหรับแถวสาธารณะ

let currentPrivateIndex = ref(-1); // ดัชนีของแถวที่กำลังแก้ไขสำหรับส่วนตัว
let currentPublicIndex = ref(-1); // ดัชนีของแถวที่กำลังแก้ไขสำหรับสาธารณะ
let currentExpenseIndex = ref(-1);
const isAddingPrivate = ref(false); // สถานะสำหรับการเพิ่มแถวส่วนตัว
const isAddingPublic = ref(false); // สถานะสำหรับการเพิ่มแถวสาธารณะ
const isAddingExpense = ref(false); // สถานะสำหรับการเพิ่มแถวประเภทเบิกจ่าย

// ฟังก์ชันเริ่มต้นการเพิ่มแถวส่วนตัว
function startAddPrivate() {
  privateRows.value.push({
    vehicleType: "",
    fareRate: null,
    isSubmitted: false,
    isDisabled: false,
    isIconChanged: false,
  });
  currentPrivateIndex.value = privateRows.value.length - 1;
  isAddingPrivate.value = true;
}

// ฟังก์ชันเริ่มต้นการเพิ่มแถวสาธารณะ
function startAddPublic() {
  publicRows.value.push({
    vehicleType: "",
    fareRate: null,
    isSubmitted: false,
    isDisabled: false,
    isIconChanged: false,
  });
  currentPublicIndex.value = publicRows.value.length - 1;
  isAddingPublic.value = true; // ตั้งค่าสถานะเป็นจริงเพื่อแสดงปุ่ม
}
function startAddExpense() {
  expenseRows.value.push({
    expenseType: "",
    fareRate: null,
    isSubmitted: false,
    isDisabled: false,
    isIconChanged: false,
  });
  currentExpenseIndex.value = expenseRows.value.length - 1;
  isAddingExpense.value = true; // ตั้งค่าสถานะเป็นจริงเพื่อแสดงปุ่ม
}

// ฟังก์ชันสำหรับยืนยันแถว
function submitPrivateRow() {
  if (currentPrivateIndex.value >= 0) {
    const currentRow = privateRows.value[currentPrivateIndex.value];
    currentRow.isSubmitted = true;
    currentPrivateIndex.value = -1;
    isAddingPrivate.value = false;
  }
}

function submitPublicRow() {
  if (currentPublicIndex.value >= 0) {
    publicRows.value[currentPublicIndex.value].isSubmitted = true;
    currentPublicIndex.value = -1;
    isAddingPublic.value = false;
  }
}

function submitExpenseRow() {
  if (currentExpenseIndex.value >= 0) {
    expenseRows.value[currentExpenseIndex.value].isSubmitted = true;
    currentExpenseIndex.value = -1;
    isAddingExpense.value = false;
  }
}

// ฟังก์ชันสำหรับยกเลิกการเพิ่มแถว
function cancelAddPrivate() {
  if (currentPrivateIndex.value >= 0) {
    privateRows.value.splice(currentPrivateIndex.value, 1);
    currentPrivateIndex.value = -1;
    isAddingPrivate.value = false;
  }
}

function cancelAddPublic() {
  if (currentPublicIndex.value >= 0) {
    publicRows.value.splice(currentPublicIndex.value, 1);
    currentPublicIndex.value = -1;
    isAddingPublic.value = false;
  }
}

function cancelAddExpense() {
  if (currentExpenseIndex.value >= 0) {
    expenseRows.value.splice(currentExpenseIndex.value, 1);
    currentExpenseIndex.value = -1;
    isAddingExpense.value = false;
  }
}

// ฟังก์ชันสำหรับเปลี่ยนสีแถวและไอคอน
async function toggleGray(index: number) {
  await expenseManageType.changeVehicle(index);
  vehiclePrivate.value = await expenseManageType.getVehiclePrivate();
  vehiclePublic.value = await expenseManageType.getVehiclePublic();
}
async function toggleGray2(index: number) {
  await expenseManageType.changeRequisitionType(index);
  expenseType.value = await expenseManageType.getRequisitionType();
}
function toggleGray3(index: number) {
  expenseRows.value[index].isDisabled = !expenseRows.value[index].isDisabled;
  expenseRows.value[index].isIconChanged =
    !expenseRows.value[index].isIconChanged;
}

const isFormSubmitted = ref(false);

// Add ประเภทค่าใช้จ่าย
const openPopupAddExpense = () => {
  isPopupAddExpenseOpen.value = true;
  isFormSubmitted.value = false; // รีเซ็ตสถานะเมื่อเปิด Popup
};

const closePopupAddExpense = () => {
  isPopupAddExpenseOpen.value = false;
  isFormSubmitted.value = false; // รีเซ็ตสถานะเมื่อปิด Popup
};
// เปิด PopupConfirmAdd ค่าใช้จ่าย
const openPopupConfirmAddExpense = () => {
  console.log(formData.value);
  isFormSubmitted.value = true; // ตั้งค่าสถานะว่าฟอร์มถูกส่งแล้ว

  // ตรวจสอบว่าฟิลด์จำเป็นถูกกรอกหรือไม่
  if (formRequisitiontype.rqtName.trim() === "") {
    return; // ไม่เปิด Popup ยืนยันถ้าฟิลด์จำเป็นไม่ถูกกรอก
  }

  isPopupConfirmAddExpenseOpen.value = true;
};
const closePopupConfirmAddExpense = () => {
  isPopupConfirmAddExpenseOpen.value = false;
};
const confirmAddExpense = async () => {
  await expenseManageType.createRequisitionType(formRequisitiontype);
  expenseType.value = await expenseManageType.getRequisitionType();

  // เปิด Popup Alert
  isAddExpenseAlertOpen.value = true;
  // ตั้งเวลาให้ Alert ปิดอัตโนมัติใน 1.5 วินาที
  setTimeout(() => {
    isAddExpenseAlertOpen.value = false; // ปิด Alert
    closePopupAddExpense(); // ปิด Popup แก้ไข
    closePopupConfirmAddExpense(); // ปิด Popup ยืนยัน
  }, 1500); // 1.5 วินาที
};

// Add ประเภทรถส่วนตัว
const isFormSubmittedPrivate = ref(false);

const openPopupAddPrivatecar = () => {
  formData.vhPayrate = 7;
  isPopupAddPrivatecarOpen.value = true;
  isFormSubmittedPrivate.value = false;
};
const closePopupAddPrivatecar = () => {
  isPopupAddPrivatecarOpen.value = false;
  isFormSubmittedPrivate.value = false;
  formData.vhVehicle = ""; // รีเซ็ตค่า
};
// เปิด PopupConfirmAdd ประเภทรถส่วนตัว
const openPopupConfirmAddPrivatecar = () => {
  isFormSubmittedPrivate.value = true;

  // ตรวจสอบว่าฟิลด์จำเป็นถูกกรอกหรือไม่
  if (formData.vhVehicle.trim() === "") {
    // เปลี่ยนจาก vhVehicle เป็น vhName
    return; // ไม่เปิด Popup ยืนยันถ้าฟิลด์จำเป็นไม่ถูกกรอก
  }

  isPopupConfirmAddPrivatecarOpen.value = true;
};
const closePopupConfirmAddPrivatecar = () => {
  isPopupConfirmAddPrivatecarOpen.value = false;
};

const isAddPrivatecarAlertOpen = ref(false);
const confirmAddPrivatecar = async () => {
  // เปิด Popup Alert
  formData.vhType = "private";
  await expenseManageType.createVehicle(formData);
  vehiclePrivate.value = await expenseManageType.getVehiclePrivate();
  // ตั้งเวลาให้ Alert ปิดอัตโนมัติใน 1.5 วินาที
  isAddPrivatecarAlertOpen.value = true;
  setTimeout(() => {
    isAddPrivatecarAlertOpen.value = false;
    closePopupAddPrivatecar(); // ปิด Popup แก้ไข
    closePopupConfirmAddPrivatecar();
  }, 1500); // 1.5 วินาที
};

// Add ประเภทสาธารณะ
const isFormSubmittedPublic = ref(false);

const openPopupAddPublictravel = () => {
  isPopupAddPublictravelOpen.value = true;
  isFormSubmittedPublic.value = false; // รีเซ็ตสถานะเมื่อเปิด Popup
};

const closePopupAddPublictravel = () => {
  isPopupAddPublictravelOpen.value = false;
  isFormSubmittedPublic.value = false; // รีเซ็ตสถานะเมื่อปิด Popup
  formData.vhVehicle = ""; // รีเซ็ตค่า
};

const openPopupConfirmAddPublictravel = () => {
  isFormSubmittedPublic.value = true; // ตั้งค่าสถานะว่าฟอร์มถูกส่งแล้ว

  // ตรวจสอบว่าฟิลด์จำเป็นถูกกรอกหรือไม่
  if (formData.vhVehicle.trim() === "") {
    // เปลี่ยนจาก vhVehicle เป็น vhName
    return; // ไม่เปิด Popup ยืนยันถ้าฟิลด์จำเป็นไม่ถูกกรอก
  }

  isPopupConfirmAddPublictravelOpen.value = true;
};

const closePopupConfirmAddPublictravel = () => {
  isPopupConfirmAddPublictravelOpen.value = false;
};
const confirmAddPublictravel = async () => {
  // เปิด Popup Alert
  formData.vhType = "public";
  formData.vhPayrate = null;
  await expenseManageType.createVehicle(formData);
  vehiclePublic.value = await expenseManageType.getVehiclePublic();

  isPublictravelAlertOpen.value = true;
  // ตั้งเวลาให้ Alert ปิดอัตโนมัติใน 1.5 วินาที
  setTimeout(() => {
    isPublictravelAlertOpen.value = false; // ปิด Alert
    closePopupAddPublictravel(); // ปิด Popup แก้ไข
    closePopupConfirmAddPublictravel(); // ปิด Popup ยืนยัน
  }, 1500); // 1.5 วินาที
};
// State for active button
const activeButton = ref<"expense" | "transport" | null>("expense");

// Toggle functions
const toggleButton = (button: "expense" | "transport") => {
  activeButton.value = button;
};

// Edit ประเภทรถส่วนตัว
const isPopupEditPrivatecarOpen = ref(false); // สำหรับเปิด/ปิด Popup Add ประเภทรถส่วนตัว
const isPopupConfirmEditPrivatecarOpen = ref(false); // สำหรับเปิด/ปิด Popup ConfirmAdd ประเภทรถส่วนตัว

// เปิด Popup edit ประเภทรถส่วนตัว
const isVhPrivateUse = ref(false);
const openPopupEditPrivatecar = async (data: any) => {
  const isInUse = await expenseManageType.validationVehicle(data.id);
  if (isInUse) {
    isVhPrivateUse.value = true;
    setTimeout(() => {
      isVhPrivateUse.value = false;
    }, 1500);
  } else {
    formVehiclePrivateEdit.vhId = data.id;
    formVehiclePrivateEdit.vhVehicle = data.vhName;
    formVehiclePrivateEdit.vhPayrate = data.payRate;
    isPopupEditPrivatecarOpen.value = true;
  }
};
const formVehiclePrivateEdit = reactive<any>({
  vhId: "",
  vhVehicle: "",
  vhPayrate: "",
});

const closePopupEditPrivatecar = () => {
  isPopupEditPrivatecarOpen.value = false;

  formVehiclePrivateEdit.vhVehicle = "";
  formVehiclePrivateEdit.vhPayrate = "";
};
// เปิด PopupConfirmAdd ประเภทรถส่วนตัว
const openPopupConfirmEditPrivatecar = () => {
  if (!formVehiclePrivateEdit.vhVehicle.trim() || !formVehiclePrivateEdit.vhPayrate.trim()) {
    return;
  }
  isPopupConfirmEditPrivatecarOpen.value = true;
};

const closePopupConfirmEditPrivatecar = () => {
  isPopupConfirmEditPrivatecarOpen.value = false;
};

const isEditPrivatecarAlertOpen = ref(false);
const confirmEditPrivatecar = async () => {
  // เปิด Popup Alert
  formData.vhType = "private";
  await expenseManageType.changeVehiclePrivate(formVehiclePrivateEdit);
  isEditPrivatecarAlertOpen.value = true;
  vehiclePrivate.value = await expenseManageType.getVehiclePrivate();
  // ตั้งเวลาให้ Alert ปิดอัตโนมัติใน 1.5 วินาที
  setTimeout(() => {
    isEditPrivatecarAlertOpen.value = false; // ปิด Alert
    closePopupEditPrivatecar(); // ปิด Popup แก้ไข
    closePopupConfirmEditPrivatecar(); // ปิด Popup ยืนยัน
  }, 1500); // 1.5 วินาที
};

// Edit ประเภทรถสาธารณะ
const isVhPublicUse = ref(false);
const openPopupEditPubliccar = async (data: any) => {
  console.log(data.id);
  const isInUse = await expenseManageType.validationVehicle(data.id);
  if (isInUse) {
    isVhPublicUse.value = true;
    setTimeout(() => {
      isVhPublicUse.value = false;
    }, 1500);
  } else {
    formVehiclePublicEdit.vhId = data.id;
    formVehiclePublicEdit.vhVehicle = data.vhName;
    isPopupEditPubliccarOpen.value = true;
  }
};
const formVehiclePublicEdit = reactive<any>({
  vhId: "",
  vhVehicle: "",
});

// Edit Popup ประเภทรถสาธารณะ
const isPopupEditPubliccarOpen = ref(false);
const isPopupConfirmEditPubliccarOpen = ref(false);

const closePopupEditPubliccar = () => {
  isPopupEditPubliccarOpen.value = false;
  formVehiclePublicEdit.vhVehicle = "";
};

const openPopupConfirmEditPubliccar = () => {
  if (!formVehiclePublicEdit.vhVehicle.trim()) {
    return;
  }
  isPopupConfirmEditPubliccarOpen.value = true;
};
const closePopupConfirmEditPubliccar = () => {
  isPopupConfirmEditPubliccarOpen.value = false;
};

const isEditPubliccarAlertOpen = ref(false);
const confirmEditPubliccar = async () => {
  // เปิด Popup Alert
  formData.vhType = "public";
  await expenseManageType.changeVehiclePublic(formVehiclePublicEdit);
  isEditPubliccarAlertOpen.value = true;
  vehiclePublic.value = await expenseManageType.getVehiclePublic();
  // ตั้งเวลาให้ Alert ปิดอัตโนมัติใน 1.5 วินาที
  setTimeout(() => {
    isEditPubliccarAlertOpen.value = false; // ปิด Alert
    closePopupEditPubliccar(); // ปิด Popup แก้ไข
    closePopupConfirmEditPubliccar(); // ปิด Popup ยืนยัน
  }, 1500); // 1.5 วินาที
};

//Edit ประเภทค่าใช้จ่าย
const isPopupConfirmEditExpenseOpen = ref(false);

const openPopupConfirmEditExpense = () => {
  if (!formRequisitionTypeEdit.rqtName.trim()) {
    return;
  }
  isPopupConfirmEditExpenseOpen.value = true;
};
const isPopupUpdateExpenseOpen = ref(false); // สำหรับเปิด/ปิด Popup Delete

const UpdateRqtId = ref(0);

const isRqtUse = ref(false);

const openPopupUpdateExpense = async (data: any) => {
  const isInUse = await expenseManageType.validationRequisitionTypes(
    data.rqtId
  );
  if (isInUse) {
    isRqtUse.value = true;
    setTimeout(() => {
      isRqtUse.value = false; // ปิด Alert
    }, 1500);
  } else {
    formRequisitionTypeEdit.rqtId = data.rqtId;
    formRequisitionTypeEdit.rqtName = data.rqtName;
    isPopupUpdateExpenseOpen.value = true;
  }
};

const closePopupUpdateExpense = () => {
  isPopupUpdateExpenseOpen.value = false;
};
const closePopupConfirmEditExpense = () => {
  isPopupConfirmEditExpenseOpen.value = false;
};

const formRequisitionTypeEdit = reactive<any>({
  rqtId: "",
  rqtName: "",
});

const isEditExpenseAlertOpen = ref(false);
const confirmUpdateExpense = async () => {
  // เปิด Popup Alert
  formData.vhType = "expense";
  await expenseManageType.updateRequisitionType(formRequisitionTypeEdit);
  isEditExpenseAlertOpen.value = true;
  expenseType.value = await expenseManageType.getRequisitionType();
  closePopupUpdateExpense();
  // ตั้งเวลาให้ Alert ปิดอัตโนมัติใน 1.5 วินาที
  setTimeout(() => {
    isEditExpenseAlertOpen.value = false; // ปิด Alert
    closePopupUpdateExpense(); // ปิด Popup แก้ไข
    closePopupConfirmEditExpense(); // ปิด Popup ยืนยัน
  }, 1500); // 1.5 วินาที
};

//DELETE ประเภทค่าเดินทาง
const isPopupDeleteOpen = ref(false); // สำหรับเปิด/ปิด Popup Delete
const delVhId = ref(0);
// เปิด Popup Delete
const openPopupDelete = async (id: number) => {
  const isInUse = await expenseManageType.validationVehicle(id);
  if (isInUse) {
    isVhPublicUse.value = true;
    setTimeout(() => {
      isVhPublicUse.value = false;
    }, 1500);
  } else {
    delVhId.value = id;
    isPopupDeleteOpen.value = true;
  }
};
const closePopupDelete = () => {
  isPopupDeleteOpen.value = false;
};

const isDeleteVehicleAlertOpen = ref(false);
const confirmDelete = async () => {
  await expenseManageType.deleteVehicle(delVhId.value);
  vehiclePrivate.value = await expenseManageType.getVehiclePrivate();
  vehiclePublic.value = await expenseManageType.getVehiclePublic();
  isDeleteVehicleAlertOpen.value = true;
  setTimeout(() => {
    isDeleteVehicleAlertOpen.value = false;
    closePopupDelete();
  }, 1500);
};

//DELETE ประเภทค่าใช้จ่าย
const isPopupDeleteExpenseOpen = ref(false); // สำหรับเปิด/ปิด Popup Delete

const delRqtId = ref(0);
const openPopupDeleteExpense = async (id: number) => {
  const isInUse = await expenseManageType.validationRequisitionTypes(id);
  if (isInUse) {
    isRqtUse.value = true;
    setTimeout(() => {
      isRqtUse.value = false;
    }, 1500);
  } else {
    delRqtId.value = id;
    isPopupDeleteExpenseOpen.value = true;
  }
};
const closePopupDeleteExpense = () => {
  isPopupDeleteExpenseOpen.value = false;
};
const isDeleteExpenseAlertOpen = ref(false);
const confirmDeleteExpense = async () => {
  await expenseManageType.deleteExpense(delRqtId.value);
  expenseType.value = await expenseManageType.getRequisitionType();
  isDeleteExpenseAlertOpen.value = true;
  setTimeout(() => {
    isDeleteExpenseAlertOpen.value = false;
    closePopupDeleteExpense();
  }, 1500);
};
</script>

<template>
  <div v-if="!isHiddenExpense">
    <div v-if="!isHiddenPublic">
      <div v-if="!isHiddenPrivate" class="flex space-x-7">
        <!-- Button 1: Expense Type -->
        <button :class="{
          'btn-gray bg-navyblue border-2 border-navyblue text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin':
            activeButton === 'expense',
          'btn-white bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin':
            activeButton !== 'expense',
        }" @click="
          () => {
            toggleButton('expense');
            handleClickTypeVehicl();
          }
        ">
          ประเภทค่าใช้จ่าย
        </button>
        <!-- Button 2: Transport -->
        <button :class="{
          'btn-gray bg-navyblue border-2 border-navyblue text-white rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin':
            activeButton === 'transport',
          'btn-white bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] p-4 flex items-center justify-center text-[14px] font-thin':
            activeButton !== 'transport',
        }" @click="
          () => {
            toggleButton('transport');
            toggleDivsType();
          }
        ">
          ประเภทค่าเดินทาง
        </button>
      </div>
    </div>
  </div>

  <div v-if="!isHiddenTypeVehical">
    <div v-if="!isHiddenPublic" class="flex flex-col items-end mb-5">
      <template v-if="!isAddingPrivate">
        <!--ปุ่มเพิ่ม ประเภทรถส่วนตัว -->
        <Button :type="'btn-private'" @click="openPopupAddPrivatecar" />
      </template>
      <template v-else>
        <div class="flex flex-row space-x-2">
          <Button :type="'btn-cancleBorderGray'" @click="handleClickPrivate2" />
          <Button :type="'btn-summit'" @click="handleClickPrivate3" />
        </div>
      </template>
    </div>
  </div>

  <!-- แถวส่วนตัว -->
  <div v-if="!isHiddenTypeVehical">
    <div v-if="!isHiddenPublic" class="flex flex-col space-y-4">

      <!-- หัวตาราง -->
      <div class="flex items-center w-full py-4">
        <div class=" w-[60%] flex space-x-4 px-3 ">
          <p class="text-sm text-black font-bold mr-4">ลำดับ</p>
          <p class="text-sm text-black font-bold">ประเภทรถส่วนตัว</p>
        </div>
        <div class="w-[30%] flex justify-end px-3">
          <p class="text-sm text-black font-bold">อัตราค่าเดินทาง (บาท/กิโลเมตร)</p>
        </div>
        <div class="w-[10%] flex justify-center ">
          <p class="text-sm text-black font-bold ml-20">จัดการ</p>
        </div>
      </div>

      <!-- ข้อมูล -->
      <div v-for="(item, index) in vehiclePrivate" :key="index"
        class="flex items-center w-full border-b border-gray-300 pb-4">

        <!-- ลำดับ + ชื่อ -->
        <div class="w-[60%] flex space-x-4 px-3">
          <p class="text-sm text-gray-800 px-3 mr-4">{{ index + 1 }}</p>
          <p :class="{ 'text-gray-400': item.vhVisible == 0 }" class="text-sm">{{ item.vhName }}</p>
        </div>

        <!-- อัตราค่าเดินทาง -->
        <div class="w-[30%] flex justify-end px-3">
          <p :class="{ 'text-gray-400': item.vhVisible == 0 }" class="text-sm">
            {{ item.payRate ? item.payRate.toLocaleString("en-US", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
            }) : '0.00' }}
          </p>
        </div>

        <!-- ปุ่มจัดการ -->
        <div class="w-[10%] flex justify-end gap-1">
          <button @click="openPopupEditPrivatecar(item)">
            <Icon icon="edit" class="text-gray-600 hover:text-gray-800" />
          </button>
          <button v-if="!item.isInUse" @click="openPopupDelete(item.id)">
            <Icon icon="bin" class="text-gray-600 hover:text-gray-800" />
          </button>
          <button @click="toggleGray(item.id)">
            <div class="flex items-center space-x-1">
              <template v-if="item.vhVisible == 1">
                <svg class="w-[24px] h-[24px] text-gray-800 dark:text-white" aria-hidden="true"
                  xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                  <path stroke="gray" stroke-width="1.5"
                    d="M21 12c0 1.2-4.03 6-9 6s-9-4.8-9-6c0-1.2 4.03-6 9-6s9 4.8 9 6Z" />
                  <path stroke="gray" stroke-width="2" d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                </svg>
              </template>
              <template v-else>
                <svg class="w-[24px] h-[24px] text-gray-800 dark:text-white" aria-hidden="true"
                  xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                  <path stroke="gray" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5"
                    d="M3.933 13.909A4.357 4.357 0 0 1 3 12c0-1 4-6 9-6m7.6 3.8A5.068 5.068 0 0 1 21 12c0 1-3 6-9 6-.314 0-.62-.014-.918-.04M5 19 19 5m-4 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                </svg>
              </template>
            </div>
          </button>
        </div>

      </div>
    </div>
    <div v-if="!isHiddenTypeVehical">
      <div v-if="!isHiddenPublic" class="mb-[24px]"></div>
    </div>
  </div>

  <!-- ปุ่มสำหรับแถวสาธารณะ -->

  <div v-if="!isHiddenTypeVehical">
    <div v-if="!isHiddenPrivate" class="flex flex-col items-end mb-5">
      <template v-if="!isAddingPublic">
        <!--ปุ่มเพิ่ม ประเภทรถสาธารณะ -->
        <Button :type="'btn-public1'" @click="openPopupAddPublictravel" />
      </template>
      <template v-else>
        <div class="flex flex-row space-x-2">
          <Button :type="'btn-cancleBorderGray'" @click="handleClickPublic2" />
          <Button :type="'btn-summit'" @click="handleClickPublic3" />
        </div>
      </template>
    </div>


    <!-- แถวสาธารณะ -->
    <div v-if="!isHiddenPrivate" class="flex flex-col space-y-4 ">
      <div class="flex items-center justify-between w-full py-4">
        <div class="flex w-1/3 space-x-4">
          <p class="text-sm text-black font-bold mr-5 ml-3">ลำดับ</p>
          <p class="text-sm text-black font-bold">ประเภทรถสาธารณะ</p>
        </div>
        <p class="w-1/2 text-sm text-right text-black"></p>
        <div class="w-[100px] flex justify-center">
          <p class="text-sm text-right text-black font-bold ml-3">จัดการ</p>
        </div>
      </div>

      <div v-for="(item, index) in vehiclePublic" :key="'public-' + index" class="flex flex-col space-y-4">
        <div :class="{ 'text-gray-400': item.isDisabled }" class="flex items-center justify-between w-full ">
          <div class="flex w-full space-x-4   ">
            <p class="text-sm px-3 text-grayNormal mr-5 ml-3" v-if="item.vhVisible == 0">
              {{ index + 1 }}
            </p>
            <p class="text-sm px-3 text-black mr-5 ml-3" v-if="item.vhVisible == 1">
              {{ index + 1 }}
            </p>
            <div v-if="!item.isSubmitted">
              <p class="text-black text-sm" v-if="item.vhVisible == 1">
                {{ item.vhName }}
              </p>
              <p class="text-grayNormal text-sm" v-if="item.vhVisible == 0">
                {{ item.vhName }}
              </p>
            </div>
            <div v-else>
              <p :class="{
                'text-gray-400': item.isDisabled,
                'text-black': !item.isDisabled,
              }" class="text-sm">
                {{ item.vehicleType }}
              </p>
            </div>
          </div>
          <div class="flex justify-end gap-1">
            <button>
              <Icon :icon="'edit'" @click="openPopupEditPubliccar(item)" />
            </button>
            <button>
              <Icon :icon="'bin'" @click="openPopupDelete(item.id)" />
            </button>
            <button @click="toggleGray(item.id)" class=" text-black">
              <div class="flex items-center space-x-1">
                <template v-if="item.vhVisible == 1">
                  <svg class="w-[24px] h-[24px] text-gray-800 dark:text-white" aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                    <path stroke="gray" stroke-width="1.5"
                      d="M21 12c0 1.2-4.03 6-9 6s-9-4.8-9-6c0-1.2 4.03-6 9-6s9 4.8 9 6Z" />
                    <path stroke="gray" stroke-width="2" d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                  </svg>
                </template>
                <template v-else>
                  <svg class="w-[24px] h-[24px] text-gray-800 dark:text-white" aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                    <path stroke="gray" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5"
                      d="M3.933 13.909A4.357 4.357 0 0 1 3 12c0-1 4-6 9-6m7.6 3.8A5.068 5.068 0 0 1 21 12c0 1-3 6-9 6-.314 0-.62-.014-.918-.04M5 19 19 5m-4 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                  </svg>
                </template>
              </div>
            </button>
          </div>
        </div>
        <hr class="border-gray-300" />
      </div>
    </div>
  </div>

  <div v-if="!isHiddenTypeVehical" class="mb-20"></div>
  <div v-if="!isHiddenType">
    <div v-if="!isHiddenPrivate" class="flex flex-col items-end mb-5">
      <template v-if="!isAddingExpense">
        <!-- ปุ่มเพิ่ม ประเภทค่าใช้จ่าย -->
        <Button :type="'btn-expenseType'" @click="openPopupAddExpense" />
      </template>
      <template v-else>
        <div class="flex flex-row space-x-2">
          <Button :type="'btn-cancleBorderGray'" @click="handleClickExpense3" />
          <Button :type="'btn-summit'" @click="handleClickExpense2" />
        </div>
      </template>
    </div>
  </div>

  <!-- แถวประเภทค่าใช้จ่าย -->
  <div v-if="!isHiddenType">
    <div v-if="!isHiddenPrivate" class="flex flex-col space-y-4">
      <div class="flex items-center justify-between w-full py-4">
        <div class="flex w-1/3 space-x-4">
          <p class="text-sm text-black font-bold mr-5 ml-3">ลำดับ</p>
          <p class="text-sm text-black font-bold">ประเภทค่าใช้จ่าย</p>
        </div>
        <p class="w-1/2 text-sm text-right text-black"></p>
        <div class="w-[100px] flex justify-center">
          <p class="text-sm text-right text-black font-bold">จัดการ</p>
        </div>
      </div>

      <div v-for="(item, index) in expenseType" :key="'public-' + index" class="flex flex-col space-y-4">
        <div :class="{ 'text-gray-400': item.rqtVisible == 0 }" class="flex items-center justify-between w-full">
          <div class="flex w-full space-x-4">
            <p class="text-sm px-3 text-grayNormal mr-3 ml-3" v-if="item.rqtVisible == 0">
              {{ index + 1 }}
            </p>
            <p class="text-sm px-3 text-black mr-3 ml-3" v-if="item.rqtVisible == 1">
              {{ index + 1 }}
            </p>
            <div v-if="!item.isSubmitted">
              <p class="text-black px-3 text-sm" v-if="item.rqtVisible == 1">
                {{ item.rqtName }}
              </p>
              <p class="text-grayNormal px-3 text-sm" v-if="item.rqtVisible == 0">
                {{ item.rqtName }}
              </p>
            </div>
            <div v-else>
              <p :class="{
                'text-gray-400': item.rqtVisible == 0,
                'text-black': !item.isDisabled,
              }" class="text-sm">
                {{ item.vehicleType }}
              </p>
            </div>
          </div>
          <div class="flex justify-end w-1/4">
            <button>
              <Icon :icon="'edit'" @click="openPopupUpdateExpense(item)" />
            </button>
            <button>
              <Icon :icon="'bin'" @click="openPopupDeleteExpense(item.rqtId)" />
            </button>
            <button @click="toggleGray2(item.rqtId)" class="px-2 py-1 text-black">
              <div class="flex items-center space-x-1">
                <template v-if="item.rqtVisible == 1">
                  <svg class="w-[24px] h-[24px] text-gray-800 dark:text-black" aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                    <path stroke="gray" stroke-width="1.5"
                      d="M21 12c0 1.2-4.03 6-9 6s-9-4.8-9-6c0-1.2 4.03-6 9-6s9 4.8 9 6Z" />
                    <path stroke="gray" stroke-width="2" d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                  </svg>
                </template>
                <template v-else>
                  <svg class="w-[24px] h-[24px] text-gray-800 dark:text-white" aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                    <path stroke="gray" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5"
                      d="M3.933 13.909A4.357 4.357 0 0 1 3 12c0-1 4-6 9-6m7.6 3.8A5.068 5.068 0 0 1 21 12c0 1-3 6-9 6-.314 0-.62-.014-.918-.04M5 19 19 5m-4 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                  </svg>
                </template>
              </div>
            </button>
          </div>
        </div>
        <hr class="border-gray-300" />
      </div>
    </div>
  </div>

  <!-- POPUP เพิ่ม1 ประเภทค่าใช้จ่าย -->
  <div v-if="isPopupAddExpenseOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        เพิ่มข้อมูลประเภทค่าใช้จ่าย
      </h2>
      <label for="rqName" class="block text-sm font-medium ml-[58px] mb-1 items-end" :class="[
        isFormSubmitted && formRequisitiontype.rqtName.trim() === ''
          ? 'text-redNormal '
          : 'border-[#d9d9d9]',
      ]">ประเภทค่าใช้จ่าย <span class="text-red-500">*</span></label>
      <div class="w-full flex justify-center">
        <!-- ฟอร์ม -->
        <div>
          <form>
            <div class="relative">
              <input type="text" required v-model="formRequisitiontype.rqtName" placeholder="กรอกข้อมูลประเภทค่าใช้จ่าย"
                :class="[
                  'w-[300px] h-[40px] bg-white border rounded-lg pl-4 text-[14px] text-black focus:outline-none',
                  isFormSubmitted && formRequisitiontype.rqtName.trim() === ''
                    ? 'border-red-500 '
                    : 'border-[#d9d9d9]',
                ]" />
            </div>
          </form>
        </div>
      </div>
      <div class="flex justify-center space-x-4 mt-3">
        <button @click="closePopupAddExpense"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmAddExpense"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>

  <!-- Alert เพิ่ม3 + ประเภทค่าใช้จ่าย -->
  <div v-if="isAddExpenseAlertOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg :class="`w-[110px] h-[110px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ยืนยันการเพิ่มข้อมูลประเภทค่าใช้จ่ายสำเร็จ
      </h2>
    </div>
  </div>

  <!-- POPUP เพิ่ม1 + ประเภทรถส่วนตัว -->
  <div v-if="isPopupAddPrivatecarOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        เพิ่มประเภทค่าเดินทางส่วนตัว
      </h2>
      <div class="w-full mb-3 flex justify-center">
        <form>
          <label for="rqName" class="block text-sm font-medium mb-1 items-end" :class="[
            isFormSubmittedPrivate && formData.vhVehicle.trim() === ''
              ? 'text-redNormal '
              : 'border-[#d9d9d9]',
          ]">ประเภทค่าเดินทางส่วนตัว <span class="text-red-500">*</span></label>
          <div class="relative mb-3">
            <form>
              <div class="relative">
                <input type="text" required v-model="formData.vhVehicle" placeholder="กรอกข้อมูลประเภทรถส่วนตัว" :class="[
                  'w-[300px] h-[40px] bg-white border rounded-lg pl-4 text-[14px] text-black focus:outline-none',
                  isFormSubmittedPrivate && formData.vhVehicle.trim() === ''
                    ? 'border-red-500'
                    : 'border-[#d9d9d9]',
                ]" />
              </div>
            </form>
          </div>
          <label for="rqName" class="block text-sm font-medium mb-1 items-end" :class="[
            isFormSubmittedPrivate && formData.vhVehicle.trim() === ''
              ? 'text-redNormal '
              : 'border-[#d9d9d9]',
          ]">อัตราค่าเดินทางส่วนตัว <span class="text-red-500">*</span></label>
          <div class="relative">
            <input type="number" required v-model="formData.vhPayrate" placeholder="กรอกอัตราค่าเดินทาง" :class="[
              'w-[300px] h-[40px] bg-white border rounded-lg pl-4 text-[14px] text-black focus:outline-none',
              isFormSubmittedPrivate && formData.vhVehicle.trim() === ''
                ? 'border-red-500'
                : 'border-[#d9d9d9]',
            ]" />
          </div>
        </form>
      </div>
      <div class="flex justify-center space-x-4">
        <button @click="closePopupAddPrivatecar"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmAddPrivatecar"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>
  <!-- Popup เพิ่ม2 ยืนยัน + ประเภทรถส่วนตัว -->
  <!-- <div v-if="isPopupConfirmAddPrivatecarOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <div class="flex justify-center mb-2">
        <svg :class="`w-[90px] h-[90px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัว
      </h2>
      <h2 class="text-[16px] text-center text-[#7E7E7E] mb-4">
        คุณยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัวหรือไม่ ?
      </h2>
      <div class="flex justify-center space-x-4">
        <button @click="closePopupConfirmAddPrivatecar"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmAddPrivatecar"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div> -->
  <!-- Alert เพิ่ม3 + ประเภทรถส่วนตัว -->
  <div v-if="isAddPrivatecarAlertOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg :class="`w-[110px] h-[110px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัวสำเร็จ
      </h2>
    </div>
  </div>

  <!-- POPUP เพิ่ม1 + ประเภทรถสาธารณะ -->
  <div v-if="isPopupAddPublictravelOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        เพิ่มประเภทค่าเดินทางสาธารณะ
      </h2>
      <label for="rqName" class="block text-sm font-medium ml-[58px] mb-1 items-end" :class="[
        isFormSubmittedPublic && formData.vhVehicle.trim() === ''
          ? 'text-redNormal '
          : 'border-[#d9d9d9]',
      ]">ประเภทค่าเดินทางสาธารณะ <span class="text-red-500">*</span></label>
      <div class="w-full flex justify-center">
        <form>
          <div class="relative">
            <input type="text" required v-model="formData.vhVehicle" placeholder="กรอกข้อมูลประเภทรถสาธารณะ" :class="[
              'w-[300px] h-[40px] bg-white border rounded-lg pl-4 text-[14px] text-black focus:outline-none',
              isFormSubmittedPublic && formData.vhVehicle.trim() === ''
                ? 'border-red-500'
                : 'border-[#d9d9d9]',
            ]" />
          </div>
        </form>
      </div>
      <div class="flex justify-center space-x-4 mt-3">
        <button @click="closePopupAddPublictravel"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmAddPublictravel"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>
  <!-- Alert เพิ่ม3 + ประเภทรถสาธารณะ -->
  <div v-if="isPublictravelAlertOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg :class="`w-[110px] h-[110px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางสาธารณะสำเร็จ
      </h2>
    </div>
  </div>

  <!-- Popup ลบ1 ประเภทค่าเดินทาง-->
  <div v-if="isPopupDeleteOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <div class="flex justify-center mb-2">
        <svg :class="`w-[90px] h-[90px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mb-2">
        ยืนยันการลบประเภทค่าเดินทาง
      </h2>
      <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
        คุณยืนยันการลบประเภทค่าเดินทางหรือไม่ ?
      </h2>
      <div class="flex justify-center space-x-4">
        <button @click="closePopupDelete"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmDelete()"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>

  <!-- Alert ลบ2 ประเภทค่าเดินทาง -->
  <div v-if="isDeleteVehicleAlertOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg :class="`w-[110px] h-[110px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ยืนยันการลบข้อมูลประเภทค่าเดินทางสำเร็จ
      </h2>
    </div>
  </div>

  <!-- Popup ลบ1 ประเภทค่าใช้จ่าย-->
  <div v-if="isPopupDeleteExpenseOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <div class="flex justify-center mb-2">
        <svg :class="`w-[90px] h-[90px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mb-2">
        ยืนยันการลบประเภทค่าใช้จ่าย
      </h2>
      <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
        คุณยืนยันการลบประเภทค่าใช้จ่ายหรือไม่ ?
      </h2>
      <div class="flex justify-center space-x-4">
        <button @click="closePopupDeleteExpense"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmDeleteExpense()"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>

  <!-- Alert ลบ2 ประเภทค่าใช้จ่าย -->
  <div v-if="isDeleteExpenseAlertOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg :class="`w-[110px] h-[110px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ยืนยันการลบข้อมูลประเภทค่าใช้จ่ายสำเร็จ
      </h2>
    </div>
  </div>

  <!-- Popup แก้ไข1 ประเภทค่าใช้จ่าย-->
  <div v-if="isPopupUpdateExpenseOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        แก้ไขข้อมูลประเภทค่าใช้จ่าย
      </h2>
      <label for="rqName" class="block text-sm font-medium ml-[58px] mb-1 items-end" :class="[
        isFormSubmitted && formRequisitiontype.rqtName.trim() === ''
          ? 'text-redNormal '
          : 'border-[#d9d9d9]',
      ]">ประเภทค่าใช้จ่าย <span class="text-red-500">*</span></label>
      <div class="w-full flex justify-center">
        <form>
          <div class="relative ">
            <input type="text" required placeholder="กรอกข้อมูลประเภทค่าใช้จ่าย"
              v-model="formRequisitionTypeEdit.rqtName"
              class="w-[300px] h-[40px] bg-white border rounded-lg pl-4 text-[14px] text-black focus:outline-none"
              :class="formRequisitionTypeEdit.rqtName.trim() === '' ? 'border-red-500' : 'border-[#d9d9d9]'" />
          </div>
        </form>
      </div>
      <div class="flex justify-center space-x-4 mt-4">
        <!-- here -->
        <button @click="closePopupUpdateExpense"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmUpdateExpense"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>

  <!-- Alert แก้ไข3 + ประเภทค่าใช้จ่าย -->
  <div v-if="isEditExpenseAlertOpen" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg :class="`w-[110px] h-[110px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ยืนยันการแก้ไขข้อมูลประเภทค่าใช้จ่ายสำเร็จ
      </h2>
    </div>
  </div>

  <!-- POPUP แก้ไข1 ประเภทรถส่วนตัว -->
  <div v-if="isPopupEditPrivatecarOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        แก้ไขประเภทค่าเดินทางส่วนตัว
      </h2>
      <div class="w-full mb-3 flex justify-center">
        <form>
          <div class="relative mb-3">
            <label for="rqName" class="block text-sm font-medium mb-1 items-end" :class="[
              isFormSubmittedPrivate && formData.vhVehicle.trim() === ''
                ? 'text-redNormal '
                : 'border-[#d9d9d9]',
            ]">ประเภทค่าเดินทางส่วนตัว <span class="text-red-500">*</span></label>
            <input type="text" required placeholder="กรอกข้อมูลประเภทค่าเดินทางส่วนตัว"
              v-model="formVehiclePrivateEdit.vhVehicle"
              class="w-[300px] h-[40px] bg-white border rounded-lg pl-4 text-[14px] text-black focus:outline-none"
              :class="formVehiclePrivateEdit.vhVehicle.trim() === '' ? 'border-red-500' : 'border-[#d9d9d9]'" />
          </div>
          <div class="relative">
            <label for="rqName" class="block text-sm font-medium mb-1 items-end" :class="[
              isFormSubmittedPrivate && formData.vhVehicle.trim() === ''
                ? 'text-redNormal '
                : 'border-[#d9d9d9]',
            ]">อัตราค่าเดินทางส่วนตัว <span class="text-red-500">*</span></label>

            <input type="number" required placeholder="กรอกอัตราประเภทค่าเดินทางส่วนตัว"
              v-model="formVehiclePrivateEdit.vhPayrate"
              class="w-[300px] h-[40px] bg-white border rounded-lg pl-4 text-[14px] text-black focus:outline-none"
              :class="String(formVehiclePrivateEdit.vhPayrate).trim() === '' ? 'border-red-500' : 'border-[#d9d9d9]'" />
          </div>
        </form>
      </div>
      <div class="flex justify-center space-x-4 ">
        <button @click="closePopupEditPrivatecar"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmEditPrivatecar"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>
  <!-- Popup แก้ไข2 ยืนยัน + ประเภทรถส่วนตัว -->
  <!-- <div v-if="isPopupConfirmEditPrivatecarOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <div class="flex justify-center mb-2">
        <svg :class="`w-[90px] h-[90px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#FFBE40" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v5a1 1 0 1 0 2 0V8Zm-1 7a1 1 0 1 0 0 2h.01a1 1 0 1 0 0-2H12Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        ยืนยันการแก้ไขข้อมูลประเภทค่าเดินทางส่วนตัว
      </h2>
      <h2 class="text-[16px] text-center text-[#7E7E7E] mb-4">
        คุณยืนยันการแก้ไขข้อมูลประเภทค่าเดินทางส่วนตัวหรือไม่ ?
      </h2>
      <div class="flex justify-center space-x-4">
        <button @click="closePopupConfirmEditPrivatecar"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmEditPrivatecar"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div> -->
  <!-- Alert แก้ไข3 + ประเภทรถส่วนตัว -->
  <div v-if="isEditPrivatecarAlertOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg :class="`w-[110px] h-[110px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ยืนยันการแก้ไขข้อมูลประเภทค่าเดินทางส่วนตัวสำเร็จ
      </h2>
    </div>
  </div>

  <!-- POPUP +แก้ไข1 ประเภทรถสาธารณะ-->
  <div v-if="isPopupEditPubliccarOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center">
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        แก้ไขประเภทค่าเดินทางสาธารณะ
      </h2>
      <label for="rqName" class="block text-sm font-medium ml-[58px] mb-1 items-end" :class="[
        isFormSubmittedPublic && formData.vhVehicle.trim() === ''
          ? 'text-redNormal '
          : 'border-[#d9d9d9]',
      ]">ประเภทค่าเดินทางสาธารณะ <span class="text-red-500">*</span></label>
      <div class="w-full flex justify-center">
        <form>
          <div class="relative">
            <input type="text" required placeholder="กรอกข้อมูลประเภทค่าเดินทางสาธารณะ"
              v-model="formVehiclePublicEdit.vhVehicle"
              class="w-[300px] h-[40px] bg-white border rounded-lg pl-4 text-[14px] text-black focus:outline-none"
              :class="String(formVehiclePublicEdit.vhVehicle).trim() === '' ? 'border-red-500' : 'border-[#d9d9d9]'" />
          </div>
        </form>
      </div>
      <div class="flex justify-center space-x-4 mt-3">
        <button @click="closePopupEditPubliccar"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยกเลิก
        </button>
        <button @click="confirmEditPubliccar"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin">
          ยืนยัน
        </button>
      </div>
    </div>
  </div>

  <!-- Alert แก้ไข3 + ประเภทรถส่วนตัว -->
  <div v-if="isEditPubliccarAlertOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg :class="`w-[110px] h-[110px] text-gray-800 dark:text-white`" aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" viewBox="0 0 24 24">
          <path fill-rule="evenodd"
            d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm13.707-1.293a1 1 0 0 0-1.414-1.414L11 12.586l-1.793-1.793a1 1 0 0 0-1.414 1.414l2.5 2.5a1 1 0 0 0 1.414 0l4-4Z"
            clip-rule="evenodd" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ยืนยันการแก้ไขข้อมูลประเภทค่าเดินทางสาธารณะสำเร็จ
      </h2>
    </div>
  </div>

  <!-- Popup แก้ไขไม่ได้ -->
  <div v-if="isRqtUse" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg xmlns="http://www.w3.org/2000/svg" width="81" height="81" viewBox="0 0 81 81" fill="none">
          <path
            d="M57.0689 0.5H23.9311L0.5 23.9311V57.0689L23.9311 80.5H57.0689L80.5 57.0689V23.9311L57.0689 0.5ZM44.9444 62.7222H36.0556V53.8333H44.9444V62.7222ZM44.9444 44.9444H36.0556V18.2778H44.9444V44.9444Z"
            fill="#E00000" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ไม่สามารถแก้ไข หรือลบประเภทค่าใช้จ่ายนี้ได้
      </h2>
    </div>
  </div>

  <!-- Popup แก้ไขไม่ได้ประเภทรถ -->
  <div v-if="isVhPublicUse || isVhPrivateUse"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center">
      <div class="flex justify-center">
        <svg xmlns="http://www.w3.org/2000/svg" width="81" height="81" viewBox="0 0 81 81" fill="none">
          <path
            d="M57.0689 0.5H23.9311L0.5 23.9311V57.0689L23.9311 80.5H57.0689L80.5 57.0689V23.9311L57.0689 0.5ZM44.9444 62.7222H36.0556V53.8333H44.9444V62.7222ZM44.9444 44.9444H36.0556V18.2778H44.9444V44.9444Z"
            fill="#E00000" />
        </svg>
      </div>
      <h2 class="text-[24px] font-bold text-center text-black mt-3">
        ไม่สามารถแก้ไข หรือลบประเภทค่าเดินทางนี้ได้
      </h2>
    </div>
  </div>
</template>

<style scoped>
/* Gray button style */
.btn-gray {
  background-color: #0066dd;
  color: white;
}

/* White button style */
.btn-white {
  background-color: white;
  color: black;
}
</style>