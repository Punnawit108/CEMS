<script setup lang="ts">
/*
* ชื่อไฟล์: TravelManage.vue
* คำอธิบาย: ไฟล์นี้สามารถตั้งค่าประเภทการเดินทางต่างๆ ที่มีอยู่ในระบบ เพิ่ม แก้ไข หรือปิดการใช้งานได้
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/
import { ref } from "vue";
import Button from "../../components/template/Button.vue";


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
const handleClickTypeVehicl = () => {
  toggleDivsTypeVehicle();
}

const toggleDivsTypeVehicle= () =>{
  isHiddenTypeVehical.value = true
  isHiddenType.value = false
}

const toggleDivsType= () =>{
  isHiddenType.value = true
  isHiddenTypeVehical.value = false
}

const toggleDivsPrivate = () =>{
  isHiddenPublic.value = !isHiddenPublic.value
}
const toggleDivsExpense = () =>{
  isHiddenExpense.value = !isHiddenExpense.value
}

const toggleDivs = () => {
  isHiddenPrivate.value = !isHiddenPrivate.value;
};
const handleClickPublic1 = () => {
  startAddPublic(); // เรียกฟังก์ชันแรก
  toggleDivsPrivate();      // เรียกฟังก์ชันสลับการแสดง/ซ่อน
  
};
const handleClickPublic2 = () => {
  cancelAddPublic();// เรียกฟังก์ชันแรก
  toggleDivsPrivate();   
   // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickPublic3 = () => {
  submitPublicRow();// เรียกฟังก์ชันแรก
  toggleDivsPrivate();   
   // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickPrivate1 = () => {
  startAddPrivate(); // เรียกฟังก์ชันแรก
  toggleDivs();      // เรียกฟังก์ชันสลับการแสดง/ซ่อน
  
};
const handleClickPrivate2 = () => {
  cancelAddPrivate();// เรียกฟังก์ชันแรก
  toggleDivs();   
   // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickPrivate3 = () => {
  submitPrivateRow();// เรียกฟังก์ชันแรก
  toggleDivs();   
   // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickExpense1 = () => {
  startAddExpense();// เรียกฟังก์ชันแรก
  toggleDivsExpense();   
   // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickExpense2= () => {
  submitExpenseRow();// เรียกฟังก์ชันแรก
  toggleDivsExpense(); 
   // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};
const handleClickExpense3 = () => {
  cancelAddExpense();// เรียกฟังก์ชันแรก
  toggleDivsExpense(); 
   // เรียกฟังก์ชันสลับการแสดง/ซ่อน
};

// State สำหรับแถวสาธารณะ

let currentPrivateIndex = ref(-1); // ดัชนีของแถวที่กำลังแก้ไขสำหรับส่วนตัว
let currentPublicIndex = ref(-1); // ดัชนีของแถวที่กำลังแก้ไขสำหรับสาธารณะ
let currentExpenseIndex = ref(-1);
const isAddingPrivate = ref(false); // สถานะสำหรับการเพิ่มแถวส่วนตัว
const isAddingPublic = ref(false); // สถานะสำหรับการเพิ่มแถวสาธารณะ
const isAddingExpense = ref(false);// สถานะสำหรับการเพิ่มแถวประเภทเบิกจ่าย

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
function toggleGray(index: number) {
  privateRows.value[index].isDisabled = !privateRows.value[index].isDisabled;
  privateRows.value[index].isIconChanged =
    !privateRows.value[index].isIconChanged;
}
function toggleGray2(index: number) {
  publicRows.value[index].isDisabled = !publicRows.value[index].isDisabled;
  publicRows.value[index].isIconChanged =
    !publicRows.value[index].isIconChanged;
}
function toggleGray3(index: number) {
  expenseRows.value[index].isDisabled = !expenseRows.value[index].isDisabled;
  expenseRows.value[index].isIconChanged =
    !expenseRows.value[index].isIconChanged;
}

</script>

<template>
  <div v-if="!isHiddenExpense">
  <div v-if="!isHiddenPublic">
  <div v-if="!isHiddenPrivate" class="flex space-x-7 " >
    <Button :type="'btn-expenseTypeGray'" @click="handleClickTypeVehicl"/>
    <Button :type="'btn-transport'" @click="toggleDivsType"/>
  </div>
</div>
</div>
<div v-if="!isHiddenTypeVehical">
  <div v-if="!isHiddenPublic" class="flex flex-col items-end mb-5">
    <template v-if="!isAddingPrivate">
      <Button :type="'btn-private'" @click="handleClickPrivate1" />
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
      v-for="(item, index) in privateRows"
      :key="index"
      class="flex flex-col space-y-4"
    >
      <div
        :class="{ 'text-gray-400': item.isDisabled }"
        class="flex items-center justify-between w-full"
      >
        <div class="flex w-full space-x-4">
          <p
            :class="{
              'text-gray-400': item.isDisabled,
              'text-black': !item.isDisabled,
            }"
            class="px-3 text-sm"
          >
            {{ index + 1 }}
          </p>
          <div v-if="!item.isSubmitted">
            <input
              type="text"
              v-model="item.vehicleType"
              placeholder="ประเภทรถส่วนตัว"
              :class="[
                'w-full px-2 py-1 border rounded',
                {
                  'text-gray-400': item.isDisabled,
                  'text-black': !item.isDisabled,
                },
              ]"
              :disabled="item.isDisabled"
            />
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

        <div
          v-if="!item.isSubmitted"
          class="flex items-center justify-end w-1/2"
        >
          <input
            type="number"
            v-model="item.fareRate"
            placeholder="อัตราค่าเดินทาง"
            :class="[
              'flex justify-center w-32 px-2 py-1 text-center border rounded',
              {
                'text-gray-400': item.isDisabled,
                'text-black': !item.isDisabled,
              },
            ]"
            :disabled="item.isDisabled"
          />
        </div>
        <div v-else class="w-1/1">
          <p
            :class="{
              'text-gray-400': item.isDisabled,
              'text-black': !item.isDisabled,
            }"
            class="text-sm"
          >
            {{ item.fareRate }}
          </p>
        </div>

        <div class="flex justify-end w-1/4">
          <button @click="toggleGray(index)" class="px-2 py-1 text-black">
            <div class="flex items-center space-x-1">
              <template v-if="!item.isIconChanged">
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
<div v-if ="!isHiddenTypeVehical">
 <div v-if="!isHiddenPublic" class="mb-20"> </div>
</div>
  <!-- ปุ่มสำหรับแถวสาธารณะ -->
  

  <div v-if="!isHiddenTypeVehical">
  <div v-if="!isHiddenPrivate" class="flex flex-col items-end mb-5">
    <template v-if="!isAddingPublic">
      <Button :type="'btn-public1'" @click="handleClickPublic1" />
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
      v-for="(item, index) in publicRows"
      :key="'public-' + index"
      class="flex flex-col space-y-4"
    >
      <div
        :class="{ 'text-gray-400': item.isDisabled }"
        class="flex items-center justify-between w-full"
      >
        <div class="flex w-full space-x-4">
          <p
            :class="[
              {
                'text-gray-400': item.isDisabled,
                'text-black': !item.isDisabled,
              },
              'text-sm px-3', // เพิ่มคลาส text-sm ที่นี่
            ]"
          >
            {{ index + 1 }}
          </p>
          <div v-if="!item.isSubmitted">
            <input
              type="text"
              v-model="item.vehicleType"
              placeholder="ประเภทรถสาธารณะ"
              :class="[
                'w-full px-2 py-1 border rounded ',
                {
                  'text-gray-400': item.isDisabled,
                  'text-black': !item.isDisabled,
                },
              ]"
              :disabled="item.isDisabled"
            />
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
        <button @click="toggleGray2(index)" class="px-2 py-1 text-black">
          <div class="flex items-center space-x-1">
            <template v-if="!item.isIconChanged">
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
  <div v-if="!isHiddenTypeVehical" class="mb-20"> </div>
  <div v-if="!isHiddenType">
  <div v-if="!isHiddenPrivate" class="flex flex-col items-end mb-5">
    <template v-if="!isAddingExpense">
      <Button :type="'btn-expenseType'" @click="handleClickExpense1"  />
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
        <p class="text-sm text-black">ประเภทเบิกค่าใช้จ่าย</p>
      </div>
      <p class="w-1/2 text-sm text-right text-black"></p>
      <p class="text-sm text-right text-black">จัดการ</p>
    </div>

    <div
      v-for="(item, index) in expenseRows"
      :key="'expense-' + index"
      class="flex flex-col space-y-4"
    >
      <div
        :class="{ 'text-gray-400': item.isDisabled }"
        class="flex items-center justify-between w-full"
      >
        <div class="flex w-full space-x-4">
          <p
            :class="[
              {
                'text-gray-400': item.isDisabled,
                'text-black': !item.isDisabled,
              },
              'text-sm px-3', 
            ]"
          >
            {{ index + 1 }}
          </p>
          <div v-if="!item.isSubmitted">
            <input
              type="text"
              v-model="item.expenseType"
              placeholder="ประเภทเบิกค่าใช้จ่าย"
              :class="[
                'w-full px-2 py-1 border rounded ',
                {
                  'text-gray-400': item.isDisabled,
                  'text-black': !item.isDisabled,
                },
              ]"
              :disabled="item.isDisabled"
            />
          </div>
          <div v-else>
            <p
              :class="{
                'text-gray-400': item.isDisabled,
                'text-black': !item.isDisabled,
              }"
              class="text-sm"
            >
              {{ item.expenseType }}
            </p>
          </div>
        </div>
        <div class="flex justify-end w-1/4">
        <button @click="toggleGray3(index)" class="px-2 py-1 text-black">
          <div class="flex items-center space-x-1">
            <template v-if="!item.isIconChanged">
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
<div></div>
</template>
