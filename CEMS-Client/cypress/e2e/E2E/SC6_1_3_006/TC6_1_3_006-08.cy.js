/*
* ชื่อไฟล์: TC6_1_3_006-05.cy
* คำอธิบาย: E2E การแสดงผลข้อมูล ยอดการเบิกจ่ายแต่ละประเภทในกราฟ
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข: 24 กุมภาพันธ์ 2567
*/
describe("Dashboard Tests", () => {
    beforeEach(() => {
      cy.login("65160341", "admin"); 
    });
  
    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/button/div[1]').click();
        cy.wait(1000);
        // ตรวจสอบว่า dropdown แสดงผล
        cy.xpath("//ul[contains(@class, 'ml-8 mt-2')]").should('be.visible');
        // เลือก "รายงานเบิกค่าใช้จ่าย" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/ul/li[2]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/expense');
        cy.wait(1000);

         // ตรวจสอบว่า element กราฟ มีหรือไม่
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[1]/div').should('exist'); 
        // ตรวจสอบว่า element ตาราง มีหรือไม่
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[2]').should('exist'); 

    });
  });