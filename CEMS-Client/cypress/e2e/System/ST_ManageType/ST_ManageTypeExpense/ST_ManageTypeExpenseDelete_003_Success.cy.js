describe("ทดสอบการลบประเภทค่าใช้จ่าย", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160341", "admin");
    });
  
    it("ตรวจสอบการลบประเภทค่าใช้จ่าย โดยกระบวนการตั้งแต่ต้นจนจบ", () => {
        cy.url().should('include','/dashboard')
        cy.contains("ตั้งค่าระบบ").should('be.visible');
        cy.get(':nth-child(4) > .relative > .flex').click()
        cy.contains("ประเภทเบิกจ่าย").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[3]/a/a/div[1]').click();
        cy.url().should('include','/systemSettings/disbursementType/expense')
        cy.contains("จัดการประเภทค่าใช้จ่าย").should('be.visible');
        cy.contains("ประเภทค่าใช้จ่าย").should('be.visible');

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[3]/div/div[7]/div/div[2]/button[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[3]/div/div[7]/div/div[2]/button[2]').click();
        cy.contains("ยืนยันการลบประเภทค่าใช้จ่าย").should('be.visible');
        cy.contains("ยกเลิก").should('be.visible');
        cy.contains("ยืนยัน").should('be.visible');
        cy.get('.btn-ยืนยัน').click();
    });
  });
  