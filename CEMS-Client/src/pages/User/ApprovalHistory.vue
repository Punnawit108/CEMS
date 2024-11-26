<script setup lang="ts">

/**
* ชื่อไฟล์: ApprovalHistory
* คำอธิบาย: ไฟล์นี้แสดงหน้า ประวัติการอนุมัติ
* Input: -
* Output: -
* ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/

import { useRouter } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
import Ctable from '../../components/template/Ctable.vue';
import StatusBudge from '../../components/template/StatusBudge.vue';
import { useExpense } from '../../store/ExpenseStore';
import { onMounted } from 'vue';
import { Expense } from '../../types';
import { useDetailStore } from '../../store/detail';

const expense = useExpense();
const router = useRouter();

onMounted(()=>{
    expense.getAllApprovalHistory()
})

const detailStore = useDetailStore();

const toDetails = async (data: Expense) => {
    await detailStore.setSelectedExpense(data); 
    router.push(`/approval/history/detail/${data.rqId}`);
};

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
    <div class="w-full border-l-[2px] border-t-[2px] border-r-[2px] border-[#b6b7ba] mt-12">
        <!-- ตาราง -->
                <div>
            <Ctable :table="'Table8-head'" />
        </div>
        <table class="w-full">
            <tbody>
                <tr v-for="(expense, index) in expense.expense":key="expense.rqId" class=" text-[14px] border-b-2 border-[#BBBBBB] ">

                    <th class="py-[12px] px-2 w-14 h-[46px]">{{index + 1}}</th>
                    <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                        style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                        title="{{expense.name}}">
                        {{expense.rqUsrName}}
                    </th>
                    <th class="py-[12px] px-2 w-52 text-start truncate overflow-hidden"
                        style="max-width: 196px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                        title="{{expense.projectDetail}}">
                        {{expense.rqPjName}}
                    </th>
                    <th class="py-[12px] px-5 w-32 text-start font-[100]">{{expense.rqRqtName}}</th>
                    <th class="py-[12px] px-2 w-20 text-end ">{{expense.rqDateWithdraw}}</th>
                    <th class="py-[12px] px-5 w-32 text-end ">{{expense.rqExpenses}}</th>

                    <th class="py-[12px] px-2 w-28 text-center text-green-500"><StatusBudge :status="'sts-'+ expense.rqStatus"></StatusBudge></th>
                    <th class="py-[10px] px-2 w-20 text-center ">
                        <span class="flex justify-center" @click="toDetails(expense)">
                            <Icon :icon="'viewDetails'" />
                        </span>
                    </th>
                </tr>
            </tbody>
        </table>
        <div>
            <Ctable :table="'Table8-footer'" />
        </div>
    </div>
    
    </div>
    <!-- content -->
</template>