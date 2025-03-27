<script setup lang="ts">
/*
 * ชื่อไฟล์: CIcon.vue
 * คำอธิบาย: ไฟล์นี้แสดง components ไอคอนทั้งระบบ
 * ชื่อผู้เขียน/แก้ไข: จักรวรรดิ
 * วันที่จัดทำ/แก้ไข: 15 กุมภาพันธ์ 2568
 */
import { shallowRef, defineProps, onMounted, type Component, watch } from "vue";

const props = defineProps<{
    icon: string;
    size?: number;
}>();

const iconMap = new Map<string, Component>([
    ["dashboard", () => import("./CIconDashboard.vue")],
    ["disbursement", () => import("./CIconDisbursement.vue")],
    ["listWithdraw", () => import("./CIconListWithdraw.vue")],
    ["createExpenseForm", () => import("./CIconListWithdraw.vue")],
    ["listWithdrawDetail", () => import("./CIconListWithdraw.vue")],
    ["editExpenseForm", () => import("./CIconListWithdraw.vue")],
    ["paymentList", () => import("./CIconListWithdraw.vue")],
    ["paymentListDetail", () => import("./CIconListWithdraw.vue")],
    ["approvalHistory", () => import("./CIconReportExpense.vue")],
    ["historyWithdraw", () => import("./CIconReportExpense.vue")],
    ["historyWithdrawDetail", () => import("./CIconReportExpense.vue")],
    ["approvalHistoryDetail", () => import("./CIconReportExpense.vue")],
    ["paymentHistory", () => import("./CIconReportExpense.vue")],
    ["paymentHistoryDetail", () => import("./CIconReportExpense.vue")],
    ["reportExpense", () => import("./CIconReportExpense.vue")],
    ["reportProject", () => import("./CIconReportProject.vue")],
    ["approvalList", () => import("./CIconApprovalList.vue")],
    ["approval", () => import("./CIconApproval.vue")],
    ["printer", () => import("./CIconPrinter.vue")],
    ["profile", () => import("./CIconProfile.vue")],
    ["systemSettings", () => import("./CIconSystemSettings.vue")],
    ["systemSettingsUser", () => import("./CIconSystemSettingsUser.vue")],
    ["systemSettingsUserDetail", () => import("./CIconSystemSettingsUser.vue")],
    ["systemSettingsUserDetailEditUser", () => import("./CIconSystemSettingsUser.vue")],
    ["systemSettingsDisbursementApprover", () => import("./CIconSystemSettingsDisbursementApprover.vue")],
    ["systemSettingsDisbursementApproverAdd", () => import("./CIconSystemSettingsDisbursementApprover.vue")],
    ["systemSettingsDisbursementTypeExpense", () => import("./CIconSystemSettingsDisbursementTypeExpense.vue")],
    ["systemSettingsDisbursementTypeExpenseAdd", () => import("./CIconSystemSettingsDisbursementTypeExpense.vue")],
    ["systemSettingsDisbursementType", () => import("./CIconSystemSettingsDisbursementType.vue")],
    ["systemSettingsDisbursementTypeVehicle", () => import("./CIconSystemSettingsDisbursementTypeVehicle.vue")],
    ["systemSettingsDisbursementTypeVehicleAddPublic", () => import("./CIconSystemSettingsDisbursementTypeVehicle.vue")],
    ["systemSettingsDisbursementTypeVehicleAddPrivate", () => import("./CIconSystemSettingsDisbursementTypeVehicle.vue")],
    ["report", () => import("./CIconReport.vue")],
    ["notification", () => import("./CIconNotification.vue")],
    ["viewDetails", () => import("./CIconViewDetails.vue")],
    ["bin", () => import("./CIconBin.vue")],
    ["edit", () => import("./CIconEdit.vue")],
    ["eye", () => import("./CIconEye.vue")],
    ["eyeSlash", () => import("./CIconEyeSlash.vue")],
    ["circlePlus", () => import("./CIconCirclePlus.vue")],
    ["pen", () => import("./CIconPen.vue")],
    ["checkCircle", () => import("./CIconCheckCircle.vue")],
    ["exclamationCircle", () => import("./CIconExclamationCircle.vue")],
    ["errorCircle", () => import("./CIconErrorCircle.vue")],
    ["userRemove", () => import("./CIconUserRemove.vue")],
    ["file", () => import("./CIconFile.vue")],
]);

const currentComponent = shallowRef<Component | null>(null);

onMounted(async () => {
    const componentLoader = iconMap.get(props.icon);
    if (componentLoader) {
        currentComponent.value = (await componentLoader()).default;
    }
});

watch(() => props.icon, async (newIcon) => {
    const componentLoader = iconMap.get(newIcon);
    if (componentLoader) {
        currentComponent.value = (await componentLoader()).default;
    }
});

</script>

<template>
    <component :is="currentComponent" :size="props.size" v-if="currentComponent" />
</template>
