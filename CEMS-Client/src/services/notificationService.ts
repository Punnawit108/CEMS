import * as signalR from '@microsoft/signalr';
import axios from 'axios';

// การเชื่อมต่อกับ SignalR Hub
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${import.meta.env.VITE_BASE_URL}/notificationHub`, {
        withCredentials: true,
    })
    .withAutomaticReconnect()
    .build();

// ฟังก์ชันเริ่มการเชื่อมต่อ
export const startHubConnection = async () => {
    if (hubConnection.state === signalR.HubConnectionState.Disconnected) {
        try {
            await hubConnection.start();
        } catch (error) {
            console.error('SignalR Connection Error:', error);
            setTimeout(() => startHubConnection(), 5000); // Retry ทุก 5 วินาที
        }
    }
};

// ฟังก์ชันหยุดการเชื่อมต่อ
export const stopHubConnection = async () => {
    try {
        await hubConnection.stop();
    } catch (error) {
        console.error('SignalR Disconnection Error:', error);
    }
};

// ฟัง Event จาก SignalR
export const onNotificationReceived = (callback: () => void) => {
    hubConnection.on('ReceiveNotification', callback);
};

// API สำหรับดึงการแจ้งเตือนจาก Backend
export const fetchNotifications = async (usrId : string) => {
    const response = await axios.get(`${import.meta.env.VITE_BASE_URL}/api/notification/list/${usrId}`); // เปลี่ยน URL ให้ถูกต้อง
    return response.data;
};
