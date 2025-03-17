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
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[5]/div[2]/div[5]/div/div[2]/button[2]').click();
      cy.get(".btn-ยืนยัน").click();
      cy.contains("ยืนยันการลบข้อมูลประเภทค่าเดินทางสำเร็จ");
      cy.get("วินมอเตอร์ไซค์").should('not.exist');
    });
  });
  