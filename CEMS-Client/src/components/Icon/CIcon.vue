<script setup lang="ts">
/*
* ชื่อไฟล์: CIcon.vue
* คำอธิบาย: ไฟล์นี้ข้อมูล icon
* ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
* วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
*/
import { defineProps, onMounted, shallowRef } from "vue";
import CIconApproval from "./CIconApproval.vue";
import CIconApprovalList from "./CIconApprovalList.vue";
import CIconBin from "./CIconBin.vue";
import CIconCheckCircle from "./CIconCheckCircle.vue";
import CIconCirclePlus from "./CIconCirclePlus.vue";
import CIconDashboard from "./CIconDashboard.vue";
import CIconDisbursement from "./CIconDisbursement.vue";
import CIconEdit from "./CIconEdit.vue";
import CIconErrorCircle from "./CIconErrorCircle.vue";
import CIconExclamationCircle from "./CIconExclamationCircle.vue";
import CIconEye from "./CIconEye.vue";
import CIconEyeSlash from "./CIconEyeSlash.vue";
import CIconFile from "./CIconFile.vue";
import CIconListWithdraw from "./CIconListWithdraw.vue";
import CIconNotification from "./CIconNotification.vue";
import CIconPen from "./CIconPen.vue";
import CIconPrinter from "./CIconPrinter.vue";
import CIconProfile from "./CIconProfile.vue";
import CIconReport from "./CIconReport.vue";
import CIconReportExpense from "./CIconReportExpense.vue";
import CIconReportProject from "./CIconReportProject.vue";
import CIconSystemSettings from "./CIconSystemSettings.vue";
import CIconSystemSettingsDisbursementApprover from "./CIconSystemSettingsDisbursementApprover.vue";
import CIconSystemSettingsDisbursementType from "./CIconSystemSettingsDisbursementType.vue";
import CIconSystemSettingsDisbursementTypeExpense from "./CIconSystemSettingsDisbursementTypeExpense.vue";
import CIconSystemSettingsDisbursementTypeVehicle from "./CIconSystemSettingsDisbursementTypeVehicle.vue";
import CIconSystemSettingsUser from "./CIconSystemSettingsUser.vue";
import CIconUserRemove from "./CIconUserRemove.vue";
import CIconViewDetails from "./CIconViewDetails.vue";

const currentComponent = shallowRef();

const props = defineProps<{
    icon: String
    size?: number; // เพิ่ม props สำหรับขนาด
}>();
onMounted(()=>{
    switch(props.icon){
        case 'dashboard':
            currentComponent.value = CIconDashboard;
            break;

        case 'disbursement':
            currentComponent.value = CIconDisbursement;
            break;

        case 'listWithdraw':
        case 'createExpenseForm':
        case 'listWithdrawDetail':
        case 'editExpenseForm':
        case 'paymentList':
        case 'paymentListDetail':
            currentComponent.value = CIconListWithdraw;
            break;  

        case 'historyWithdraw ':
        case 'approvalHistory':
        case 'historyWithdraw':
        case 'historyWithdrawDetail':
        case 'approvalHistoryDetail':
        case 'paymentHistory':
        case 'paymentHistoryDetail':
        case 'reportExpense':
            currentComponent.value = CIconReportExpense;
            break;

        case 'reportProject':
            currentComponent.value = CIconReportProject;
            break;

        case 'approvalList':
            currentComponent.value = CIconApprovalList;
            break;  

        case 'approval':
            currentComponent.value = CIconApproval;
            break;

        case 'printer':
            currentComponent.value = CIconPrinter;
            break;

        case 'profile':
            currentComponent.value = CIconProfile;
            break;

        case 'systemSettings':
            currentComponent.value = CIconSystemSettings;
            break;

        case 'systemSettingsUser':
        case 'systemSettingsUserDetail':
        case 'systemSettingsUserDetailEditUser':
            currentComponent.value = CIconSystemSettingsUser;
            break;

        case 'systemSettingsDisbursementApprover':
        case 'systemSettingsDisbursementApproverAdd':
            currentComponent.value = CIconSystemSettingsDisbursementApprover;
            break;

        case 'systemSettingsDisbursementTypeExpense':
        case 'systemSettingsDisbursementTypeExpenseAdd':
            currentComponent.value = CIconSystemSettingsDisbursementTypeExpense;
            break;

        case 'systemSettingsDisbursementType':
            currentComponent.value = CIconSystemSettingsDisbursementType;
            break;

        case 'systemSettingsDisbursementTypeVehicle':
        case 'systemSettingsDisbursementTypeVehicleAddPublic':
        case 'systemSettingsDisbursementTypeVehicleAddPrivate':
            currentComponent.value = CIconSystemSettingsDisbursementTypeVehicle;
            break;

        case 'report':
            currentComponent.value = CIconReport;
            break;

        case 'notification':
            currentComponent.value = CIconNotification;
            break;

        case 'viewDetails':
            currentComponent.value = CIconViewDetails;
            break;

        case 'bin':
            currentComponent.value = CIconBin;
            break;

        case 'edit':
            currentComponent.value = CIconEdit;
            break;

        case 'eye':
            currentComponent.value = CIconEye;
            break;

        case 'eyeSlash':
            currentComponent.value = CIconEyeSlash;
            break;

        case 'circlePlus':
            currentComponent.value = CIconCirclePlus;
            break;

        case 'pen':
            currentComponent.value = CIconPen;
            break;

        case 'checkCircle':
            currentComponent.value = CIconCheckCircle;
            break;

        case 'exclamationCircle':
            currentComponent.value = CIconExclamationCircle;
            break;

        case 'errorCircle':
            currentComponent.value = CIconErrorCircle;
            break;

        case 'userRemove':
            currentComponent.value = CIconUserRemove;
            break;

        case 'file':
            currentComponent.value = CIconFile;
            break;            
    }
})
</script>


<template>
    <component :is="currentComponent" :size="props.size" v-if="currentComponent" />
</template>