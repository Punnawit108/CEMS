import { createWebHistory, createRouter } from 'vue-router'
import Icon from '../components/template/Icon.vue';
import Content from '../components/template/Content.vue';

const router = createRouter({
  history: createWebHistory(),
  routes : [
    { 
      path: '/icon', 
      name: 'icon',
      component:  Content
    },
    { 
      path: '/', 
      name: 'content',
      component:  Content
      
    },{
      path: '/btn', 
      name: 'Button',
      component:  Icon
    },
    { 
      path: '/nav', 
      name: 'nav',
      component:  Icon
    },
    { 
      path: '/filter', 
      name: 'filter',
      component:  Icon
    },
  ]
})

export default router ;