import { defineConfig } from "cypress";

export default defineConfig({
  e2e: {
    setupNodeEvents(on, config) {
      // เพิ่มการตั้งค่า event handler ที่จำเป็น (ถ้ามี)
    },
    baseUrl: "http://localhost:4000", // URL ของแอปพลิเคชันคุณ
    specPattern: "cypress/e2e/**/*.cy.{js,jsx,ts,tsx}", // รูปแบบไฟล์ทดสอบ
    supportFile: "cypress/support/e2e.ts", // support file (ปรับตามต้องการ)
  },
});