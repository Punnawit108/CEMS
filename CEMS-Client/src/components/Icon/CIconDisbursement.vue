<script setup lang="ts">defineProps<{
    size?: number;
}>();
</script>

<template>
    <div>
        <svg :class="`w-[${size}px] h-[${size}px] text-gray-800 dark:text-white`" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
            <path stroke="red" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5"
                d="M17.651 7.65a7.131 7.131 0 0 0-12.68 3.15M18.001 4v4h-4m-7.652 8.35a7.13 7.13 0 0 0 12.68-3.15M6 20v-4h3" />
            <g transform="translate(12, 12) scale(0.4) translate(-12, -12) ">
                <path stroke="red" stroke-linecap="round" stroke-linejoin="round" stroke-width="3"
                    d="M8 17.345a4.76 4.76 0 0 0 2.558 1.618c2.274.589 4.512-.446 4.999-2.31.487-1.866-1.273-3.9-3.546-4.49-2.273-.59-4.034-2.623-3.547-4.488.486-1.865 2.724-2.899 4.998-2.31.982.236 1.87.793 2.538 1.592m-3.879 12.171V21m0-18v2.2" />
            </g>
        </svg>
    </div>
</template>