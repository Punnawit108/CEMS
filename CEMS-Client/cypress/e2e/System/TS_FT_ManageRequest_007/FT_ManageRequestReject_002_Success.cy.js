describe("FT_ManageRequestReject_002_Success", () => {
  beforeEach(() => {
    cy.login("65160093", "admin");
  });

  it("ตรวจสอบการทำงานของของระบบ เมื่อสร้างใบเบิกเป็นค่าเดินทาง รถสาธารณะ", () => {
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
    cy.xpath(
      '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]'
    ).click();

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[1]/th[8]/div'
    ).click();

    cy.xpath('//*[@id="btn-ไม่อนุมัติ"]').click();

    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div').should(
      "be.visible"
    );

    cy.xpath('//*[@id="rqReason"]').should("be.visible").type("ไม่มีเงิน");

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div/div[2]/button[2]'
    ).click();

    //cy.screenshot();

    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[2]/a/a/div[1]')
      .should("be.visible") // ตรวจสอบว่าปุ่มปรากฏบนหน้า
      .click(); // คลิกที่ปุ่ม

    //cy.screenshot();
  });
});
