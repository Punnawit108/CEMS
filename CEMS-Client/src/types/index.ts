/*
* ชื่อไฟล์: index.ts
* คำอธิบาย: ไฟล์นี้ประกาศ type ของข้อมูล API ทั้งระบบ
* ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
* วันที่จัดทำ/แก้ไข: 21 พฤศจิกายน 2567
*/

//ตัวแปรคำขอเบิก
export interface Expense {
    rqId: string;
    rqName: number;
    rqUsrName: string;
    rqPjName: string;
    rqRqtName: string;
    rqVhName: string;
    rqVhType: string;
    rqVhPayrate: string;
    rqPayDate: Date;
    rqDateWithdraw: Date;
    rqCode: string | null;
    rqInsteadName: string | null;
    rqExpenses: number;
    rqLocation: string | null;
    rqStartLocation: string | null;
    rqEndLocation: string | null;
    rqDistance: string | null;
    rqPurpose: string | null;
    rqReason: string | null;
    rqProof: string | null;
    rqStatus: string;
    rqProgress: string;
}

export interface createRequisition {
    rqName: string;
    rqUsrId: string;
    rqPjId: string;
    rqRqtId: number;
    rqVhId: number | null;
    rqPayDate: string | null;
    rqWithdrawDate: string | null;
    rqInsteadEmail: string | null;
    rqExpenses: number;
    rqStartLocation: string | null;
    rqEndLocation: string | null;
    rqDistance: string | null;
    rqPurpose: string | null;
    rqStatus: string;
    rqProgress: string;
    rqAny: string | null;
}

export interface Approval {
    aprId: number;
    usrFirstName: string;
    usrLastName: string;
    aprName: string | null;
    aprDate: Date;
    aprStatus: string;
}

export interface ApproverRequisition {
    aprId: number;
    aprApId: number;
    aprName: string;
    aprDate: Date;
    aprStatus: string;
    rqReason: string;
}

//ตัวแปรประเภทค่าใช้จ่าย
export interface ExpenseManage {
    rqtId?: number;
    rqtName: string;
    rqtVisible: number
}
//ตัวแปรประเภทการเดินทาง
export interface TravelManage {
    vhId: number;
    vhType: string;
    vhVehicle: string;
    vhPayrate?: number | null;
    vhVisible:number ;
}
//ตัวแปรผู้ใช้
export interface User {
    usrId: string;
    usrEmployeeId: string;
    usrRolName: string;
    usrCpnName: string;
    usrPstName: string;
    usrDptName: string;
    usrStName: string;
    usrFirstName: string;
    usrLastName: string;
    usrPhoneNumber: string;
    usrEmail: string;
    usrIsSeeReport: number;
    usrIsActive: number;
}

export interface UserInstead {
    usrId: string;
    usrName: string;
    usrEmail: string;
}

//ข้อมูลแสดงผลของกราฟ และตาราง project 
export interface ProjectReport {
    pjId: number;
    pjName: string;
    pjSumAmountExpenses: string;
}
//ข้อมูลแสดงผลของกราฟ expense
export interface ExpenseReportGraph {
    rqRqtId: number;
    rqRqtName: string;
    rqSumExpenses: number;
}
//ข้อมูลแสดงผลของตาราง expense
export interface ExpenseReportList {
    rqId: number;
    rqName: number;
    rqUsrName: string;
    rqPjName: string;
    rqRqtName: string;
    rqDatePay: Date;
    rqExpenses: number;
    // rqSumExpenses: number;
}

export interface Dashboard {
    //ตารางการเบิกสูงสุดของโครงการ
    PjId: number;
    PjName: string;
    pjSumAmountExpenses: string;

    //กราฟ ประเภทค่าใช้จ่ายของรายการเบิก
    rqRqtId: number;
    rqRqtName: string;
    rqSumExpenses: number;

    //กราฟ ยอดการเบิกที่นำจ่ายจริง
    rqMonth: number;
    rqSumExpensesInMonth: number;
}

//ข้อมูล dashboard ตรงสี่เหลี่ยม 4 อัน ของ User
export interface DashboardForUser {
    rqTotalUserWaiting: number;
    rqTotalUserComplete: number;
    rqTotalUserProject: number;
    rqTotalExpense: number;
}

//ข้อมูล dashboard ตรงสี่เหลี่ยม 4 อัน ของ Accountant
export interface DashboardForAccountant {
    totalRqPay: number;
    totalRqComplete: number;
    totalRequisition: number;
    totalRqExpense: number;
}

//ข้อมูล dashboard ตรงสี่เหลี่ยม 4 อัน ของ Admin
export interface DashboardForAdmin {
    totalUser: number;
    totalRqAccept: number;
    totalProject: number;
    totalRqAcceptExpense: number;
}

export interface DashboardProject {
    //ตารางการเบิกสูงสุดของโครงการ
    PjId: number;
    PjName: string;
    totalPjExpense: string;
}

export interface DashboardRequisitionType {
    //กราฟ ประเภทค่าใช้จ่ายของรายการเบิก
    rqtId: number;
    rqtName: string;
    totalRqt: number;
}
export interface DashboardRequisitionTypeMonth {
    //กราฟ ประเภทค่าใช้จ่ายของรายการเบิก
    rqtId: number;
    rqtUsrId: number;
    totalRqt: number;

}

export interface DashboardPayment {
    rqMonth: number;
    rqSumExpensesInMonth: number;
}

export interface Project {
    pjId: number;
    pjName: string;
    pjAmountExpenses: number;
    pjIsActice : number;
}

//ข้อมูล notification
export interface Notification {
    NtId: number,
    NtStatus: "read" | "unread",
    NtAprRqPjName: string,
    NtAprRqId: number,
    NtAprStatus: "waiting" | "accept" | "edit" | "reject",
    NtAprDate: string,
    NtAprRqUsrId: number,
    NtAprRqProgress: "accepting" | "paying " | "complete",
}

export interface ApproverSequence {
    apId: number,
    apSequence: number
}