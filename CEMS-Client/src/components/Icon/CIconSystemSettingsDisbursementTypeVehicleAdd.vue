<script setup lang="ts">
defineProps<{
    size?: number;
}>();
</script>

<template>
    <div>
        <svg :class="`w-${size ?? 24}px h-${size ?? 24}px text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
            <path stroke="red" stroke-linecap="round" stroke-width="2"
                d="M8 7V6a1 1 0 0 1 1-1h11a1 1 0 0 1 1 1v7a1 1 0 0 1-1 1h-1M3 18v-7a1 1 0 0 1 1-1h11a1 1 0 0 1 1 1v7a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1Zm8-3.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0Z" />
        </svg>
    </div>
</template>
