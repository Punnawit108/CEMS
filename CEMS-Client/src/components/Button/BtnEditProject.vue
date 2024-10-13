<template>
    <button
          class="btn- แก้ไขโครงการ bg-yellow text-white rounded-[6px] h-[40px] w-[125px] flex items-center text-[14px] font-thin"
        >
        <svg class="mr-[4px] ml-[10px]" xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 20 20" fill="none">
          <path d="M19.0218 0.949768C18.3848 0.340177 17.5377 0 16.6566 0C15.7755 0 14.9284 0.340177 14.2914 0.949768L0 15.2625V20H4.73045L19.0218 5.68729C19.6482 5.05857 20 4.20668 20 3.31853C20 2.43037 19.6482 1.57848 19.0218 0.949768ZM3.69536 17.496H2.50024V16.2991L13.0454 5.73903L14.2405 6.9351L3.69536 17.496Z" fill="white"/>
        </svg>
  <slot>แก้ไขโครงการ</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnEditProject',
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