<script setup lang="ts">
/*
 * ชื่อไฟล์: SingleDatePicker.vue
 * คำอธิบาย: Component สำหรับแสดงปฏิทินในระบบ
 * ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
 * วันที่จัดทำ/แก้ไข: 22 มีนาคม 2568
 */
import { ref, computed, onMounted, onUnmounted, watch } from "vue";
import { ChevronLeft, ChevronRight, Calendar } from "lucide-vue-next";

interface Props {
  modelValue?: Date;
  confirmedDate?: Date;
  placeholder?: string;
  disabled?: boolean;
  isOpen?: boolean;
  minDate?: Date; // เพิ่ม prop สำหรับวันที่ต่ำสุดที่สามารถเลือกได้
}

interface Emits {
  (e: "update:modelValue", value: Date): void;
  (e: "confirm", value: Date): void;
  (e: "cancel"): void;
  (e: "update:isOpen", value: boolean): void;
}

const props = withDefaults(defineProps<Props>(), {
  modelValue: () => new Date(),
  confirmedDate: undefined,
  placeholder: "dd/mm/yyyy",
  disabled: false,
  isOpen: false,
  minDate: undefined,
});

const emit = defineEmits<Emits>();

const pickerRef = ref<HTMLElement | null>(null);
const tempSelectedDate = ref(props.modelValue);
const currentMonth = ref(new Date(props.modelValue));
const selectedDay = ref(props.modelValue.getDate());

// คำนวณวันที่ปัจจุบัน
const today = computed(() => {
  const now = new Date();
  now.setHours(0, 0, 0, 0);
  return now;
});

// คำนวณวันที่ย้อนหลัง 10 ปี
const defaultMinDate = computed(() => {
  const date = new Date(today.value);
  date.setFullYear(date.getFullYear() - 10);
  date.setDate(1);
  return date;
});

// คำนวณวันที่ต่ำสุดที่สามารถเลือกได้ (ใช้ minDate ที่กำหนดหรือค่าเริ่มต้น)
const effectiveMinDate = computed(() => {
  if (props.minDate) {
    const date = new Date(props.minDate);
    date.setHours(0, 0, 0, 0);
    return date;
  }
  return defaultMinDate.value;
});

// ตรวจสอบการเปลี่ยนแปลงของ minDate
watch(() => props.minDate, (newMinDate) => {
  if (newMinDate) {
    // ตรวจสอบว่าวันที่ที่เลือกอยู่น้อยกว่า minDate หรือไม่
    if (tempSelectedDate.value < newMinDate) {
      // ถ้าน้อยกว่า ให้เปลี่ยนเป็น minDate
      tempSelectedDate.value = new Date(newMinDate);
      currentMonth.value = new Date(newMinDate);
      selectedDay.value = tempSelectedDate.value.getDate();
    }
  }
});

// Handle outside clicks
const handleClickOutside = (event: MouseEvent) => {
  if (
    props.isOpen &&
    pickerRef.value &&
    !pickerRef.value.contains(event.target as Node)
  ) {
    handleCancel();
  }
};

onMounted(() => {
  document.addEventListener("mousedown", handleClickOutside);
});

onUnmounted(() => {
  document.removeEventListener("mousedown", handleClickOutside);
});

const selectedDate = computed({
  get: () => props.modelValue,
  set: (value: Date) => emit("update:modelValue", value),
});

const months = [
  "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.",
  "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค."
];

const toBuddhistYear = (year: number): number => {
  return year + 543;
};

const toCivilYear = (buddhistYear: number): number => {
  return buddhistYear - 543;
};

// สร้างตัวเลือกปีย้อนหลัง 10 ปีจากปีปัจจุบัน
const years = computed(() => {
  const currentYear = today.value.getFullYear();
  const years = [];
  for (let i = 0; i < 10; i++) {
    years.push(toBuddhistYear(currentYear - i));
  }
  return years;
});

