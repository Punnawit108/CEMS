<template>
    <button
          id="btn-ค้นหา"
          class="btn-ค้นหา bg-redNormal text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
      <slot>ค้นหา</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnSearch',
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