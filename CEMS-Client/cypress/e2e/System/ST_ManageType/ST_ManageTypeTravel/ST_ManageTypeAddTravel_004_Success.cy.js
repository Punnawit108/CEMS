describe("ทดสอบการเพิ่มประเภทค่าเดินทางรถส่วนตัว", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160341", "admin");
    });
  
    it("ตรวจสอบการเพิ่มประเภทค่าค่าเดินทางรถส่วนตัว โดยกระบวนการตั้งแต่ต้นจนจบ", () => {
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
        cy.contains("เพิ่มประเภทรถส่วนตัว").should('be.visible');
        cy.xpath('//*[@id="btn-ประเภทรถส่วนตัว"]').should('be.visible');
        cy.xpath('//*[@id="btn-ประเภทรถส่วนตัว"]').click();
        cy.contains("การเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัว").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[1]/form/div[1]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[1]/form/div[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[1]/form/div[1]').type('Test Cypress Add');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[1]/form/div[2]').type('100');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[2]/button[1]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[2]/button[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[7]/div/div[2]/button[2]').click();
        cy.contains("ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัว").should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[8]/div/div[2]/button[1]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[8]/div/div[2]/button[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[8]/div/div[2]/button[2]').click();
        cy.contains("ยืนยันการเพิ่มข้อมูลประเภทค่าเดินทางส่วนตัวสำเร็จ").should('be.visible');
        cy.contains("Test Cypress Add").should('be.visible');
        
    });
  });
  