const formatDate = (date: Date): string => {
  if (!date) return '';
  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const year = toBuddhistYear(date.getFullYear());
  return `${day}/${month}/${year}`;
};

const getDaysInMonth = (date: Date): number => {
  return new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
};

const getFirstDayOfMonth = (date: Date): number => {
  return new Date(date.getFullYear(), date.getMonth(), 1).getDay();
};

// ฟังก์ชันเช็คว่าวันที่อยู่ในช่วงที่อนุญาตหรือไม่
const isDateInAllowedRange = (date: Date): boolean => {
  const startOfDay = new Date(date);
  startOfDay.setHours(0, 0, 0, 0);
  return startOfDay >= effectiveMinDate.value && startOfDay <= today.value;
};

const handleMonthChange = (offset: number): void => {
  const newDate = new Date(currentMonth.value);
  newDate.setMonth(newDate.getMonth() + offset);

  const newYear = newDate.getFullYear();
  const newMonth = newDate.getMonth();
  const todayYear = today.value.getFullYear();
  const todayMonth = today.value.getMonth();

  if (offset > 0) {
    if (newYear < todayYear ||
      (newYear === todayYear && newMonth <= todayMonth)) {
      // เก็บวันที่เดิมไว้
      const currentDay = tempSelectedDate.value.getDate();
      currentMonth.value = newDate;

      // สร้างวันที่ใหม่
      const newSelectedDate = new Date(newDate);
      // ตรวจสอบว่าวันที่ยังอยู่ในเดือนนั้นหรือไม่
      const daysInNewMonth = getDaysInMonth(newDate);

      // ถ้าเดือนและปีเท่ากับปัจจุบัน ให้ใช้วันที่ไม่เกินวันที่ปัจจุบัน
      if (newDate.getFullYear() === today.value.getFullYear() &&
        newDate.getMonth() === today.value.getMonth()) {
        newSelectedDate.setDate(Math.min(currentDay, today.value.getDate()));
      } else {
        // ถ้าเป็นเดือนอื่น ใช้วันที่เดิมหรือวันสุดท้ายของเดือน
        newSelectedDate.setDate(Math.min(currentDay, daysInNewMonth));
      }

      if (isDateInAllowedRange(newSelectedDate)) {
        tempSelectedDate.value = newSelectedDate;
        selectedDay.value = newSelectedDate.getDate();
      }
    }
  } else {
    // ตรวจสอบว่าเดือนที่เลื่อนไปไม่น้อยกว่า effectiveMinDate
    const minDateYear = effectiveMinDate.value.getFullYear();
    const minDateMonth = effectiveMinDate.value.getMonth();

    if (newYear > minDateYear || (newYear === minDateYear && newMonth >= minDateMonth)) {
      // เก็บวันที่เดิมไว้
      const currentDay = tempSelectedDate.value.getDate();
      currentMonth.value = newDate;

      // สร้างวันที่ใหม่
      const newSelectedDate = new Date(newDate);
      // ตรวจสอบว่าวันที่ยังอยู่ในเดือนนั้นหรือไม่
      const daysInNewMonth = getDaysInMonth(newDate);

      // ถ้าเดือนและปีเท่ากับปัจจุบัน ให้ใช้วันที่ไม่เกินวันที่ปัจจุบัน
      if (newDate.getFullYear() === today.value.getFullYear() &&
        newDate.getMonth() === today.value.getMonth()) {
        newSelectedDate.setDate(Math.min(currentDay, today.value.getDate()));
      } else {
        // ถ้าเป็นเดือนอื่น ใช้วันที่เดิมหรือวันสุดท้ายของเดือน
        newSelectedDate.setDate(Math.min(currentDay, daysInNewMonth));
      }

      // ถ้าอยู่ในเดือนเดียวกับ minDate ให้ตรวจสอบว่าวันที่ไม่น้อยกว่า minDate
      if (newYear === minDateYear && newMonth === minDateMonth) {
        const minDateDay = effectiveMinDate.value.getDate();
        newSelectedDate.setDate(Math.max(minDateDay, newSelectedDate.getDate()));
      }

      if (isDateInAllowedRange(newSelectedDate)) {
        tempSelectedDate.value = newSelectedDate;
        selectedDay.value = newSelectedDate.getDate();
      }
    }
  }
};

