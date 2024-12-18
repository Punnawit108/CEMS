<script setup lang="ts">
/**
* ชื่อไฟล์: SingleDatePicker.vue
* คำอธิบาย: Component สำหรับแสดงปฏิทินในระบบ
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 17 ธันวาคม 2567
*/
import { ref, computed } from 'vue';
import { ChevronLeft, ChevronRight, Calendar } from 'lucide-vue-next';

interface Props {
  modelValue?: Date;
  confirmedDate?: Date;
  placeholder?: string;
  disabled?: boolean;
}

interface Emits {
  (e: 'update:modelValue', value: Date): void;
  (e: 'confirm', value: Date): void;
  (e: 'cancel'): void;
}

const props = withDefaults(defineProps<Props>(), {
  modelValue: () => new Date(),
  confirmedDate: () => new Date(),
  placeholder: 'Select date',
  disabled: false
});

const emit = defineEmits<Emits>();

const selectedDate = computed({
  get: () => props.modelValue,
  set: (value: Date) => emit('update:modelValue', value)
});

const showPicker = ref(false);
const currentMonth = ref(new Date(props.modelValue));
const tempSelectedDate = ref(props.modelValue);

const months = Array.from({ length: 12 }, (_, i) => {
  return new Date(2000, i).toLocaleString('default', { month: 'short' });
});

const years = computed(() => {
  const currentYear = new Date().getFullYear();
  return Array.from(
    { length: currentYear - 1901 + 1 },
    (_, i) => 1901 + i
  ).reverse();
});

const formatDate = (date: Date): string => {
  return `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
};

const getDaysInMonth = (date: Date): number => {
  return new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
};

const getFirstDayOfMonth = (date: Date): number => {
  return new Date(date.getFullYear(), date.getMonth(), 1).getDay();
};

const handleMonthChange = (offset: number): void => {
  const newMonth = new Date(currentMonth.value);
  newMonth.setMonth(newMonth.getMonth() + offset);
  currentMonth.value = newMonth;
};

const handleMonthSelect = (event: Event): void => {
  const target = event.target as HTMLSelectElement;
  const newDate = new Date(currentMonth.value);
  newDate.setMonth(months.indexOf(target.value));
  currentMonth.value = newDate;
};

const handleYearSelect = (event: Event): void => {
  const target = event.target as HTMLSelectElement;
  const newDate = new Date(currentMonth.value);
  newDate.setFullYear(parseInt(target.value));
  currentMonth.value = newDate;
};

const handleDateSelect = (day: number): void => {
  const newDate = new Date(currentMonth.value);
  newDate.setDate(day);
  tempSelectedDate.value = newDate;
};

const handleConfirm = () => {
  selectedDate.value = tempSelectedDate.value;
  emit('confirm', tempSelectedDate.value);
  showPicker.value = false;
};

const handleCancel = () => {
  tempSelectedDate.value = props.confirmedDate;
  emit('cancel');
  showPicker.value = false;
};

interface CalendarDay {
  value: number;
  type: 'prev' | 'current' | 'next';
  isSelected: boolean;
}

const calendarDays = computed((): CalendarDay[] => {
  const days: CalendarDay[] = [];
  const daysInMonth = getDaysInMonth(currentMonth.value);
  const firstDay = getFirstDayOfMonth(currentMonth.value);
  const prevMonthDays = new Date(
    currentMonth.value.getFullYear(),
    currentMonth.value.getMonth(),
    0
  ).getDate();

  // Previous month days
  for (let i = firstDay - 1; i >= 0; i--) {
    days.push({
      value: prevMonthDays - i,
      type: 'prev',
      isSelected: false
    });
  }

  // Current month days
  for (let i = 1; i <= daysInMonth; i++) {
    const isSelected =
      i === tempSelectedDate.value.getDate() &&
      currentMonth.value.getMonth() === tempSelectedDate.value.getMonth() &&
      currentMonth.value.getFullYear() === tempSelectedDate.value.getFullYear();

    days.push({
      value: i,
      type: 'current',
      isSelected
    });
  }

  // Next month days
  const remainingCells = 42 - days.length;
  for (let i = 1; i <= remainingCells; i++) {
    days.push({
      value: i,
      type: 'next',
      isSelected: false
    });
  }

  return days;
});
</script>

<template>
  <div class="relative h-[32px] w-[200px] justify-center items-center">
    <input type="text"
      class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-black border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8"
      :value="props.confirmedDate ? formatDate(props.confirmedDate) : ''" :placeholder="placeholder" readonly :disabled="disabled"
      @click="showPicker = !showPicker" />
    <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
      <Calendar class="w-4 h-4 text-black" />
    </div>

    <div v-if="showPicker" class="absolute mt-1 bg-white border rounded shadow-lg p-2 z-50 w-[250px]">
      <div class="flex justify-between items-center mb-2">
        <ChevronLeft class="h-4 w-4 cursor-pointer text-gray-600 hover:text-gray-800" @click="handleMonthChange(-1)" />
        <div class="flex gap-1 text-sm">
          <select class="border rounded px-1 py-0.5 text-sm cursor-pointer hover:border-gray-400"
            :value="months[currentMonth.getMonth()]" @change="handleMonthSelect">
            <option v-for="month in months" :key="month">{{ month }}</option>
          </select>
          <select class="border rounded px-1 py-0.5 text-sm cursor-pointer hover:border-gray-400"
            :value="currentMonth.getFullYear()" @change="handleYearSelect">
            <option v-for="year in years" :key="year">{{ year }}</option>
          </select>
        </div>
        <ChevronRight class="h-4 w-4 cursor-pointer text-gray-600 hover:text-gray-800" @click="handleMonthChange(1)" />
      </div>

      <div class="grid grid-cols-7 gap-0.5 text-sm">
        <div v-for="day in ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส']" :key="day"
          class="text-center font-medium p-1 text-gray-600">
          {{ day }}
        </div>
        <div v-for="(day, index) in calendarDays" :key="index" @click="day.type === 'current' && handleDateSelect(day.value)"
          :class="[
            'p-1 text-center text-sm',
            day.type === 'current' ? 'cursor-pointer' : 'text-gray-300',
            day.type === 'current' && day.isSelected ? 'bg-blue-500 text-white rounded-sm' : '',
            day.type === 'current' && !day.isSelected ? 'hover:bg-gray-100 rounded-sm' : ''
          ]">
          {{ day.value }}
        </div>
      </div>

      <div class="flex justify-end gap-1 mt-2">
        <button class="px-2 py-1 text-sm border rounded hover:bg-gray-50" @click="handleCancel">
          ยกเลิก
        </button>
        <button class="px-2 py-1 text-sm bg-blue-500 text-white rounded hover:bg-blue-600" @click="handleConfirm">
          ตกลง
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.custom-select {
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  background-image: none;
}

.custom-select::-ms-expand {
  display: none;
}

select,
select option {
  background-color: white;
  color: #000000;
}

select:invalid,
select option[value=""] {
  color: #999999;
}

[hidden] {
  display: none;
}
</style>