<script setup lang="ts">
import { useRouter } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
const router = useRouter();

const toDetails = (id: string) => {
    router.push(`/payment/history/detail/${id}`);
}
</script>
<!-- path for test = /approval/history -->
<template>
    <!-- content -->
    <div class="content">
        <div class="filter flex flex-nowrap ">

        <div class="ค้นหา mr-6">
            <!-- Filter ค้นหา -->
            <div class="h-[32px] w-[266px]">
                <form class="grid">
                    <label for="SearchBar" class="py-0.5 text-[14px] text-black text-start">ค้นหา</label>
                    <div class="relative h-[32px] w-[266px]  justify-center items-center">

                        <div class="absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                            <svg width="19" height="20" viewBox="0 0 19 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                                </svg>                                
                        </div>

                        <input type="text" id="SearchBar"
                            class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-9"
                            placeholder="ชื่อ-นามสกุล" />
                    </div>
                </form>
            </div>
        </div>

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
                    <label for="ExpenseType" class="py-0.5 text-[14px] text-black text-start">ประเภทค่าใช้จ่าย</label>
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
    </div>
    
    <div class="Table 8 ผู้มีสิทธิอนุมัติ-ประวัติอนุมัติคำขอเบิก mt-12">
        <!-- Table 8 ผู้มีสิทธิอนุมัติ-ประวัติอนุมัติคำขอเบิก -->
    
    <div class="w-full h-fit border-[1px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อ-นามสกุล</th>
                    <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อโครงการ</th>
                    <th class="py-[11px] px-5 text-start w-36 font-bold">ประเภทค่าใช้จ่าย</th>
                    <th class="py-[11px] px-2 text-end w-20 font-bold">วันที่เบิก</th>
                    <th class="py-[11px] px-5 text-end w-36 font-bold">จำนวนเงิน(บาท)</th>
                    <th class="py-[11px] px-2 text-center w-32 font-bold">สถานะ</th>
                    <th class="py-[11px] px-2 text-center w-24 font-bold">รายละเอียด</th>
                </tr>
            </thead>
            <!-- ข้อมูลชุดที่ 1 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-5 text-end ">200.00</th>
                <th class="py-[12px] px-2"> 
                    <div class="ml-5 rounded-[28px] w-[92px] h-[24px] bg-green text-white text-Sarabun-Reqular12 grid justify-items-center flex items-center">อนุมัติแล้ว</div>
                </th>
                <th class="flex justify-center mt-1">
                    <button v-on:click="toDetails"> 
                        <Icon :icon="'viewDetails'" /> 
                    </button>
                </th>
            </tr>
            <!-- ข้อมูลชุดที่ 2 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-5 text-end ">200.00</th>
                <th class="py-[12px] px-2"> 
                    <div class="ml-5 rounded-[28px] w-[92px] h-[24px] bg-redNormal text-white text-Sarabun-Reqular12 grid justify-items-center flex items-center">ไม่อนุมัติ</div>
                </th>
                <th class="flex justify-center mt-1">
                    <button v-on:click="toDetails"> 
                        <Icon :icon="'viewDetails'" /> 
                    </button>
                </th>
            </tr>
            <!-- ข้อมูลว่าง 3-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 4-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 5-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 6-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 7-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 8-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 9-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 10-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 11-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 12-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 13-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 14-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด 15 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-1 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end pr-12">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-4 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>
    </div>
    
    </div>
    <!-- content -->
</template>