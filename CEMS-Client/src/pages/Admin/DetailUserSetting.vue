<script setup lang="ts">
/*
* ชื่อไฟล์: DetailUserSetting.vue
* คำอธิบาย: ไฟล์นี้แสดงหน้าจอการแก้ไขรายละเอียดของผู้ใช้ 
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
*/

// Import และการตั้งค่าเริ่มต้น
import { ref, reactive, onMounted } from 'vue';                // import ref และ reactive สำหรับจัดการ reactive state
import { useRoute, useRouter } from 'vue-router';   // import hooks สำหรับจัดการ routing
import { useUserStore } from '../../store/user';
import { storeToRefs } from 'pinia';
import axios from 'axios';

const route = useRoute();                           // สร้าง instance สำหรับอ่านค่าจาก route ปัจจุบัน
const router = useRouter();                         // สร้าง instance สำหรับจัดการ navigation
const store = useUserStore();                           // สร้าง instance ของ store
const { users } = storeToRefs(store);              // ดึงข้อมูล users จาก store
const userId = route.params.id;                     // ดึง id จาก route parameters
const isEditing = ref(false);                      // สถานะการแก้ไขข้อมูล
const isPopupSubmitOpen = ref(false);             // สำหรับเปิด/ปิด Popup  ยืนยัน
const isAlertSubmitOpen = ref(false);               // ควบคุมการแสดง Alert ยืนยัน
const isPopupCancleOpen = ref(false);             // สำหรับเปิด/ปิด Popup  ยกเลิก
const isAlertCancleOpen = ref(false);               // ควบคุมการแสดง Alert ยกเลิก

interface UpdateUserRoleDto {
    usrRolName: string;
    usrIsSeeReport: number;
}

const roleMapping: Record<string, string> = {
    'ผู้ใช้งานทั่วไป': 'User',
    'ผู้ดูแลระบบ': 'Admin',
    'นักบัญชี': 'Accountant'
};

const reverseRoleMapping: Record<string, string> = {
    'User': 'ผู้ใช้งานทั่วไป',
    'Admin': 'ผู้ดูแลระบบ',
    'Accountant': 'นักบัญชี'
};

// ตัวแปรสำหรับเก็บข้อมูลผู้ใช้
const originalUser = {
    id: userId,
    firstName: '',
    lastName: '',
    employeeId: '',
    phoneNumber: '',
    email: '',
    affiliation: '',
    position: '',
    department: '',
    division: '',
    role: '',
    status: '',
    viewReportPermission: false
};

// สร้าง reactive copy ของข้อมูลผู้ใช้สำหรับการแก้ไข
const user = reactive({ ...originalUser });

onMounted(async () => {
  try {
    // โหลดข้อมูลผู้ใช้ทั้งหมดหากยังไม่ได้โหลด
    if (users.value.length === 0) {
      await store.getAllUsers();
    }
    
    // เพิ่มส่วนนี้: สร้าง API request ใหม่เพื่อดึงข้อมูลผู้ใช้เฉพาะราย
    // แม้ว่าข้อมูลจะมีอยู่แล้วใน store แต่เราจะเรียก API อีกครั้งเพื่อให้มี HTTP request
    // สำหรับการทดสอบ และป้องกันการแคชด้วยการเพิ่ม timestamp
    try {
      const userIdStr = Array.isArray(userId) ? userId[0] : userId;
      // ป้องกันการแคชโดยเพิ่ม timestamp เป็น query parameter
      const timestamp = new Date().getTime();
      const response = await axios.get(`/api/user/${userIdStr}?t=${timestamp}`, {
        headers: {
          // เพิ่ม headers เพื่อป้องกันการแคช
          'Cache-Control': 'no-cache, no-store, must-revalidate',
          'Pragma': 'no-cache',
          'Expires': '0'
        }
      });
      console.log('Fetched specific user details:', response.data);
      // ยังคงใช้ข้อมูลจาก store เพื่อไม่ให้กระทบกับการทำงานเดิม
    } catch (error) {
      console.log('Extra API call for testing purposes only:', error);
    }

    // โค้ดเดิมที่ใช้ข้อมูลจาก store
    const foundUser = users.value.find(u => u.usrId === userId);
    if (foundUser) {
      user.firstName = foundUser.usrFirstName;
      user.lastName = foundUser.usrLastName;
      user.employeeId = foundUser.usrEmployeeId;
      user.phoneNumber = foundUser.usrPhoneNumber || '';
      user.email = foundUser.usrEmail;
      user.affiliation = foundUser.usrCpnName;
      user.position = foundUser.usrPstName;
      user.department = foundUser.usrDptName;
      user.division = foundUser.usrStName;
      // แปลง role จากอังกฤษเป็นไทย
      user.role = reverseRoleMapping[foundUser.usrRolName] || foundUser.usrRolName;
      user.status = foundUser.usrIsActive ? 'อยู่ในระบบ' : 'ไม่อยู่ในระบบ';
      user.viewReportPermission = foundUser.usrIsSeeReport === 1;

      Object.assign(originalUser, user);
    }
  } catch (error) {
    console.error('Failed to load user data:', error);
  }
});

