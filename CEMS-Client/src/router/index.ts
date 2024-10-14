import { createWebHistory, createRouter } from 'vue-router'
import Icon from '../components/template/Icon.vue';
import Dashboard from '../pages/User/Dashboard.vue';
import ExpenseReimbursementList from '../pages/User/expenseReimbursementList.vue';
import CreateExpenseForm from '../pages/User/CreateExpenseForm.vue';
import Details from '../pages/User/Details.vue';
import EditExpenseForm from '../pages/User/EditExpenseForm.vue';
import ExpenseReimbursementHistory from '../pages/User/ExpenseReimbursementHistory.vue';
const router = createRouter({
  history: createWebHistory(),
  routes : [
    { 
      path: '/icon', 
      name: 'icon',
      component:  ExpenseReimbursementList
    },
    { 
      path: '/', 
      name: 'Dashboard',
      component:  Dashboard
      
    },
    {
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

    // ส่วนการเบิกค่าใช้จ่าย
    {
      path: '/disbursement',
      name: 'disbursement',
      redirect: '/disbursement/listWithdraw', //เมื่อกด การเบิกค่าใช้จ่ายแล้วไปหน้า รายการ
      meta: { breadcrumb: 'การเบิกค่าใช้จ่าย' }  //ไว้ใช้ทำ breadcrumb
    },

    //รายการเบิกค่าใช้จ่าย
    {
      path: '/disbursement/listWithdraw',
      name: 'listWithdraw',
      component: ExpenseReimbursementList,  
      meta: { 
        breadcrumb: 'รายการเบิกค่าใช้จ่าย' ,
        parent: 'disbursement'
      }
    },
    {
      path: '/disbursement/listWithdraw/createExpenseForm',
      name: 'createExpenseForm',
      component: CreateExpenseForm,
      meta: {
        breadcrumb: 'สร้างใบเบิกค่าใช้จ่าย',
        parent: 'listWithdraw'  
      } 
    },
    {
      path: '/disbursement/listWithdraw/detail/:id',
      name: 'listWithdrawDetail',
      component: Details,
      meta: {
        breadcrumb: 'รายละเอียด',
        parent: 'listWithdraw'  
      } 
    },
    {
      path: '/disbursement/listWithdraw/detail/:id/editExpenseForm',
      name: 'editExpenseForm',
      component: EditExpenseForm,
      meta: {
        breadcrumb: 'แก้ไขรายการเบิกค่าใช้จ่าย',
        parent: 'listWithdrawDetail'  
      } 
    },

    //ประวัติการเบิกค่าใช้จ่าย
    {
      path: '/disbursement/historyWithdraw',
      name: 'historyWithdraw',
      component: ExpenseReimbursementHistory,  
      meta: { 
        breadcrumb: 'ประวัติการเบิกค่าใช้จ่าย' ,
        parent: 'disbursement'
      }
    },
    {
      path: '/disbursement/historyWithdraw/detail/:id',
      name: 'historyWithdrawDetail',
      component: Details,
      meta: {
        breadcrumb: 'รายละเอียด',
        parent: 'historyWithdraw'  
      } 
    },



  ]
})

export default router ;