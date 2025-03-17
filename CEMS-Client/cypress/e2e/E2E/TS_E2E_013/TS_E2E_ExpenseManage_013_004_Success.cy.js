describe("ExpenseManage Tests", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160093", "admin");
    });
  
    it("ลบประเภทค่าใช้จ่าย", () => {
      Cypress.on("uncaught:exception", (err) => {
        if (
          err.message.includes("lockSystem.ts") ||
          err.message.includes("AxiosError") ||
          err.message.includes("Cannot read properties of null (reading 'usrId')")
        ) {
          return false; 
        }
        return true;
      });
      cy.get(".text-white").click();
      cy.get(":nth-child(5) > .relative > .flex").click();
      cy.get(
        '[href="/systemSettings/disbursementType"] > .relative > .absolute'
      ).click();
      cy.get(".btn-gray").click();
      cy.get(':nth-child(10) > .justify-between > .justify-end > :nth-child(3)').click();
      cy.get('.text-grayNormal.px-3.text-sm').should('contain', 'ค่าเน็ต');
    });
});