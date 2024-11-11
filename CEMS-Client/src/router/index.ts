/**
* ชื่อไฟล์: index.ts
* คำอธิบาย: สำหรับเก็บ Route ที่สร้างไว้ทั้งหมด
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 10 พฤศจิกายน 2567
*/

import { createWebHistory, createRouter } from 'vue-router'
import dashboard from '../pages/User/Dashboard.vue';
import ExpenseReimbursementList from '../pages/User/ExpenseReimbursementList.vue';
import CreateExpenseForm from '../pages/User/CreateExpenseForm.vue';
import Details from '../pages/User/Details.vue';
import EditExpenseForm from '../pages/User/EditExpenseForm.vue';
import ExpenseReimbursementHistory from '../pages/User/ExpenseReimbursementHistory.vue';
import Notifications from '../pages/User/Notifications.vue';
import ProjectReport from '../pages/User/ProjectReport.vue';
import ExpenseReport from '../pages/User/ExpenseReport.vue';
import ApprovalHistory from '../pages/User/ApprovalHistory.vue';
import UserSetting from '../pages/Admin/UserSetting.vue';
import DetailUserSetting from '../pages/Admin/DetailUserSetting.vue';
import DisbursementApprover from '../pages/Admin/DisbursementApprover.vue';
import ExpenseManage from '../pages/Admin/ExpenseManage.vue';
import TravelManage from '../pages/Admin/TravelManage.vue';
import PaymentList from '../pages/Accountant/PaymentList.vue';
import PaymentHistory from '../pages/Accountant/PaymentHistory.vue';
import ApprovalList from '../pages/User/ApprovalList.vue';
import Login from '../pages/Login.vue'
//template
import icon from '../components/template/Icon.vue';
import DropDown from '../components/template/Dropdown.vue';
import Filter from '../components/template/Filter.vue';
import Progress from '../components/template/Progress.vue';
import Table from '../components/template/Table.vue';
import Popup from '../components/template/Popup.vue';
import UploadPicture from '../components/template/UploadPicture.vue';
import ForUseButton from '../components/template/ForUseButton.vue';
import ForUseStatus from '../components/template/ForUseStatus.vue';





