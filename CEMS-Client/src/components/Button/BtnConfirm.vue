<template>
    <button
          id="btn-ยืนยัน"
          class="btn-ยืนยัน bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
      <slot>ยืนยัน</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnConfirm',
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