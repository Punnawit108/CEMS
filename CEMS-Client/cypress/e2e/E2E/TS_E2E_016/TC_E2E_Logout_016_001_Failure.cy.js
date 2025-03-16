/*
* ชื่อไฟล์: TC_E2E_historyWithdraw_004_001_Failure.cy.js
* คำอธิบาย: E2E Logout Failure
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข:  13 มีนาคม 2567
*/
describe("Dashboard Tests", () => {
    beforeEach(() => {
      cy.login("65160221", "admin");
    });
  
    it('หลังจากเข้าสู่ระบบ', () => {
      //ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
      cy.url().should('include', '/dashboard');
      //*[@id="btn-logout"]
      cy.wait(3000);
      cy.xpath('//*[@id="btn-logout"]').click();
      //ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
      cy.url().should('include', '/dashboard');
  
    });
  });