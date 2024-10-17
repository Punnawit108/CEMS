<script setup lang="ts">
import { ref, reactive } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const userId = route.params.id;
const isEditing = ref(false);
const fileInput = ref<HTMLInputElement | null>(null);

// ตัวอย่างข้อมูลผู้ใช้
const originalUser = {
    id: userId,
    firstName: 'เทียนชัย',
    lastName: 'คูเมือง',
    employeeId: '651122',
    phoneNumber: '098-000-0000',
    email: 'tianchai@example.com',
    affiliation: 'คลิกเน็กซ์ กรุงเทพ',
    position: 'Developer',
    department: 'ไอที',
    division: 'การตลาด',
    role: 'ผู้ใช้งาน',
    status: 'อยู่ในระบบ',
    profileImage: null as string | null,
    viewReportPermission: false
};

const user = reactive({ ...originalUser });

const toggleEdit = () => {
    isEditing.value = !isEditing.value;
    if (isEditing.value) {
        // เมื่อเริ่มแก้ไข ให้เก็บข้อมูลต้นฉบับไว้และเปลี่ยน path
        Object.assign(originalUser, user);
        router.push(`/systemSettings/user/detail/${userId}/editUser`);
    } else {
        // เมื่อออกจากโหมดแก้ไข ให้กลับไปยัง path เดิม
        router.push(`/systemSettings/user/detail/${userId}`);
    }
};

const saveChanges = () => {
    isEditing.value = false;
    // อัปเดตเฉพาะข้อมูลที่สามารถแก้ไขได้
    originalUser.role = user.role;
    originalUser.viewReportPermission = user.viewReportPermission;
    originalUser.profileImage = user.profileImage;
    // กลับไปยัง path เดิมหลังจากบันทึก
    router.push(`/systemSettings/user/detail/${userId}`);
};

const cancelEdit = () => {
    // คืนค่าข้อมูลกลับไปเป็นข้อมูลต้นฉบับเฉพาะส่วนที่แก้ไขได้
    user.role = originalUser.role;
    user.viewReportPermission = originalUser.viewReportPermission;
    user.profileImage = originalUser.profileImage;
    isEditing.value = false;
    // กลับไปยัง path เดิมเมื่อยกเลิกการแก้ไข
    router.push(`/systemSettings/user/detail/${userId}`);
};

// ฟังก์ชันอื่นๆ ยังคงเหมือนเดิม
const handleImageUpload = (event: Event) => {
    const target = event.target as HTMLInputElement;
    const file = target.files?.[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
            user.profileImage = e.target?.result as string;
        };
        reader.readAsDataURL(file);
    }
};

const openFileInput = () => {
    fileInput.value?.click();
};

const removeImage = () => {
    user.profileImage = null;
};
</script>


