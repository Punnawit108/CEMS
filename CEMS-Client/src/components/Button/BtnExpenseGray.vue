<template>
    <button
          id="btn-ประเภทค่าใช้จ่าย"
          class="btn-ประเภทค่าใช้จ่าย bg-white border-2 border-grayNormal text-grayNormal rounded-[6px] h-[40px] w-[148px] text-[14px] font-thin"
        >
    <slot>ประเภทค่าใช้จ่าย</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnExpenseGray',
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