<template>
    <button
          id="btn-ยกเลิก"
          class="btn-ยกเลิก bg-redNormal text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
      <slot>ยกเลิก</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnCancleRed',
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