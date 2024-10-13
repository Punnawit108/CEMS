<template>
    <button
          id="btn-บันทึก"
          class="btn-บันทึก bg-grayNormal text-black rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
      <slot>บันทึก</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnSave',
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