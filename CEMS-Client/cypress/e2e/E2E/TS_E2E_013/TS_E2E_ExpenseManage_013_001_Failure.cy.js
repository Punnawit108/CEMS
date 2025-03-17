describe("ExpenseManage Tests", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160093", "admin");
    });
  
    it("เพิ่มประเภทค่าใช้จ่าย", () => {
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
      cy.get(".btn-สร้างใบเบิกค่าใช้จ่าย").click();
      cy.xpath(
        '//*[@id="app"]/div/div/div/div/div[2]/div[4]/div/div[1]/div/form/div/input'
      ).type("ค่าเน็ต");
      cy.get(".btn-ยืนยัน").click();
      cy.xpath(
        '//*[@id="app"]/div/div/div/div/div[2]/div[5]/div/div[2]/button[2]'
      ).click();
      cy.contains("ยืนยันการเพิ่มข้อมูลประเภทค่าใช้จ่ายไม่สำเร็จ");
    });
  });
  