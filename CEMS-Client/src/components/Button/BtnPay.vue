<template>
    <button
          id="btn-นำจ่าย"
          class="btn-นำจ่าย bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
  <slot>นำจ่าย</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnPay',
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