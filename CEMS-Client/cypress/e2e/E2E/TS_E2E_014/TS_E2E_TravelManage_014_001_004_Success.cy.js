describe("TravelManage Tests", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160093", "admin");
    });
  
    it("เพิ่มประเภทค่าเดินทาง", () => {
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
      cy.get(".btn-white").click();
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[3]/div/div[8]/div/div[2]/button[3]').click();
      cy.get('.text-grayNormal.px-3.text-sm').should('contain', 'รถมอเตอร์ไซค์2');
    });
  });