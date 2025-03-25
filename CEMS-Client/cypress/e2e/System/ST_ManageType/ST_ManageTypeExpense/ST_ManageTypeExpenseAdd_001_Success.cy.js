describe("ทดสอบการเพิ่มประเภทค่าใช้จ่าย", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160341", "admin");
    });
  
    it("ตรวจสอบการเพิ่มประเภทค่าใช้จ่าย โดยกระบวนการตั้งแต่ต้นจนจบ", () => {
        cy.url().should('include','/dashboard')
        cy.contains("ตั้งค่าระบบ").should('be.visible');
        cy.get(':nth-child(4) > .relative > .flex').click()
        cy.contains("ประเภทเบิกจ่าย").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[3]/a/a/div[1]').click();
        cy.url().should('include','/systemSettings/disbursementType/expense')
        cy.contains("จัดการประเภทค่าใช้จ่าย").should('be.visible');
        cy.contains("ประเภทค่าใช้จ่าย").should('be.visible');
        cy.contains("เพิ่มประเภทค่าใช้จ่าย").should('be.visible');
        cy.get('.btn-สร้างใบเบิกค่าใช้จ่าย').click()
        cy.contains("การเพิ่มข้อมูลประเภทค่าใช้จ่าย").should('be.visible');
        cy.contains("ข้อมูลประเภทค่าใช้จ่าย").should('be.visible');
        cy.contains("ยกเลิก").should('be.visible');
        cy.contains("ยืนยัน").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[4]/div/div[1]/div/form/div/input').type("Test Cypress Add");
        cy.get('.btn-ยืนยัน').click();
        cy.contains("ยืนยันการเพิ่มข้อมูลประเภทค่าใช้จ่าย").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[5]/div/div[2]/button[1]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[5]/div/div[2]/button[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[5]/div/div[2]/button[2]').click();
        cy.contains("ยืนยันการเพิ่มข้อมูลประเภทค่าใช้จ่ายสำเร็จ").should('be.visible');
        cy.contains("Test Cypress Add").should('be.visible');
    });
  });
  