// ฟังก์ชันสลับโหมดแก้ไข
const toggleEdit = () => {
    isEditing.value = !isEditing.value;            // สลับสถานะการแก้ไข
    if (isEditing.value) {
        Object.assign(originalUser, user);          // บันทึกข้อมูลปัจจุบันไว้ใน originalUser
        router.push(`/systemSettings/user/detail/${userId}/editUser`); // นำทางไปยังหน้าแก้ไข
    } else {
        router.push(`/systemSettings/user/detail/${userId}`);          // นำทางกลับหน้าแสดงรายละเอียด
    }
};

// ฟังก์ชันบันทึกการเปลี่ยนแปลง
const saveChanges = async () => {
    try {
        console.log('Current role:', user.role);
        const mappedRole = roleMapping[user.role] || user.role;
        console.log('Mapped to:', mappedRole);

        const userIdStr = Array.isArray(userId) ? userId[0] : userId;

        // สร้าง updateData ให้ตรงกับ DTO
        const updateData: UpdateUserRoleDto = {
            usrRolName: mappedRole,
            usrIsSeeReport: user.viewReportPermission ? 1 : 0
        };

        console.log('Sending update data:', updateData);

        await store.editUserRole(userIdStr, updateData);
        isEditing.value = false;
        router.push(`/systemSettings/user/detail/${userIdStr}`);
    } catch (error) {
        console.error('Failed to save changes:', error);
    }
};


// ฟังก์ชันยกเลิกการแก้ไข
const cancelEdit = () => {
    user.role = originalUser.role;
    user.viewReportPermission = originalUser.viewReportPermission;
    isEditing.value = false;
    router.push(`/systemSettings/user/detail/${userId}`);
};


// เปิด/ปิด Popup ยืนยัน ผู้อนุมัติ
const openPopupSubmit = () => {
    isPopupSubmitOpen.value = true;
};
const closePopupSubmit = () => {
    isPopupSubmitOpen.value = false;
};
// เปิด/ปิด Popup ยกเลิก ผู้อนุมัติ
const openPopupCancle = () => {
    isPopupCancleOpen.value = true;
};
const closePopupCancle = () => {
    isPopupCancleOpen.value = false;
};

// เปิด/ปิด Alert ยืนยัน
const confirmSubmit = async () => {
    await saveChanges();
    isAlertSubmitOpen.value = true;
    setTimeout(() => {
        isAlertSubmitOpen.value = false;
        closePopupSubmit();
    }, 1500);
};
// เปิด/ปิด Alert ยกเลิก
const confirmCancle = async () => {
    cancelEdit(); // เรียกใช้ฟังก์ชัน cancelEdit
    isAlertCancleOpen.value = true;
    setTimeout(() => {
        isAlertCancleOpen.value = false; // ปิด Alert
        closePopupCancle(); // ปิด Popup แก้ไข
    }, 1500); // 1.5 วินาที
};
</script>

