<script setup lang="ts">

/**
* ชื่อไฟล์: ApprovalList
* คำอธิบาย: ไฟล์นี้แสดงหน้า รายการอนุมัติ
* Input: -
* Output: -
* ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/

import { useRouter } from 'vue-router';
import Icon from '../../components/template/CIcon.vue';
import Ctable from '../../components/template/Ctable.vue';
import { useExpense } from '../../store/ExpenseStore';
import { onMounted } from 'vue';

const expense = useExpense();
const router = useRouter();
onMounted(() => {
    expense.getAllApprovalList()
})

const toDetails = (id: string) => {
    router.push(`/approval/list/detail/${id}`);
}
</script>

<!-- path for test = /approval/list -->
<template>
  <!-- content -->
  <div>
    <!-- Filter -->
    <div class="w-full mb-12 flex flex-wrap items-center gap-5">
      <!-- Filter ค้นหา -->
      <div class="h-[32px] w-[266px]">
        <form class="grid">
          <label
            for="SearchBar"
            class="py-0.5 text-[14px] text-black text-start"
            >ค้นหา</label
          >
          <div class="relative h-[32px] w-[266px] justify-center items-center">
            <div
              class="absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none"
            >
              <svg
                width="19"
                height="20"
                viewBox="0 0 19 20"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z"
                  stroke="black"
                  stroke-width="2"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                />
              </svg>
            </div>

            <input
              type="text"
              id="SearchBar"
              class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-9"
              placeholder="ชื่อ-นามสกุล"
            />
          </div>
        </form>
      </div>

      <!-- Filter โครงการ -->
      <div class="h-[32px] w-[266px]">
        <form class="grid">
          <label
            for="SelectProject"
            class="py-0.5 text-[14px] text-black text-start"
            >โครงการ</label
          >
          <div class="relative h-[32px] w-[266px] justify-center items-center">
            <select
              required
              class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4"
            >
              <option value="" disabled selected hidden class="placeholder">
                โครงการ
              </option>
              <option value="item1">โครงการที่ 1</option>
              <option value="item2">โครงการที่ 2</option>
            </select>

            <div
              class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none"
            >
              <svg
                width="13"
                height="8"
                viewBox="0 0 13 8"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  clip-rule="evenodd"
                  d="M7.2071 7.2071C6.8166 7.5976 6.1834 7.5976 5.7929 7.2071L0.79289 2.20711C0.40237 1.81658 0.40237 1.18342 0.79289 0.79289C1.18342 0.40237 1.81658 0.40237 2.20711 0.79289L6.5 5.0858L10.7929 0.79289C11.1834 0.40237 11.8166 0.40237 12.2071 0.79289C12.5976 1.18342 12.5976 1.81658 12.2071 2.20711L7.2071 7.2071Z"
                  fill="black"
                />
              </svg>
            </div>
          </div>
        </form>
      </div>

      <!-- Filter ประเภทค่าใช้จ่าย -->
      <div class="h-[32px] w-[266px]">
        <form class="grid">
          <label
            for="ExpenseType"
            class="py-0.5 text-[14px] text-black text-start"
            >ประเภทค่าใช้จ่าย</label
          >
          <div class="relative h-[32px] w-[266px] justify-center items-center">
            <select
              required
              class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4"
            >
              <option value="" disabled selected hidden class="placeholder">
                ประเภทค่าใช้จ่าย
              </option>
              <option value="Type1">ประเภทที่ 1</option>
              <option value="Type2">ประเภทที่ 2</option>
            </select>

            <div
              class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none"
            >
              <svg
                width="13"
                height="8"
                viewBox="0 0 13 8"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  clip-rule="evenodd"
                  d="M7.2071 7.2071C6.8166 7.5976 6.1834 7.5976 5.7929 7.2071L0.79289 2.20711C0.40237 1.81658 0.40237 1.18342 0.79289 0.79289C1.18342 0.40237 1.81658 0.40237 2.20711 0.79289L6.5 5.0858L10.7929 0.79289C11.1834 0.40237 11.8166 0.40237 12.2071 0.79289C12.5976 1.18342 12.5976 1.81658 12.2071 2.20711L7.2071 7.2071Z"
                  fill="black"
                />
              </svg>
            </div>
            <table class="w-full">
                <tbody>
                    <tr v-for="(expense, index) in expense.expense" :key="expense.rqId"
                        class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                        <th class="py-[12px] px-2 w-14 h-[46px]">{{ index + 1 }}</th>
                        <th class="py-[12px] px-2 w-56 text-start truncate overflow-hidden"
                            style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            title="{{expense.rqName}}">
                            {{ expense.rqName }}
                        </th>
                        <th class="py-[12px] px-2 w-56 text-start truncate overflow-hidden"
                            style="max-width: 224px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                            title="{{expense.rqPjName}}">
                            {{ expense.rqPjName }}
                        </th>
                        <th class="py-[12px] px-5 w-44 text-start ">{{ expense.rqRqtName }}</th>
                        <th class="py-[12px] px-2 w-24 text-end ">{{ expense.rqDateWithdraw }}</th>
                        <th class="py-[12px] px-2 w-40 text-end ">{{ expense.rqExpenses }}</th>
                        <th class="py-[10px] px-2 w-32 text-center">
                            <span class="flex justify-center" v-on:click="toDetails">
                                <Icon :icon="'viewDetails'" />
                            </span>
                        </th>
                    </tr>
                </tbody>
            </table>
            <div>
                <Ctable :table="'Table2-footer'" />
            </div>
          </div>
        </form>
      </div>
    </div>

    <!-- Filter -->

    <div
      class="w-full border-l-[2px] border-t-[2px] border-r-[2px] border-[#b6b7ba] mt-12 text-black"
    >
      <table class="table-auto w-full text-center text-gray">
        <thead class="bg-[#F2F4F8]">
          <tr class="text-[16px] border-b-2 border-[#BBBBBB]">
            <th class="py-[11px] px-2 w-14 font-bold h-[46px]">ลำดับ</th>
            <th class="py-[11px] px-2 text-start w-56 font-bold">
              ชื่อ-นามสกุล
            </th>
            <th class="py-[11px] px-2 text-start w-56 font-bold">
              ชื่อโครงการ
            </th>
            <th class="py-[11px] px-5 text-start w-44 font-bold">
              ประเภทค่าใช้จ่าย
            </th>
            <th class="py-[11px] px-2 text-end w-24 font-bold">วันที่เบิก</th>
            <th class="py-[11px] px-2 text-end w-40 font-bold">
              จำนวนเงิน(บาท)
            </th>
            <th class="py-[11px] px-2 text-center w-32 font-bold">
              รายละเอียด
            </th>
          </tr>
        </thead>
        <!-- Table2-ข้อมูล -->
        <tbody>
          <tr
            v-for="(item, index) in paginatedTodos"
            :key="item.id"
            class="border-t"
            :class="{
              'border-b border-gray': index === paginatedTodos.length - 1,
            }"
          >
            <td class="px-4 py-2">
              {{ index + 1 + (currentPage - 1) * itemsPerPage }}
            </td>
            <td class="px-4 py-2 text-start">{{ item.name }}</td>
            <td class="px-4 py-2 text-start">{{ item.projectName }}</td>
            <td class="px-4 py-2 text-start">{{ item.selectExpenseType }}</td>
            <td class="px-4 py-2 text-end">{{ item.date }}</td>
            <td class="px-4 py-2 text-end">{{ item.amount }}</td>
            <!-- <td class="px-4 py-2">
              <button
                @click="toDetails(item.id)"
                class="text-blue-500 hover:underline"
              >
                ดูรายละเอียด
              </button>
            </td> -->
            <td class="py-[10px] px-2 w-32 text-center">
              <button v-on:click="toDetails" >
                <Icon :icon="'viewDetails'" />
              </button>
            </td>
          </tr>
          <!-- Show empty rows if there are less than 15 items -->
          <tr v-if="paginatedTodos.length < itemsPerPage">
            <td v-for="index in 7" :key="'empty' + index" class="px-4 py-2">
              &nbsp;
              <!-- Empty cell for spacing -->
            </td>
          </tr>
          <!-- Fill remaining rows with empty cells for consistent row height -->
          <tr v-for="index in remainingRows" :key="'empty-row' + index">
            <td v-for="i in 7" :key="'empty-cell' + i" class="px-4 py-2">
              &nbsp;
              <!-- Empty cell for spacing -->
            </td>
          </tr>
        </tbody>
        <!-- Table2-footer -->
        <tfoot class="border-t" v-if="table === 'Table1-footer'">
          <tr class="text-[14px] border-b-2 border-[#BBBBBB]">
            <th></th>
            <th></th>
            <th></th>
            <th></th>
            <th></th>

            <th class="py-[12px] text-end">
              {{ currentPage }} of {{ totalPages }}
            </th>
            <th class="py-[12px] flex justify-evenly text-[14px] font-bold">
              <span class="ml-6 text-[#A0A0A0]">
                <button
                  @click="prevPage"
                  :disabled="currentPage === 1"
                  class="px-3 py-1 rounded"
                >
                  <span class="text-sm">&lt;</span>
                </button>
              </span>
              <span class="mr-6">
                <button
                  @click="nextPage"
                  :disabled="currentPage === totalPages"
                  class="px-3 py-1 rounded"
                >
                  <span class="text-sm">&gt;</span>
                </button>
              </span>
            </th>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
</template>

<style scoped>
/* Style adjustments for better UI/UX */
.custom-select {
  appearance: none;
  background-color: white;
  padding-left: 1rem;
  border-radius: 4px;
  border: 1px solid #ccc;
  font-size: 0.875rem;
}

.custom-select:focus {
  border-color: #5b9bd5;
  outline: none;
}

button:disabled {
  cursor: not-allowed;
}
</style>
