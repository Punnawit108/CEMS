<script setup lang="ts">

</script>

<template>
    <!-- table1-รายการอัปเดตสถานะคำขอเบิก -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 1 รายการอัปเดตสถานะคำขอเบิก</h1>
    <div class="w-full h-fit border-[16px] flex items-start">

        <table class="table-auto w-screen text-center text-black">
            <!-- แถบหัวข้อ -->
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-start w-64 font-bold">ชื่อ-นามสกุล</th>
                    <th class="py-[11px] px-2 text-start w-60 font-bold">ชื่อโครงการ</th>
                    <th class="py-[11px] px-5 text-start w-40 font-bold">ประเภทค่าใช้จ่าย</th>
                    <th class="py-[11px] px-2 text-end w-32 font-bold">จำนวนเงิน(บาท)</th>
                    <th class="py-[11px] px-2 text-end w-28 font-bold">วันที่ขอเบิก</th>
                    <th class="py-[11px] px-2 text-end w-28 font-bold">วันที่เบิก</th>
                    <th class="py-[11px] px-2 text-center w-28 font-bold">รายละเอียด</th>
                </tr>
            </thead>
            <!-- ข้อมูลชุดที่ 1 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 w-14">1</th>
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
                <th class="py-[12px] px-5 text-start w-40">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end w-28">200.00</th>
                <th class="py-[12px] px-2 text-end w-28">07/10/2567</th>
                <th class="py-[12px] px-2 text-end w-28">08/10/2567</th>
                <th class="py-[12px] px-2 text-center w-28">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2 "></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
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
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] pl-8">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-4 text-[#A0A0A0]">
                        < </span><span class="mr-7"> > </span>
                </th>
            </tr>
        </table>

    </div>


    <!-- table2-รายการรออนุมัติ -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 2 รายการรออนุมัติ</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-start w-56 font-bold">ชื่อ-นามสกุล</th>
                    <th class="py-[11px] px-2 text-start w-56 font-bold">ชื่อโครงการ</th>
                    <th class="py-[11px] px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                    <th class="py-[11px] px-2 text-end w-24 font-bold">วันที่เบิก</th>
                    <th class="py-[11px] px-2 text-end w-40 font-bold">จำนวนเงิน(บาท)</th>
                    <th class="py-[11px] px-2 text-center w-32 font-bold">รายละเอียด</th>
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
                <th class="py-[12px] px-5 text-start ">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 text-end ">200.00</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-14 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>


    <!-- Table3.1ผู้ใช้-รายการเบิกค่าใช้จ่าย -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 3.1 ผู้ใช้-รายการเบิกค่าใช้จ่าย</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อโครงการ</th>
                    <th class="py-[11px] px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                    <th class="py-[11px] px-2 text-end w-20 font-bold">วันที่เบิก</th>
                    <th class="py-[11px] px-2 text-end w-40 font-bold">จำนวนเงิน(บาท)</th>
                    <th class="py-[11px] px-2 text-center w-32 font-bold">สถานะ</th>
                    <th class="py-[11px] px-2 text-center w-24 font-bold">รายละเอียด</th>
                </tr>
            </thead>
            <!-- ข้อมูลชุดที่ 1 สถานะรออนุมัติ-->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    สัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 text-end ">200.00</th>
                <th class="py-[12px] px-2 text-center ">รออนุมัติ</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลชุดที่ 2 สถานะแบบร่าง-->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    สัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 text-end ">200.00</th>
                <th class="py-[12px] px-2 text-center ">แบบร่าง</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลชุดที่ 3 สถานะแก้ไข-->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    สัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 text-end ">200.00</th>
                <th class="py-[12px] px-2 text-center ">แก้ไข</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end pr-7">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-10 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>


    <!-- Table3.2ผู้ใช้-ประวัติเบิกค่าใช้จ่าย -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 3.2 ผู้ใช้-ประวัติเบิกค่าใช้จ่าย</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อโครงการ</th>
                    <th class="py-[11px] px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                    <th class="py-[11px] px-2 text-end w-20 font-bold">วันที่เบิก</th>
                    <th class="py-[11px] px-2 text-end w-40 font-bold">จำนวนเงิน(บาท)</th>
                    <th class="py-[11px] px-2 text-center w-32 font-bold">สถานะ</th>
                    <th class="py-[11px] px-2 text-center w-24 font-bold">รายละเอียด</th>
                </tr>
            </thead>
            <!-- ข้อมูลชุดที่ 1 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 text-end ">200.00</th>
                <th class="py-[12px] px-2 text-center ">อนุมัติแล้ว</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end pr-7">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-10 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>


    <!-- Table 4 รายการโครงการ -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 4 รายการโครงการ</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อโครงการ</th>
                    <th class="py-[11px] px-2 text-end w-28 font-bold">งบประมาณ</th>
                    <th class="py-[11px] px-2 text-end w-28 font-bold">ยอดเบิกจ่าย</th>
                    <th class="py-[11px] px-5 text-end w-36 font-bold">วันที่จัดโครงการ</th>
                    <th class="py-[11px] px-2 text-end w-24 font-bold">วันที่สิ้นสุด</th>
                    <th class="py-[11px] px-2 text-center w-16 font-bold">รายละเอียด</th>
                </tr>
            </thead>
            <!-- ข้อมูลชุดที่ 1 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-2 text-end font-[100]">1,000,000.00</th>
                <th class="py-[12px] px-2 text-end ">600,000.00</th>
                <th class="py-[12px] px-5 text-end ">08/09/2567</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end pr-7">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-10 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>


    <!-- Table5ดูแลระบบ-จัดการผู้ใช้งาน -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 5 ดูแลระบบ-จัดการผู้ใช้งาน</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-12 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-center w-24 font-bold">รหัสพนักงาน</th>
                    <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อ-นามสกุล</th>
                    <th class="py-[11px] px-2 text-start w-20 font-bold">แผนก</th>
                    <th class="py-[11px] px-2 text-start w-24 font-bold">ฝ่าย</th>
                    <th class="py-[11px] px-2 text-start w-20 font-bold">บทบาท</th>
                    <th class="py-[11px] px-2 text-start w-24 font-bold">สถานะ</th>
                    <th class="py-[11px] px-2 text-center w-24 font-bold">ดูรายงาน</th>
                    <th class="py-[11px] px-2 text-center w-24 font-bold">จัดการ</th>
                </tr>
            </thead>
            <!-- ข้อมูลชุดที่ 1 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 ">651122</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 text-start font-[100]">ไอที</th>
                <th class="py-[12px] px-2 text-start ">การตลาด</th>
                <th class="py-[12px] px-2 text-start ">ผู้ใช้งาน</th>
                <th class="py-[12px] px-2 text-start ">อยู่ในระบบ</th>
                <th class="py-[12px] px-2 text-center ">box</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end pr-7">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-10 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>


    <!-- Table 6 ดูแลระบบ-จัดการลำดับการอนุมัติ -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 6 ดูแลระบบ-จัดการลำดับการอนุมัติ </h1>
    <div class="w-full h-fit border-[16px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-12 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-center w-28 font-bold">รหัสพนักงาน</th>
                    <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อ-นามสกุล</th>
                    <th class="py-[11px] px-2 text-start w-28 font-bold">ฝ่าย</th>
                    <th class="py-[11px] px-2 text-start w-28 font-bold">บทบาท</th>
                    <th class="py-[11px] px-2 text-center w-32 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-center w-20 font-bold">จัดการ</th>
                </tr>
            </thead>
            <!-- ข้อมูลชุดที่ 1 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 ">651122</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-2 text-start ">การตลาด</th>
                <th class="py-[12px] px-2 text-start ">ผู้ใช้งาน</th>
                <th class="py-[12px] px-2 text-center ">box</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end pr-7">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-10 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>


    <!-- Table7.1 นักบัญชี-ประวัติอัปเดตสถานะคำขอเบิก -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 7.1 นักบัญชี-ประวัติอัปเดตสถานะคำขอเบิก</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-start w-56 font-bold">ชื่อ-นามสกุล</th>
                    <th class="py-[11px] px-2 text-start w-56 font-bold">ชื่อโครงการ</th>
                    <th class="py-[11px] px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                    <th class="py-[11px] px-2 text-end w-24 font-bold">วันที่เบิก</th>
                    <th class="py-[11px] px-2 text-end w-40 font-bold">จำนวนเงิน(บาท)</th>
                    <th class="py-[11px] px-2 text-center w-32 font-bold">รายละเอียด</th>
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
                <th class="py-[12px] px-5 text-start ">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 text-end ">200.00</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-14 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>


    <!-- Table7.2 นักมีสิทธิ-ประวัติคำขอเบิก -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 7.2 นักมีสิทธิ-ประวัติคำขอเบิก</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-start w-56 font-bold">ชื่อ-นามสกุล</th>
                    <th class="py-[11px] px-2 text-start w-56 font-bold">ชื่อโครงการ</th>
                    <th class="py-[11px] px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                    <th class="py-[11px] px-2 text-end w-24 font-bold">วันที่เบิก</th>
                    <th class="py-[11px] px-2 text-end w-40 font-bold">จำนวนเงิน(บาท)</th>
                    <th class="py-[11px] px-2 text-center w-32 font-bold">รายละเอียด</th>
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
                <th class="py-[12px] px-5 text-start ">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 text-end ">200.00</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-14 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>


