/*
* ชื่อไฟล์: TC_E2E_historyWithdraw_004_003_Success.cy.js
* คำอธิบาย: E2E test Filter
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข:  13 มีนาคม 2567
*/
describe("Dashboard Tests", () => {
    beforeEach(() => {
      cy.login("65160221", "admin");
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
      // กรอก "เบิกค่าใช้จ่าย"
      cy.xpath('//*[@id="SearchRqName"]').type('เบิกค่าใช้จ่าย');
      // กดปุ่ม "ค้นหา"
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[5]/div[2]/div[2]/button[2]').click();
      
      cy.xpath('/html/body/div/div/div/div/div/div[2]/div/div[1]/div[2]/form/div/select').type('งานเลี้ยง');
      
  
    });
  });
  