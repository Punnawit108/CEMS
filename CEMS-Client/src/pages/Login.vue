<script setup lang="ts">

</script>

<template>
    <div class="h-screen w-screen overflow-hidden">
        <main class="bg-[#FFDCDC] h-full">                                       
            <div class="flex flex-col lg:flex-row h-full">
                
                <div class="left basis-1/2 h-full flex justify-center items-center mt-3.5">
                    <div class="frame bg-[#FFF] w-full h-full border-t-[10px] border-r-[10px] flex-shrink-0 rounded-tr-[300px] overflow-hidden">
                        <div class="flex flex-col p-4 h-full">
                            <div class="col1 mr-4">
                                <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/e4ee6b90239817772fda3e0280afcee6fccf893263e541c7d09bdf9fae7fbab6?placeholderIfAbsent=true&apiKey=963991dcf23f4b60964b821ef12710c5"
                                alt="Company logo"
                                class="w-full h-auto max-w-[450px] object-contain"
                                />
                            </div>
                            <div class="col2 text-black text-base font-[500] leading-normal mt-4 ml-16 flex-grow">
                                <p class="ml-14">ระบบเบิกค่าใช้จ่ายนี้จะเป็นเครื่องมือที่มีประโยชน์อย่างยิ่งสำหรับองค์กร</p>
                                <p>ที่ต้องการจัดการเรื่องการเบิกค่าใช้จ่ายอย่างมีประสิทธิภาพ ลดความยุ่งยาก</p>
                                <p>และเพิ่มความสะดวกสะบายในการทำงาน</p> 
                            </div>
                            <div class="col3 ml-4 flex-grow flex items-center justify-center">
                                <img src="https://s3-alpha-sig.figma.com/img/3e23/ecb2/5e0d67a1f4b7121ca3c336fa2ba0864a?Expires=1727654400&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4&Signature=gMrzr0giBBhS50p4sUI92iVgcnTe5mXyhC73ijL9BE6ng7NxV9-~TDlg0F4Zuv~rGrEeO-y-GmrJYBZXGiLeuQpAR3mpyrO3xbFLCIsvyUa9kwS4B9Ts4e3w4pLg-q6ejhP~zSJ7yQbM79qllJ7aCl0K7VvuKdwzA7LtctORNdgkTDxJm5T1iq-6L3gfTbtOkhDauTthalWC1L8hc528gRoCLwDlPRNBTBcOzjkhSOaUhMIb~iskRn1tyyVbav9ZU4Eg2ORf0L-6VR5kq7GKhKwgOVTl6zN-Oph9aigRhfYPitgz0OgrdafmTOCQGFxk1LsdXCqTjDhaoGgQcyIPxw__" alt="">
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="right lg:basis-1/2 flex justify-center items-center lg:ml-12 h-full">
                    <div class="flex flex-col items-center">
                        <div class="flex justify-center text-[#393C8A] text-4xl font-[600] leading-normal mb-6">ระบบเบิกค่าใช้จ่าย</div>
                        <div>
                            <button class="flex border-[#FF0000] border-[1px] rounded-[30px] bg-white w-96 h-12 justify-center items-center shrink-0 gap-2">
                                <div class="flex items-center mr-3">
                                    <img src="https://s3-alpha-sig.figma.com/img/5c54/d19d/0ad83ee06d41892ec1de34449b1752e2?Expires=1727654400&amp;Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4&amp;Signature=B8V5vnCnz-lu6H1UkpJ70oW0TZEtr0rIqaBQvIdWVSoTz3Jj-W3rpunq4YjiTsXA6m8JdQ711Hf0KJhIYwKWpg-1IYbRbcP9CGhGKP0pQWX0qpdbKjfcSlJIu5GpTpjnmtIkwMNaB6gjCTw0oRvcRGiD3wgZ4CjAfdKt2QPEuU-KJfR-rmQLjP1pEy28bofD9u4I8-KwqNQK-WcnOLKE6kAI8csf8Cti1drOOLaXWh7IwAyDpnAfMpnMZ~BMoDB3JQ9t9-cWSc1uKDk0hVJkCdeTFjAahZ9nu0sqhpmQOfr36bPZDH4temCLGFR40VyvA9EVuw4Zbo-RPXPyf3vEaQ__" 
                                    class="w-[30.993px] h-[33px]" alt="">
                                </div>
                                <router-link to="/dashboard"><div class=" text-black text-lg font-[500] leading-normal flex justify-center items-center">เข้าสู่ระบบด้วย Google</div></router-link>
                            </button>
                        </div>
                    </div>
                </div>
              </div>
        </main>
    </div>
</template>
