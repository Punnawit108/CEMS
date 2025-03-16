describe("ทดสอบการเพิ่มประเภทค่าเดินทางรถสาธารณะ", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160341", "admin");
    });
  
    it("ตรวจสอบการเพิ่มประเภทค่าค่าเดินทางรถสาธารณะ โดยกระบวนการตั้งแต่ต้นจนจบ", () => {
        cy.url().should('include','/dashboard')
        cy.contains("ตั้งค่าระบบ").should('be.visible');
        cy.get(':nth-child(4) > .relative > .flex').click()
        cy.contains("ประเภทเบิกจ่าย").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[3]/a/a/div[1]').click();
        cy.url().should('include','/systemSettings/disbursementType/expense')
        cy.contains("จัดการประเภทค่าใช้จ่าย").should('be.visible');

        cy.contains("ประเภทค่าเดินทาง").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[1]/div/div/button[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[1]/div/div/button[2]').click();

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[5]/div[1]/button').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[5]/div[1]/button').click();
        cy.contains("การเพิ่มข้อมูลประเภทค่าเดินทางสาธารณะ").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[1]/form/div').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[1]/form/div').type('Test Cypress Add');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[2]/button[1]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[2]/button[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[2]/button[2]').click();
        cy.contains("ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางสาธารณะ").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[8]/div/div[2]/button[1]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[8]/div/div[2]/button[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[8]/div/div[2]/button[2]').click();
        cy.contains("ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางสาธารณะสำเร็จ").should('be.visible');
        cy.contains("Test Cypress Add").should('be.visible');
  });
});