describe("FT_ManageWithdraw_001_Success", () => {
    beforeEach(() => {
      cy.login("65160095", "admin");
    });
  
    it("ตรวจสอบการทำงานของของระบบ เมื่อสร้างใบเบิกเป็นค่าเดินทาง รถสาธารณะ", () => {
      // FT_ManageWithdraw_001_Success
      cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
      cy.xpath(
        '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]'
      ).click();
  
      //cy.screenshot();
    });
  });
  