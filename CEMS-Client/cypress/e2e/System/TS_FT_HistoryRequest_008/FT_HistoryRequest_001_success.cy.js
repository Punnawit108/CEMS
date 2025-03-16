describe("FT_HistoryRequest_001_success", () => {
  beforeEach(() => {
    cy.login("65160356", "admin");
  });

  it("ตรวจสอบการทำงานของของระบบ เมื่อสร้างใบเบิกเป็นค่าเดินทาง รถสาธารณะ", () => {
    // FT_HistoryRequest_001_success
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/button/div[1]').click();
    cy.xpath(
      '//*[@id="app"]/div/div/div/nav/ul/li[3]/ul/li[2]/a/a/div[1]'
    ).click();

    //cy.screenshot();
  });
});
