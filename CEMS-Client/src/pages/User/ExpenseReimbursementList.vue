<script setup lang="ts">
/**
 * ชื่อไฟล์ : ExpenseReimbursementList.vue
 * คำอธิบาย : ไฟล์นี้แสดงรายการเบิกค่าใช้จ่าย
 * Input : -
 * Output : รายการเบิกค่าใช้จ่าย
 * ชื่อผู้เขียน / แก้ไข : อังคณา อุ่นเสียม
 * วันที่จัดทำ / วัยที่แก้ไข : 26 พฤศจิกายน 2567
 */
import { useRouter } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
import Ctable from '../../components/template/Ctable.vue';
import StatusBudge from '../../components/template/StatusBudge.vue';
import { onMounted, ref } from 'vue';
import { useExpenseReimbursementList } from '../../store/expenseReimbursementListStore';

const router = useRouter();
const ExpenseReimbursementList = useExpenseReimbursementList();

onMounted(async () => {
    await ExpenseReimbursementList.getAllExpenseReimbursementList();
    console.log(ExpenseReimbursementList.expenseReimbursementList)
});

// ไปหน้า detail
const toDetails = (id: string) => {
    router.push(`/payment/list/detail/${id}`);
};

const showModal = ref(false); // ควบคุมการแสดง Modal
const selectedItemId = ref<number | null>(null); // เก็บ ID ของรายการที่กำลังลบ

// ฟังก์ชันเปิด Modal ยืนยัน
const openConfirmationModal = (id: number) => {
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
            await ExpenseReimbursementList.deleteExpenseReimbursementItem(selectedItemId.value); // ลบรายการ
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
        <!-- Table -->
        <div class="w-full h-fit border-2 border-b-grayDark items-start">
            <Ctable :table="'Table9-head-New'" />
            <table class="table-auto w-full text-center text-black">
                <tbody>
                    <tr v-for="(ExpenseReimbursementList, index) in ExpenseReimbursementList.expenseReimbursementList"
                        :key="ExpenseReimbursementList.rqId" class="text-[14px] border-b-2 border-[#BBBBBB]">
                        <th class="py-[12px] px-2 w-14">{{ index + 1 }}</th>
                        <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                            :title="ExpenseReimbursementList.rqUsrName">
                            {{ ExpenseReimbursementList.rqUsrName }}
                        </th>
                        <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                            :title="ExpenseReimbursementList.rqName">
                            {{ ExpenseReimbursementList.rqName }}
                        </th>
                        <th class="py-[12px] px-5 w-32 text-start font-[100]">
                            {{ ExpenseReimbursementList.rqPjName }}
                        </th>
                        <th class="py-[12px] px-2 w-20 text-end">
                            {{ ExpenseReimbursementList.rqDatePay }}
                        </th>
                        <th class="py-[12px] px-2 w-40 text-end">
                            {{ ExpenseReimbursementList.rqExpenses }}
                        </th>
                        <th class="py-[12px] px-2 w-32 text-center">
                            <span>
                                <StatusBudge :status="'sts-'+ExpenseReimbursementList.rqStatus" />
                            </span>
                        </th>
                        <th class="py-[10px] px-2 w-24 text-center">
                            <span class="flex justify-center">
                                <Icon :icon="'viewDetails'" v-on:click="toDetails" />
                                <Icon :icon="'bin'" @click="openConfirmationModal(ExpenseReimbursementList.rqId)" />
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