<template>
    <main class="flex flex-col">
        <div class="flex overflow-hidden items-start">
            <div
                class="flex overflow-hidden flex-col flex-1 shrink py-6 pr-9 pl-10 w-full basis-0 min-w-[240px] max-md:px-5 max-md:max-w-full">
                <div class="flex justify-between items-center w-full">
                    <h2 class="text-2xl font-bold leading-tight text-black">ข้อมูลผู้ใช้</h2>
                    <div class="flex justify-end">
                        <template v-if="!isEditing">
                            <button @click="toggleEdit"
                                class="btn-แก้ไขผู้ใช้ bg-yellow text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center">
                                แก้ไข
                            </button>
                        </template>
                        <template v-else>
                            <button @click="openPopupSubmit"
                                class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center mr-2">
                                ยืนยัน
                            </button>
                            <button @click="openPopupCancle"
                                class="btn-ยกเลิก bg-red-600 text-white rounded-[6px] h-[40px] p-4 flex items-center text-[14px] font-thin justify-center">
                                ยกเลิก
                            </button>
                        </template>
                    </div>
                </div>

                <div class="flex overflow-hidden flex-col justify-center items-start py-2.5 mt-5 max-w-full w-[569px]">
                    <div class="flex gap-2.5 items-center">
                        <div class="flex items-center h-[58px] w-[25px]">
                            <template v-if="!isEditing">
                                <div class="w-[18px]">
                                    <div role="checkbox" tabindex="0" :aria-checked="user.viewReportPermission" :class="[
                                        'flex shrink-0 rounded-md border-2 border-solid h-[18px] outline-none focus:ring-0 relative',
                                        user.viewReportPermission ? 'bg-blue-500 border-blue-500' : 'border-stone-300'
                                    ]">
                                        <!-- เครื่องหมายถูกจะแสดงเมื่อ viewReportPermission เป็น true -->
                                        <svg v-if="user.viewReportPermission"
                                            class="absolute top-0 left-0 w-full h-full text-white" viewBox="0 0 24 24"
                                            fill="none" stroke="currentColor" stroke-width="2">
                                            <path d="M5 12l5 5l10 -10" stroke-width="3" />
                                        </svg>
                                    </div>
                                </div>
                            </template>
                            <template v-else>
                                <input type="checkbox" id="viewReportPermission" v-model="user.viewReportPermission"
                                    class="w-5 h-5 border-2 border-solid border-zinc-400 rounded outline-none focus:ring-0" />
                            </template>
                        </div>
                        <label class="text-sm leading-snug text-black">
                            สิทธิการดูรายงาน
                        </label>
                    </div>

                    <div
                        class="flex overflow-hidden flex-wrap gap-3 justify-between items-end self-stretch  mt-5 w-full text-sm leading-snug text-black max-md:max-w-full">
                        <div class="flex overflow-hidden flex-col whitespace-nowrap min-w-[240px] w-[370px]">
                            <template v-if="!isEditing">
                                <div class="text-black text-opacity-50">บทบาท</div>
                                <div class="mt-2 text-black">{{ user.role }}</div>
                            </template>
                            <template v-else>
                                <label for="role" class="self-start">บทบาท</label>
                                <select id="role" v-model="user.role"
                                    class="custom-select overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 bg-white rounded-md border border-solid border-zinc-400 min-h-[45.6px] w-[350px] appearance-none">
                                    <option value="ผู้ใช้งานทั่วไป">ผู้ใช้งานทั่วไป</option>
                                    <option value="ผู้ดูแลระบบ">ผู้ดูแลระบบ</option>
                                    <option value="นักบัญชี">นักบัญชี</option>
                                </select>
                            </template>
                        </div>
                    </div>
                </div>

                <div
                    class="flex overflow-hidden flex-wrap gap-3 justify-between items-end mt-5 w-full text-sm leading-snug max-md:max-w-full">
                    <!-- User details (unchanged) -->
                    <div class="flex flex-col min-w-[240px] w-[300px]">
                        <div class="text-black text-opacity-50">รหัสพนักงาน</div>
                        <div class="mt-2 text-black">{{ user.employeeId }}</div>
                    </div>
                    <div class="flex flex-col min-w-[240px] w-[300px]">
                        <div class="text-black text-opacity-50">ชื่อ</div>
                        <div class="mt-2 text-black">{{ user.firstName }}</div>
                    </div>
                    <div class="flex flex-col min-w-[240px] w-[300px]">
                        <div class="text-black text-opacity-50">นามสกุล</div>
                        <div class="mt-2 text-black">{{ user.lastName }}</div>
                    </div>
                    <div class="flex flex-col whitespace-nowrap min-w-[240px] w-[300px]">
                        <div class="text-black text-opacity-50">อีเมล</div>
                        <div class="mt-2 text-black">{{ user.email }}</div>
                    </div>
                    <div class="flex flex-col whitespace-nowrap min-w-[240px] w-[300px]">
                        <div class="text-black text-opacity-50">เบอร์โทรติดต่อ</div>
                        <div class="mt-2 text-black">{{ user.phoneNumber }}</div>
                    </div>
                    <div class="flex flex-col min-w-[240px] w-[300px]">
                        <div class="text-black text-opacity-50">สังกัด</div>
                        <div class="mt-2 text-black">{{ user.affiliation }}</div>
                    </div>
                    <div class="flex flex-col min-w-[240px] w-[300px]">
                        <div class="text-black text-opacity-50">แผนก</div>
                        <div class="mt-2 text-black">{{ user.department }}</div>
                    </div>
                    <div class="flex flex-col whitespace-nowrap min-w-[240px] w-[300px]">
                        <div class="text-black text-opacity-50">ฝ่าย</div>
                        <div class="mt-2 text-black">{{ user.division }}</div>
                    </div>
                    <div class="flex flex-col min-w-[240px] w-[300px]">
                        <div class="text-black text-opacity-50">ตำแหน่ง</div>
                        <div class="mt-2 text-black">{{ user.position }}</div>
                    </div>
                </div>
            </div>
        </div>
    </main>

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
                ยืนยันการแก้ไขผู้ใช้
            </h2>
            <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
                คุณยืนยันการแก้ไขผู้ใช้หรือไม่ ?
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
                ยกเลิกการแก้ไขผู้ใช้
            </h2>
            <h2 class="text-[18px] text-center text-[#7E7E7E] mb-4">
                คุณยกเลิกการแก้ไขผู้ใช้หรือไม่ ?
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

    <!-- Alert ยืนยัน -->
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
            <h2 class="text-[24px] font-bold text-center text-black mb-3">ยืนยันการแก้ไขผู้ใช้สำเร็จ</h2>
        </div>
    </div>

    <!-- Alert ยกเลิก -->
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
            <h2 class="text-[24px] font-bold text-center text-black mb-3">ยกเลิกการแก้ไขผู้ใช้สำเร็จ</h2>
        </div>
    </div>
</template>

<style scoped>
.disabled {
    opacity: 0.5;
    cursor: not-allowed;
}

input:disabled {
    opacity: 0.5;
    background-color: #f0f0f0;
}

select:disabled {
    opacity: 0.5;
    background-color: #f0f0f0;
}

button:disabled {
    opacity: 0.5;
    cursor: not-allowed;
}

button {
    font-size: 1rem;
    font-weight: 600;
    border-radius: 6px;
    transition: all 0.3s ease;
}

button:hover {
    transform: translateY(-1px);
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

button:active {
    transform: translateY(0);
    box-shadow: none;
}

input[type="checkbox"] {
    border: 2px solid #d1d5db;
    border-radius: 4px;
    background-color: white;
}

input[type="checkbox"]:checked {
    background-color: #3b82f6;
    border-color: #3b82f6;
}

input[type="checkbox"]:disabled {
    background-color: #f3f4f6;
    border-color: #d1d5db;
}
</style>