const handleMonthSelect = (event: Event): void => {
  const target = event.target as HTMLSelectElement;
  const selectedMonthIndex = months.indexOf(target.value);
  const newDate = new Date(currentMonth.value);

  // ตรวจสอบว่าเดือนที่เลือกไม่เกินเดือนปัจจุบัน
  if (newDate.getFullYear() === today.value.getFullYear() &&
    selectedMonthIndex > today.value.getMonth()) {
    return;
  }

  // ตรวจสอบว่าเดือนที่เลือกไม่น้อยกว่า minDate
  const minDateYear = effectiveMinDate.value.getFullYear();
  const minDateMonth = effectiveMinDate.value.getMonth();

  if (newDate.getFullYear() === minDateYear && selectedMonthIndex < minDateMonth) {
    return;
  }

  // เก็บวันที่เดิมไว้
  const currentDay = tempSelectedDate.value.getDate();
  newDate.setMonth(selectedMonthIndex);

  if (isDateInAllowedRange(newDate)) {
    currentMonth.value = newDate;

    // สร้างวันที่ใหม่
    const newSelectedDate = new Date(newDate);
    // ตรวจสอบว่าวันที่ยังอยู่ในเดือนนั้นหรือไม่
    const daysInNewMonth = getDaysInMonth(newDate);

    // ถ้าเดือนและปีเท่ากับปัจจุบัน ให้ใช้วันที่ไม่เกินวันที่ปัจจุบัน
    if (newDate.getFullYear() === today.value.getFullYear() &&
      newDate.getMonth() === today.value.getMonth()) {
      newSelectedDate.setDate(Math.min(currentDay, today.value.getDate()));
    } else {
      // ถ้าเป็นเดือนอื่น ใช้วันที่เดิมหรือวันสุดท้ายของเดือน
      newSelectedDate.setDate(Math.min(currentDay, daysInNewMonth));
    }

    // ถ้าอยู่ในเดือนเดียวกับ minDate ให้ตรวจสอบว่าวันที่ไม่น้อยกว่า minDate
    if (newDate.getFullYear() === minDateYear && selectedMonthIndex === minDateMonth) {
      const minDateDay = effectiveMinDate.value.getDate();
      newSelectedDate.setDate(Math.max(minDateDay, newSelectedDate.getDate()));
    }

    if (isDateInAllowedRange(newSelectedDate)) {
      tempSelectedDate.value = newSelectedDate;
      selectedDay.value = newSelectedDate.getDate();
    }
  }
};

