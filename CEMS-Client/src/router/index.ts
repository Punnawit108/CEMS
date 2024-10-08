import { createWebHistory, createRouter } from 'vue-router'
import Icon from '../components/template/icon.vue';
import Content from '../components/template/content.vue';
import Navbar from '../components/template/navbar.vue';
import Filter from '../components/template/Filter.vue';

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