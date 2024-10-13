<template>
    <button
          id="btn-พิมพ์"
          class="btn-พิมพ์ bg-navyNormal text-white rounded-[6px] h-[40px] w-[90px] flex items-center text-[14px] font-thin"
        >
          <svg
            class="mr-[5.5px] ml-[16px]"
            xmlns="http://www.w3.org/2000/svg"
            width="20"
            height="18"
            viewBox="0 0 20 18"
            fill="none"
          >
            <path
              d="M14.5833 3.66667H5.41667V1.88889C5.41667 1.65314 5.51324 1.42705 5.68515 1.26035C5.85706 1.09365 6.09022 1 6.33333 1H13.6667C13.9098 1 14.1429 1.09365 14.3148 1.26035C14.4868 1.42705 14.5833 1.65314 14.5833 1.88889V3.66667ZM15.5 10.7778H4.5V17H15.5V10.7778Z"
              fill="white"
            />
            <path
              d="M15.5 13.4445H17.3333C17.5764 13.4445 17.8096 13.3509 17.9815 13.1842C18.1534 13.0175 18.25 12.7914 18.25 12.5556V4.55564C18.25 4.31989 18.1534 4.0938 17.9815 3.9271C17.8096 3.7604 17.5764 3.66675 17.3333 3.66675H2.66667C2.42355 3.66675 2.19039 3.7604 2.01849 3.9271C1.84658 4.0938 1.75 4.31989 1.75 4.55564V12.5556C1.75 12.7914 1.84658 13.0175 2.01849 13.1842C2.19039 13.3509 2.42355 13.4445 2.66667 13.4445H4.5"
              stroke="white"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M14.5833 7.22222H12.75M14.5833 3.66667H5.41667V1.88889C5.41667 1.65314 5.51324 1.42705 5.68515 1.26035C5.85706 1.09365 6.09022 1 6.33333 1H13.6667C13.9098 1 14.1429 1.09365 14.3148 1.26035C14.4868 1.42705 14.5833 1.65314 14.5833 1.88889V3.66667ZM15.5 10.7778H4.5V17H15.5V10.7778Z"
              stroke="white"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        <slot>พิมพ์</slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'BtnPrint',
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