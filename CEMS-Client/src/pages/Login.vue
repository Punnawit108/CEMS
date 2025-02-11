<!-- <script setup lang="ts">
/*
* ชื่อไฟล์: Login.vue
* คำอธิบาย: ไฟล์นี้แสดงหน้าเข้าสู่ระบบ
* ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 14 พฤศจิกายน 2567
*/
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import { useUserStore } from '../store/user';

const userStore = useUserStore();
const userData = ref(null);
const router = useRouter();

onMounted(() => {
    userStore.getLocalUsers();

})

const selectUser = (user: any) => {
    localStorage.setItem('user', JSON.stringify(user));
    router.push({ path: '/dashboard' });
};

</script>
<template>
    <div>
        <div>
            <dialog id="roleSelected" class="modal">
                <div class="modal-box">
                    <div class="modal-action flex justify-center items-center m-0 flex-col">
                        <select class="select select-bordered w-full max-w-xs mb-4" v-model="userData">
                            <option :value="null" disabled selected>Select User</option>
                            <option :value="user" v-for="user in userStore.users">{{ user.usrId + " " + user.usrFirstName + " " + user.usrLastName + " (" + user.usrRolName + ")" }}</option>
                        </select>
                        <button class="btn" @click="selectUser(userData)">ยืนยัน</button>
                    </div>
                </div>
            </dialog>
        </div>

        <div class="h-screen w-screen overflow-hidden">
            <main class="bg-[#FFFFFF] h-full">
                <div class="flex flex-col lg:flex-row h-full">

                    <div class="left basis-1/2 h-full flex justify-center items-center">
                        <div class="frame bg-[#FFF] w-full h-full ">
                            <div class="flex flex-col p-4 h-full">
                                <div class="col1 mr-4">
                                    <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/e4ee6b90239817772fda3e0280afcee6fccf893263e541c7d09bdf9fae7fbab6?placeholderIfAbsent=true&apiKey=963991dcf23f4b60964b821ef12710c5"
                                        alt="Company logo" class="w-full h-auto max-w-[450px] object-contain" />
                                </div>
                                <div
                                    class="col2 text-black text-base font-[500] leading-normal mt-[30px] ml-16 flex-grow">
                                    <p class="ml-14">
                                        ระบบเบิกค่าใช้จ่ายนี้จะเป็นเครื่องมือที่มีประโยชน์อย่างยิ่งสำหรับองค์กร
                                    </p>
                                    <p>ที่ต้องการจัดการเรื่องการเบิกค่าใช้จ่ายอย่างมีประสิทธิภาพ ลดความยุ่งยาก</p>
                                    <p>และเพิ่มความสะดวกสะบายในการทำงาน</p>
                                </div>
                                <div class="col3 ml-4 flex-grow flex items-center justify-center">
                                    <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/79484dda0518e08b70f0cc357d13bbce2ef31243ee3cf6ee0f38f2ef7e23eb63?placeholderIfAbsent=true&apiKey=9804936447ee44d9b9e8514cc347c2f4"
                                        width="1125" height="750" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="right lg:basis-1/2 flex justify-center items-center lg:ml-12 h-full">
                        <div class="flex flex-col items-center">
                            <div class="flex justify-center text-[#393C8A] text-[42px] font-[800] leading-normal ">
                                เข้าสู่ระบบ</div>
                            <div class="login">
                                <button onclick="roleSelected.showModal()"
                                    class="flex border-[#FF0000] border-[1px] rounded-[50px] bg-white w-[400px] h-[50px] justify-center items-center shrink-0 gap-2 mt-[39px]">
                                    <div class="google flex items-center mr-2">
                                        <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/32f25e6cc5e16af815ba918f90bbf9de73c55b345d40fb1d258f3575e687a563?placeholderIfAbsent=true&apiKey=ce9e005cbe534bedb4f28b1cd72e0e50"
                                            width="100" height="100" />
                                    </div>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </div>
    </div>
</template> -->

<script setup lang="ts">
/*
 * ชื่อไฟล์: Login.vue
 * คำอธิบาย: ไฟล์นี้แสดงหน้าเข้าสู่ระบบ
 * ชื่อผู้เขียน/แก้ไข: พรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 8 กุมภาพันธ์ 2567
 */
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useUserStore } from "../store/user";

const userStore = useUserStore();
const router = useRouter();
const username = ref("");
const password = ref("");

const handleLogin = async (e: Event) => {
  e.preventDefault();
  try {
    await userStore.loginUser(username.value, password.value);

    await userStore.getUserById(username.value);
    localStorage.setItem("user", JSON.stringify(userStore.user));
    router.push({ path: '/dashboard' });
  } catch (err) {
    alert("Username or Password wrong !");
  }
};
</script>
<template>
  <div>
    <div class="h-screen w-screen overflow-hidden">
      <main class="bg-[#FFFFFF] h-full">
        <div class="flex flex-col lg:flex-row h-full">
          <div class="left basis-1/2 h-full flex justify-center items-center">
            <div class="frame bg-[#FFF] w-full h-full">
              <div class="flex flex-col p-4 h-full">
                <div class="col1 mr-4">
                  <img
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/e4ee6b90239817772fda3e0280afcee6fccf893263e541c7d09bdf9fae7fbab6?placeholderIfAbsent=true&apiKey=963991dcf23f4b60964b821ef12710c5"
                    alt="Company logo"
                    class="w-full h-auto max-w-[450px] object-contain"
                  />
                </div>
                <div
                  class="col2 text-black text-base font-[500] leading-normal mt-[30px] ml-16 flex-grow"
                >
                  <p class="ml-14">
                    ระบบเบิกค่าใช้จ่ายนี้จะเป็นเครื่องมือที่มีประโยชน์อย่างยิ่งสำหรับองค์กร
                  </p>
                  <p>
                    ที่ต้องการจัดการเรื่องการเบิกค่าใช้จ่ายอย่างมีประสิทธิภาพ
                    ลดความยุ่งยาก
                  </p>
                  <p>และเพิ่มความสะดวกสะบายในการทำงาน</p>
                </div>
                <div
                  class="col3 ml-4 flex-grow flex items-center justify-center"
                >
                  <img
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/79484dda0518e08b70f0cc357d13bbce2ef31243ee3cf6ee0f38f2ef7e23eb63?placeholderIfAbsent=true&apiKey=9804936447ee44d9b9e8514cc347c2f4"
                    width="1125"
                    height="750"
                  />
                </div>
              </div>
            </div>
          </div>

          <div
            class="right lg:basis-1/2 flex justify-center items-center lg:ml-12 h-full flex-col"
          >
            <div class="w-[400px]">
              <div
                class="flex justify-center text-[#393C8A] text-[42px] font-[800] leading-normal"
              >
                เข้าสู่ระบบ
              </div>
              <div class="login" @submit="handleLogin">
                <form>
                  <label class="text-black font-bold">Username</label>
                  <br />
                  <input
                    type="text"
                    class="w-full rounded-md text-black"
                    v-model="username"
                  />
                  <div class="my-5"></div>
                  <label class="text-black font-bold">Password</label>
                  <br />
                  <input
                    type="password"
                    class="w-full rounded-md text-black"
                    v-model="password"
                  />
                  <button
                    type="submit"
                    class="w-full text-white bg-redNormal mt-5 rounded-md"
                  >
                    ยืนยัน
                  </button>
                </form>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>
