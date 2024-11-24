<script setup lang="ts">
/**
 * ชื่อไฟล์ : ForUseButton.vue
 * คำอธิบาย : ไฟล์นี้แสดงการใช้ปุ่ม
 * Input : -
 * Output : แสดงปุ่ม
 * ชื่อผู้เขียน / แก้ไข : นครียา วัฒนศรี และพงศธร บุญญามา
 * วันที่จัดทำ / วันที่แก้ไข : 13 พฤศจิกายน 2567
 */
import Button from './Button.vue';

</script>

<template>
    <div class="grid grid-cols-5 gap-5 m-5 text-Sarabun-Reqular12 ">
        <div>
            <!-- เพิ่มโครงการ -->
            <Button :type="'btn-addProject'"></Button>
            <p>btn-addProject</p>
        </div>
        <div>
            <!-- เชิญผู้ใช่ -->
            <Button :type="'btn-inviteUser'"></Button>
            <p>btn-inviteUser</p>
        </div>
        <div>
            <!-- อนุมัติ -->
            <Button :type="'btn-approve'"></Button>
            <p>btn-approve</p>
        </div>
        <div>
            <!-- อนุมัติ (1) -->
            <Button :type="'btn-approve(1)'"></Button>
            <p>btn-approve(1)</p>
        </div>
        <div>
            <!-- ยกเลิกสีกรอบเทา  -->
            <Button :type="'btn-cancleBorderGray'"></Button>
            <p>btn-cancleBorderGray</p>
        </div>
        <div>
            <!-- ยกเลิกสีเทา -->
            <Button :type="'btn-cancleGray'"></Button>
            <p>btn-cancleGray</p>
        </div>
        <div>
            <!-- ยกเลิกสีแดง -->
            <Button :type="'btn-cancleRed'"></Button>
            <p>btn-cancleRed</p>
        </div>
        <div>
            <!-- ล้างคำค้นหา -->
            <Button :type="'btn-clear'"></Button>
            <p>btn-clear</p>
        </div>
        <div>
            <!-- ยืนยัน -->
            <Button :type="'btn-summit'"></Button>
            <p>btn-summit</p>
        </div>
        <div>
            <!-- แก้ไขโครงการ -->
            <Button :type="'btn-editProject'"></Button>
            <p>btn-editProject</p>
        </div>
        <div>
            <!-- แก้ไขคำขอเบิก -->
            <Button :type="'btn-editRequest'"></Button>
            <p>btn-editRequest</p>
        </div>
        <div>
            <!-- ส่งแก้ไข -->
            <Button :type="'btn-editSend'"></Button>
            <p>btn-editSend</p>
        </div>
        <div>
            <!-- แก้ไขผู้ใช้ -->
            <Button :type="'btn-editUser'"></Button>
            <p>btn-editUser</p>
        </div>
        <div>
            <!-- สร้างใบเบิกค่าใช้จ่าย -->
            <Button :type="'btn-expense'"></Button>
            <p>btn-expense</p>
        </div>
        <div>
            <!-- สร้างประเภทค่าใช้จ่าย -->
            <Button :type="'btn-expenseTypeGray'"></Button>
            <p>btn-expenseTypeGray</p>
        </div>
        <div>
            <!-- นำจ่าย -->
            <Button :type="'btn-payment1'"></Button>
            <p>btn-payment1</p>
        </div>

        <div>
            <!-- นำจ่าย (1) -->
            <Button :type="'btn-payment2'"></Button>
            <p>btn-payment2</p>
        </div>

        <div>
            <!-- พิมพ์1 -->
            <Button :type="'btn-print1'"></Button>
            <p>btn-print1</p>
        </div>

        <div>
            <!-- พิมพ์2 -->
            <Button :type="'btn-print2'"></Button>
            <p>btn-print2</p>
        </div>

        <div>
            <!-- ประเภทรถส่วนตัว  -->
            <Button :type="'btn-private'"></Button>
            <p>btn-private</p>
        </div>

        <div>
            <!-- ประเภทรถสาธารณะ1  -->
            <Button :type="'btn-public1'"></Button>
            <p>btn-public1</p>
        </div>

        <div>
            <!-- ประเภทรถสาธารณะ2  -->
            <Button :type="'btn-public2'"></Button>
            <p>btn-public2</p>
        </div>

        <div>
            <!-- บันทึก  -->
            <Button :type="'btn-save'"></Button>
            <p>bbtn-save</p>
        </div>

        <div>
            <!-- ค้นหา  -->
            <Button :type="'btn-search'"></Button>
            <p>btn-search</p>
        </div>

        <div>
            <!-- ประเภทค่าเดินทาง  -->
            <Button :type="'btn-transport'"></Button>
            <p>btn-transport</p>
        </div>

        <div>
            <!-- ไม่อนุมัติ  -->
            <Button :type="'btn-unapprove'"></Button>
            <p>btn-unapprove</p>
        </div>

        <div>
            <!-- แก้ไขแบบย่อ  -->
            <Button :type="'btn-edit2'"></Button>
            <p>btn-edit2</p>
        </div>
    </div>




</template>