<template>
    <button
          id="btn-อนุมัติ (1)"
          class="btn-อนุมัติ (1) bg-green text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
      <slot>อนุมัติ (1)</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnApprove1',
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