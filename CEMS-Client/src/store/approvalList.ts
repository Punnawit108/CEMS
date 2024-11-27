
/**
 * ชื่อไฟล์ : ApprovalListStore.ts
 * คำอธิบาย : FileTest Api จาก MockAPI
 * Input : -
 * Output : MockAPI
 * ชื่อผู้เขียน / แก้ไข : อังคณา อุ่นเสียม
 * วันที่จัดทำ / วัยที่แก้ไข : 25 พฤศจิกายน 2567
 */
import axios from "axios";
import { defineStore } from "pinia";
import { ref } from "vue";

export const useTodoStore = defineStore("todoStore", () => {
    // State
    const todos = ref<TodoItem[]>([]);
    const selectedTodo = ref<TodoItem | null>(null);
    const loading = ref(false);

    // Actions
    const loadTodos = async () => {
        loading.value = true;
        try {
            todos.value = await fetchTodos();
        } catch (error) {
            console.error("Error fetching todos:", error);
        } finally {
            loading.value = false;
        }
    };

    const loadTodoById = async (id: string) => {
        loading.value = true;
        try {
            selectedTodo.value = await fetchTodoById(id);
        } catch (error) {
            console.error("Error fetching todo by ID:", error);
        } finally {
            loading.value = false;
        }
    };

    const addTodo = async (data: Partial<TodoItem>) => {
        loading.value = true;
        try {
            const newTodo = await createTodo(data);
            todos.value.push(newTodo);
        } catch (error) {
            console.error("Error creating todo:", error);
        } finally {
            loading.value = false;
        }
    };

    const editTodo = async (id: string, data: Partial<TodoItem>) => {
        loading.value = true;
        try {
            const updatedTodo = await updateTodo(id, data);
            const index = todos.value.findIndex((todo) => todo.id === id);
            if (index !== -1) todos.value[index] = updatedTodo;
        } catch (error) {
            console.error("Error updating todo:", error);
        } finally {
            loading.value = false;
        }
    };

    const removeTodo = async (id: string) => {
        loading.value = true;
        try {
            await deleteTodo(id);
            todos.value = todos.value.filter((todo) => todo.id !== id);
        } catch (error) {
            console.error("Error deleting todo:", error);
        } finally {
            loading.value = false;
        }
    };

    return {
        todos,
        selectedTodo,
        loading,
        loadTodos,
        loadTodoById,
        addTodo,
        editTodo,
        removeTodo,
    };
});

interface TodoItem {
    id: string;
    name: string;
    projectName: string;
    selectExpenseType: string;
    date: string;
    amount: string;
    status: string;
}

const API_URL = "https://66a2a7e5967c89168f20c0e1.mockapi.io/api/todo";

// ดึงข้อมูลทั้งหมด
export const fetchTodos = async () => {
    const response = await axios.get(API_URL);
    return response.data;
};

// ดึงข้อมูลตาม ID
export const fetchTodoById = async (id: string) => {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
};

// สร้างรายการใหม่
export const createTodo = async (data: Partial<TodoItem>) => {
    const response = await axios.post(API_URL, data);
    return response.data;
};

// อัปเดตรายการ
export const updateTodo = async (id: string, data: Partial<TodoItem>) => {
    const response = await axios.put(`${API_URL}/${id}`, data);
    return response.data;
};

// ลบรายการ
export const deleteTodo = async (id: string) => {
    const response = await axios.delete(`${API_URL}/${id}`);
    return response.data;
};