<!-- Table 8 ผู้มีสิทธิอนุมัติ-ประวัติอนุมัติคำขอเบิก -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 8 ผู้มีสิทธิอนุมัติ-ประวัติอนุมัติคำขอเบิก</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
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
                <th class="py-[12px] px-2 text-center text-green-500">อนุมัติแล้ว</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
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
                <th class="py-[12px] px-2 text-center text-red-500">ไม่อนุมัติ</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
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
            <!-- ข้อมูลว่างอันล่างสุด -->
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
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
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


<!-- Table 9 ประวัติเบิกจ่าย -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 9 ประวัติเบิกจ่าย</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
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
                <th class="py-[12px] px-2 text-center text-green-500">นำจ่ายแล้ว</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
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
                <th class="py-[12px] px-2 text-center text-yellow-500">รอนำจ่าย</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
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
            <!-- ข้อมูลว่างอันล่างสุด -->
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
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
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


    <!-- Table 10 เพิ่มมีสิทธิอนุมัติ -->
    <h1 class="text-center text-[25px] text-rose-500"># Table 10 เพิ่มมีสิทธิอนุมัติ</h1>
    <div class="w-full h-fit border-[16px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-10 font-bold"></th>
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-cener w-32 font-bold">รหัสพนักงาน</th>
                    <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อ-นามสกุล</th>
                    <th class="py-[11px] px-5 text-start w-36 font-bold">แผนก</th>
                    <th class="py-[11px] px-2 text-start w-40 font-bold">ฝ่าย</th>
                    <th class="py-[11px] px-5 text-start w-32 font-bold">บทบาท</th>
                    <th class="py-[11px] px-2 text-start w-28 font-bold">สถานะ</th>
                    <th class="py-[11px] px-2 text-center w-24 font-bold">จัดการ</th>
                </tr>
            </thead>
            <!-- ข้อมูลชุดที่ 1 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th>
                    <span class="flex justify-center">
                        <div class="w-4 h-4 border-2 border-[#BBBBBB] rounded"></div>
                    </span>
                </th>
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-center">651122</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายเทียนชัย คูเมือง">
                    นายเทียนชัย คูเมือง
                </th>
                <th class="py-[12px] px-5 text-start ">การตลาด</th>
                <th class="py-[12px] px-2 text-start ">การตลาด</th>
                <th class="py-[12px] px-5 text-start ">ผู้ใช้งาน</th>
                <th class="py-[12px] px-2 text-start">อยู่ในระบบ</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลชุดที่ 2 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th>
                    <span class="flex justify-center">
                        <div class="w-4 h-4 border-2 border-[#BBBBBB] rounded"></div>
                    </span>
                </th>
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-center">651653</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="นายศตวรรษ ไตรธิเลน">
                    นายศตวรรษ ไตรธิเลน
                </th>
                <th class="py-[12px] px-5 text-start ">ไอที</th>
                <th class="py-[12px] px-2 text-start ">การตลาด</th>
                <th class="py-[12px] px-5 text-start ">ผู้ใช้งาน</th>
                <th class="py-[12px] px-2 text-start">อยู่ในระบบ</th>
                <th class="py-[12px] px-2 text-center ">icon</th>
            </tr>
            <!-- ข้อมูลว่าง -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่างอันล่างสุด -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th></th>
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
</template>

<style>
th {
    @apply font-[100]
}


@tailwind components;
@tailwind utilities;
</style>
