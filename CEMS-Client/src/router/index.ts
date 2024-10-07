import { createWebHistory, createRouter } from 'vue-router'
import Icon from '../components/template/icon.vue';
const router = createRouter({
  history: createWebHistory(),
  routes : [
    { 
      path: '/', 
      name: 'home',
      component:  Icon
    },
  ]
})

export default router ;