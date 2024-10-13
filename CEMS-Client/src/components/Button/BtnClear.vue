<template>
    <button
          id="btn-ล้างคำค้นหา"
          class="btn-ล้างคำค้นหา bg-white border-2 border-redNormal text-redNormal rounded-[6px] h-[32px] w-[111px] text-[14px] font-thin"
        >
      <slot>ล้างคำค้นหา</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnClear',
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