<template>

    <div class="flex flex-col items-center text-center ">
        <!-- content -->
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
                <label class="flex items-start ml-[42px] text-base font-normal text-black mb-2 mt-2">ระบุเหตุผล<span class="text-[#E63C3C] ml-1">*</span></label>
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