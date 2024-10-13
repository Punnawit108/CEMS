<template>
    <button
          id="btn-ประเภทค่าเดินทาง"
          class="btn-ประเภทค่าเดินทาง bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[148px] text-[14px] font-thin"
        >
  <slot>ประเภทค่าเดินทาง</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnTransport',
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