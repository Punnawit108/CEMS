<script setup lang="ts">
/*
* ชื่อไฟล์: Ctable.vue
* คำอธิบาย: ไฟล์นี้เป็น Component Code table
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/
import { defineProps } from 'vue';
import Icon from '../Icon/CIcon.vue';

const props = defineProps<{
    table: String;
}>();
</script>

<template>

    <!-- table1-รายการอัปเดตสถานะคำขอเบิก -->
    <!-- Table1-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table1-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-14 h-[46px] font-bold">ลำดับ</th>
                <th class="py-3 px-2 text-start w-64 font-bold">ชื่อ-นามสกุล</th>
                <th class="py-3 px-2 text-start w-60 font-bold">ชื่อโครงการ</th>
                <th class="py-3 px-5 text-start w-40 font-bold">ประเภทค่าใช้จ่าย</th>
                <th class="py-3 px-2 text-end w-32 font-bold">จำนวนเงิน(บาท)</th>
                <th class="py-3 px-2 text-end w-28 font-bold">วันที่ขอเบิก</th>
                <th class="py-3 px-2 text-end w-28 font-bold">วันที่เบิก</th>
                <th class="py-3 px-2 text-center w-28 font-bold">รายละเอียด</th>
            </tr>
        </thead>
        <!-- Table1-ข้อมูล -->
        <tbody v-if="table === 'Table1-data'">
            <tr class="text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 w-14 h-[46px]">1</th>
                <th class="py-[12px] px-2 w-64 text-start truncate overflow-hidden"
                    style="max-width: 256px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 w-60 text-start truncate overflow-hidden"
                    style="max-width: 240px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระซับมิตรความสัมพันธ์ในองค์กรทีม 4 Elegant">
                    กระซับมิตรความสัมพันธ์ในองค์กรทีม 4 Elegant
                </th>
                <th class="py-[12px] px-5 w-40 text-start">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 w-32 text-end">200.00</th>
                <th class="py-[12px] px-2 w-28 text-end">07/10/2567</th>
                <th class="py-[12px] px-2 w-28 text-end">08/10/2567</th>
                <th class="py-[10px] px-2 w-28 text-center">
                    <span class="flex justify-center">
                        <Icon :icon="'viewDetails'" />
                    </span>
                </th>
            </tr>
        </tbody>
        <!-- Table1-footer -->
        <tfoot v-if="table === 'Table1-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]"></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


    <!-- Table2-รายการรออนุมัติ -->
    <!-- Table2-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table2-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-14 font-bold h-[46px]">ลำดับ</th>
                <th class="py-3 px-2 text-start font-bold">ชื่อ-นามสกุล</th>
                <th class="py-3 px-2 text-start w-40 font-bold">รายการเบิก</th>
                <th class="py-3 px-2 text-start w-40 font-bold">โครงการ</th>
                <th class="py-3 px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                <th class="py-3 px-2 text-start w-40 font-bold">วันที่ขอเบิก</th>
                <th class="py-3 px-2 text-end w-32 font-bold">จำนวนเงิน (บาท)</th>
                <th class="py-3 px-2 text-center w-20 font-bold">จัดการ</th>
            </tr>
        </thead>
        <!-- Table2-ข้อมูล -->
        <tbody v-if="table === 'Table2-data'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 w-14 h-[46px]">1</th>
                <th class="py-[12px] px-2 w-56 text-start truncate overflow-hidden"
                    style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 w-56 text-start truncate overflow-hidden"
                    style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="เบิกค่าใช้จ่าย">
                    เบิกค่าใช้จ่าย
                </th>
                <th class="py-[12px] px-2 w-56 text-start truncate overflow-hidden"
                    style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 w-44 text-start ">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 w-24 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 w-40 text-end ">200.00</th>
                <th class="py-[10px] px-2 w-32 text-center">
                    <span class="flex justify-center">
                        <Icon :icon="'viewDetails'" />
                    </span>
                </th>
            </tr>
        </tbody>
        <!-- Table2-footer -->
        <tfoot v-if="table === 'Table2-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]"></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


    <!-- Table3ผู้ใช้-รายการเบิกค่าใช้จ่าย -->
    <!-- Table3-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table3-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-14 font-bold h-[46px]">ลำดับ</th>
                <th class="py-3 px-2 text-start w-52 font-bold">ชื่อโครงการ</th>
                <th class="py-3 px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                <th class="py-3 px-2 text-end w-20 font-bold">วันที่เบิก</th>
                <th class="py-3 px-2 text-end w-40 font-bold">จำนวนเงิน(บาท)</th>
                <th class="py-3 px-2 text-center w-32 font-bold">สถานะ</th>
                <th class="py-3 px-2 text-center w-24 font-bold">รายละเอียด</th>
            </tr>
        </thead>
        <!-- Table3.1-ข้อมูล -->
        <tbody v-if="table === 'Table3-data'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 w-14 h-[46px]">1</th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 211px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    สัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 w-44 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 w-20 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 w-40 text-end ">200.00</th>
                <th class="py-[12px] px-2 w-32 text-center ">รออนุมัติ</th>
                <th class="py-[10px] px-2 w-24 text-center ">
                    <span class="flex justify-center">
                        <Icon :icon="'viewDetails'" />
                    </span>
                </th>
            </tr>
        </tbody>
        <!-- Table3.1-footer -->
        <tfoot v-if="table === 'Table3-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]"></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


    <!-- Table 4 รายการโครงการ -->
    <!-- Table4-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table4-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] h-[46px]">
                <th class="py-3 px-2 w-14 font-bold">ลำดับ</th>
                <th class="py-3 px-2 w-auto text-start  font-bold">โครงการ</th>
                <th class="py-3 px-2 w-60 text-end font-bold">ยอดเบิกค่าใช้จ่าย (บาท)</th>
            </tr>
        </thead>
        <!-- Table4-ข้อมูล -->
        <tbody v-if="table === 'Table4-data'">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] h-[46px]">
                <th class="py-3 px-2 w-14">1</th>
                <th class="py-3 px-2 w-auto text-start truncate overflow-hidden"
                    style="max-width: 208px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-3 px-2 w-60 text-end font-[100]">1,000,000.00</th>
            </tr>
        </tbody>
        <!-- Table4-footer -->
        <tfoot v-if="table === 'Table4-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]
                "></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


    <!-- Table5 ตั้งค่าผู้ใช้งาน-->
    <!-- Table5-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table5-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-12 font-bold h-[46px]">ลำดับ</th>
                <th class="py-3 px-2 text-center w-24 font-bold">รหัสพนักงาน</th>
                <th class="py-3 px-2 text-start w-52 font-bold">ชื่อ-นามสกุล</th>
                <th class="py-3 px-2 text-start w-20 font-bold">แผนก</th>
                <th class="py-3 px-2 text-start w-24 font-bold">ฝ่าย</th>
                <th class="py-3 px-2 text-start w-20 font-bold">บทบาท</th>
                <th class="py-3 px-2 text-start w-24 font-bold">สถานะ</th>
                <th class="py-3 px-2 text-center w-24 font-bold">ดูรายงาน</th>
                <th class="py-3 px-2 text-center w-24 font-bold">จัดการ</th>
            </tr>
        </thead>
        <!-- Table5-ข้อมูล -->
        <tbody v-if="table === 'Table5-data'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 w-12 h-[46px]">1</th>
                <th class="py-[12px] px-2 w-24">651122</th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 208px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 w-20 text-start font-[100]">ไอที</th>
                <th class="py-[12px] px-2 w-24 text-start ">การตลาด</th>
                <th class="py-[12px] px-2 w-20 text-start ">ผู้ใช้งาน</th>
                <th class="py-[12px] px-2 w-24 text-start ">อยู่ในระบบ</th>
                <th class="w-24">
                    <span class="flex justify-center">
                        <div class="w-4 h-4 border-2 border-[#BBBBBB] rounded"></div>
                    </span>
                </th>
                <th class="py-[10px] px-2 w-24 text-center ">
                    <span class="flex justify-center">
                        <Icon :icon="'viewDetails'" />
                    </span>
                </th>
            </tr>
        </tbody>
        <!-- Table5-footer -->
        <tfoot v-if="table === 'Table5-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]
                "></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


    <!-- Table 6 ดูแลระบบ-จัดการลำดับการอนุมัติ-->
    <!-- Table6-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table6-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-12 font-bold h-[46px]">ลำดับ</th>
                <th class="py-3 px-2 text-center w-28 font-bold">รหัสพนักงาน</th>
                <th class="py-3 px-2 text-start w-52 font-bold">ชื่อ-นามสกุล</th>
                <th class="py-3 px-2 text-start w-28 font-bold">ฝ่าย</th>
                <th class="py-3 px-2 text-start w-28 font-bold">บทบาท</th>
                <th class="py-3 px-2 text-center w-32 font-bold">ลำดับ</th>
                <th class="py-3 px-2 text-center w-20 font-bold">จัดการ</th>
            </tr>
        </thead>
        <!-- Table6-ข้อมูล -->
        <tbody v-if="table === 'Table6-data'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 w-12 h-[46px]">1</th>
                <th class="py-[12px] px-2 w-28">651122</th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 208px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 w-28 text-start ">การตลาด</th>
                <th class="py-[12px] px-2 w-28 text-start ">ผู้ใช้งาน</th>
                <th class="py-[12px] px-2 w-32 text-center ">box</th>
                <th class="py-[10px] px-2 w-20 text-center ">
                    <span class="flex justify-center">
                        <Icon :icon="'viewDetails'" />
                    </span>
                </th>
            </tr>
        </tbody>
        <!-- Table6-footer -->
        <tfoot v-if="table === 'Table6-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]
                "></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


    <!-- Table7 รายการรอนำจ่าบ และ รายงานเบิกค่าใช้จ่าย -->
    <!-- Table7-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table7-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-14 font-bold h-[46px]">ลำดับ</th>
                <th class="py-3 px-2 text-start w-56 font-bold">ชื่อ-นามสกุล</th>
                <th class="py-3 px-2 text-start w-56 font-bold">รายการเบิก</th>
                <th class="py-3 px-2 text-start w-56 font-bold">โครงการ</th>
                <th class="py-3 px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                <th class="py-3 px-2 text-start w-24 font-bold">วันที่ขอเบิก</th>
                <th class="py-3 px-2 text-end w-40 font-bold">จำนวนเงิน (บาท)</th>
                <th class="py-3 px-2 text-center w-32 font-bold">จัดการ</th>
            </tr>
        </thead>
        <!-- Table7-ข้อมูล -->
        <tbody v-if="table === 'Table7-data'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 w-14 h-[46px]">1</th>
                <th class="py-[12px] px-2 w-56 text-start truncate overflow-hidden"
                    style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 w-56 text-start truncate overflow-hidden"
                    style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="เบิกค่าใช้จ่าย">
                    เบิกค่าใช้จ่าย
                </th>
                <th class="py-[12px] px-2 w-56 text-start truncate overflow-hidden"
                    style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 w-44 text-start ">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 w-24 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 w-40 text-end ">200.00</th>
                <th class="py-[10px] px-2 w-32 text-center ">
                    <span class="flex justify-center">
                        <Icon :icon="'viewDetails'" />
                    </span>
                </th>
            </tr>
        </tbody>
        <!-- Table7-footer -->
        <tfoot v-if="table === 'Table7-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]
                "></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


    <!-- Table 8 ประวัติการนำจ่าย -->
    <!-- Table8-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table8-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-14 font-bold h-[46px]">ลำดับ</th>
                <th class="py-3 px-2 text-start font-bold">ชื่อ-นามสกุล</th>
                <th class="py-3 px-2 text-start w-40 font-bold">รายการเบิก</th>
                <th class="py-3 px-2 text-start w-40 font-bold">โครงการ</th>
                <th class="py-3 px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                <th class="py-3 px-2 text-start w-40 font-bold">วันที่ขอเบิก</th>
                <th class="py-3 px-2 text-start w-32 font-bold">สถานะ</th>
                <th class="py-3 px-2 text-center w-20 font-bold">จัดการ</th>
            </tr>
        </thead>
        <!-- Table8-ข้อมูล -->
        <tbody v-if="table === 'Table8-data'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 w-14 h-[46px]">1</th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="เบิกค่าใช้จ่าย">
                    เบิกค่าใช้จ่าย
                </th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 w-32 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-5 w-32 text-end ">200.00</th>
                <th class="py-[12px] px-2 w-28 text-center text-green-500">อนุมัติแล้ว</th>
                <th class="py-[10px] px-2 w-20 text-center ">
                    <span class="flex justify-center">
                        <Icon :icon="'viewDetails'" />
                    </span>
                </th>
            </tr>
        </tbody>
        <!-- Table8-footer -->
        <tfoot v-if="table === 'Table8-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]
                "></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


    <!-- Table 9 ประวัติการนำจ่าย -->
    <!-- Table9-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table9-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-14 font-bold">ลำดับ</th>
                <th class="py-3 px-2 text-start w-48 font-bold">ชื่อ-นามสกุล</th>
                <th class="py-3 px-2 text-start w-48 font-bold">รายการเบิก</th>
                <th class="py-3 px-2 text-start w-48 font-bold">โครงการ</th>
                <th class="py-3 px-5 text-start w-32 font-bold">ประเภทค่าใช้จ่าย</th>
                <th class="py-3 px-2 text-end w-20 font-bold">วันที่เบิก</th>
                <th class="py-3 px-5 text-end w-32 font-bold">จำนวนเงิน(บาท)</th>
                <th class="py-3 px-2 text-center w-20 font-bold">จัดการ</th>
            </tr>
        </thead>
        <!-- Table 9 new รายการเบิก และ ประวัติการเบิก -->
        <thead v-if="table === 'Table9-head-New'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-14 font-bold">ลำดับ</th>
                <th class="py-3 px-2 text-start w-48 font-bold">รายการเบิก</th>
                <th class="py-3 px-2 text-start w-48 font-bold">โครงการ</th>
                <th class="py-3 px-5 text-start w-32 font-bold">ประเภทค่าใช้จ่าย</th>
                <th class="py-3 px-2 text-start w-20 font-bold">วันที่ขอเบิก</th>
                <th class="py-3 px-5 text-end w-32 font-bold">จำนวนเงิน (บาท)</th>
                <th class="py-3 px-2 text-start w-20 font-bold">สถานะ</th>
                <th class="py-3 px-2 text-center w-20 font-bold">จัดการ</th>
            </tr>
        </thead>

        <!-- Table9-ข้อมูล -->
        <tbody v-if="table === 'Table9-data'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 w-14">1</th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="เบิกค่าใช้จ่าย">
                    เบิกค่าใช้จ่าย
                </th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 w-32 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 w-20 text-end ">08/10/2567</th>
                <th class="py-[12px] px-5 w-32 text-end ">200.00</th>
                <th class="py-[10px] px-2 w-20 text-center ">
                    <span class="flex justify-center">
                        <Icon :icon="'viewDetails'" />
                    </span>
                </th>
            </tr>
        </tbody>
        <!-- Table9-footer -->
        <tfoot v-if="table === 'Table9-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]
                "></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


    <!-- Table 10 เพิ่มมีสิทธิอนุมัติ -->
    <!-- Table10-หัวข้อ -->
    <table class="table-auto w-full text-center text-black">
        <thead v-if="table === 'Table10-head'" class="bg-[#F2F4F8]">
            <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                <th class="py-3 px-2 w-10 font-bold"></th>
                <th class="py-3 px-2 w-14 font-bold">ลำดับ</th>
                <th class="py-3 px-2 text-cener w-32 font-bold">รหัสพนักงาน</th>
                <th class="py-3 px-2 text-start w-52 font-bold">ชื่อ-นามสกุล</th>
                <th class="py-3 px-5 text-start w-36 font-bold">แผนก</th>
                <th class="py-3 px-2 text-start w-40 font-bold">ฝ่าย</th>
                <th class="py-3 px-5 text-start w-32 font-bold">บทบาท</th>
                <th class="py-3 px-2 text-start w-28 font-bold">สถานะ</th>
                <th class="py-3 px-2 text-center w-24 font-bold">จัดการ</th>
            </tr>
        </thead>
        <!-- Table10-ข้อมูล -->
        <tbody v-if="table === 'Table10-data'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="w-10">
                    <span class="flex justify-center">
                        <div class="w-4 h-4 border-2 border-[#BBBBBB] rounded"></div>
                    </span>
                </th>
                <th class="py-[12px] px-2 w-14">1</th>
                <th class="py-[12px] px-2 w-32 text-center">651122</th>
                <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                    style="max-width: 208px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-5 w-36 text-start ">การตลาด</th>
                <th class="py-[12px] px-2 w-40 text-start ">การตลาด</th>
                <th class="py-[12px] px-5 w-32 text-start ">ผู้ใช้งาน</th>
                <th class="py-[12px] px-2 w-28 text-start">อยู่ในระบบ</th>
                <th class="py-[10px] px-2 w-24 text-center ">
                    <span class="flex justify-center">
                        <Icon :icon="'viewDetails'" />
                    </span>
                </th>
            </tr>
        </tbody>
        <!-- Table10-footer -->
        <tfoot v-if="table === 'Table10-footer'">
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="w-5/6 h-[46px]
                "></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-6 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </tfoot>
    </table>


</template>


<style>
th {
    @apply font-[100]
}


@tailwind components;
@tailwind utilities;
</style>
