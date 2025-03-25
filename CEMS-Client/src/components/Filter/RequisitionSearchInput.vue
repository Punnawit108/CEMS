<script setup lang="ts">
defineProps<{
  modelValue: string
  loading?: boolean
}>()

defineEmits<{
  'update:modelValue': [value: string]
}>()
</script>

<template>
  <div class="h-[32px] min-w-[200px] flex-1">
    <form class="grid" @submit.prevent>
      <label for="SearchRqName" class="py-0.5 text-[14px] text-black text-start">ค้นหารายการเบิก</label>
      <div class="relative h-[32px] w-full justify-center items-center">
        <div class="absolute left-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
          <svg width="19" height="20" viewBox="0 0 19 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M12.6629 13.1759L17 17.5M14.5 8.75C14.5 12.2017 11.7017 15 8.25 15C4.79822 15 2 12.2017 2 8.75C2 5.29822 4.79822 2.5 8.25 2.5C11.7017 2.5 14.5 5.29822 14.5 8.75Z" 
              stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
          </svg>
        </div>
        <input 
          id="SearchRqName"
          type="text"
          :value="modelValue"
          @input="$emit('update:modelValue', ($event.target as HTMLInputElement).value)"
          class="appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-9"
          placeholder="ชื่อรายการเบิก"
          :disabled="loading"
        />
      </div>
    </form>
  </div>
</template>