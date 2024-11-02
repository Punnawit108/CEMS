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

<script setup lang="ts">
import { ref } from 'vue';

const fileInput = ref<HTMLInputElement | null>(null);
const selectedFile = ref<File | null>(null);
const previewUrl = ref<string | null>(null);

const MAX_WIDTH = 800;
const MAX_HEIGHT = 800;

const triggerFileInput = () => {
    fileInput.value?.click();
};

const handleFileChange = (event: Event) => {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
        const file = target.files[0];
        uploadFile(file);
    }
};

const handleDrop = (event: DragEvent) => {
    if (event.dataTransfer?.files.length) {
        uploadFile(event.dataTransfer.files[0]);
    }
};

const checkImageDimensions = (file: File): Promise<boolean> => {
    return new Promise((resolve) => {
        const img = new Image();
        img.onload = () => {
            URL.revokeObjectURL(img.src);
            resolve(img.width <= MAX_WIDTH && img.height <= MAX_HEIGHT);
        };
        img.src = URL.createObjectURL(file);
    });
};

const uploadFile = async (file: File) => {
    if (!['image/svg+xml', 'image/png', 'image/jpeg'].includes(file.type)) {
        alert('กรุณาอัปโหลดไฟล์ SVG, PNG หรือ JPG เท่านั้น');
        return;
    }

    const isValidSize = await checkImageDimensions(file);
    if (isValidSize) {
        selectedFile.value = file;
        previewUrl.value = URL.createObjectURL(file);
        // uploadToServer(file);
    } else {
        alert(`กรุณาอัปโหลดรูปภาพที่มีขนาดไม่เกิน ${MAX_WIDTH} x ${MAX_HEIGHT} พิกเซล`);
        // รีเซ็ตค่าเมื่อไฟล์ไม่ถูกต้อง
        selectedFile.value = null;
        previewUrl.value = null;
    }

    // uploadToServer(file);
};

// const uploadToServer = (file: File) => {
//   const formData = new FormData();
//   formData.append('file', file);
//
//   // fetch('/api/upload', {
//   //   method: 'POST',
//   //   body: formData
//   // }).then(response => {
//   //   // จัดการการตอบกลับจากเซิร์ฟเวอร์
//   // });
// };
</script>

<style scoped>
/* สไตล์เพิ่มเติมตามต้องการ */
</style>