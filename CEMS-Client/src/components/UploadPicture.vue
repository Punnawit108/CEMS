<script setup lang="ts">
/*
* ชื่อไฟล์: UploadPicture.vue
* คำอธิบาย: ไฟล์นี้เป็น Component สำหรับเรียกใช้ในหน้าที่จำเป็นต้องมีการอัปโหลดรูปภาพ
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/

import { ref } from 'vue';

// ตัวแปร ref สำหรับเก็บค่าต่างๆ
const fileInput = ref<HTMLInputElement | null>(null);     // อ้างอิงถึง input element ที่ใช้เลือกไฟล์
const selectedFile = ref<File | null>(null);             // เก็บไฟล์ที่ผู้ใช้เลือก
const previewUrl = ref<string | null>(null);             // URL สำหรับแสดงตัวอย่างรูปภาพ

// ค่าคงที่สำหรับกำหนดขนาดสูงสุดของรูปภาพ
const maxWidth = 800;                                   // ความกว้างสูงสุดที่ยอมรับ (พิกเซล)
const maxHeight = 800;                                  // ความสูงสูงสุดที่ยอมรับ (พิกเซล)

// ฟังก์ชันสำหรับเปิด file input dialog
const triggerFileInput = () => {
    fileInput.value?.click();                           // จำลองการคลิกที่ input file เมื่อผู้ใช้คลิกที่พื้นที่ drop zone
};

// ฟังก์ชันจัดการเมื่อมีการเลือกไฟล์ผ่าน file input
const handleFileChange = (event: Event) => {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
        const file = target.files[0];                   // เลือกไฟล์แรกที่ผู้ใช้เลือก
        uploadFile(file);                               // ส่งไฟล์ไปประมวลผล
    }
};

// ฟังก์ชันจัดการเมื่อมีการลากไฟล์มาวาง (drag & drop)
const handleDrop = (event: DragEvent) => {
    if (event.dataTransfer?.files.length) {
        uploadFile(event.dataTransfer.files[0]);        // ส่งไฟล์แรกที่ถูกลากมาวางไปประมวลผล
    }
};

// ฟังก์ชันตรวจสอบขนาดของรูปภาพ
const checkImageDimensions = (file: File): Promise<boolean> => {
    return new Promise((resolve) => {
        const img = new Image();
        img.onload = () => {
            URL.revokeObjectURL(img.src);               // คืน URL object เพื่อเป็นการจัดการหน่วยความจำ
            resolve(img.width <= maxWidth && img.height <= maxHeight);  // ตรวจสอบว่าขนาดไม่เกินที่กำหนด
        };
        img.src = URL.createObjectURL(file);            // สร้าง URL สำหรับรูปภาพเพื่อตรวจสอบขนาด
    });
};

// ฟังก์ชันหลักในการจัดการไฟล์ที่อัปโหลด
const uploadFile = async (file: File) => {
    // ตรวจสอบประเภทไฟล์ว่าเป็น SVG, PNG หรือ JPG
    if (!['image/svg+xml', 'image/png', 'image/jpeg'].includes(file.type)) {
        alert('กรุณาอัปโหลดไฟล์ SVG, PNG หรือ JPG เท่านั้น');
        return;
    }

    // ตรวจสอบขนาดรูปภาพ
    const isValidSize = await checkImageDimensions(file);
    if (isValidSize) {
        selectedFile.value = file;                      // เก็บไฟล์ที่ผ่านการตรวจสอบ
        previewUrl.value = URL.createObjectURL(file);   // สร้าง URL สำหรับแสดงตัวอย่าง
    } else {
        alert(`กรุณาอัปโหลดรูปภาพที่มีขนาดไม่เกิน ${maxWidth} x ${maxHeight} พิกเซล`);
        // รีเซ็ตค่าเมื่อไฟล์ไม่ถูกต้อง
        selectedFile.value = null;
        previewUrl.value = null;
    }
};

</script>

<template>
    <section class="flex relative flex-col leading-snug max-w-[536px]">
        <h2 class="z-0 text-base font-bold text-black max-md:max-w-full">
            แนบหลักฐานการเบิก
        </h2>
        <div class="flex z-0 mt-1 w-full bg-white rounded-md border border-solid border-zinc-400 min-h-[395px] max-md:max-w-full cursor-pointer relative"
            @click="triggerFileInput" @dragover.prevent @drop.prevent="handleDrop">
            <input type="file" ref="fileInput" @change="handleFileChange" accept="image/" style="display: none;" />
            <div v-if="!selectedFile"
                class="flex flex-col items-center justify-center absolute inset-0 text-sm text-[color:var(--,#B8B8B8)]">
                <img loading="lazy"
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/5da245b200f054a57a812257a8291e28aacdd77733a878e94699b2587a54360d?placeholderIfAbsent=true&apiKey=963991dcf23f4b60964b821ef12710c5"
                    alt="Upload icon" class="object-contain w-16 aspect-[1.1]" />
                <p class="mt-3">อัปโหลดไฟล์ที่นี่</p>
                <p class="mt-3">SVG, PNG หรือ JPG (MAX 800 800 px)</p>
            </div>
            <img v-else :src="previewUrl!" alt="Preview"
                class="max-w-full max-h-full object-contain absolute inset-0 m-auto" />
        </div>
    </section>
</template>

<style scoped>
/* สไตล์เพิ่มเติมตามต้องการ */
</style>
