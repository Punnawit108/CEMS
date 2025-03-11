<script setup lang="ts">
/*
* ชื่อไฟล์: Notifications.vue
* คำอธิบาย: ไฟล์นี้แสดงการแจ้งเตือนที่เข้ามาในระบบ
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
*/
import { ref, computed } from 'vue';
import { useNotificationStore } from '../../store/notification';
import { onMounted } from 'vue';
import CardNotification from '../../components/CardNotification.vue';



let filterNotification = ref("All")
/*
* คำอธิบาย: แสดงข้อมูลการแจ้งเตือน
* Output: ข้อมูลแจ้งเตือน
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
*/

const notificationStore = useNotificationStore();

// const user = ref<any>(null);
// onMounted(async () => {
//     const storedUser = localStorage.getItem("user");
//     if (storedUser) {
//         try {
//             user.value = await JSON.parse(storedUser);
//         } catch (error) {
//             console.log("Error loading user:", error);
//         }
//     }
//     if (user) {
//         await paymentHistory.getAllPaymentHistory(user.value.usrId);
//     }
// }

const user = ref<any>(null);
onMounted(async () => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
        try {
            // Parse JSON และตรวจสอบว่าได้ผลลัพธ์เป็น object
            const parsedUser = JSON.parse(storedUser);
            if (parsedUser && typeof parsedUser === "object") {
                user.value = parsedUser;

                // ตรวจสอบว่า user.value และ usrId มีอยู่
                if (user.value && user.value.usrId) {
                    user.value = await notificationStore.loadNotifications(user.value.usrId);
                    await notificationStore.initSignalR(user.value.usrId);
                } else {
                    console.log("Invalid user data: missing usrId");
                }
            } else {
                console.log("Invalid user data: not an object");
            }
        } catch (error) {
            console.log("Error loading user:", error);
        }
    }
});

const clickAllNotification = ref(true);
const clickReadedNotification = ref(false);
const clickNotReadNotification = ref(false);
/*
* คำอธิบาย: แสดงข้อมูลการแจ้งเตือนทั้งหมด
* Output: ข้อมูลแจ้งเตือนทุกสถานะทั้งหมด
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
*/
const toggleAllNotification = () => {
    resetAllToggles();
    filterNotification.value = "All"
    clickAllNotification.value = true;
};
/*
* คำอธิบาย: แสดงข้อมูลการแจ้งเตือนสถานะอ่านแล้ว
* Output: ข้อมูลแจ้งเตือนสถานะอ่านแล้ว
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
*/
const toggleReadedNotification = () => {
    resetAllToggles();
    filterNotification.value = "read"
    clickReadedNotification.value = true;
};
/*
* คำอธิบาย: แสดงข้อมูลการแจ้งเตือนสถานะยังไม่อ่าน
* Output: ข้อมูลแจ้งเตือนสถานะยังไม่อ่าน
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
*/
const toggleNotReadNotification = () => {
    resetAllToggles();
    filterNotification.value = "unread"
    clickNotReadNotification.value = true;
};
/*
* คำอธิบาย: เปลี่ยนสถานะของตัวแปรเพื่อแสดงสถานะที่ต้องการ
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 2 ธันวาคม 2567
*/
const resetAllToggles = () => {
    clickAllNotification.value = false;
    clickReadedNotification.value = false;
    clickNotReadNotification.value = false;

};

const filteredNotifications = computed(() => {
    if (filterNotification.value === 'read') {
        return notificationStore.notifications?.filter((item: any) => item.ntStatus === 'read');
    } else if (filterNotification.value === 'unread') {
        return notificationStore.notifications.filter((item: any) => item.ntStatus === 'unread');
    }
    return notificationStore.notifications;
});
</script>

