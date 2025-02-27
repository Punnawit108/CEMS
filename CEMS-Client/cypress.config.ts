import { defineConfig } from "cypress";

export default defineConfig({
  e2e: {
    setupNodeEvents(on, config) {
      // เพิ่มการตั้งค่า event handler ที่จำเป็น (ถ้ามี)
    },
    baseUrl: "http://localhost:5247", // URL ของแอปพลิเคชันคุณ
    specPattern: "cypress/e2e/**/*.cy.{js,jsx,ts,tsx}", // รูปแบบไฟล์ทดสอบ
    supportFile: "cypress/support/e2e.ts", // support file (ปรับตามต้องการ)
  },
  viewportWidth: 1920, // ความกว้างหน้าจอ
  viewportHeight: 1080, // ความสูงหน้าจอ
});