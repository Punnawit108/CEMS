<script setup lang="ts">
import { useRouter } from 'vue-router';
import Ctable from '../../components/template/Ctable.vue';
import Icon from '../../components/template/CIcon.vue';

const router = useRouter();

const toDetails = (id: string) => {
    router.push(`/payment/history/detail/${id}`);
}
</script>
<!-- path for test = /payment/history -->
<template>
    <!-- content -->
    <div>
        <div class="w-full flex flex-wrap  items-center gap-6 mb-12">

            <!-- Filter ค้นหา -->
            <div class="h-[32px] w-[266px] ">
                <form class="grid">
                    <label for="SearchBar" class="py-0.5 text-[14px] text-black text-start">ค้นหา</label>
                    <div class="relative h-[32px] w-[266px]  justify-center items-center">

                        <div
                            class="custom-select absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                            <svg width="19" height="20" viewBox="0 0 19 20" fill="none"
                                xmlns="http://www.w3.org/2000/svg">
                                <path
                                    d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z"
                                    stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                            </svg>
                        </div>

                        <input type="text" id="SearchBar"
                            class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-9"
                            placeholder="ชื่อ-นามสกุล" />
                    </div>
                </form>
            </div>


            <!-- Filter โครงการ -->
            <div class="h-[32px] w-[266px]">
                <form class="grid">
                    <label for="SelectProject" class="py-0.5 text-[14px] text-black text-start">โครงการ</label>
                    <div class="relative h-[32px] w-[266px]  justify-center items-center">

                        <select required
                            class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4">
                            <option value="" disabled selected hidden class="placeholder">โครงการ</option>
                            <option value="item1">โครงการที่ 1</option>
                            <option value="item2">โครงการที่ 2</option>
                        </select>

                        <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                            <svg width="13" height="8" viewBox="0 0 13 8" fill="none"
                                xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd" clip-rule="evenodd"
                                    d="M7.2071 7.2071C6.8166 7.5976 6.1834 7.5976 5.7929 7.2071L0.79289 2.20711C0.40237 1.81658 0.40237 1.18342 0.79289 0.79289C1.18342 0.40237 1.81658 0.40237 2.20711 0.79289L6.5 5.0858L10.7929 0.79289C11.1834 0.40237 11.8166 0.40237 12.2071 0.79289C12.5976 1.18342 12.5976 1.81658 12.2071 2.20711L7.2071 7.2071Z"
                                    fill="black" />
                            </svg>
                        </div>
                    </div>
                </form>
            </div>


            <!-- Filter ประเภทค่าใช้จ่าย -->
            <div class="h-[32px] w-[266px]">
                <form class="grid">
                    <label for="ExpenseType" class="py-0.5 text-[14px] text-black text-start">ประเภทค่าใช้จ่าย</label>
                    <div class="relative h-[32px] w-[266px]  justify-center items-center">

                        <select required
                            class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4">
                            <option value="" disabled selected hidden class="placeholder">ประเภทค่าใช้จ่าย</option>
                            <option value="Type1">ประเภทที่ 1</option>
                            <option value="Type2">ประเภทที่ 2</option>
                        </select>

                        <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                            <svg width="13" height="8" viewBox="0 0 13 8" fill="none"
                                xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd" clip-rule="evenodd"
                                    d="M7.2071 7.2071C6.8166 7.5976 6.1834 7.5976 5.7929 7.2071L0.79289 2.20711C0.40237 1.81658 0.40237 1.18342 0.79289 0.79289C1.18342 0.40237 1.81658 0.40237 2.20711 0.79289L6.5 5.0858L10.7929 0.79289C11.1834 0.40237 11.8166 0.40237 12.2071 0.79289C12.5976 1.18342 12.5976 1.81658 12.2071 2.20711L7.2071 7.2071Z"
                                    fill="black" />
                            </svg>
                        </div>
                    </div>
                </form>
            </div>


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

        <div class="w-full  border-r-[2px] border-l-[2px] border-t-[2px]">
            <!-- ตาราง -->
            <div>
                <Ctable :table="'Table9-head'" />
            </div>
            <table class="w-full">
                <tbody>
                    <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                        <th class="py-[12px] px-2 w-14">1</th>
                        <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                            style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            title="นายเทียนชัย คูเมือง">
                            นายเทียนชัย คูเมือง
                        </th>
                        <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                            style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                            กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                        </th>
                        <th class="py-[12px] px-5 w-32 text-start font-[100]">ค่าเดินทาง</th>
                        <th class="py-[12px] px-2 w-20 text-end ">08/10/2567</th>
                        <th class="py-[12px] px-5 w-32 text-end ">200.00</th>
                        <th class="py-[12px] px-2 w-28 text-center text-green-500">นำจ่ายแล้ว</th>
                        <th class="py-[10px] px-2 w-20 text-center ">
                            <span class="flex justify-center" v-on:click="toDetails">
                                <Icon :icon="'viewDetails'" />
                            </span>
                        </th>
                    </tr>
                </tbody>
            </table>
            <div>
                <Ctable :table="'Table9-footer'" />
            </div>
        </div>
    </div>
    <!-- content -->
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