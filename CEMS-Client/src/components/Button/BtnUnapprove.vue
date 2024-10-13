<template>
    <button
          id="btn-ไม่อนุมัติ"
          class="btn-ไม่อนุมัติ text-Content-Regular bg-redNormal text-white rounded-[6px] h-[40px] w-[95px] font-thin"
        >
    <slot>ไม่อนุมัติ</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnUnapprove',
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