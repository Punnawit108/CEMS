<script setup lang="ts">
/*
* ชื่อไฟล์: Popup.vue
* คำอธิบาย: ไฟล์นี้เป็นComponentสำหรับแสดงPopup
* ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/

import { defineProps, defineEmits, ref } from "vue";
import Button from "./Button.vue";

const props = defineProps({
    type: String, // ประเภท popup เช่น 'reject', 'edit', 'approve'
    data: Object, // ข้อมูลที่ต้องการส่งเข้ามา
});

const type = ref("approve");

const emit = defineEmits(["close", "confirm"]); // สร้าง event สำหรับ close และ confirm
</script>
<template>
    <div class="flex flex-col items-center text-center ">
        <!-- content -->
        <!-- popup approve -->
        <div v-if="type === 'approve'" class="fixed inset-0 bg-black/50 flex justify-center items-center z-50">
            <div class="bg-white rounded-lg shadow-lg p-6 w-[460px] h-[417px]">
                <!-- Icon Section -->
                <div class="flex justify-center mb-2 mt-12">
                    <svg xmlns="http://www.w3.org/2000/svg" width="60" height="61" viewBox="0 0 60 61" fill="none">
                        <path
                            d="M29.9999 0C37.9187 0 45.5131 3.19263 51.1126 8.87555C56.712 14.5585 59.8577 22.2662 59.8577 30.303C59.8577 38.3399 56.712 46.0476 51.1126 51.7305C45.5131 57.4134 37.9187 60.6061 29.9999 60.6061C22.0811 60.6061 14.4867 57.4134 8.88724 51.7305C3.28782 46.0476 0.14209 38.3399 0.14209 30.303C0.14209 22.2662 3.28782 14.5585 8.88724 8.87555C14.4867 3.19263 22.0811 0 29.9999 0ZM29.9999 12.987C29.4584 12.9865 28.9227 13.1007 28.4271 13.3222C27.9315 13.5437 27.4868 13.8677 27.1216 14.2735C26.7563 14.6792 26.4784 15.1578 26.3057 15.6787C26.133 16.1996 26.0692 16.7514 26.1184 17.2987L27.6753 34.6407C27.7356 35.2237 28.0066 35.7634 28.4358 36.1557C28.8651 36.548 29.4223 36.7651 29.9999 36.7651C30.5776 36.7651 31.1347 36.548 31.564 36.1557C31.9933 35.7634 32.2642 35.2237 32.3246 34.6407L33.8772 17.2987C33.9264 16.7517 33.8627 16.2004 33.6902 15.6797C33.5177 15.1591 33.2402 14.6807 32.8754 14.275C32.5106 13.8694 32.0665 13.5453 31.5714 13.3235C31.0763 13.1018 30.5411 12.9871 29.9999 12.987ZM29.9999 47.619C30.9049 47.619 31.7729 47.2542 32.4128 46.6047C33.0527 45.9552 33.4122 45.0743 33.4122 44.1558C33.4122 43.2373 33.0527 42.3565 32.4128 41.707C31.7729 41.0575 30.9049 40.6926 29.9999 40.6926C29.0949 40.6926 28.227 41.0575 27.587 41.707C26.9471 42.3565 26.5876 43.2373 26.5876 44.1558C26.5876 45.0743 26.9471 45.9552 27.587 46.6047C28.227 47.2542 29.0949 47.619 29.9999 47.619Z"
                            fill="#FFBE40" />
                    </svg>
                </div>
                <!-- Title Section -->
                <h2 class="text-center text-[24px] text-black mb-4">
                    ยืนยันการอนุมัติค่าใช้จ่าย
                </h2>
                <!-- Details Section -->
                <p class="text-center text-black text-[18px] mb-4">
                    นางสาว อลิสา ปะกังพลัง
                    <br />
                    วันที่ขอเบิก 11/9/2567
                </p>
                <!-- Confirmation Text -->
                <p class="text-center text-gray-400 text-[18px] mb-8">
                    คุณยืนยันการอนุมัติค่าใช้จ่ายหรือไม่ ?
                </p>
                <!-- Button Section -->
                <div class="flex justify-center gap-5">
                    <Button :type="'btn-cancleGray'"></Button>
                    <Button :type="'btn-summit'"></Button>
                </div>
            </div>
        </div>

        <div v-if="type === 'reject'" class="fixed inset-0 bg-black/50 flex justify-center items-center z-50 ">
            <div class="bg-white rounded-lg shadow-lg w-[460px] h-[417px] px-[75px] py-[69px] ">
                <!-- Icon Section -->
                <div class="flex justify-center mb-4  items-center align-middle gap-3">
                    <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 60 60" fill="none">
                        <path
                            d="M33.6873 7.875H29.9998H20.1665C16.0934 7.875 12.7915 11.1769 12.7915 15.25V44.75C12.7915 48.8232 16.0934 52.125 20.1665 52.125H27.5415M33.6873 7.875L47.2082 21.7031M33.6873 7.875V19.2448C33.6873 20.6025 34.7879 21.7031 36.1457 21.7031H47.2082M47.2082 21.7031V29.5391"
                            stroke="#E63C3C" stroke-width="5" stroke-linecap="round" stroke-linejoin="round" />
                        <path
                            d="M37.375 39.833L43.5208 45.9788M43.5208 45.9788L49.6667 52.1247M43.5208 45.9788L49.6667 39.833M43.5208 45.9788L37.375 52.1247"
                            stroke="#E63C3C" stroke-width="5" stroke-linecap="round" stroke-linejoin="round" />
                    </svg>
                    <!-- Title Section -->
                    <h2 class="text-center text-[24px] text-black ">
                        ยืนยันการปฏิเสธคำขอ
                    </h2>
                </div>
                <!-- Confirmation Text -->
                <p class="text-center text-gray-400 text-[18px] mb-4">
                    คุณยืนยันการปฏิเสธคำขอหรือไม่ ?
                </p>
                <p class="text-start text-gray-400 text-[18px] mb-4">ระบุเหตุผลการปฏิเสธ <span
                        class="text-red-500">*</span></p>
                <textarea id="rejectReason" required
                    class="flex overflow-hidden gap-1.5 items-start mb-4 px-2.5 pt-1.5 pb-7 w-full text-sm text-gray-500 bg-white rounded-md border border-solid border-slate-200 min-h-[70px]"
                    aria-label="ระบุเหตุผลการปฏิเสธ" />
                <!-- Button Section -->
                <div class="flex justify-center gap-5">
                    <Button :type="'btn-cancleGray'"></Button>
                    <Button :type="'btn-summit'"></Button>
                </div>
            </div>
        </div>

        <!-- format1 -->
        <button class="btn" onclick="my_modal_1.showModal()">format1</button>
        <dialog id="my_modal_1" class="modal">
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
                <p class="text-2xl font-bold text-black mt-1">ยืนยันการตั้งค่าโปรไฟล์</p>
                <p class="text-lg font-bold text-[#B6B7BA] mt-1">คุณยืนยันการตั้งค่าโปรไฟล์หรือไม่?</p>
                <div class="modal-action flex justify-center">
                    <form method="dialog">
                        <!-- if there is a button in form, it will close the modal -->
                        <button
                            class="bg-white border-solid border-[#B6B7BA] border-2 rounded px-7 py-2 text-[#B6B7BA] text-sm font-normal mr-3">ยกเลิก</button>
                        <button
                            class="bg-[#12B669] border-solid border-[#12B669] border-2 rounded px-7 py-2 text-white text-sm font-normal"
                            onclick="my_modal_2.showModal()">ยืนยัน</button>
                    </form>
                </div>
            </div>
        </dialog>
        <!-- format2 -->
        <button class="btn" onclick="my_modal_2.showModal()">format2</button>
        <dialog id="my_modal_2" class="modal">
            <div class="modal-box bg-white w-[460px] h-[295px]">
                <div class="flex justify-center mt-[50px]">
                    <svg width="80px" height="80px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
                        <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g>
                        <g id="SVGRepo_iconCarrier">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                                d="M22 12C22 17.5228 17.5228 22 12 22C6.47715 22 2 17.5228 2 12C2 6.47715 6.47715 2 12 2C17.5228 2 22 6.47715 22 12ZM16.0303 8.96967C16.3232 9.26256 16.3232 9.73744 16.0303 10.0303L11.0303 15.0303C10.7374 15.3232 10.2626 15.3232 9.96967 15.0303L7.96967 13.0303C7.67678 12.7374 7.67678 12.2626 7.96967 11.9697C8.26256 11.6768 8.73744 11.6768 9.03033 11.9697L10.5 13.4393L12.7348 11.2045L14.9697 8.96967C15.2626 8.67678 15.7374 8.67678 16.0303 8.96967Z"
                                fill="#12B669"></path>
                        </g>
                    </svg>
                </div>
                <p class="text-2xl font-bold text-black mt-4">ยืนยันการตั้งค่าโปรไฟล์สำเร็จ</p>
            </div>
            <form method="dialog" class="modal-backdrop">
                <button>close</button>
            </form>
        </dialog>
        <!-- format3 -->
        <button class="btn" onclick="my_modal_3.showModal()">format3</button>
        <dialog id="my_modal_3" class="modal">
            <div class="modal-box bg-white w-[460px] h-[400px] ">
                <div class="flex justify-center mt-[45px]">
                    <div class="flex justify-between items-center">
                        <svg width="60px" height="60px" viewBox="0 0 24 24" fill="none"
                            xmlns="http://www.w3.org/2000/svg">
                            <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
                            <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g>
                            <g id="SVGRepo_iconCarrier">
                                <path
                                    d="M13.5 3H12H8C6.34315 3 5 4.34315 5 6V18C5 19.6569 6.34315 21 8 21H11M13.5 3L19 8.625M13.5 3V7.625C13.5 8.17728 13.9477 8.625 14.5 8.625H19M19 8.625V11.8125"
                                    stroke="#E63C3C" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                                </path>
                                <path d="M15 16L17.5 18.5M20 21L17.5 18.5M17.5 18.5L20 16M17.5 18.5L15 21"
                                    stroke="#E63C3C" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                                </path>
                            </g>
                        </svg>
                        <p class="text-2xl font-bold text-black ml-4">ยืนยันการปฏิเสธคำขอ</p>
                    </div>
                </div>
                <p class="text-lg font-normal text-[#B6B7BA] mt-1">คุณยืนยันการปฏิเสธคำขอหรือไม่</p>
                <label class="flex items-start ml-[42px] text-base font-normal text-black mb-2 mt-2">ระบุเหตุผล<span
                        class="text-[#E63C3C] ml-1">*</span></label>
                <div class="flex justify-center">
                    <textarea placeholder="ระบุเหตุผล"
                        class="textarea bg-white border-[#f2f4f8] border-solid border-2 textarea-sm w-full max-w-xs "></textarea>
                </div>
                <div class="modal-action flex justify-center">
                    <form method="dialog">
                        <!-- if there is a button in form, it will close the modal -->
                        <button
                            class="bg-white border-solid border-[#B6B7BA] border-2 rounded px-7 py-2 text-[#B6B7BA] text-sm font-normal mr-3">ยกเลิก</button>
                        <button
                            class="bg-[#12B669] border-solid border-[#12B669] border-2 rounded px-7 py-2 text-white text-sm font-normal"
                            onclick="my_modal_2.showModal()">ยืนยัน</button>
                    </form>
                </div>
            </div>
        </dialog>
        <!-- content -->
    </div>
</template>
