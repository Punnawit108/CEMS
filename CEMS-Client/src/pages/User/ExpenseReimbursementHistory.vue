<script setup lang="ts">

</script>
<!-- path for test = /approval/history -->
<template>
    <!-- content -->
    <div class="content">
        <div class="filter flex flex-nowrap ">
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

        <div class="วันที่เบิก mr-6">
            <!-- Filter วันที่เบิก -->
            <div class="h-[32px] w-[266px]">
                <form class="grid">
                    <label for="Calendar" class="py-0.5 text-[14px] text-black text-start">วันที่เบิก</label>
                    <div class="relative h-[32px] w-[266px]  justify-center items-center">

                        <input type="text" id="Calendar"
                            class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none pl-4"
                            placeholder="01/01/2567-31/12/2567" />

                        <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
                            <svg width="19" height="20" viewBox="0 0 19 20" fill="none"
                                xmlns="http://www.w3.org/2000/svg">
                                <path
                                    d="M3.31847 16.72C2.94262 16.72 2.62905 16.5998 2.37775 16.3596C2.12646 16.1193 2.00054 15.8196 2 15.4602V5.49922C2 5.1404 2.12592 4.84087 2.37775 4.60062C2.62959 4.36037 2.94316 4.23999 3.31847 4.23947H4.76176V2.5H5.64047V4.23947H11.4773V2.5H12.2932V4.23947H13.7365C14.1118 4.23947 14.4253 4.35985 14.6772 4.60062C14.929 4.84139 15.0547 5.14119 15.0541 5.5V9.20593C15.0541 9.31721 15.0152 9.41004 14.9374 9.4844C14.8597 9.55876 14.7626 9.59594 14.6462 9.59594C14.5298 9.59594 14.4327 9.55876 14.3549 9.4844C14.2771 9.41004 14.2382 9.31721 14.2382 9.20593V8.61934H2.81588V15.4602C2.81588 15.5798 2.8681 15.6898 2.97253 15.7902C3.07696 15.8905 3.192 15.9405 3.31765 15.9399H8.56785C8.68425 15.9399 8.78134 15.9771 8.85912 16.0515C8.93691 16.1258 8.9758 16.2187 8.9758 16.33C8.9758 16.4412 8.93691 16.5341 8.85912 16.6084C8.78134 16.6828 8.68425 16.72 8.56785 16.72H3.31847ZM13.7365 17.5C12.8276 17.5 12.0563 17.1973 11.4226 16.592C10.7895 15.9857 10.4729 15.2483 10.4729 14.3799C10.4729 13.5114 10.7895 12.7743 11.4226 12.1685C12.0558 11.5627 12.8268 11.2597 13.7357 11.2597C14.6445 11.2597 15.4158 11.5627 16.0495 12.1685C16.6832 12.7743 17 13.5114 17 14.3799C17 15.2483 16.6832 15.9857 16.0495 16.592C15.4158 17.1984 14.6448 17.501 13.7365 17.5ZM15.0868 16.0975L15.5322 15.6716L14.0498 14.2535V12.1303H13.4224V14.5062L15.0868 16.0975Z"
                                    fill="black" />
                            </svg>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
    
    <div class="Table3.2ผู้ใช้-ประวัติเบิกค่าใช้จ่าย mt-12">
    <!-- Table3.2ผู้ใช้-ประวัติเบิกค่าใช้จ่าย -->
    
    <div class="w-full h-fit border-[1px] flex items-start">
        <!-- แถบหัวข้อ -->
        <table class="table-auto w-screen text-center text-black">
            <thead class="bg-[#F2F4F8]">
                <tr class="text-[16px] border-b-2 border-[#BBBBBB] ">
                    <th class="py-[11px] px-2 w-14 font-bold">ลำดับ</th>
                    <th class="py-[11px] px-2 text-start w-52 font-bold">ชื่อโครงการ</th>
                    <th class="py-[11px] px-5 text-start w-44 font-bold">ประเภทค่าใช้จ่าย</th>
                    <th class="py-[11px] px-2 text-end w-20 font-bold">วันที่เบิก</th>
                    <th class="py-[11px] px-2 text-end w-40 font-bold">จำนวนเงิน(บาท)</th>
                    <th class="py-[11px] px-2 text-center w-32 font-bold">สถานะ</th>
                    <th class="py-[11px] px-2 text-center w-24 font-bold">รายละเอียด</th>
                </tr>
            </thead>
            <!-- ข้อมูลชุดที่ 1 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB] ">
                <th class="py-[12px] px-2 ">1</th>
                <th class="py-[12px] px-2 text-start truncate overflow-hidden"
                    style="max-width: 200px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;"
                    title="กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Elegant">
                    กระชับมิตรความสัมพันธ์ในองค์กรทีม 4 Eleant
                </th>
                <th class="py-[12px] px-5 text-start font-[100]">ค่าเดินทาง</th>
                <th class="py-[12px] px-2 text-end ">08/10/2567</th>
                <th class="py-[12px] px-2 text-end ">200.00</th>
                <th class="py-[12px] px-2"> 
                    <div class="ml-10 rounded-[28px] w-[92px] h-[24px] bg-green text-white text-Sarabun-Reqular12 grid justify-items-center flex items-center">อนุมัติแล้ว</div>
                </th>
                <th class="py-[12px] px-2 flex justify-center">
                    <svg xmlns="http://www.w3.org/2000/svg" width="21" height="24" viewBox="0 0 21 24" fill="none">
                    <path d="M12.0778 6.35302V0H5.72474C5.1631 0 4.62446 0.223112 4.22732 0.620253C3.83018 1.01739 3.60706 1.55603 3.60706 2.11767V10.2439C4.79207 9.82444 6.07488 9.76596 7.29313 10.0759C8.51139 10.3857 9.61033 11.0501 10.4509 11.9848C11.2914 12.9195 11.8358 14.0826 12.015 15.3268C12.1943 16.571 12.0005 17.8404 11.458 18.9744L14.2816 21.7979C14.5117 22.0281 14.6783 22.2991 14.7813 22.5885H18.4308C18.9924 22.5885 19.5311 22.3654 19.9282 21.9683C20.3253 21.5711 20.5485 21.0325 20.5485 20.4709V8.4707H14.1954C13.6338 8.4707 13.0952 8.24759 12.698 7.85045C12.3009 7.45331 12.0778 6.91467 12.0778 6.35302ZM13.4895 6.35302V0.352946L20.1955 7.05892H14.1954C14.0082 7.05892 13.8287 6.98454 13.6963 6.85216C13.5639 6.71978 13.4895 6.54024 13.4895 6.35302ZM5.72474 21.1767C6.8344 21.1767 7.85935 20.8097 8.68384 20.1927L12.2839 23.7928C12.3494 23.8584 12.4272 23.9105 12.5129 23.946C12.5986 23.9816 12.6904 23.9999 12.7832 24C12.8759 24.0001 12.9678 23.9819 13.0535 23.9464C13.1392 23.911 13.2171 23.859 13.2827 23.7935C13.3484 23.7279 13.4004 23.6501 13.436 23.5645C13.4715 23.4788 13.4899 23.387 13.4899 23.2942C13.49 23.2015 13.4718 23.1096 13.4364 23.0239C13.4009 22.9382 13.349 22.8603 13.2834 22.7946L9.68338 19.1946C10.3498 18.3035 10.6958 17.2135 10.6656 16.1011C10.6353 14.9888 10.2305 13.9192 9.51664 13.0656C8.80279 12.212 7.82173 11.6244 6.73229 11.3978C5.64286 11.1712 4.50883 11.319 3.51385 11.8172C2.51886 12.3154 1.72116 13.1349 1.24991 14.1429C0.778659 15.1509 0.66145 16.2885 0.91726 17.3715C1.17307 18.4544 1.78692 19.4193 2.65942 20.1099C3.53192 20.8005 4.61199 21.1764 5.72474 21.1767ZM5.72474 19.765C4.78867 19.765 3.89094 19.3931 3.22904 18.7312C2.56713 18.0693 2.19528 17.1716 2.19528 16.2355C2.19528 15.2994 2.56713 14.4017 3.22904 13.7398C3.89094 13.0779 4.78867 12.706 5.72474 12.706C6.66081 12.706 7.55854 13.0779 8.22044 13.7398C8.88234 14.4017 9.2542 15.2994 9.2542 16.2355C9.2542 17.1716 8.88234 18.0693 8.22044 18.7312C7.55854 19.3931 6.66081 19.765 5.72474 19.765Z" fill="#B6B7BA"/>
                    </svg>
                </th>
            </tr>
            <!-- ข้อมูลว่าง 2 -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 3 -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 4 -->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 5-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 6-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 7-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 8-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 9-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 10-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 11-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 12-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 13-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- ข้อมูลว่าง 14-->
            <tr class=" text-[14px] ">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            
            <!-- ข้อมูลว่างอันล่างสุด 15 -->
            <tr class=" text-[14px] border-b-2 border-[#BBBBBB]">
                <th class="py-[12px] px-2 h-[47px]"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-5"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
                <th class="py-[12px] px-2"></th>
            </tr>
            <!-- footer -->
            <tr class=" text-[14px] border-b-1 border-[#BBBBBB]">
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
                <th class="py-[12px] text-end pr-7">1 of 10</th>
                <th class="py-[12px] flex justify-between text-[14px] font-bold">
                    <span class="ml-10 text-[#A0A0A0]">
                        < </span>
                            <span class="mr-6"> > </span>
                </th>
            </tr>
        </table>
    </div>
    </div>
    </div>
    <!-- content -->
</template>