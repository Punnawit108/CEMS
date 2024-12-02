/**
* คำอธิบาย : createExpenseForm.ts
* Input :  ข้อมูล ExpenseForm
* Output : ข้อมูล ExpenseForm
* ชื่อผู้เขียน / แก้ไข : อังคณา อุ่นเสียม
* วันที่จัดทำ / วัยที่แก้ไข : 20 พฤศจิกายน 2567
*/
import axios from "axios";

// กำหนด API Base URL
const Api = "https://66a2a7e5967c89168f20c0e1.mockapi.io/api/todo";

// ประเภทข้อมูลสำหรับ API
export interface Expense {
    id?: string;
    rqId: string;
    rqName: string;
    rqUsrName: string;
    rqPjName: string;
    rqRqtName: string;
    rqVhName: string;
    rqDatePay: string;
    rqDateWithdraw: string;
    rqCode: string;
    rqInsteadEmail: string;
    rqExpenses: string;
    rqLocation: string;
    rqStartLocation: string;
    rqEndLocation: string;
    rqDistance: string;
    rqPurpose: string;
    rqReason: string;
    rqProof: string;
    rqStatus: string;
    rqProgress: string;
}

// ดึงข้อมูลทั้งหมด
export const fetchTodos = async () => {
    const response = await axios.get(`${Api}`);
    return response.data;
};

// ดึงข้อมูลตาม ID
export const fetchTodoById = async (id: string) => {
    const response = await axios.get(`${Api}/${id}`);
    return response.data;
};

// สร้างรายการใหม่
export const createTodo = async (data: Partial<Expense>) => {
    const response = await axios.post(`${Api}`, data);
    return response.data;
};

// อัปเดตรายการ
export const updateTodo = async (id: string, data: Partial<Expense>) => {
    const response = await axios.put(`${Api}/${id}`, data);
    return response.data;
};

// ลบรายการ
export const deleteTodo = async (id: string) => {
    const response = await axios.delete(`${Api}expense/${id}`);
    return response.data;
};