const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/temp/icon',
      name: 'icon',
      component: icon
    },
    {
      path: '/temp/dropdown',
      name: 'dropdown',
      component: DropDown
    },
    {
      path: '/temp/filter',
      name: 'filter',
      component: Filter
    },
    {
      path: '/temp/progress',
      name: 'progress',
      component: Progress
    },
    {
      path: '/temp/table',
      name: 'table',
      component: Table
    },
    {
      path: '/temp/uploadpic',
      name: 'uploadpic',
      component: UploadPicture
    },
    {
      path: '/temp/table',
      name: 'table',
      component: Table
    },
    {
      path: '/temp/popup',
      name: 'popup',
      component: Popup
    },
    //button
    {
      path: '/temp/btn',
      name: 'btn',
      component: ForUseButton
    },
    //
    //StatusBudge
    {
      path: '/temp/Status',
      name: 'ForUseStatus',
      component: ForUseStatus
    },
    //
    // ผู้ใช้งานทั่วไป + ผู้มีสิทธิอนุมัติ
    {
      path: '/dashboard',
      name: 'dashboard',
      component: dashboard,
      meta: { breadcrumb: 'แดชบอร์ด' }
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
    },
    {
      path: '/',
      name: 'home',
      redirect: '/login'
    },
    // การเบิกค่าใช้จ่าย
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
        breadcrumb: 'รายการเบิกค่าใช้จ่าย',
        parent: 'disbursement'
      }
    },
    //สร้างใบเบิกค่าใช้จ่าย
    {
      path: '/disbursement/listWithdraw/createExpenseForm',
      name: 'createExpenseForm',
      component: CreateExpenseForm,
      meta: {
        breadcrumb: 'สร้างใบเบิกค่าใช้จ่าย',
        parent: 'listWithdraw'
      }
    },
    //รายละเอียดเบิกค่าใช้จ่าย
    {
      path: '/disbursement/listWithdraw/detail/:id',
      name: 'listWithdrawDetail',
      component: Details,
      meta: {
        breadcrumb: 'รายละเอียด',
        parent: 'listWithdraw'
      }
    },
    //แก้ไขใบเบิกค่าใช้จ่าย
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
        breadcrumb: 'ประวัติการเบิกค่าใช้จ่าย',
        parent: 'disbursement'
      }
    },
    //รายละเอียดประวัติการเบิกค่าใช้จ่าย
    {
      path: '/disbursement/historyWithdraw/detail/:id',
      name: 'historyWithdrawDetail',
      component: Details,
      meta: {
        breadcrumb: 'รายละเอียด',
        parent: 'historyWithdraw'
      }
    },
    //การแจ้งเตือน
    {
      path: '/notification',
      name: 'notification',
      component: Notifications,
      meta: { breadcrumb: 'การแจ้งเตือน' }
    },
    //รายงาน
    {
      path: '/report',
      name: 'report',
      redirect: '/report/project',
      meta: { breadcrumb: 'รายงาน' }
    },
    //รายงานโครงการ
    {
      path: '/report/project',
      name: 'reportProject',
      component: ProjectReport,
      meta: {
        breadcrumb: 'รายงานโครงการ',
        parent: 'report'
      }
    },
    //รายงานเบิกค่าใช้จ่าย
    {
      path: '/report/expense',
      name: 'reportExpense',
      component: ExpenseReport,
      meta: {
        breadcrumb: 'รายงานเบิกค่าใช้จ่าย',
        parent: 'report'
      }
    },
    //การอนุมัติ
    {
      path: '/approval',
      name: 'approval',
      redirect: '/approval/list',
      meta: { breadcrumb: 'การอนุมัติ' }
    },
    //รายการรออนุมัติ
    {
      path: '/approval/list',
      name: 'approvalList',
      component: ApprovalList,
      meta: {
        breadcrumb: 'รายการรออนุมัติ',
        parent: 'approval'
      }
    },
    //ประวัติการอนุมัติ
    {
      path: '/approval/history',
      name: 'approvalHistory',
      component: ApprovalHistory,
      meta: {
        breadcrumb: 'ประวัติการอนุมัติ',
        parent: 'approval'
      }
    },
    //รายละเอียดประวัติการอนุมัติ
    {
      path: '/approval/history/detail/:id',
      name: 'approvalHistoryDetail',
      component: Details,
      meta: {
        breadcrumb: 'รายละเอียด',
        parent: 'approvalHistory'
      }
    },
    //ผู้ดูแลระบบ
    //ตั้งค่าระบบ systemsettings
    {
      path: '/systemSettings',
      name: 'systemSettings',
      redirect: '/systemSettings/user',
      meta: { breadcrumb: 'ตั้งค่าระบบ' }
    },
    //ตั้งค่าผู้ใช้
    {
      path: '/systemSettings/user',
      name: 'systemSettingsUser',
      component: UserSetting,
      meta: {
        breadcrumb: 'ตั้งค่าผู้ใช้',
        parent: 'systemSettings'
      }
    },
    //รายละเอียดตั้งค่าผู้ใช้
    {
      path: '/systemSettings/user/detail/:id',
      name: 'systemSettingsUserDetail',
      component: DetailUserSetting,
      meta: {
        breadcrumb: 'รายละเอียด',
        parent: 'systemSettingsUser'
      }
    },
    //แก้ไขรายละเอียดตั้งค่าผู้ใช้
    {
      path: '/systemSettings/user/detail/:id/editUser',
      name: 'systemSettingsUserDetailEditUser',
      component: DetailUserSetting,   //อยากให้ทำเป็น fn เมื่อกดปุ่มแก้ไขแล้วแสดงหน้าเดิม แต่สามารถแก้ไขได้ 
      meta: {                         //ถ้าทำไม่ได้บอกทีม DEV ด้วย
        breadcrumb: 'แก้ไขรายละเอียดผู้ใช้',
        parent: 'systemSettingsUserDetail'
      }
    },
    //ตั้งค่าผู้อนุมัติการเบิกจ่าย
    {
      path: '/systemSettings/disbursementApprover',
      name: 'systemSettingsDisbursementApprover',
      component: DisbursementApprover,
      meta: {
        breadcrumb: 'ผู้อนุมัติการเบิกจ่าย',
        parent: 'systemSettings'
      }
    },
    //เพิ่มผู้อนุมัติการเบิกจ่าย
    {
      path: '/systemSettings/disbursementApprover/add',
      name: 'systemSettingsDisbursementApproverAdd',
      component: DisbursementApprover,
      meta: {
        breadcrumb: 'เพิ่มผู้อนุมัติการเบิกจ่าย',
        parent: 'systemSettingsDisbursementApprover'
      }
    },
    //ประเภทเบิกจ่าย
    {
      path: '/systemSettings/disbursementType',
      name: 'systemSettingsDisbursementType',
      redirect: '/systemSettings/disbursementType/expense',
      meta: {
        breadcrumb: 'ประเภทเบิกจ่าย',
        parent: 'systemSettings'
      }
    },
    //จัดการประเภทค่าใช้จ่าย
    {
      path: '/systemSettings/disbursementType/expense',
      name: 'systemSettingsDisbursementTypeExpense',
      component: ExpenseManage,
      meta: {
        breadcrumb: 'จัดการประเภทค่าใช้จ่าย',
        parent: 'systemSettingsDisbursementType'
      }
    },
    //เพิ่มประเภทค่าใช้จ่าย
    {
      path: '/systemSettings/disbursementType/expense/add',
      name: 'systemSettingsDisbursementTypeExpenseAdd',
      component: ExpenseManage,
      meta: {
        breadcrumb: 'เพิ่มประเภทค่าใช้จ่าย',
        parent: 'systemSettingsDisbursementTypeExpense'
      }
    },
    //จัดการประเภทการเดินทาง
    {
      path: '/systemSettings/disbursementType/vehicle',
      name: 'systemSettingsDisbursementTypeVehicle',
      component: TravelManage,
      meta: {
        breadcrumb: 'จัดการประเภทการเดินทาง',
        parent: 'systemSettingsDisbursementType'
      }
    },
    //เพิ่มประเภทรถสาธารณะ
    {
      path: '/systemSettings/disbursementType/vehicle/addPublic',
      name: 'systemSettingsDisbursementTypeVehicleAddPublic',
      component: TravelManage,
      meta: {
        breadcrumb: 'เพิ่มประเภทรถสาธารณะ',
        parent: 'systemSettingsDisbursementTypeVehicle'
      }
    },
    //เพิ่มประเภทรถส่วนตัว
    {
      path: '/systemSettings/disbursementType/vehicle/addPrivate',
      name: 'systemSettingsDisbursementTypeVehicleAddPrivate',
      component: TravelManage,
      meta: {
        breadcrumb: 'เพิ่มประเภทรถส่วนตัว',
        parent: 'systemSettingsDisbursementTypeVehicle'
      }
    },
    //นักบัญชี
    //การนำจ่าย
    {
      path: '/payment',
      name: 'payment',
      redirect: '/payment/paymentlist',
      meta: { breadcrumb: 'การนำจ่าย' }
    },
    //รายการรอนำจ่าย
    {
      path: '/payment/list',
      name: 'paymentList',
      component: PaymentList,
      meta: {
        breadcrumb: 'รายการรอนำจ่าย',
        parent: 'payment'
      }
    },
    //รายละเอียดรายการรอนำจ่าย
    {
      path: '/payment/list/detail/:id',
      name: 'paymentListDetail',
      component: Details,
      meta: {
        breadcrumb: 'รายละเอียด',
        parent: 'paymentList'
      }
    },
    //ประวัติการนำจ่าย
    {
      path: '/payment/history',
      name: 'paymentHistory',
      component: PaymentHistory,
      meta: {
        breadcrumb: 'ประวัติการนำจ่าย',
        parent: 'payment'
      }
    },
    //รายละเอียดประวัติการนำจ่าย
    {
      path: '/payment/history/detail/:id',
      name: 'paymentHistoryDetail',
      component: Details,
      meta: {
        breadcrumb: 'รายละเอียด',
        parent: 'paymentHistory'
      }
    },
  ]
})

export default router;