<script setup lang="ts">
/*
 * ชื่อไฟล์: TravelManage.vue
 * คำอธิบาย: ไฟล์นี้สามารถตั้งค่าประเภทการเดินทางต่างๆ ที่มีอยู่ในระบบ เพิ่ม แก้ไข หรือปิดการใช้งานได้
 * ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
 * วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
 */
import { onMounted, ref, reactive } from "vue";
import Button from "../../components/template/Button.vue";
import { useExpenseManageStore } from "../../store/expenseManageStore";

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
const isPrivatecarAlertOpen = ref(false); // ควบคุมการแสดง Alert รถส่วนตัว

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
  await expenseManageType.changeVehicle(index)
  vehiclePrivate.value = await expenseManageType.getVehiclePrivate();
  vehiclePublic.value = await expenseManageType.getVehiclePublic();
}
async function toggleGray2(index: number) {
  await expenseManageType.changeRequisitionType(index)
  expenseType.value = await expenseManageType.getRequisitionType();

}
function toggleGray3(index: number) {
  expenseRows.value[index].isDisabled = !expenseRows.value[index].isDisabled;
  expenseRows.value[index].isIconChanged =
    !expenseRows.value[index].isIconChanged;
}

// เปิด Popup Add ประเภทค่าใช้จ่าย
const openPopupAddExpense = () => {
  isPopupAddExpenseOpen.value = true;
};
const closePopupAddExpense = () => {
  isPopupAddExpenseOpen.value = false;
};
// เปิด PopupConfirmAdd ผู้อนุมัติ
const openPopupConfirmAddExpense = () => {
  console.log(formData.value);
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

// เปิด Popup Add ประเภทรถส่วนตัว
const openPopupAddPrivatecar = () => {
  isPopupAddPrivatecarOpen.value = true;
};
const closePopupAddPrivatecar = () => {
  isPopupAddPrivatecarOpen.value = false;
};
// เปิด PopupConfirmAdd ประเภทรถส่วนตัว
const openPopupConfirmAddPrivatecar = () => {
  isPopupConfirmAddPrivatecarOpen.value = true;
};
const closePopupConfirmAddPrivatecar = () => {
  isPopupConfirmAddPrivatecarOpen.value = false;
};

const confirmAddPrivatecar = async () => {
  // เปิด Popup Alert
  formData.vhType = "private";
  await expenseManageType.createVehicle(formData);
  isPrivatecarAlertOpen.value = true;
  vehiclePrivate.value = await expenseManageType.getVehiclePrivate()
  // ตั้งเวลาให้ Alert ปิดอัตโนมัติใน 1.5 วินาที
  setTimeout(() => {
    isPrivatecarAlertOpen.value = false; // ปิด Alert
    closePopupAddPrivatecar(); // ปิด Popup แก้ไข
    closePopupConfirmAddPrivatecar(); // ปิด Popup ยืนยัน
  }, 1500); // 1.5 วินาที
};

// เปิด Popup Add ประเภทสาธารณะ
const openPopupAddPublictravel = () => {
  isPopupAddPublictravelOpen.value = true;
};
const closePopupAddPublictravel = () => {
  isPopupAddPublictravelOpen.value = false;
};
// เปิด PopupConfirmAdd ประเภทสาธารณะ
const openPopupConfirmAddPublictravel = () => {
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
  vehiclePublic.value = await expenseManageType.getVehiclePublic()

  isPublictravelAlertOpen.value = true;
  // ตั้งเวลาให้ Alert ปิดอัตโนมัติใน 1.5 วินาที
  setTimeout(() => {
    isPublictravelAlertOpen.value = false; // ปิด Alert
    closePopupAddPublictravel(); // ปิด Popup แก้ไข
    closePopupConfirmAddPublictravel(); // ปิด Popup ยืนยัน
  }, 1500); // 1.5 วินาที
};


</script>

<template>
  
  <div v-if="!isHiddenExpense">
    <div v-if="!isHiddenPublic">
      <div v-if="!isHiddenPrivate" class="flex space-x-7 ">
        <Button :type="'btn-expenseTypeGrayClick'" @click="handleClickTypeVehicl" />
        <Button :type="'btn-transport'" @click="toggleDivsType" />
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
      <div class="flex items-center justify-between w-full">
        <div class="flex w-1/3 space-x-4">
          <p class="text-sm text-black">ลำดับ</p>
          <p class="text-sm text-black">ประเภทรถส่วนตัว</p>
        </div>
        <p class="w-1/2 text-sm text-right text-black">
          อัตราค่าเดินทาง (บาท/กิโลเมตร)
        </p>
        <p class="text-sm text-right text-black">จัดการ</p>
      </div>
      <div
        v-for="(item, index) in vehiclePrivate"
        :key="index"
        class="flex flex-col space-y-4"
      >
        <div
          :class="{ 'text-gray-400': item.isDisabled }"
          class="flex items-center justify-between w-full"
        >
        <div class="flex w-full space-x-4">
            <p class="text-sm px-3 text-grayNormal" v-if="item.vhVisible == 0">
              {{ index + 1 }}
            </p>
            <p class="text-sm px-3 text-black" v-if="item.vhVisible == 1">
              {{ index + 1 }}
            </p>
            
              <p class="text-black px-3 text-sm" v-if="item.vhVisible == 1">
                {{ item.vhName }}
              </p>
              <p class="text-grayNormal px-3 text-sm" v-if="item.vhVisible == 0">
                {{ item.vhName }}
              </p>
            </div>
            
              <p class="text-black px-3  w-1/2 text-sm text-right" v-if="item.vhVisible == 1">
                {{ item.payRate }}
              </p>
              <p class="text-grayNormal px-3  w-1/2 text-sm text-right" v-if="item.vhVisible == 0">
                {{ item.payRate }}
              </p>
            
          

          <div class="flex justify-end w-1/4">
            <button @click="toggleGray(item.id)" class="px-2 py-1 text-black">
              <div class="flex items-center space-x-1">
                <template v-if="item.vhVisible == 1">
                  <svg
                    class="w-[24px] h-[24px] text-gray-800 dark:text-white"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    width="24"
                    height="24"
                    fill="none"
                    viewBox="0 0 24 24"
                  >
                    <path
                      stroke="gray"
                      stroke-width="1.5"
                      d="M21 12c0 1.2-4.03 6-9 6s-9-4.8-9-6c0-1.2 4.03-6 9-6s9 4.8 9 6Z"
                    />
                    <path
                      stroke="gray"
                      stroke-width="2"
                      d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                    />
                  </svg>
                </template>
                <template v-else>
                  <svg
                    class="w-[24px] h-[24px] text-gray-800 dark:text-white"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    width="24"
                    height="24"
                    fill="none"
                    viewBox="0 0 24 24"
                  >
                    <path
                      stroke="gray"
                      stroke-linecap="round"
                      stroke-linejoin="round"
                      stroke-width="1.5"
                      d="M3.933 13.909A4.357 4.357 0 0 1 3 12c0-1 4-6 9-6m7.6 3.8A5.068 5.068 0 0 1 21 12c0 1-3 6-9 6-.314 0-.62-.014-.918-.04M5 19 19 5m-4 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                    />
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
  <div v-if="!isHiddenTypeVehical">
    <div v-if="!isHiddenPublic" class="mb-20"></div>
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

    <div v-if="!isHiddenPrivate" class="flex flex-col space-y-4">
      <div class="flex items-center justify-between w-full">
        <div class="flex w-1/3 space-x-4">
          <p class="text-sm text-black">ลำดับ</p>
          <p class="text-sm text-black">ประเภทรถสาธารณะ</p>
        </div>
        <p class="w-1/2 text-sm text-right text-black"></p>
        <p class="text-sm text-right text-black">จัดการ</p>
      </div>

      <div
        v-for="(item, index) in vehiclePublic"
        :key="'public-' + index"
        class="flex flex-col space-y-4"
      >
        <div
          :class="{ 'text-gray-400': item.isDisabled }"
          class="flex items-center justify-between w-full"
        >
          <div class="flex w-full space-x-4">
            <p class="text-sm px-3 text-grayNormal" v-if="item.vhVisible == 0">
              {{ index + 1 }}
            </p>
            <p class="text-sm px-3 text-black" v-if="item.vhVisible == 1">
              {{ index + 1 }}
            </p>
            <div v-if="!item.isSubmitted">
              <p class="text-black px-3 text-sm" v-if="item.vhVisible == 1">
                {{ item.vhName }}
              </p>
              <p class="text-grayNormal px-3 text-sm" v-if="item.vhVisible == 0">
                {{ item.vhName }}
              </p>
            </div>
            <div v-else>
              <p
                :class="{
                  'text-gray-400': item.isDisabled,
                  'text-black': !item.isDisabled,
                }"
                class="text-sm"
              >
                {{ item.vehicleType }}
              </p>
            </div>
          </div>
          <div class="flex justify-end w-1/4">
            <button @click="toggleGray(item.id)" class="px-2 py-1 text-black">
              <div class="flex items-center space-x-1">
                <template v-if="item.vhVisible == 1">

                  <svg
                    class="w-[24px] h-[24px] text-gray-800 dark:text-white"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    width="24"
                    height="24"
                    fill="none"
                    viewBox="0 0 24 24"
                  >
                    <path
                      stroke="gray"
                      stroke-width="1.5"
                      d="M21 12c0 1.2-4.03 6-9 6s-9-4.8-9-6c0-1.2 4.03-6 9-6s9 4.8 9 6Z"
                    />
                    <path
                      stroke="gray"
                      stroke-width="2"
                      d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                    />
                  </svg>
                </template>
                <template v-else>
                  <svg
                    class="w-[24px] h-[24px] text-gray-800 dark:text-white"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    width="24"
                    height="24"
                    fill="none"
                    viewBox="0 0 24 24"
                  >
                    <path
                      stroke="gray"
                      stroke-linecap="round"
                      stroke-linejoin="round"
                      stroke-width="1.5"
                      d="M3.933 13.909A4.357 4.357 0 0 1 3 12c0-1 4-6 9-6m7.6 3.8A5.068 5.068 0 0 1 21 12c0 1-3 6-9 6-.314 0-.62-.014-.918-.04M5 19 19 5m-4 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                    />
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
      <div class="flex items-center justify-between w-full">
        <div class="flex w-1/3 space-x-4">
          <p class="text-sm text-black">ลำดับ</p>
          <p class="text-sm text-black">ประเภทค่าใช้จ่าย</p>
        </div>
        <p class="w-1/2 text-sm text-right text-black"></p>
        <p class="text-sm text-right text-black">จัดการ</p>
      </div>


      <div
        v-for="(item, index) in expenseType"
        :key="'public-' + index"
        class="flex flex-col space-y-4"
      >
        <div
          :class="{ 'text-gray-400': item.rqtVisible == 0 }"
          class="flex items-center justify-between w-full"
        >
          <div class="flex w-full space-x-4">
            <p class="text-sm px-3 text-grayNormal" v-if="item.rqtVisible == 0">
              {{ index + 1 }}
            </p>
            <p class="text-sm px-3 text-black" v-if="item.rqtVisible == 1">
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
              <p
                :class="{
                  'text-gray-400': item.rqtVisible == 0,
                  'text-black': !item.isDisabled,
                }"
                class="text-sm"
              >
                {{ item.vehicleType }}
              </p>
            </div>
          </div>
          <div class="flex justify-end w-1/4">
            <button @click="toggleGray2(item.rqtId)" class="px-2 py-1 text-black">
              <div class="flex items-center space-x-1">
                <template v-if="item.rqtVisible == 1">
                  <svg
                    class="w-[24px] h-[24px] text-gray-800 dark:text-black"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    width="24"
                    height="24"
                    fill="none"
                    viewBox="0 0 24 24"
                  >
                    <path
                      stroke="gray"
                      stroke-width="1.5"
                      d="M21 12c0 1.2-4.03 6-9 6s-9-4.8-9-6c0-1.2 4.03-6 9-6s9 4.8 9 6Z"
                    />
                    <path
                      stroke="gray"
                      stroke-width="2"
                      d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                    />
                  </svg>
                </template>
                <template v-else>
                  <svg
                    class="w-[24px] h-[24px] text-gray-800 dark:text-white"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    width="24"
                    height="24"
                    fill="none"
                    viewBox="0 0 24 24"
                  >
                    <path
                      stroke="gray"
                      stroke-linecap="round"
                      stroke-linejoin="round"
                      stroke-width="1.5"
                      d="M3.933 13.909A4.357 4.357 0 0 1 3 12c0-1 4-6 9-6m7.6 3.8A5.068 5.068 0 0 1 21 12c0 1-3 6-9 6-.314 0-.62-.014-.918-.04M5 19 19 5m-4 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                    />
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


  <!-- POPUP +ประเภทค่าใช้จ่าย -->
  <div
    v-if="isPopupAddExpenseOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
  >
    <div
      class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center"
    >
      <h2 class="text-[16px] font-bold text-center text-black mb-3">
        การเพิ่มข้อมูลประเภทค่าใช้จ่าย
      </h2>
      <div class="w-full my-3 flex justify-center">
        <!-- ฟอร์ม -->
        <div>
          <form>
            <div class="relative">
              <input
                type="text"
                required
                v-model="formRequisitiontype.rqtName"
                placeholder="ข้อมูลประเภทค่าใช้จ่าย"
                class="w-[300px] h-[40px] bg-white border border-[#d9d9d9] rounded-lg pl-4 text-[14px] text-black focus:outline-none"
              />
            </div>
          </form>
        </div>
      </div>
      <div class="flex justify-center space-x-4 mt-3">
        <button
          @click="closePopupAddExpense"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยกเลิก
        </button>
        <button
          @click="openPopupConfirmAddExpense"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยืนยัน
        </button>
      </div>
    </div>
  </div>
  <!-- Popup ยืนยัน + ประเภทค่าใช้จ่าย -->
  <div
    v-if="isPopupConfirmAddExpenseOpen"
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
        ยืนยันการเพิ่มข้อมูลประเภทค่าใช้จ่าย
      </h2>
      <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
        คุณยืนยันการเพิ่มข้อมูลประเภทค่าใช้จ่ายหรือไม่ ?
      </h2>
      <div class="flex justify-center space-x-4">
        <button
          @click="closePopupConfirmAddExpense"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยกเลิก
        </button>
        <button
          @click="confirmAddExpense"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยืนยัน
        </button>
      </div>
    </div>
  </div>
  <!-- Alert + ประเภทค่าใช้จ่าย -->
  <div
    v-if="isAddExpenseAlertOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
  >
    <div
      class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center"
    >
      <div class="flex justify-center">
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
        ยืนยันการเพิ่มข้อมูลประเภทค่าใช้จ่ายสำเร็จ
      </h2>
    </div>
  </div>

  <!-- POPUP +ประเภทรถส่วนตัว -->
  <div
    v-if="isPopupAddPrivatecarOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
  >
    <div
      class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center"
    >
      <h2 class="text-[16px] font-bold text-center text-black mb-3">
        การเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัว
      </h2>
      <div class="w-full my-3 flex justify-center">
        <form>
          <div class="relative mb-6">
            <input
              type="text"
              required
              placeholder="ข้อมูลประเภทค่าเดินทางส่วนตัว"
              v-model="formData.vhVehicle"
              class="w-[300px] h-[40px] bg-white border border-[#d9d9d9] rounded-lg pl-4 text-[14px] text-black focus:outline-none"
            />
          </div>
          <div class="relative">
            <input
              type="text"
              required
              placeholder="อัตราค่าเดินทางส่วนตัว"
              v-model="formData.vhPayrate"
              class="w-[300px] h-[40px] bg-white border border-[#d9d9d9] rounded-lg pl-4 text-[14px] text-black focus:outline-none"
            />
          </div>
        </form>
      </div>
      <div class="flex justify-center space-x-4 mt-3">
        <button
          @click="closePopupAddPrivatecar"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยกเลิก
        </button>
        <button
          @click="openPopupConfirmAddPrivatecar"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยืนยัน
        </button>
      </div>
    </div>
  </div>
  <!-- Popup ยืนยัน + ประเภทรถส่วนตัว -->
  <div
    v-if="isPopupConfirmAddPrivatecarOpen"
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
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัว
      </h2>
      <h2 class="text-[16px] text-center text-[#7E7E7E] mb-4">
        คุณยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัวหรือไม่ ?
      </h2>
      <div class="flex justify-center space-x-4">
        <button
          @click="closePopupConfirmAddPrivatecar"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยกเลิก
        </button>
        <button
          @click="confirmAddPrivatecar"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยืนยัน
        </button>
      </div>
    </div>
  </div>
  <!-- Alert + ประเภทรถส่วนตัว -->
  <div
    v-if="isPrivatecarAlertOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
  >
    <div
      class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center"
    >
      <div class="flex justify-center">
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
        ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัวสำเร็จ
      </h2>
    </div>
  </div>

  <!-- POPUP +ประเภทรถสาธารณะ -->
  <div
    v-if="isPopupAddPublictravelOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
  >
    <div
      class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center"
    >
      <h2 class="text-[16px] font-bold text-center text-black mb-3">
        การเพิ่มข้อมูลประเภทค่าเดินทางสาธารณะ
      </h2>
      <div class="w-full my-3 flex justify-center">
        <form>
          <div class="relative">
            <input
              type="text"
              required
              v-model="formData.vhVehicle"
              placeholder="ข้อมูลประเภทค่าเดินทางสาธารณะ"
              class="w-[300px] h-[40px] bg-white border border-[#d9d9d9] rounded-lg pl-4 text-[14px] text-black focus:outline-none"
            />
          </div>
        </form>
      </div>
      <div class="flex justify-center space-x-4 mt-3">
        <button
          @click="closePopupAddPublictravel"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยกเลิก
        </button>
        <button
          @click="openPopupConfirmAddPublictravel"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยืนยัน
        </button>
      </div>
    </div>
  </div>
  <!-- Popup ยืนยัน + ประเภทรถส่วนตัว -->
  <div
    v-if="isPopupConfirmAddPublictravelOpen"
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
      <h2 class="text-[24px] font-bold text-center text-black mb-3">
        ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางสาธารณะ
      </h2>
      <h2 class="text-[16px] text-center text-[#7E7E7E] mb-4">
        คุณยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางสาธารณะหรือไม่ ?
      </h2>
      <div class="flex justify-center space-x-4">
        <button
          @click="closePopupConfirmAddPublictravel"
          class="btn-ยกเลิก bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยกเลิก
        </button>
        <button
          @click="confirmAddPublictravel"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
          ยืนยัน
        </button>
      </div>
    </div>
  </div>
  <!-- Alert + ประเภทรถส่วนตัว -->
  <div
    v-if="isPublictravelAlertOpen"
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50"
  >
    <div
      class="bg-white w-[460px] h-[295px] rounded-lg shadow-lg px-6 py-4 flex flex-col justify-center items-center"
    >
      <div class="flex justify-center">
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
        ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางสาธารณะสำเร็จ
      </h2>
    </div>
  </div>
</template>
