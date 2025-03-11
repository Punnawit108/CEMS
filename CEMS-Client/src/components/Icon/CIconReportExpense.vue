<script setup lang="ts">defineProps<{
    size?: number;
}>();
</script>

<template>
    <div>
        <svg :class="`w-[${size}px] h-[${size}px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
            <path stroke="red" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M10 3v4a1 1 0 0 1-1 1H5m4 10v-2m3 2v-6m3 6v-3m4-11v16a1 1 0 0 1-1 1H6a1 1 0 0 1-1-1V7.914a1 1 0 0 1 .293-.707l3.914-3.914A1 1 0 0 1 9.914 3H18a1 1 0 0 1 1 1Z" />
        </svg>
    </div>
</template>