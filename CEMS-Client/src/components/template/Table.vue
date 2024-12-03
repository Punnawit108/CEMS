<script setup lang="ts">
/*
* ชื่อไฟล์: Table.vue
* คำอธิบาย: ไฟล์นี้เป็น Component Table
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/
import { defineProps } from 'vue';
import Ctable from './CTable.vue';
</script>
<!-- ### -->
<!-- วิธีเรียกใช้ตาราง -->
<!-- 1. ตารางนึงจะมีอยู่ 3 ส่วน 1)หัวข้อ(head)  2)ข้อมูล(data)  3)footer-->
<!-- 2. ชื่อตารางมาจาก Standard ใน figma ชื่อตารางเดียวกันเลย -->
<!-- 3. ดูชื่อตารางของตนเองเเล้วมาเลือกใช้ -->
<!-- เช่น  ได้ตารางที่ 3  เราก็ต้องใช้ตารางที่ 3-->
<!-- ส่วนหัวข้อ <Ctable :table="'Table3-head'" /> -->
<!-- ส่วนข้อมูล <Ctable :table="'Table3-data'" /> -->
<!-- ส่วน footer <Ctable :table="'Table3-footer'" /> -->
<!-- ###หมายเหตุ -->
<!-- footerโค้ดเหมือนกันทุกอัน เรียกใช้อันไหนก็ได้ -->
<template>
    <!-- Table1-รายการอัปเดตสถานะคำขอเบิก -->
    <h1 class="text-center text-[25px] text-rose-500"># table1-รายการอัปเดตสถานะคำขอเบิก</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table1-head'" /> <!-- Table1-หัวข้อ -->
        <Ctable :table="'Table1-data'" /> <!-- Table1-ข้อมูล -->
        <Ctable :table="'Table1-footer'" /> <!-- Table1-footer -->
    </div>


    <!-- Table2-รายการรออนุมัติ -->
    <h1 class="text-center text-[25px] text-rose-500"># Table2-รายการรออนุมัติ</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table2-head'" /> <!-- Table2-หัวข้อ -->
        <Ctable :table="'Table2-data'" /> <!-- Table2-ข้อมูล -->
        <Ctable :table="'Table2-footer'" /> <!-- Table2-footer -->
    </div>


    <!-- Table3.1 และ 3.2 ใช้ตารางเดียวกันต่างกันแค่สถานะ -->
    <h1 class="text-center text-[25px] text-rose-500"># Table3.1 และ 3.2 ใช้ตารางเดียวกันต่างกันแค่สถานะ</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table3-head'" /> <!-- Table3-หัวข้อ -->
        <Ctable :table="'Table3-data'" /> <!-- Table3-ข้อมูล -->
        <Ctable :table="'Table3-footer'" /> <!-- Table3-footer -->
    </div>


    <!-- Table 4 รายการโครงการ -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 4 รายการโครงการ</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table4-head'" /> <!-- Table4-หัวข้อ -->
        <Ctable :table="'Table4-data'" /> <!-- Table4-ข้อมูล -->
        <Ctable :table="'Table4-footer'" /> <!-- Table4-footer -->
    </div>


    <!-- Table5ดูแลระบบ-จัดการผู้ใช้งาน -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 5 ดูแลระบบ-จัดการผู้ใช้งาน</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table5-head'" /> <!-- Table5-หัวข้อ -->
        <Ctable :table="'Table5-data'" /> <!-- Table5-ข้อมูล -->
        <Ctable :table="'Table5-footer'" /> <!-- Table5-footer -->
    </div>


    <!-- Table 6 ดูแลระบบ-จัดการลำดับการอนุมัติ -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 6 ดูแลระบบ-จัดการลำดับการอนุมัติ</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table6-head'" /> <!-- Table6-หัวข้อ -->
        <Ctable :table="'Table6-data'" /> <!-- Table6-ข้อมูล -->
        <Ctable :table="'Table6-footer'" /> <!-- Table6-footer -->
    </div>


    <!-- Table7 = 7.1 and 7.2 -->
    <h1 class="text-center text-[25px] text-rose-500"># Table7 = 7.1 and 7.2</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table7-head'" /> <!-- Table7-หัวข้อ -->
        <Ctable :table="'Table7-data'" /> <!-- Table7-ข้อมูล -->
        <Ctable :table="'Table7-footer'" /> <!-- Table7-footer -->
    </div>


    <!-- Table 8 ผู้มีสิทธิอนุมัติ-ประวัติอนุมัติคำขอเบิก -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 8 ผู้มีสิทธิอนุมัติ-ประวัติอนุมัติคำขอเบิก</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table8-head'" /> <!-- Table8-หัวข้อ -->
        <Ctable :table="'Table8-data'" /> <!-- Table8-ข้อมูล -->
        <Ctable :table="'Table8-footer'" /> <!-- Table8-footer -->
    </div>


    <!-- Table 9 ประวัติเบิกจ่าย -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 9 ประวัติเบิกจ่าย</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table9-head'" /> <!-- Table9-หัวข้อ -->
        <Ctable :table="'Table9-data'" /> <!-- Table9-ข้อมูล -->
        <Ctable :table="'Table9-footer'" /> <!-- Table9-footer -->
    </div>


    <!-- Table 10 เพิ่มมีสิทธิอนุมัติ -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 10 เพิ่มมีสิทธิอนุมัติ</h1>
    <div class="w-full h-fit border-[16px] items-start">
        <Ctable :table="'Table10-head'" /> <!-- Table10-หัวข้อ -->
        <Ctable :table="'Table10-data'" /> <!-- Table10-ข้อมูล -->
        <Ctable :table="'Table10-footer'" /> <!-- Table10-footer -->
    </div>

</template>

<style>
th {
    @apply font-[100]
}


@tailwind components;
@tailwind utilities;
</style>
