<template>
    <button
          id="btn-นำจ่าย (1)"
          class="btn-นำจ่าย (1) bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
  <slot>นำจ่าย (1)</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnPay1',
    props: {
      type: {
        type: String,
        default: 'primary'
      },
    },
    computed: {
      buttonClasses() {
        return {
          [`btn-${this.type}`]: this.type,
        }
      }
    }
  }
  </script>