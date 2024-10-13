<template>
    <button
          id="btn-อนุมัติ"
          class="btn-อนุมัติ bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
      <slot>อนุมัติ</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnApprove',
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