const handleYearSelect = (event: Event): void => {
  const target = event.target as HTMLSelectElement;
  const selectedYear = toCivilYear(parseInt(target.value));
  const newDate = new Date(currentMonth.value);

  // ตรวจสอบว่าปีที่เลือกไม่เกินปีปัจจุบัน
  if (selectedYear > today.value.getFullYear()) {
    return;
  }

  // ตรวจสอบว่าปีที่เลือกไม่น้อยกว่า minDate
  const minDateYear = effectiveMinDate.value.getFullYear();
  if (selectedYear < minDateYear) {
    return;
  }

  // เก็บวันที่เดิมไว้
  const currentDay = tempSelectedDate.value.getDate();
  newDate.setFullYear(selectedYear);

  // ถ้าเลือกปีเท่ากับปีปัจจุบัน แต่เดือนเกินเดือนปัจจุบัน
  if (selectedYear === today.value.getFullYear() &&
    newDate.getMonth() > today.value.getMonth()) {
    newDate.setMonth(today.value.getMonth());
  }

  // ถ้าเลือกปีเท่ากับปี minDate แต่เดือนน้อยกว่าเดือน minDate
  if (selectedYear === minDateYear && newDate.getMonth() < effectiveMinDate.value.getMonth()) {
    newDate.setMonth(effectiveMinDate.value.getMonth());
  }

  if (isDateInAllowedRange(newDate)) {
    currentMonth.value = newDate;

    // สร้างวันที่ใหม่
    const newSelectedDate = new Date(newDate);
    // ตรวจสอบว่าวันที่ยังอยู่ในเดือนนั้นหรือไม่
    const daysInNewMonth = getDaysInMonth(newDate);

    // ถ้าเดือนและปีเท่ากับปัจจุบัน ให้ใช้วันที่ไม่เกินวันที่ปัจจุบัน
    if (newDate.getFullYear() === today.value.getFullYear() &&
      newDate.getMonth() === today.value.getMonth()) {
      newSelectedDate.setDate(Math.min(currentDay, today.value.getDate()));
    } else {
      // ถ้าเป็นเดือนอื่น ใช้วันที่เดิมหรือวันสุดท้ายของเดือน
      newSelectedDate.setDate(Math.min(currentDay, daysInNewMonth));
    }

    // ถ้าอยู่ในปีและเดือนเดียวกับ minDate ให้ตรวจสอบว่าวันที่ไม่น้อยกว่า minDate
    if (selectedYear === minDateYear && newDate.getMonth() === effectiveMinDate.value.getMonth()) {
      const minDateDay = effectiveMinDate.value.getDate();
      newSelectedDate.setDate(Math.max(minDateDay, newSelectedDate.getDate()));
    }

    if (isDateInAllowedRange(newSelectedDate)) {
      tempSelectedDate.value = newSelectedDate;
      selectedDay.value = newSelectedDate.getDate();
    }
  }
};

const handleDateSelect = (day: number): void => {
  const newDate = new Date(currentMonth.value);
  newDate.setDate(day);
  newDate.setHours(0, 0, 0, 0);

  // ถ้าเป็นเดือนปัจจุบัน และวันที่เลือกเกินวันที่ปัจจุบัน
  if (newDate.getFullYear() === today.value.getFullYear() &&
    newDate.getMonth() === today.value.getMonth() &&
    day > today.value.getDate()) {
    newDate.setDate(today.value.getDate());
  }

  if (isDateInAllowedRange(newDate)) {
    tempSelectedDate.value = newDate;
    selectedDay.value = newDate.getDate();
  }
};

const handleInputClick = () => {
  if (!props.disabled) {
    emit("update:isOpen", !props.isOpen);
  }
};

const handleConfirm = () => {
  if (isDateInAllowedRange(tempSelectedDate.value)) {
    selectedDate.value = tempSelectedDate.value;
    emit("confirm", tempSelectedDate.value);
    emit("update:isOpen", false);
  }
};

const handleCancel = () => {
  tempSelectedDate.value = props.confirmedDate || new Date();
  selectedDay.value = tempSelectedDate.value.getDate();
  emit("cancel");
  emit("update:isOpen", false);
};

interface CalendarDay {
  value: number;
  type: "prev" | "current" | "next";
  isSelected: boolean;
  isDisabled: boolean;
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
      type: "prev",
      isSelected: false, isDisabled: true
    });
  }

  // Current month days
  for (let i = 1; i <= daysInMonth; i++) {
    const currentDate = new Date(
      currentMonth.value.getFullYear(),
      currentMonth.value.getMonth(),
      i
    );
    currentDate.setHours(0, 0, 0, 0);

    const isSelected = i === selectedDay.value &&
      currentMonth.value.getMonth() === tempSelectedDate.value.getMonth() &&
      currentMonth.value.getFullYear() === tempSelectedDate.value.getFullYear();

    days.push({
      value: i,
      type: "current",
      isSelected,
      isDisabled: !isDateInAllowedRange(currentDate)
    });
  }

  // Next month days
  const remainingCells = 42 - days.length;
  for (let i = 1; i <= remainingCells; i++) {
    days.push({
      value: i,
      type: "next",
      isSelected: false,
      isDisabled: true
    });
  }

  return days;
});
</script>

