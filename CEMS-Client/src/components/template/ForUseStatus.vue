<script setup lang="ts">
/*
* ชื่อไฟล์: ForUseStatus.vue
* คำอธิบาย: ไฟล์นี้แสดงการใช้สถานะ
* ชื่อผู้เขียน/แก้ไข: 
* วันที่จัดทำ/แก้ไข: 
*/
import StatusBudge from './StatusBudge.vue';
</script>

<template>
    <div class="grid grid-cols-5 gap-5 m-5">
        <div>
            <!-- อนุมัติ -->
            <StatusBudge :status="'sts-approve'"></StatusBudge>
        </div>
        <div>
            <!-- รออนุมัติ -->
            <StatusBudge :status="'sts-waiting'"></StatusBudge>
        </div>
        <div>
            <!-- ไม่อนุมัติ -->
            <StatusBudge :status="'sts-notApproved'"></StatusBudge>
        </div>
        <div>
            <!-- แก้ไข -->
            <StatusBudge :status="'sts-edit'"></StatusBudge>
        </div>
        <div>
            <!-- แบบร่าง -->
            <StatusBudge :status="'sts-draft'"></StatusBudge>
        </div>
    </div>




</template>