/*
* ชื่อไฟล์: TC9_1_1_009-01.cy
* คำอธิบาย: E2e
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข:  
*/
describe("ManageExpeneFormTest", () => {
    beforeEach(() => {
      cy.login("65160095", "admin"); 
    });
  
    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[5]/button/div[1]').click();
        cy.wait(1000);
        // ตรวจสอบว่า dropdown แสดงผล
        cy.xpath("//ul[contains(@class, 'ml-8 mt-2')]").should('be.visible');
        // เลือก "รายการเบิกค่าใช้จ่าย" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[5]/ul/li[1]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/payment/list');
        // ตรวจสอบว่าหน้ามีข้อความ "รายการเบิกค่าใช้จ่าย"
        cy.wait(2000);
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[2]/table/tbody/tr/th[7]/span').click();
        cy.wait(1000);

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div/button').click();
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div[2]/button[2]').click();

     
        cy.wait(1000);
    });
});