<template>
  <div class="relative h-[32px] w-[200px] justify-center items-center" ref="pickerRef">
    <!-- ปรับปรุง input style ใน template ของ SingleDatePicker.vue -->
    <input type="text"
      class="custom-select appearance-none text-sm flex justify-between w-full h-[32px] bg-white rounded-md border border-solid focus:outline-none focus:border-blue-500 pl-4 pr-8"
      :class="[
        $attrs.class || '',
        { 'border-black': !$attrs.class || (typeof $attrs.class === 'string' && !$attrs.class.includes('border-red-500')) }
      ]" :value="props.confirmedDate ? formatDate(props.confirmedDate) : ''" :placeholder="placeholder" readonly
      :disabled="disabled" @click="handleInputClick" />
    <div class="absolute right-2 top-1/2 transform -translate-y-1/2 pointer-events-none">
      <Calendar class="w-4 h-4 text-black" />
    </div>

    <div v-if="isOpen" class="absolute mt-1 bg-white border rounded shadow-lg p-2 z-50 w-[250px]">
      <div class="flex justify-between items-center mb-2">
        <ChevronLeft class="h-4 w-4 cursor-pointer text-gray-600 hover:text-gray-800" :class="{
          'opacity-50 cursor-not-allowed':
            currentMonth.getFullYear() === effectiveMinDate.getFullYear() &&
            currentMonth.getMonth() === effectiveMinDate.getMonth()
        }" @click="handleMonthChange(-1)" />
        <div class="flex gap-1 text-sm">
          <select class="border rounded px-1 py-0.5 text-sm cursor-pointer hover:border-gray-400"
            :value="months[currentMonth.getMonth()]" @change="handleMonthSelect">
            <option v-for="(month, index) in months" :key="month" :disabled="(currentMonth.getFullYear() === today.getFullYear() && index > today.getMonth()) ||
              (currentMonth.getFullYear() === effectiveMinDate.getFullYear() && index < effectiveMinDate.getMonth())
              ">
              {{ month }}
            </option>
          </select>
          <select class="border rounded px-1 py-0.5 text-sm cursor-pointer hover:border-gray-400"
            :value="toBuddhistYear(currentMonth.getFullYear())" @change="handleYearSelect">
            <option v-for="year in years" :key="year" :disabled="toCivilYear(year) > today.getFullYear() ||
              toCivilYear(year) < effectiveMinDate.getFullYear()
              ">
              {{ year }}
            </option>
          </select>
        </div>
        <ChevronRight class="h-4 w-4 cursor-pointer text-gray-600 hover:text-gray-800" :class="{
          'opacity-50 cursor-not-allowed':
            currentMonth.getFullYear() === today.getFullYear() &&
            currentMonth.getMonth() === today.getMonth()
        }" @click="handleMonthChange(1)" />
      </div>

      <div class="grid grid-cols-7 gap-0.5 text-sm">
        <div v-for="day in ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส']" :key="day"
          class="text-center font-medium p-1 text-gray-600">
          {{ day }}
        </div>
        <div v-for="(day, index) in calendarDays" :key="index"
          @click="!day.isDisabled && day.type === 'current' && handleDateSelect(day.value)" :class="[
            'p-1 text-center text-sm',
            day.type === 'current' && !day.isDisabled ? 'cursor-pointer' : 'text-gray-300',
            day.type === 'current' && day.isSelected ? 'bg-blue-500 text-white rounded-sm' : '',
            day.type === 'current' && !day.isSelected && !day.isDisabled ? 'hover:bg-gray-100 rounded-sm' : '',
            day.isDisabled ? 'cursor-not-allowed' : ''
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