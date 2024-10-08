import { createWebHistory, createRouter } from 'vue-router'
import Icon from '../components/template/icon.vue';
import Content from '../components/template/content.vue';

const router = createRouter({
  history: createWebHistory(),
  routes : [
    { 
      path: '/', 
      name: 'home',
      component:  Content
    },
  ]
})

export default router ;