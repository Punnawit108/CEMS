<script setup lang="ts">
/*
 * ชื่อไฟล์: Progress.vue
 * คำอธิบาย: ไฟล์นี้ใช้สำหรับแสดงความก้าวหน้าของการอนุมัติ
 * ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ , นายพงศธร บุญญามา
 * วันที่จัดทำ/แก้ไข: 8 ตุลาคม 2567
 */
import { defineProps } from "vue";

const props = defineProps(["progressInfo", "colorStatus"]);

function hasRejectStatus(): { index: number } {
  const index = props.progressInfo.acceptor.findIndex(
    (item: any) => item.aprStatus === "reject" || item.aprStatus === "edit"
  );
  return { index }; // ส่ง index ออกมา (ถ้าไม่พบจะเป็น -1)
}

console.log(props.progressInfo.disbursement[0]?.rqStatus);
</script>

<template>
  <div class="border border-[#B6B7BA] px-[16px] py-[16px] w-[232px]">
    <div class="row">
      <svg
        width="48"
        height="48"
        viewBox="0 0 48 48"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          v-if="props.progressInfo.disbursement[0]?.rqStatus == 'sketch'"
          d="M48 24C48 37.2548 37.2548 48 24 48C10.7452 48 0 37.2548 0 24C0 10.7452 10.7452 0 24 0C37.2548 0 48 10.7452 48 24Z"
          fill="#B6B7BA"
        />
        <path
          v-else
          d="M48 24C48 37.2548 37.2548 48 24 48C10.7452 48 0 37.2548 0 24C0 10.7452 10.7452 0 24 0C37.2548 0 48 10.7452 48 24Z"
          fill="#12B669"
        />
        <path
          d="M32 12.6667H27.23C26.6 11.12 24.95 10 23 10C21.05 10 19.4 11.12 18.77 12.6667H14C12.35 12.6667 11 13.8667 11 15.3333V35.3333C11 36.8 12.35 38 14 38H23.165C22.2764 37.2352 21.5543 36.3312 21.035 35.3333H14V15.3333H17V16.6667C17 18.1333 18.35 19.3333 20 19.3333H26C27.65 19.3333 29 18.1333 29 16.6667V15.3333H32V22.1067C33.065 22.24 34.07 22.52 35 22.9067V15.3333C35 13.8667 33.65 12.6667 32 12.6667ZM23 15.3333C22.175 15.3333 21.5 14.7333 21.5 14C21.5 13.2667 22.175 12.6667 23 12.6667C23.825 12.6667 24.5 13.2667 24.5 14C24.5 14.7333 23.825 15.3333 23 15.3333ZM30.5 24.6667C26.36 24.6667 23 27.6533 23 31.3333C23 35.0133 26.36 38 30.5 38C34.64 38 38 35.0133 38 31.3333C38 27.6533 34.64 24.6667 30.5 24.6667ZM32.435 34L29.96 31.8C29.8187 31.6769 29.7378 31.5092 29.735 31.3333V28.0133C29.735 27.64 30.065 27.3467 30.485 27.3467C30.905 27.3467 31.235 27.64 31.235 28.0133V31.0667L33.485 33.0667C33.5551 33.1277 33.6109 33.2005 33.6491 33.2809C33.6874 33.3612 33.7073 33.4475 33.7078 33.5347C33.7083 33.6219 33.6893 33.7084 33.652 33.7891C33.6147 33.8698 33.5597 33.9431 33.4903 34.0047C33.4209 34.0664 33.3385 34.1153 33.2477 34.1484C33.157 34.1816 33.0597 34.1985 32.9616 34.198C32.8634 34.1976 32.7664 34.1799 32.676 34.1459C32.5856 34.1119 32.5037 34.0623 32.435 34Z"
          fill="white"
        />
      </svg>
      <div class="text">
        <p class="font-bold">รอดำเนินการ</p>
      </div>
    </div>
    <div class="row my-[50px]" v-for="(item, index) in progressInfo.acceptor">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="48"
        height="48"
        fill="none"
        viewBox="0 0 33 33"
        v-if="
          (item.aprStatus && index <= hasRejectStatus().index) ||
          hasRejectStatus().index == -1
        "
      >
        <path
          :fill="colorStatus[item.aprStatus]"
          fill-rule="evenodd"
          d="M22.5 12a6 6 0 1 1-12 0 6 6 0 0 1 12 0Zm-3 0a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
          clip-rule="evenodd"
        />
        <path
          :fill="colorStatus[item.aprStatus]"
          fill-rule="evenodd"
          d="M16.5 0C7.387 0 0 7.388 0 16.5 0 25.613 7.388 33 16.5 33 25.613 33 33 25.613 33 16.5S25.613 0 16.5 0ZM3 16.5c0 3.135 1.07 6.021 2.862 8.313A13.485 13.485 0 0 1 16.598 19.5a13.469 13.469 0 0 1 10.637 5.187A13.499 13.499 0 0 0 8.63 5.534 13.5 13.5 0 0 0 3 16.5ZM16.5 30a13.44 13.44 0 0 1-8.508-3.018 10.485 10.485 0 0 1 8.605-4.482 10.484 10.484 0 0 1 8.534 4.38A13.44 13.44 0 0 1 16.5 30Z"
          clip-rule="evenodd"
        />
      </svg>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="48"
        height="48"
        fill="none"
        viewBox="0 0 33 33"
        v-else
      >
        <path
          fill="#B6B7BA"
          fill-rule="evenodd"
          d="M22.5 12a6 6 0 1 1-12 0 6 6 0 0 1 12 0Zm-3 0a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
          clip-rule="evenodd"
        />
        <path
          fill="#B6B7BA"
          fill-rule="evenodd"
          d="M16.5 0C7.387 0 0 7.388 0 16.5 0 25.613 7.388 33 16.5 33 25.613 33 33 25.613 33 16.5S25.613 0 16.5 0ZM3 16.5c0 3.135 1.07 6.021 2.862 8.313A13.485 13.485 0 0 1 16.598 19.5a13.469 13.469 0 0 1 10.637 5.187A13.499 13.499 0 0 0 8.63 5.534 13.5 13.5 0 0 0 3 16.5ZM16.5 30a13.44 13.44 0 0 1-8.508-3.018 10.485 10.485 0 0 1 8.605-4.482 10.484 10.484 0 0 1 8.534 4.38A13.44 13.44 0 0 1 16.5 30Z"
          clip-rule="evenodd"
        />
      </svg>
      <div class="text">
        <!-- ถ้าอนุมัติแล้วใช้ aprName -->
        <p v-if="item.aprId == null" class="w-fit font-bold">
          ผู้อนุมัติคนที่ {{ index + 1 }}
        </p>
        <p
          v-if="item.aprName == null && item.aprId != null"
          class="w-fit font-bold"
        >
          {{ item.usrFirstName + " " + item.usrLastName }}
        </p>
        <p v-if="item.aprName != null" class="w-fit text-black">
          {{ item.aprName }}
        </p>
        <div v-if="item.aprStatus === 'accept'">
          <p class="text-[11px] text-gray-400">
            อนุมัติเมื่อ :{{ item.aprDate }}
          </p>
        </div>
        <div v-else-if="item.aprStatus === 'reject'">
          <p class="text-[11px] text-gray-400">
            ไม่อนุมัติเมื่อ :{{ item.aprDate }}
          </p>
        </div>
        <div v-else-if="item.aprStatus === 'edit'">
          <p class="text-[11px] text-gray-400">
            ส่งกลับเมื่อ :{{ item.aprDate }}
          </p>
        </div>
        <div v-else="item.aprStatus === 'waiting'"></div>
      </div>
    </div>
    <div class="row">
      <svg
        width="48"
        height="48"
        viewBox="0 0 48 48"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
      >
        <circle
          v-if="props.progressInfo.disbursement[0]?.rqProgress != 'complete'"
          cx="24"
          cy="24"
          r="24"
          :fill="colorStatus[props.progressInfo.disbursement[0]?.rqProgress]"
        />
        <circle
          v-if="
            props.progressInfo.disbursement[0]?.rqProgress == 'complete' &&
            props.progressInfo.disbursement[0]?.rqStatus == 'reject'
          "
          cx="24"
          cy="24"
          r="24"
          fill="#B6B7BA"
        />
        <circle
          v-if="
            props.progressInfo.disbursement[0]?.rqProgress == 'complete' &&
            props.progressInfo.disbursement[0]?.rqStatus == 'accept'
          "
          cx="24"
          cy="24"
          r="24"
          fill="#12B669"
        />

        <path
          d="M31.8434 20.7412C32.6924 19.8922 32.6924 18.5275 32.6924 15.7951C32.6924 13.0627 32.6924 11.698 31.8434 10.849M31.8434 20.7412C30.9944 21.5902 29.6297 21.5902 26.8973 21.5902H21.1022C18.3698 21.5902 17.0051 21.5902 16.1561 20.7412M31.8434 10.849C30.9944 10 29.6297 10 26.8973 10H21.1022C18.3698 10 17.0051 10 16.1561 10.849M16.1561 10.849C15.3071 11.698 15.3071 13.0627 15.3071 15.7951C15.3071 18.5275 15.3071 19.8922 16.1561 20.7412M25.4485 15.7951C25.4485 16.1793 25.2959 16.5478 25.0242 16.8195C24.7525 17.0912 24.384 17.2439 23.9998 17.2439C23.6155 17.2439 23.247 17.0912 22.9753 16.8195C22.7036 16.5478 22.551 16.1793 22.551 15.7951C22.551 15.4109 22.7036 15.0424 22.9753 14.7707C23.247 14.499 23.6155 14.3463 23.9998 14.3463C24.384 14.3463 24.7525 14.499 25.0242 14.7707C25.2959 15.0424 25.4485 15.4109 25.4485 15.7951Z"
          stroke="white"
          stroke-width="2"
        />
        <path
          d="M32.6924 14.3463C31.5397 14.3463 30.4342 13.8884 29.6191 13.0733C28.804 12.2582 28.3461 11.1527 28.3461 10M32.6924 17.2439C31.5397 17.2439 30.4342 17.7018 29.6191 18.5169C28.804 19.332 28.3461 20.4375 28.3461 21.5902M15.3072 14.3463C16.4599 14.3463 17.5654 13.8884 18.3805 13.0733C19.1956 12.2582 19.6535 11.1527 19.6535 10M15.3072 17.2439C16.4599 17.2439 17.5654 17.7018 18.3805 18.5169C19.1956 19.332 19.6535 20.4375 19.6535 21.5902M13.8584 35.1913H17.1326C18.5959 35.1913 20.078 35.3448 21.5021 35.6375C24.0449 36.1582 26.6607 36.2161 29.2241 35.8084C30.4816 35.6056 31.716 35.2941 32.8344 34.7552C33.8428 34.2669 35.0786 33.5817 35.9087 32.8124C36.7374 32.0445 37.6009 30.7884 38.2123 29.8062C38.7396 28.963 38.4846 27.93 37.653 27.3027C37.1855 26.9634 36.6226 26.7807 36.0449 26.7807C35.4672 26.7807 34.9043 26.9634 34.4368 27.3027L31.8188 29.2803C30.8047 30.0481 29.6964 30.7537 28.3765 30.9637C28.2172 30.9888 28.0506 31.0115 27.8767 31.0318M27.8767 31.0318L27.7174 31.0492M27.8767 31.0318C28.1086 30.969 28.3212 30.8495 28.4953 30.6841C28.714 30.4955 28.8926 30.2649 29.0205 30.006C29.1485 29.7471 29.2232 29.4652 29.2402 29.1769C29.2572 28.8886 29.2162 28.5998 29.1196 28.3277C29.023 28.0555 28.8728 27.8055 28.6779 27.5924C28.4891 27.3829 28.2692 27.2035 28.0259 27.0607C23.9737 24.6427 17.6672 26.4841 13.8584 29.1861M27.8767 31.0318C27.8244 31.0435 27.771 31.0493 27.7174 31.0492M27.7174 31.0492C26.8436 31.1368 25.9634 31.1387 25.0893 31.055"
          stroke="white"
          stroke-width="2"
          stroke-linecap="round"
        />
        <path
          d="M13.858 28.1097C13.858 26.9095 12.8851 25.9365 11.6849 25.9365C10.4847 25.9365 9.51172 26.9095 9.51172 28.1097V35.3535C9.51172 36.5537 10.4847 37.5267 11.6849 37.5267C12.8851 37.5267 13.858 36.5537 13.858 35.3535V28.1097Z"
          stroke="white"
          stroke-width="2"
        />
      </svg>
      <div class="text">
        <p class="font-bold">เบิกจ่าย</p>
      </div>
    </div>
    <div class="row my-[50px]">
      <svg
        width="48"
        height="48"
        viewBox="0 0 48 48"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          v-if="
            props.progressInfo.disbursement[0]?.rqProgress === 'complete' &&
            props.progressInfo.disbursement[0]?.rqStatus == 'accept'
          "
          d="M48 24C48 37.2548 37.2548 48 24 48C10.7452 48 0 37.2548 0 24C0 10.7452 10.7452 0 24 0C37.2548 0 48 10.7452 48 24Z"
          fill="#12B669"
        />
        <path
          v-else
          d="M48 24C48 37.2548 37.2548 48 24 48C10.7452 48 0 37.2548 0 24C0 10.7452 10.7452 0 24 0C37.2548 0 48 10.7452 48 24Z"
          fill="#B6B7BA"
        />
        <path
          d="M11 24L14.25 20.6667L20.75 27.3333L33.75 14L37 17.3333L20.75 34L11 24Z"
          stroke="white"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
      </svg>
      <div class="text">
        <p class="font-bold">เสร็จสิ้น</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
@tailwind base;
@tailwind components;

.row {
  @apply flex items-center text-black;
}

.text {
  @apply ml-[8px];
}

@tailwind utilities;

p {
  font-size: 14px;
  font: normal;
}
</style>
