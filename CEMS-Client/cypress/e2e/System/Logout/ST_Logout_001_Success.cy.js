describe("ทดสอบการออกจากระบบ", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160341", "admin");
    });
  
    it("ตรวจสอบการออกจากระบบ โดยกระบวนการตั้งแต่ต้นจนจบ", () => {
        cy.url().should('include','/dashboard')
        cy.contains("ออกจากระบบ").should('be.visible');
        cy.get('#btn-logout').click();
    });
  });
  