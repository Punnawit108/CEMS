import { createWebHistory, createRouter } from 'vue-router'
import Content from '../components/template/content.vue';
import Button from "../components/UseButton.vue";



const router = createRouter({
  history: createWebHistory(),
  routes : [
    { 
      path: '/', 
      name: 'home',
      component:  Content
   
    },{
      path: '/btn', 
      name: 'Button',
      component:  Button
    },
  ]
})

export default router ;