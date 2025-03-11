/*
* ชื่อไฟล์: TC10_1_1_010-01.cy
* คำอธิบาย: E2E เข้าสู่หน้าผู้ใช้งาน
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข: 27 กุมภาพันธ์ 2567
*/
describe("Dashboard Tests", () => {
    beforeEach(() => {
      cy.login("65160341", "admin"); 
    });
  
    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "ตั้งค่า" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
        cy.wait(1000);
        // ตรวจสอบว่า dropdown แสดงผล
        cy.xpath("//ul[contains(@class, 'ml-8 mt-2')]").should('be.visible');
        // เลือก "ผู้ใช้งาน" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/systemSettings/user');
        cy.wait(1000);

    });
  });