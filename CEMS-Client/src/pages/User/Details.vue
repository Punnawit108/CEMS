<script setup lang="ts">
import Progress from '../../components/template/Progress.vue';

</script>
<!-- path for test = /disbursement/listWithdraw/detailsExpenseForm/:id -->
<!-- path for test = /disbursement/historyWithdraw/detail/:id -->
<!-- path for test = /approval/history/detail/:id -->
<!-- path for test = /payment/List/detail/:id -->
<!-- path for test = /payment/history/detail/:id -->

<template>
    <!-- content -->
    <div class="ml-[16px] flex">
        <div class="left w-[85%]">
            <h3 class="text-base font-bold text-black">รายละเอียดคำขอเบิก</h3>
            <div class="row">
                <p>โครงการ</p>
                <p>อบรมการบริหาร</p>
            </div>
            <div class="row flex justify-around">
                <div class="col">
                    <p>ชื่อผู้เบิก</p>
                    <p>นางสาวอลิสา ปะกังพลัง</p>
                </div>
                <div class="col">
                    <p>ชื่อผู้เบิกแทน</p>
                    <p>นายปุณณวิชน์ วิเชียร์มาร์น</p>
                </div>
            </div>
            <div class="row flex justify-around">
                <div class="col">
                    <p>ประเภทค่าใช้จ่าย</p>
                    <p>ค่าเดินทาง</p>
                </div>
                <div class="col">
                    <p>วันที่ขอเบิก</p>
                    <p>11/09/2567</p>
                </div>
                <div class="col">
                    <p>วันที่อนุมัติ</p>
                    <p>13/09/2567</p>
                </div>
                <div class="col">
                    <p>จำนวนเงิน(บาท)</p>
                    <p>315.00</p>
                </div>
            </div>
            <div class="travel row flex">
                <div class="col">
                    <p>ประเภทการเดินทาง</p>
                    <p>รถสาธารณะ</p>
                </div>
                <div class="col">
                    <p>ประเภทรถ</p>
                    <p>รถไฟฟ้า</p>
                </div>
                <div class="col">
                    <p>ระยะทาง</p>
                    <p>40 กิโลเมตร</p>
                </div>
                <div class="col">
                    <p>อัตราค่าเดินทาง</p>
                    <p>7 บาท/กิโลเมตร</p>
                </div>
            </div>
            <div class="row">
                <p>สถานที่เริ่มต้น</p>
                <p>Clicknext กรุงเทพฯ</p>
            </div>
            <div class="row">
                <p>วัตถุประสงค์</p>
                <p>เพื่อติดต่อสหกิจศึกษา</p>
            </div>
            <div class="row flex">
                <div class="flex-1">
                    <h3 class="mb-[16px] text-base font-bold text-black">รูปหลักฐาน</h3>
                    <img src="/evidence.PNG" alt="" class="w-[50%] h-auto cursor-pointer">
                </div>
                <div class="flex-1">
                </div>
            </div>
        </div>
        <div class="right">
            <Progress />
        </div>
    </div>
    <!-- content -->
</template>
<style scoped>

p{
    font-size: 14px;
    color:black;
    
}


.row {
    margin: 16px 0;
}

.col {
    flex: 1;
}
</style>