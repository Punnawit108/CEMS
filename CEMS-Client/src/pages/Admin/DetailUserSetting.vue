<script setup lang="ts">
/*
* ชื่อไฟล์: DetailUserSetting.vue
* คำอธิบาย: ไฟล์นี้แสดงหน้าจอการแก้ไขรายละเอียดของผู้ใช้ 
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/

// Import และการตั้งค่าเริ่มต้น
import { ref, reactive, onMounted } from 'vue';                // import ref และ reactive สำหรับจัดการ reactive state
import { useRoute, useRouter } from 'vue-router';   // import hooks สำหรับจัดการ routing
import { useUserStore } from '../../store/user';
import { storeToRefs } from 'pinia';

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

// โหลดข้อมูลผู้ใช้เมื่อ component ถูกโหลด
onMounted(async () => {
    try {
        if (users.value.length === 0) {
            await store.getAllUsers();
        }
        const foundUser = users.value.find(u => u.usrId.toString() === userId);
        if (foundUser) {
            // แปลงข้อมูลจาก store ให้ตรงกับ format ที่ใช้
            user.firstName = foundUser.usrFirstName;
            user.lastName = foundUser.usrLastName;
            user.employeeId = foundUser.usrEmployeeId;
            user.phoneNumber = foundUser.usrPhoneNumber || '';
            user.email = foundUser.usrEmail;
            user.affiliation = foundUser.usrCpnName;      // เปลี่ยนจาก usrOffice เป็น usrCpnName
            user.position = foundUser.usrPstName;         // เปลี่ยนจาก usrPosition เป็น usrPstName
            user.department = foundUser.usrDptName;
            user.division = foundUser.usrStName;
            user.role = foundUser.usrRolName;
            user.status = foundUser.usrIsActive ? 'อยู่ในระบบ' : 'ไม่อยู่ในระบบ';
            user.viewReportPermission = foundUser.usrIsSeeReport === 1;

            // เก็บข้อมูลต้นฉบับ
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
        await store.editUserRole(Number(userId), {
            usrRolName: user.role,
            usrIsSeeReport: user.viewReportPermission ? 1 : 0
        });
        isEditing.value = false;
        router.push(`/systemSettings/user/detail/${userId}`);
    } catch (error) {
        console.error('Failed to save changes:', error);
    }
};

// ฟังก์ชันยกเลิกการแก้ไข
const cancelEdit = () => {
    // คืนค่าข้อมูลที่แก้ไขได้กลับไปเป็นค่าเดิม
    user.role = originalUser.role;
    user.viewReportPermission = originalUser.viewReportPermission;
    isEditing.value = false;                       // ปิดโหมดแก้ไข
    router.push(`/systemSettings/user/detail/${userId}`);  // นำทางกลับหน้าแสดงรายละเอียด
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
    // เปิด Popup Alert
    isAlertSubmitOpen.value = true;
    setTimeout(() => {
        isAlertSubmitOpen.value = false; // ปิด Alert
        closePopupSubmit(); // ปิด Popup แก้ไข
    }, 1500); // 1.5 วินาที
};
// เปิด/ปิด Alert ยกเลิก
const confirmCancle = async () => {
    // เปิด Popup Alert
    isAlertCancleOpen.value = true;
    setTimeout(() => {
        isAlertCancleOpen.value = false; // ปิด Alert
        closePopupCancle(); // ปิด Popup แก้ไข
    }, 1500); // 1.5 วินาที
};
</script>

<template>
    <main class="flex flex-col">
        <section class="flex overflow-hidden items-start">
            <div
                class="flex overflow-hidden flex-col flex-1 shrink items-start py-6 pr-9 pl-16 basis-0 min-w-[240px] max-md:px-5 max-md:max-w-full">
                <div class="flex justify-between items-center w-full">
                    <h2 class="text-2xl font-bold leading-tight text-black">ข้อมูลผู้ใช้</h2>
                    <button @click="toggleEdit" v-if="!isEditing" class="px-4 py-2 bg-blue-500 text-white rounded">
                        แก้ไข
                    </button>
                    <div v-else class="flex justify-end">
                        <!--  -->
                        <button @click="openPopupSubmit" class="px-4 py-2 bg-green-500 text-white rounded mr-2">
                            <!-- saveChanges -->
                            บันทึก
                        </button>
                        <button @click="openPopupCancle" class="px-4 py-2 bg-red-500 text-white rounded ">
                            <!-- cancelEdit -->
                            ยกเลิก
                        </button>

                    </div>
                </div>
                <div class="flex overflow-hidden flex-col justify-center items-start p-2.5 mt-5 max-w-full w-[569px]">
                    <div class="flex gap-2.5 items-center">
                        <div class="flex justify-center items-center self-stretch my-auto min-h-[58px] w-[25px]">
                            <input type="checkbox" id="viewReportPermission" v-model="user.viewReportPermission"
                                :disabled="!isEditing" class="w-5 h-5 border-2 border-solid border-zinc-400 rounded" />
                        </div>
                        <span class="self-stretch my-auto text-sm leading-snug text-black">สิทธิการดูรายงาน</span>
                    </div>
                    <div
                        class="flex overflow-hidden flex-wrap gap-3 justify-between items-end self-stretch px-2 mt-5 w-full text-sm leading-snug text-black max-md:max-w-full">
                        <div class="flex overflow-hidden flex-col whitespace-nowrap min-w-[240px] w-[370px]">
                            <label for="role" class="self-start">บทบาท</label>
                            <select id="role" v-model="user.role" :disabled="!isEditing"
                                class="custom-select overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 bg-white rounded-md border border-solid border-zinc-400 min-h-[45.6px] w-[350px] appearance-none">
                                <option value="ผู้ใช้งานทั่วไป">ผู้ใช้งานทั่วไป</option>
                                <option value="ผู้ดูแลระบบ">ผู้ดูแลระบบ</option>
                                <option value="นักบัญชี">นักบัญชี</option>
                            </select>
                        </div>
                    </div>
                </div>
                <form
                    class="flex overflow-hidden flex-wrap gap-3 justify-between items-end self-stretch px-2 mt-5 w-full text-sm leading-snug text-black max-md:max-w-full">
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="firstName" class="self-start">ชื่อ</label>
                        <input id="firstName" v-model="user.firstName" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 max-w-full bg-gray-100 rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="lastName" class="self-start">นามสกุล</label>
                        <input id="lastName" v-model="user.lastName" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 max-w-full bg-gray-100 rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="employeeId" class="self-start">รหัสพนักงาน</label>
                        <input id="employeeId" v-model="user.employeeId" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="phoneNumber" class="self-start">เบอร์โทรติดต่อ</label>
                        <input id="phoneNumber" v-model="user.phoneNumber" type="tel" disabled
                            class="overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="email" class="self-start">อีเมล</label>
                        <input id="email" v-model="user.email" type="email" disabled
                            class="overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 min-w-[240px] w-[370px]">
                        <label for="affiliation" class="self-start">สังกัด</label>
                        <input id="affiliation" v-model="user.affiliation" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 min-w-[240px] w-[370px]">
                        <label for="position" class="self-start">ตำแหน่ง</label>
                        <input id="position" v-model="user.position" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 min-w-[240px] w-[370px]">
                        <label for="department" class="self-start">แผนก</label>
                        <input id="department" v-model="user.department" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="division" class="self-start">ฝ่าย</label>
                        <input id="division" v-model="user.division" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch pl-8 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                </form>
            </div>
        </section>
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

    <!-- Alert -->
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