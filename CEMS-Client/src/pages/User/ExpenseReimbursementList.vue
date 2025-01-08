<script setup lang="ts">
/*
* ชื่อไฟล์: ExpenseReimbursementList.vue
* คำอธิบาย: ไฟล์นี้แสดงรายการเบิกค่าใช้จ่าย
* ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
*/
import { useRouter } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
import Ctable from '../../components/template/CTable.vue';
import StatusBudge from '../../components/template/StatusBudge.vue';
import { onMounted, ref } from 'vue';
import { useExpenseReimbursement } from '../../store/expenseReimbursement';

const router = useRouter();
const expenseReimbursementStore = useExpenseReimbursement();
const user = ref<any>(null);

onMounted(async () => {
    const storedUser = localStorage.getItem("user"); // ดึงข้อมูลผู้ใช้จาก localStorage
    if (storedUser) {
        try {
            user.value = await JSON.parse(storedUser); // แปลงข้อมูลที่ได้จาก JSON String เป็น Object
        } catch (error) {
            console.log("Error loading user:", error); // ถ้าล้มเหลวแสดงข้อความ Error 
        }
    }
    if (user) {
        await expenseReimbursementStore.getAllExpenseReimbursementList(user.value.usrId); // เรียกใช้ฟังก์ชันดึงข้อมูลประวัติการอนุมัติ
    }
});

// ไปหน้า detail
const toDetails = (id: string) => {
    router.push(`/disbursement/listWithdraw/detail/${id}`);
};

const showModal = ref(false); // ควบคุมการแสดง Modal
const selectedItemId = ref<string | null>(null); // เก็บ ID ของรายการที่กำลังลบ

// ฟังก์ชันเปิด Modal ยืนยัน
const openConfirmationModal = (id: string) => {
    selectedItemId.value = id; // กำหนด ID ของรายการที่ต้องการลบ
    showModal.value = true; // เปิด Modal
};

// ฟังก์ชันปิด Modal
const closeModal = () => {
    showModal.value = false; // ปิด Modal
    selectedItemId.value = null; // ล้างค่า ID
};

// ฟังก์ชันยืนยันการลบ
const confirmDelete = async () => {
    if (selectedItemId.value !== null) {
        try {
            await expenseReimbursementStore.deleteExpenseReimbursementItem(user.value.usrId,selectedItemId.value); // ลบรายการ
        } catch (error) {
            console.error("Failed to delete item:", error);
        } finally {
            closeModal(); // ปิด Modal
        }
    }
};


</script>


<!-- path for test = /disbursement/listWithdraw -->


