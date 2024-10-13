/** @type {import('tailwindcss').Config} */

export default {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}", 'node_modules/flowbite/**/*.js'],
  
  themes: [],
  theme: {
    colors: {
        'green': "#12B669",

        'blue': "#1976D2",

        'black': "#000000",

        'white': "#FFFFFF",

        'sidebarhoverRed' : "#FFC0C0",

        'sidebarhoverGray' : "#F7F7F7",

        'redNormal': "#E1032B",

        'redDark': "#A90220",

        'yellow': "#FFBE40",

        'redError': "#D92C20",

        'redErrorDark': "#A32118",

        'grayDark': "#777777",

        'grayNormal': "#B6B7BA",

        'navyNormal': "#393C8A",

        'navyDark': "#2B2D68",
    },
    fontSize: {
      'Header-Bold-Navbar': '32px',
      'Content-Regular': '14px',
      'Content-Bold': '14px',
      'Header-Bold': '16px',
      'Breadcrumb Navigation' : '14px',
      'Sarabun-Reqular12' : '12px'
    },
    
  },

  plugins: [require("daisyui")],
};


