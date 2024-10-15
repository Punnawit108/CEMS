<script setup lang="ts">
import Button from './Button.vue';

</script>

<template>
    <div class="grid grid-cols-5 m-5 gap-5">
        <div>
            <!-- เพิ่มโครงการ -->
            <Button :type="'btn-addProject'"></Button>
        </div>
        <div>
            <!-- เชิญผู้ใช่ -->
            <Button :type="'btn-inviteUser'"></Button>
        </div>
        <div>
            <!-- อนุมัติ -->
            <Button :type="'btn-approve'"></Button>
        </div>
        <div>
            <!-- อนุมัติ (1) -->
            <Button :type="'btn-approve(1)'"></Button>
        </div>
        <div>
            <!-- ยกเลิกสีกรอบเทา  -->
            <Button :type="'btn-cancleBorderGray'"></Button>
        </div>
        <div>
            <!-- ยกเลิกสีเทา -->
            <Button :type="'btn-cancleGray'"></Button>
        </div>
        <div>
            <!-- ยกเลิกสีแดง -->
            <Button :type="'btn-cancleRed'"></Button>
        </div>
        <div>
            <!-- ล้างคำค้นหา -->
            <Button :type="'btn-clear'"></Button>
        </div>
        <div>
            <!-- ยืนยัน -->
            <Button :type="'btn-summit'"></Button>
        </div>
        <div>
            <!-- แก้ไขโครงการ -->
            <Button :type="'btn-editProject'"></Button>
        </div>
        <div>
            <!-- แก้ไขคำขอเบิก -->
            <Button :type="'btn-editRequest'"></Button>
        </div>
        <div>
            <!-- ส่งแก้ไข -->
            <Button :type="'btn-editSend'"></Button>
        </div>
        <div>
            <!-- แก้ไขผู้ใช้ -->
            <Button :type="'btn-editUser'"></Button>
        </div>
        <div>
            <!-- สร้างใบเบิกค่าใช้จ่าย -->
            <Button :type="'btn-expense'"></Button>
        </div>
        <div>
            <!-- สร้างประเภทค่าใช้จ่าย -->
            <Button :type="'btn-expenseTypeGray'"></Button>
        </div>
        <div>
            <!-- นำจ่าย -->
            <Button :type="'btn-payment1'"></Button>
        </div>

        <div>
            <!-- นำจ่าย (1) -->
            <Button :type="'btn-payment2'"></Button>
        </div>

        <div>
            <!-- พิมพ์1 -->
            <Button :type="'btn-print1'"></Button>
        </div>

        <div>
            <!-- พิมพ์2 -->
            <Button :type="'btn-print2'"></Button>
        </div>

        <div>
            <!-- ประเภทรถส่วนตัว  -->
            <Button :type="'btn-private'"></Button>
        </div>

        <div>
            <!-- ประเภทรถสาธารณะ1  -->
            <Button :type="'btn-public1'"></Button>
        </div>

        <div>
            <!-- ประเภทรถสาธารณะ2  -->
            <Button :type="'btn-public2'"></Button>
        </div>

        <div>
            <!-- บันทึก  -->
            <Button :type="'btn-save'"></Button>
        </div>

        <div>
            <!-- ค้นหา  -->
            <Button :type="'btn-search'"></Button>
        </div>

        <div>
            <!-- ประเภทค่าเดินทาง  -->
            <Button :type="'btn-transport'"></Button>
        </div>

        <div>
            <!-- ไม่อนุมัติ  -->
            <Button :type="'btn-unapprove'"></Button>
        </div>

        <div>
            <!-- แก้ไขแบบย่อ  -->
            <Button :type="'btn-edit2'"></Button>
        </div>
    </div>




</template>