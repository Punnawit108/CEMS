import { createWebHistory, createRouter } from 'vue-router'
import Content from '../components/template/Content.vue';
import Button from "../components/template/UseButton.vue";



const router = createRouter({
  history: createWebHistory(),
  routes : [
    { 
      path: '/icon', 
      name: 'icon',
      component:  Icon
    },
    { 
      path: '/', 
      name: 'content',
      component:  Content
   
    },{
      path: '/btn', 
      name: 'Button',
      component:  Button
    },
    { 
      path: '/nav', 
      name: 'nav',
      component:  Navbar
    },
    { 
      path: '/filter', 
      name: 'filter',
      component:  Filter
    },
  ]
})

export default router ;