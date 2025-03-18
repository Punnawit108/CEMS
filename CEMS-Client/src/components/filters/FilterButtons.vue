<script setup lang="ts">
interface Props {
  loading?: boolean
}

const props = defineProps<Props>()

const emit = defineEmits<{
  'reset': []
  'search': []
}>()
</script>

<template>
  <div class="h-[32px]">
    
    <div class="grid grid-cols-2 gap-4">
      <button 
        type="button"
        class="bg-transparent hover:bg-gray-50 text-[#B67D12] rounded-md text-sm border border-[#B67D12] transition-colors h-[32px]"
        :disabled="loading"
        @click="$emit('reset')"
      >
        ล้าง
      </button>
      <button 
        type="button"
        class="bg-[#B67D12] hover:bg-[#9e6c0f] text-white rounded-md text-sm transition-colors h-[32px]"
        :disabled="loading"
        @click="$emit('search')"
      >
        ค้นหา
      </button>
    </div>
  </div>
</template>