<template>
    <div>
        <nav class="flex overflow-hidden items-center whitespace-nowrap" aria-label="Filter options">
            <ul
                class="flex flex-wrap gap-4 self-stretch py-2 pr-20 pl-2 my-auto text-sm leading-snug w-[1136px] max-md:pr-5 max-md:max-w-full">
                <li>
                    <button @click="toggleAllNotification" :class="[
                        'flex px-4 py-1.5 bg-white rounded-3xl border-2 border-solid',
                        clickAllNotification ? 'border-blue-600 text-blue-600' : 'border-neutral-400 text-neutral-500 text-opacity-80'
                    ]">
                        <svg :style="{ fill: clickAllNotification ? '#0066DD' : '#777777' }" width="18" height="17"
                            viewBox="0 0 18 17" xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M16.5647 5.49614C16.1523 4.48182 15.5402 3.56079 14.7647 2.78781C13.9908 2.013 13.0717 1.39834 12.06 0.978974C11.0484 0.559605 9.964 0.34375 8.86887 0.34375C7.77375 0.34375 6.68936 0.559605 5.6777 0.978974C4.66605 1.39834 3.74698 2.013 2.97304 2.78781C2.19455 3.56003 1.57955 4.48112 1.16471 5.49614C0.746995 6.50548 0.531803 7.58711 0.531374 8.67947C0.525409 9.75734 0.735077 10.8255 1.14804 11.8211L0.731374 14.8628C0.647242 15.254 0.710409 15.6625 0.908772 16.01C1.10713 16.3575 1.42675 16.6196 1.80637 16.7461C2.05387 16.8278 2.31721 16.8478 2.57304 16.8045L5.61471 16.3545C6.63646 16.7916 7.7367 17.0156 8.84804 17.0128C10.2178 17.0126 11.5664 16.6747 12.7745 16.0291C13.9826 15.3834 15.0129 14.4499 15.7743 13.3112C16.5356 12.1725 17.0045 10.8637 17.1395 9.50055C17.2744 8.13742 17.0713 6.76205 16.548 5.49614H16.5647ZM5.29804 10.0211C5.12185 10.0211 4.9474 9.98633 4.78464 9.91885C4.62188 9.85138 4.47401 9.7525 4.34946 9.62788C4.22491 9.50325 4.12613 9.35532 4.05876 9.19252C3.99139 9.02972 3.95674 8.85525 3.95679 8.67906C3.95685 8.50287 3.9916 8.32841 4.05908 8.16565C4.12655 8.0029 4.22543 7.85502 4.35005 7.73048C4.47467 7.60593 4.62261 7.50715 4.78541 7.43977C4.94821 7.3724 5.12268 7.33775 5.29887 7.33781C5.65471 7.33781 5.99596 7.47916 6.24758 7.73077C6.49919 7.98238 6.64054 8.32364 6.64054 8.67947C6.64054 9.0353 6.49919 9.37656 6.24758 9.62817C5.99596 9.87979 5.65387 10.0211 5.29804 10.0211ZM8.86554 10.0211C8.68935 10.0211 8.5149 9.98633 8.35214 9.91885C8.18938 9.85138 8.04151 9.7525 7.91696 9.62788C7.79241 9.50325 7.69363 9.35532 7.62626 9.19252C7.55889 9.02972 7.52424 8.85525 7.52429 8.67906C7.52435 8.50287 7.5591 8.32841 7.62658 8.16565C7.69405 8.0029 7.79293 7.85502 7.91755 7.73048C8.04217 7.60593 8.19011 7.50715 8.35291 7.43977C8.51571 7.3724 8.69018 7.33775 8.86637 7.33781C9.22221 7.33781 9.56346 7.47916 9.81508 7.73077C10.0667 7.98238 10.208 8.32364 10.208 8.67947C10.208 9.0353 10.0667 9.37656 9.81508 9.62817C9.56346 9.87979 9.22221 10.0211 8.86637 10.0211M12.433 10.0211C12.2569 10.0211 12.0824 9.98633 11.9196 9.91885C11.7569 9.85138 11.609 9.7525 11.4845 9.62788C11.3599 9.50325 11.2611 9.35532 11.1938 9.19252C11.1264 9.02972 11.0917 8.85525 11.0918 8.67906C11.0918 8.50287 11.1266 8.32841 11.1941 8.16565C11.2616 8.0029 11.3604 7.85502 11.4851 7.73048C11.6097 7.60593 11.7576 7.50715 11.9204 7.43977C12.0832 7.3724 12.2577 7.33775 12.4339 7.33781C12.7897 7.33781 13.131 7.47916 13.3826 7.73077C13.6342 7.98238 13.7755 8.32364 13.7755 8.67947C13.7755 9.0353 13.6342 9.37656 13.3826 9.62817C13.131 9.87979 12.7889 10.0211 12.433 10.0211Z"
                                :fill="clickAllNotification ? '#0066DD' : '#888888'" />
                        </svg>
                        <span class="ml-1">ทั้งหมด</span>
                    </button>
                </li>



                <li>
                    <button @click="toggleReadedNotification"
                        :class="['flex px-4 py-1.5 bg-white rounded-3xl border-2 border-solid', clickReadedNotification ? 'border-[#12B669] text-[#12B669]' : 'border-neutral-400 text-neutral-500 text-opacity-80']">
                        <svg :style="{ fill: clickReadedNotification ? '#12B669' : '#777777' }" width="18" height="17"
                            viewBox="0 0 18 17" xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M16.7208 5.31503C16.308 4.29892 15.6928 3.37744 14.9125 2.60669C13.7471 1.44059 12.262 0.646347 10.6452 0.324455C9.0283 0.00256366 7.35228 0.167488 5.82916 0.798361C4.81827 1.22095 3.89861 1.835 3.12082 2.60669C2.34879 3.38251 1.73715 4.3028 1.32082 5.31503C0.898852 6.32308 0.683489 7.40556 0.687489 8.49836C0.679254 9.57644 0.889009 10.6451 1.30416 11.64L0.887489 14.6817C0.825257 14.9391 0.825257 15.2076 0.887489 15.465C0.964989 15.7184 1.09582 15.9509 1.27082 16.1484C1.45427 16.3373 1.67987 16.4801 1.92916 16.565C2.17928 16.6453 2.44484 16.6653 2.70416 16.6234L5.74582 16.1734C6.76687 16.6128 7.86758 16.8369 8.97916 16.8317C10.0745 16.8346 11.1595 16.6193 12.1708 16.1984C13.1808 15.7786 14.0982 15.1641 14.8708 14.39C15.6502 13.6217 16.2654 12.7032 16.6792 11.69C17.5241 9.64902 17.5241 7.35604 16.6792 5.31503H16.7208ZM13.0458 7.03169L9.14582 10.9234C9.00416 11.0734 8.82915 11.19 8.63749 11.265C8.44824 11.3476 8.24398 11.3903 8.03749 11.3903C7.831 11.3903 7.62674 11.3476 7.43749 11.265C7.24337 11.1883 7.0674 11.072 6.92082 10.9234L4.99582 8.99836C4.91812 8.92066 4.85649 8.82842 4.81444 8.7269C4.77239 8.62538 4.75075 8.51658 4.75075 8.40669C4.75075 8.18478 4.8389 7.97195 4.99582 7.81503C5.15274 7.65811 5.36557 7.56995 5.58749 7.56995C5.80941 7.56995 6.02224 7.65811 6.17916 7.81503L8.04582 9.68169L11.8625 5.85669C11.9396 5.779 12.0313 5.71725 12.1324 5.67499C12.2334 5.63273 12.3417 5.61078 12.4512 5.6104C12.5607 5.61001 12.6692 5.63119 12.7705 5.67274C12.8718 5.71428 12.964 5.77538 13.0417 5.85253C13.1194 5.92968 13.1811 6.02138 13.2234 6.12239C13.2656 6.2234 13.2876 6.33175 13.288 6.44125C13.2883 6.55074 13.2672 6.65924 13.2256 6.76055C13.1841 6.86186 13.123 6.954 13.0458 7.03169Z"
                                :fill="clickReadedNotification ? '#12B669' : '#888888'" />
                        </svg>
                        <span class="ml-1">อ่านแล้ว</span>
                    </button>
                </li>


                <li>
                    <button @click="toggleNotReadNotification"
                        :class="['flex px-4 py-1.5 bg-white rounded-3xl border-2 border-solid', clickNotReadNotification ? 'border-[#D92C20] text-[#D92C20]' : 'border-neutral-400 text-neutral-500 text-opacity-80']">
                        <svg :style="{ fill: clickNotReadNotification ? '#D92C20' : '#777777' }" width="18" height="17"
                            viewBox="0 0 18 17" xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M16.7208 5.31645C16.3045 4.30423 15.6929 3.38393 14.9208 2.60812C14.1469 1.83331 13.2278 1.21866 12.2162 0.799287C11.2045 0.379918 10.1201 0.164062 9.02499 0.164062C7.92986 0.164062 6.84547 0.379918 5.83382 0.799287C4.82217 1.21866 3.9031 1.83331 3.12916 2.60812C2.34888 3.37887 1.73362 4.30034 1.32082 5.31645C0.898852 6.3245 0.683489 7.40699 0.687489 8.49978C0.679254 9.57786 0.889009 10.6465 1.30416 11.6415L0.887489 14.6831C0.829156 14.9415 0.829156 15.209 0.887489 15.4665C0.960359 15.7185 1.0917 15.9498 1.27082 16.1415C1.45134 16.334 1.67668 16.4789 1.92677 16.5632C2.17685 16.6475 2.44391 16.6687 2.70416 16.6248L5.74582 16.1748C6.76758 16.6119 7.86781 16.836 8.97916 16.8331C10.3489 16.8329 11.6976 16.495 12.9056 15.8494C14.1137 15.2037 15.144 14.2703 15.9054 13.1315C16.6667 11.9928 17.1356 10.684 17.2706 9.32086C17.4056 7.95773 17.2024 6.58236 16.6792 5.31645H16.7208ZM9.90416 12.2831C9.90416 12.5041 9.81636 12.7161 9.66008 12.8724C9.5038 13.0287 9.29184 13.1165 9.07082 13.1165C8.84981 13.1165 8.63785 13.0287 8.48157 12.8724C8.32529 12.7161 8.23749 12.5041 8.23749 12.2831V7.65812C8.23749 7.4371 8.32529 7.22514 8.48157 7.06886C8.63785 6.91258 8.84981 6.82478 9.07082 6.82478C9.29184 6.82478 9.5038 6.91258 9.66008 7.06886C9.81636 7.22514 9.90416 7.4371 9.90416 7.65812V12.2831ZM8.97916 5.74145C8.70289 5.74145 8.43794 5.6317 8.24259 5.43635C8.04724 5.241 7.93749 4.97605 7.93749 4.69978C7.93749 4.42352 8.04724 4.15857 8.24259 3.96321C8.43794 3.76786 8.70289 3.65812 8.97916 3.65812C9.25542 3.65812 9.52038 3.76786 9.71572 3.96321C9.91108 4.15857 10.0208 4.42352 10.0208 4.69978C10.0208 4.97605 9.91108 5.241 9.71572 5.43635C9.52038 5.6317 9.25542 5.74145 8.97916 5.74145Z"
                                :fill="clickNotReadNotification ? '#D92C20' : '#888888'" />
                        </svg>
                        <span class="ml-1">ยังไม่อ่าน</span>
                    </button>
                </li>
            </ul>

        </nav>
        <article class="flex flex-col border border-solid border-zinc-400">
            <CardNotification v-if="filteredNotifications !== null" :notificationInfo="filteredNotifications" />

            <footer
                class="flex overflow-hidden flex-wrap gap-9 items-center px-2 w-full text-2xl leading-none text-center bg-white border-t border-solid border-t-zinc-400 min-h-[56px] max-md:max-w-full">
                <div class="flex grow shrink self-stretch my-auto h-5 min-w-[240px] w-[907px]"></div>
                <p
                    class="self-stretch my-auto text-xs tracking-wide leading-loose text-right text-black text-opacity-90">
                    1 of 10
                </p>
                <button
                    class="grow shrink self-stretch px-1 my-auto whitespace-nowrap min-h-[32px] rounded-[32px] text-black text-opacity-20 w-[26px]"
                    aria-label="Previous page">
                    <svg width="32" height="33" viewBox="0 0 32 33" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path
                            d="M19.4219 11.9062L14.8281 16.5L19.4219 21.0938L18.0156 22.5L12.0156 16.5L18.0156 10.5L19.4219 11.9062Z"
                            fill="black" fill-opacity="0.2" />
                    </svg>

                </button>
                <button
                    class="grow shrink self-stretch px-1 my-auto whitespace-nowrap min-h-[32px] rounded-[32px] text-black text-opacity-50 w-[26px]"
                    aria-label="Next page">
                    <svg width="32" height="33" viewBox="0 0 32 33" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path
                            d="M13.9844 10.5L19.9844 16.5L13.9844 22.5L12.5781 21.0938L17.1719 16.5L12.5781 11.9062L13.9844 10.5Z"
                            fill="black" fill-opacity="0.54" />
                    </svg>

                </button>
            </footer>
        </article>
    </div>
</template>