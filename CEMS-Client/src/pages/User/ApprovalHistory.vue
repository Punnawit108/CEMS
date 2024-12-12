<script setup lang="ts">
/*
* ชื่อไฟล์: ApprovalHistory
* คำอธิบาย: ไฟล์นี้แสดงหน้า ประวัติการอนุมัติ
* ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/

import { useRouter } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
import Ctable from '../../components/template/CTable.vue';
import StatusBudge from '../../components/template/StatusBudge.vue';
import { useApprovalStore } from '../../store/approvalList';
import { onMounted } from 'vue';
import { Expense } from '../../types';

// เรียกใช้ ApprovalStore
const approvalStore = useApprovalStore();

// เรียกใช้ Router
const router = useRouter();

// เมื่อ Component ถูก Mounted ให้ดึงข้อมูลประวัติการอนุมัติสำหรับผู้ใช้ที่ระบุ
onMounted(async () => {
    const userId = 9999; // ตัวอย่าง ID ผู้ใช้ (ควรดึงจาก session หรือ Store ของระบบ)
    await approvalStore.getApprovalHistory(userId); // ดึงข้อมูลประวัติการอนุมัติ
});

// ฟังก์ชันสำหรับเปลี่ยนเส้นทางไปยังหน้ารายละเอียดของรายการที่เลือก
const toDetails = async (data: Expense) => {
    router.push(`/approval/history/detail/${data.rqId}`); // นำไปที่ URL: /approval/history/detail/:rqId
};
</script>

<template>
    <!-- content -->
    <div class="content">
        <!-- ฟิลเตอร์ -->
        <div class="filter flex flex-nowrap">
            <div class="ค้นหา mr-6">
                <!-- ค้นหา -->
                <form class="grid">
                    <label for="SearchBar" class="py-0.5 text-[14px] text-black text-start">ค้นหา</label>
                    <input type="text" id="SearchBar" placeholder="ชื่อ-นามสกุล" 
                        class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black pl-9" />
                </form>
            </div>
            <!-- เพิ่มฟิลเตอร์อื่นๆ (เช่น โครงการ ประเภทค่าใช้จ่าย) -->
        </div>

        <!-- ตาราง -->
        <div class="w-full border mt-12">
            <Ctable :table="'Table8-head'" />
            <table class="w-full">
                <tbody>
                    <tr v-for="(item, index) in approvalStore.approvalList" :key="item.rqId" class="border-b">
                        <th>{{ index + 1 }}</th>
                        <th class="truncate" :title="item.usrName">{{ item.usrName }}</th>
                        <th class="truncate" :title="item.rqPjName">{{ item.rqName }}</th>
                        <th>{{ item.pjName }}</th>
                        <th>{{ item.rqtName }}</th>
                        <th>{{ item.rqExpenses }}</th>
                        <th><StatusBudge :status="'sts-' + item.RqStatus" /></th>
                        <th @click="toDetails(item)">
                            <Icon :icon="'viewDetails'" />
                        </th>
                    </tr>
                </tbody>
            </table>
            <Ctable :table="'Table8-footer'" />
        </div>
    </div>
</template>
