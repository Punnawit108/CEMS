/*
* ชื่อไฟล์: index.ts
* คำอธิบาย: ไฟล์นี้ประกาศ type ของข้อมูล API ทั้งระบบ
* ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
* วันที่จัดทำ/แก้ไข: 21 พฤศจิกายน 2567
*/

//ตัวแปรคำขอเบิก
export interface Expense{
    rqId: number;
    rqName: number;
    rqUsrName: string;
    rqPjName: string;
    rqRqtName: string;
    rqVhName: string;
    rqVhType: string;
    rqVhPayrate: string;
    rqDatePay: Date;
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

export interface Approval {
    aprId: number;           
    usrFirstName: string;    
    usrLastName: string;     
    aprName: string | null;  
    aprDate: string;         
}


//ตัวแปรประเภทค่าใช้จ่าย
export interface ExpenseManage{
    rqtId?: number;
    rqtName: string;
    
}
//ตัวแปรประเภทการเดินทาง
export interface TravelManage {
    vhId: number;
    vhType: string;
    vhVehicle: string;
    vhPayrate?: number | null;
}
//ตัวแปรผู้ใช้
export interface User {
    usrId: number;
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

//ข้อมูลแสดงผลของกราฟ และตาราง project 
export interface ProjectReport{
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
    rqSumOfWaiting: number;
    rqSumOfApprove: number;
    rqSumOfExpense: number;
    rqSumExpense: number;
}

//ข้อมูล dashboard ตรงสี่เหลี่ยม 4 อัน ของ Accountant
export interface DashboardForAccountant {
    rqSumOfWaiting: number ;
    rqSumOfComplate: number ;
    rqSumOfExpense: number ;
    rqSumExpense: number ;
}

//ข้อมูล dashboard ตรงสี่เหลี่ยม 4 อัน ของ Admin
export interface DashboardForAdmin {
    usrAll: number ;
    rqSumOfExpense: number ;
    pjAll: number ;
    rqSumExpense: number ;
}

export interface Project {
    pjId : number ;
    pjName : string ;
    pjAmountExpenses : number ;
}

//ข้อมูล notification
export interface Notification{
    id:number,
    idWithdraw:number,
    nameProject:string,
    statusNoti:boolean,
    description:string,
}
