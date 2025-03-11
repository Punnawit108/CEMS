describe("ExpenseManage Tests", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160093", "admin");
    });
  
    it("ประวัติค่าใช้จ่าย", () => {
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
      cy.get(':nth-child(2) > .relative > .flex').click();
      cy.get('[href="/disbursement/historyWithdraw"] > .relative > .absolute').click();
      cy.get('.โครงการ > :nth-child(1) > .grid > .relative > .appearance-none').select('โครงการที่ 1');
      cy.get('.ประเภทค่าใช้จ่าย > :nth-child(1) > .grid > .relative > .appearance-none').select('ประเภทที่ 1');
      cy.get('#Calendar').click();
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table/tbody/tr[1]/th[8]').click();
      cy.get('#btn-พิมพ์').click();
      cy.get('.btn-ยืนยัน').click();
    });
  });
  