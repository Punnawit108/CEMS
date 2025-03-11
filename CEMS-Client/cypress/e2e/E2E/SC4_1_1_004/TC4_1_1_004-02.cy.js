/*
ยังไม่เสร็จ
* ชื่อไฟล์: TC4_1_1_004-02.cy
* คำอธิบาย: E2E การกรองรายการเบิก (โครงการ)
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข:  24 กุมภาพันธ์ 2567
*/
describe("Dashboard Tests", () => {
    beforeEach(() => {
      cy.login("65160341", "admin"); 
    });
  
    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
        cy.wait(1000);
        // ตรวจสอบว่า dropdown แสดงผล
        cy.xpath("//ul[contains(@class, 'ml-8 mt-2')]").should('be.visible');
        // เลือก "ประวัติการเบิกจ่าย" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[2]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/disbursement/historyWithdraw');
        cy.wait(1000);
        
    });
  });