<template>
    <div>
        <div class="filter flex flex-nowrap ">
            <div class="โครงการ mr-6">
                <!-- Filter โครงการ -->
                <div class="h-[32px] w-[266px]">
                    <form class="grid">
                        <label for="SelectProject" class="py-0.5 text-[14px] text-black text-start">โครงการ</label>
                        <div class="relative h-[32px] w-[266px]  justify-center items-center">

                            <select required
                                class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4">
                                <option value="" disabled selected hidden class="placeholder">โครงการ</option>
                                <option value="item1">โครงการที่ 1</option>
                                <option value="item2">โครงการที่ 2</option>
                            </select>

                            <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">

                            </div>
                        </div>
                    </form>
                </div>
            </div>

            <!-- Filter ประเภทค่าใช้จ่าย -->
            <div class="ประเภทค่าใช้จ่าย mr-6">
                <div class="h-[32px] w-[266px]">
                    <form class="grid">
                        <label for="ExpenseType"
                            class="py-0.5 text-[14px] text-black text-start">ประเภทค่าใช้จ่าย</label>
                        <div class="relative h-[32px] w-[266px]  justify-center items-center">

                            <select required
                                class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4">
                                <option value="" disabled selected hidden class="placeholder">ประเภทค่าใช้จ่าย</option>
                                <option value="Type1">ประเภทที่ 1</option>
                                <option value="Type2">ประเภทที่ 2</option>
                            </select>

                            <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">

                            </div>
                        </div>
                    </form>
                </div>
            </div>

            <div class="วันที่เบิก mr-6">
                <!-- Filter วันที่เบิก -->
                <div class="h-[32px] w-[266px]">
                    <form class="grid">
                        <label for="Calendar" class="py-0.5 text-[14px] text-black text-start">วันที่เบิก</label>
                        <div class="relative h-[32px] w-[266px]  justify-center items-center">

                            <input type="text" id="Calendar"
                                class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4"
                                placeholder="01/01/2567-31/12/2567" />

                            <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                                <svg width="19" height="20" viewBox="0 0 19 20" fill="none"
                                    xmlns="http://www.w3.org/2000/svg">
                                    <path
                                        d="M3.31847 16.72C2.94262 16.72 2.62905 16.5998 2.37775 16.3596C2.12646 16.1193 2.00054 15.8196 2 15.4602V5.49922C2 5.1404 2.12592 4.84087 2.37775 4.60062C2.62959 4.36037 2.94316 4.23999 3.31847 4.23947H4.76176V2.5H5.64047V4.23947H11.4773V2.5H12.2932V4.23947H13.7365C14.1118 4.23947 14.4253 4.35985 14.6772 4.60062C14.929 4.84139 15.0547 5.14119 15.0541 5.5V9.20593C15.0541 9.31721 15.0152 9.41004 14.9374 9.4844C14.8597 9.55876 14.7626 9.59594 14.6462 9.59594C14.5298 9.59594 14.4327 9.55876 14.3549 9.4844C14.2771 9.41004 14.2382 9.31721 14.2382 9.20593V8.61934H2.81588V15.4602C2.81588 15.5798 2.8681 15.6898 2.97253 15.7902C3.07696 15.8905 3.192 15.9405 3.31765 15.9399H8.56785C8.68425 15.9399 8.78134 15.9771 8.85912 16.0515C8.93691 16.1258 8.9758 16.2187 8.9758 16.33C8.9758 16.4412 8.93691 16.5341 8.85912 16.6084C8.78134 16.6828 8.68425 16.72 8.56785 16.72H3.31847ZM13.7365 17.5C12.8276 17.5 12.0563 17.1973 11.4226 16.592C10.7895 15.9857 10.4729 15.2483 10.4729 14.3799C10.4729 13.5114 10.7895 12.7743 11.4226 12.1685C12.0558 11.5627 12.8268 11.2597 13.7357 11.2597C14.6445 11.2597 15.4158 11.5627 16.0495 12.1685C16.6832 12.7743 17 13.5114 17 14.3799C17 15.2483 16.6832 15.9857 16.0495 16.592C15.4158 17.1984 14.6448 17.501 13.7365 17.5ZM15.0868 16.0975L15.5322 15.6716L14.0498 14.2535V12.1303H13.4224V14.5062L15.0868 16.0975Z"
                                        fill="black" />
                                </svg>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <!-- Table -->
        <div class="w-full border-r-[2px] border-l-[2px] border-t-[2px] mt-12">
            <Ctable :table="'Table9-head-New'" />
            <table class="table-auto w-full text-center text-black">
                <tbody>
                    <tr v-for="(expenseReimbursementList, index) in expenseReimbursementStore.expenseReimbursementList"
                        :key="expenseReimbursementList.rqId" class="text-[14px] border-b-2 border-[#BBBBBB]">
                        <th class="py-[12px] px-2 w-14">{{ index + 1 }}</th>
                        <th class="py-[12px] px-2 w-48 text-start truncate overflow-hidden"
                            style="max-width: 240px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            :title="expenseReimbursementList.rqName">
                            {{ expenseReimbursementList.rqName }}
                        </th>
                        <th class="py-[12px] px-2 w-48 text-start truncate overflow-hidden"
                            style="max-width: 240px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            :title="expenseReimbursementList.rqPjName">
                            {{ expenseReimbursementList.rqPjName }}
                        </th>
                        <th class="py-[12px] px-5 w-32 text-start font-[100]">
                            {{ expenseReimbursementList.rqRqtName }}
                        </th>
                        <th class="py-[12px] px-2 w-20 text-end">
                            {{ expenseReimbursementList.rqWithDrawDate }}
                        </th>
                        <th class="py-[12px] px-5 w-32 text-end">
                            {{ expenseReimbursementList.rqExpenses }}
                        </th>
                        <th class="py-[12px] px-2 w-28 text-center">
                            <span>
                                <StatusBudge :status="'sts-'+expenseReimbursementList.rqStatus" />
                            </span>
                        </th>
                        <th class="py-[10px] px-2 w-20 text-center">
                            <span class="flex justify-center">
                                <Icon :icon="'viewDetails'" v-on:click="toDetails(expenseReimbursementList.rqId)"  />
                                <Icon v-if = "expenseReimbursementList.rqStatus === 'sketch'" :icon="'bin'" @click="openConfirmationModal(expenseReimbursementList.rqId)" />
                            </span>
                        </th>
                    </tr>
                </tbody>
            </table>
            <Ctable :table="'Table3-footer'" />
        </div>

        <div v-if="showModal" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
            <div class="modal-box bg-white w-[460px] h-[295px] ">
                <div class="flex justify-center mt-[25px]">
                    <svg width="70px" height="70px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
                        <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g>
                        <g id="SVGRepo_iconCarrier">
                            <path
                                d="M12 2C6.4898 2 2 6.4898 2 12C2 17.5102 6.4898 22 12 22C17.5102 22 22 17.5102 22 12C22 6.4898 17.5102 2 12 2ZM11.1837 8.42857C11.1837 8.02041 11.4898 7.61225 12 7.61225C12.5102 7.61225 12.8163 7.91837 12.8163 8.42857V12.5102C12.8163 12.9184 12.5102 13.3265 12 13.3265C11.4898 13.3265 11.1837 13.0204 11.1837 12.5102V8.42857ZM12 16.5918C11.4898 16.5918 10.9796 16.0816 10.9796 15.5714C10.9796 15.0612 11.4898 14.551 12 14.551C12.5102 14.551 13.0204 15.0612 13.0204 15.5714C13.0204 16.0816 12.5102 16.5918 12 16.5918Z"
                                fill="#FFBE40"></path>
                        </g>
                    </svg>
                </div>
                <p class="text-2xl font-bold text-black mt-1 flex justify-center">ยืนยันการลบคำขอเบิกค่าใช้จ่าย</p>
                <p class="text-lg font-bold text-[#B6B7BA] mt-1 flex justify-center">
                    คุณยืนยันการลบคำขอเบิกค่าใช้จ่ายหรือไม่?</p>
                <div class="modal-action flex justify-center">
                    <form method="dialog">
                        <!-- if there is a button in form, it will close the modal -->
                        <button @click="closeModal"
                            class="bg-white border-solid border-[#B6B7BA] border-2 rounded px-7 py-2 text-[#B6B7BA] text-sm font-normal mr-3">ยกเลิก</button>
                        <button @click="confirmDelete"
                            class="bg-[#12B669] border-solid border-[#12B669] border-2 rounded px-7 py-2 text-white text-sm font-normal"
                            onclick="my_modal_2.showModal()">ยืนยัน</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.custom-select {
    -webkit-appearance: none;
    -moz-appearance: none;
    appearance: none;
    background-image: none;
}

.custom-select::-ms-expand {
    display: none;
}

select,
select option {
    background-color: white;
    color: #000000;
}

select:invalid,
select option[value=""] {
    color: #999999;
}

[hidden] {
    display: none;
}

/* Additional styles to ensure the dropdown arrow is hidden in WebKit browsers */
@media screen and (-webkit-min-device-pixel-ratio:0) {
    .custom-select {
        background-image: url("data:image/svg+xml;utf8,<svg fill='transparent' height='24' viewBox='0 0 24 24' width='24' xmlns='http://www.w3.org/2000/svg'><path d='M7 10l5 5 5-5z'/></svg>");
        background-repeat: no-repeat;
        background-position-x: 100%;
        background-position-y: 5px;
    }
}
</style>