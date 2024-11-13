<script setup lang="ts">
/**
* ชื่อไฟล์: Dropdown.vue
* คำอธิบาย: ไฟล์นี้เป็นComponentDropdown
* Input: -
* Output: -
* ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/
</script>
<template>
    <div class="flex flex-col items-center text-center ">
        <!-- content -->
        <div class="w-full h-screen border-[16px] flex justify-center items-center">
            <div>
                <!-- template dorpDown -->
                <form class="grid">
                    <label class="flex justify-start mb-1 text-black font-bold text-base">Label</label>
                    <div class="relative">
                        <select required
                            class="appearance-none flex justify-between w-[400px] bg-white border-solid border-[#B6B7BA] border-2 rounded py-2.5 px-4 focus:outline-none">
                            <option value="" disabled selected hidden class="placeholder">placeholder</option>
                            <option class="font-sm text-base" value="item">item1
                            </option>
                            <option class="font-sm text-base" value="item">item2</option>
                        </select>
                    
                        <!-- icon -->
                        <div class="absolute inset-y-0 right-0 flex items-center px-2 pointer-events-none">
                            <svg class="w-6 h-6" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <g id="SVGRepo_bgCarrier" stroke-width="0" />
                                <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round" />
                                <g id="SVGRepo_iconCarrier">
                                    <path fill-rule="evenodd" clip-rule="evenodd"
                                        d="M12.7071 14.7071C12.3166 15.0976 11.6834 15.0976 11.2929 14.7071L6.29289 9.70711C5.90237 9.31658 5.90237 8.68342 6.29289 8.29289C6.68342 7.90237 7.31658 7.90237 7.70711 8.29289L12 12.5858L16.2929 8.29289C16.6834 7.90237 17.3166 7.90237 17.7071 8.29289C18.0976 8.68342 18.0976 9.31658 17.7071 9.70711L12.7071 14.7071Z"
                                        fill="#B6B7BA" />
                                </g>
                            </svg>
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <!-- content -->
    </div>
</template>

<style scoped>
select,
select option {
    background-color: white;
    color: #000000;     

}

select:invalid,
select option[value=""] {
    color: #999999;
}

</style>

