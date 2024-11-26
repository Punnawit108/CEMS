<script setup lang="ts">
/**
* ชื่อไฟล์: DetailUserSetting.vue
* คำอธิบาย: ไฟล์นี้แสดงหน้าจอการแก้ไขรายละเอียดของผู้ใช้ 
* Input: รายละเอียดข้อมูลของผู้ใช้
* Output: รายละเอียดข้อมูลของผู้ใช้
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

// Import และการตั้งค่าเริ่มต้น
import { ref, reactive, onMounted } from 'vue';                // import ref และ reactive สำหรับจัดการ reactive state
import { useRoute, useRouter } from 'vue-router';   // import hooks สำหรับจัดการ routing
import { useUsers } from '../../store/user';
import { storeToRefs } from 'pinia';

const route = useRoute();                           // สร้าง instance สำหรับอ่านค่าจาก route ปัจจุบัน
const router = useRouter();                         // สร้าง instance สำหรับจัดการ navigation
const store = useUsers();                           // สร้าง instance ของ store
const { users } = storeToRefs(store);              // ดึงข้อมูล users จาก store
const userId = route.params.id;                     // ดึง id จาก route parameters
const isEditing = ref(false);                      // สถานะการแก้ไขข้อมูล

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
                        <button @click="cancelEdit" class="px-4 py-2 bg-red-500 text-white rounded mr-2">
                            ยกเลิก
                        </button>
                        <button @click="saveChanges" class="px-4 py-2 bg-green-500 text-white rounded">
                            บันทึก
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