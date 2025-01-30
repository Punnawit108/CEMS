import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${import.meta.env.VITE_BASE_URL}/notificationHub`)
    .build();

connection.start()
    .then(() => console.log("Connected to SignalR"))
    .catch(err => console.error("Error connecting to SignalR: ", err));

connection.on("ReceiveNotification", (message: string) => {
    console.log("Notification received: ", message);
    // แสดงการแจ้งเตือนในหน้า UI
});

export default connection;