<template>
    <main class="flex flex-col">
        <section class="flex overflow-hidden flex-wrap items-start w-full max-md:max-w-full">
            <div class="flex flex-col justify-center items-center p-6 min-w-[240px] w-[270px] max-md:px-5">
                <h2 class="text-2xl font-bold leading-tight text-center text-black">รูปโปรไฟล์</h2>
                <div class="flex overflow-hidden flex-col justify-center items-center mt-6 max-w-full w-[180px]">
                    <div class="profile-image-container">
                        <img v-if="user.profileImage" :src="user.profileImage" alt="User profile picture"
                            class="profile-image" />
                        <img v-else loading="lazy"
                            src="https://cdn.builder.io/api/v1/image/assets/TEMP/d5d9160b561ee2e005189b15b23b34fd112deada053a4f8329f90bf63fbc5d01?placeholderIfAbsent=true&apiKey=963991dcf23f4b60964b821ef12710c5"
                            class="profile-image default-image" alt="Default profile picture" />
                    </div>
                </div>
                <div v-if="isEditing"
                    class="flex flex-col justify-center items-center mt-6 w-full text-base text-center whitespace-nowrap">
                    <input type="file" ref="fileInput" @change="handleImageUpload" accept="image/*" class="hidden" />
                    <button @click="openFileInput"
                        class="w-full py-3 px-4 bg-indigo-900 text-white rounded-md font-semibold hover:bg-indigo-800 transition duration-300 mb-2">
                        อัปโหลดรูปภาพ
                    </button>
                    <button v-if="user.profileImage" @click="removeImage"
                        class="w-full py-3 px-4 bg-white text-red-600 rounded-md font-semibold border border-red-600 hover:bg-red-50 transition duration-300">
                        ลบรูปภาพ
                    </button>
                </div>
            </div>
            <div
                class="flex overflow-hidden flex-col flex-1 shrink items-start py-6 pr-9 pl-16 basis-0 min-w-[240px] max-md:px-5 max-md:max-w-full">
                <div class="flex justify-between items-center w-full">
                    <h2 class="text-2xl font-bold leading-tight text-center text-black">ข้อมูลผู้ใช้</h2>
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
                        <div
                            class="flex overflow-hidden gap-2.5 justify-center items-center self-stretch py-5 pr-1 pl-1 my-auto bg-white min-h-[58px] w-[25px]">
                            <input type="checkbox" id="viewReportPermission" v-model="user.viewReportPermission"
                                :disabled="!isEditing" class="w-5 h-5">
                        </div>
                        <span
                            class="self-stretch my-auto text-sm leading-snug text-center text-black">สิทธิการดูรายงาน</span>
                    </div>
                    <div
                        class="flex overflow-hidden flex-wrap gap-3 justify-between items-end self-stretch px-2 mt-5 w-full text-sm leading-snug text-center text-black max-md:max-w-full">
                        <div class="flex overflow-hidden flex-col whitespace-nowrap min-w-[240px] w-[370px]">
                            <label for="role" class="self-start">บทบาท</label>
                            <select id="role" v-model="user.role" :disabled="!isEditing"
                                class="custom-select overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 bg-white rounded-md border border-solid border-zinc-400 min-h-[45.6px] w-[350px] appearance-none">
                                <option value="ผู้ใช้งาน">ผู้ใช้งาน</option>
                                <option value="ผู้ดูแลระบบ">ผู้ดูแลระบบ</option>
                                <option value="ผู้จัดการ">นักบัญชี</option>
                            </select>
                            
                        </div>
                    </div>
                </div>
                <form
                    class="flex overflow-hidden flex-wrap gap-3 justify-between items-end self-stretch px-2 mt-5 w-full text-sm leading-snug text-center text-black max-md:max-w-full">
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="firstName" class="self-start">ชื่อ</label>
                        <input id="firstName" v-model="user.firstName" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 max-w-full bg-gray-100 rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="lastName" class="self-start">นามสกุล</label>
                        <input id="lastName" v-model="user.lastName" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 max-w-full bg-gray-100 rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="employeeId" class="self-start">รหัสพนักงาน</label>
                        <input id="employeeId" v-model="user.employeeId" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="phoneNumber" class="self-start">เบอร์โทรติดต่อ</label>
                        <input id="phoneNumber" v-model="user.phoneNumber" type="tel" disabled
                            class="overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div
                        class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[754px] max-md:max-w-full">
                        <label for="email" class="self-start">อีเมล</label>
                        <input id="email" v-model="user.email" type="email" disabled
                            class="overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[734px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 min-w-[240px] w-[370px]">
                        <label for="affiliation" class="self-start">สังกัด</label>
                        <input id="affiliation" v-model="user.affiliation" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 min-w-[240px] w-[370px]">
                        <label for="position" class="self-start">ตำแหน่ง</label>
                        <input id="position" v-model="user.position" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 min-w-[240px] w-[370px]">

                        <label for="department" class="self-start">แผนก</label>
                        <input id="department" v-model="user.department" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                    <div class="flex overflow-hidden flex-col p-2.5 whitespace-nowrap min-w-[240px] w-[370px]">
                        <label for="division" class="self-start">ฝ่าย</label>
                        <input id="division" v-model="user.division" type="text" disabled
                            class="overflow-hidden gap-2.5 self-stretch px-5 py-2.5 mt-2.5 max-w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[38px] w-[350px]" />
                    </div>
                </form>
            </div>
        </section>
    </main>
</template>

<style scoped>
/* Add any additional styles here if needed */
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

.profile-image-container {
    width: 180px;
    height: 180px;
    border-radius: 50%;
    overflow: hidden;
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: #f3f4f6;
    /* สีพื้นหลังเทาอ่อน */
    border: 1px solid #d1d5db;
    /* ขอบสีเทา */
}

.profile-image {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.default-image {
    width: 50%;
    height: 50%;
    object-fit: contain;
}

.upload-button,
.remove-button {
    margin-top: 10px;
    width: 100%;
    padding: 10px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s;
}

.upload-button {
    background-color: #4a5568;
    color: white;
}

.remove-button {
    background-color: #fff;
    color: #e53e3e;
    border: 1px solid #e53e3e;
}

.upload-button:hover {
    background-color: #2d3748;
}

.remove-button:hover {
    background-color: #fed7d7;
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
</style>