<!--  -->
<template>
    <button
          id="btn-เชิญผู้ใช้"
          class="btn-เชิญผู้ใช้ bg-redNormal text-white rounded-[6px] h-[40px] w-[95px] text-[14px] font-thin"
        >
  <slot>เชิญผู้ใช้</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnAddUser',
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