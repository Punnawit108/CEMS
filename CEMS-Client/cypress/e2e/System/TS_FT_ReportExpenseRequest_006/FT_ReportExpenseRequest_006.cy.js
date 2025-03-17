describe("FT_ReportExpenseRequest_006", () => {
    beforeEach(() => {
      cy.login("65160320", "admin");
    });
  
    it("ตรวจสอบการทำงานของของระบบ เมื่อสร้างใบเบิกเป็นค่าเดินทาง รถสาธารณะ", () => {
      // FT_ReportExpenseRequest_001_Success
      cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/button/div[1]').click();
      cy.xpath(
        '//*[@id="app"]/div/div/div/nav/ul/li[3]/ul/li[2]/a/a/div[1]'
      ).click();
      cy.wait(1000);
      cy.url().should("include", "/report/expense");
      //cy.screenshot();
  
      //FT_ReportExpenseExport_002_Success
      cy.xpath('//*[@id="btn-พิมพ์"]').should("be.visible").click();
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[4]/div/div/div/div[1]/button[1]').should("be.visible").click();
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[4]/div/div/div/div[3]/button[2]').should("be.visible").click();
  
      const downloadsFolder = Cypress.config("downloadsFolder");
      const filename = "ExportedExpenseData.pdf";
      cy.readFile(`${downloadsFolder}/${filename}`).should("exist");
       //cy.screenshot();
    });
  });
  
  
  