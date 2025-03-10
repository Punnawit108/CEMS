/*
ยังไม่เสร็จ
* ชื่อไฟล์: TC2_1_1_002-05.cy
* คำอธิบาย: E2E ตรวจสอบยอดการเบิกตรงกับ Databse
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข:  18 กุมภาพันธ์ 2568
*/
describe("Dashboard Tests", () => {
    beforeEach(() => {
      cy.login("65160341", "admin"); 
    });
  
    it('ตรวจสอบอดการเบิกตรงกับ database', () => {
        //ตรวจสอบว่า เข้ามาหน้า แดชบอร์ด ยัง
        cy.url().should('include', '/dashboard');
        
        cy.get('p').contains('ยอดเบิกจ่ายแล้ว (บาท)')
            .should('be.visible') // ตรวจสอบว่า h1 มองเห็นได้
            .and('have.css', 'font-size', '16px') // ตรวจสอบขนาดตัวอักษร
            .and('have.css', 'font-family', 'Sarabun');

    });
  });
  