/*
* ชื่อไฟล์: TC12_1_1_012-02.cy
* คำอธิบาย: E2e
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข:  
*/
describe("ManageExpeneFormTest", () => {
    beforeEach(() => {
      cy.login("65160341", "admin"); 
    });
  
    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
        cy.wait(1000);
        
        // เลือก "รายการเบิกค่าใช้จ่าย" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[3]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/systemSettings/disbursementType/expense');
        
        cy.wait(1000);

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/button').click();

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[4]/div/div[1]/div/form/div/input').type('cypress test');

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[4]/div/div[2]/button[2]').click();

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[5]/div/div[2]/button[2]').click();


    });
  });