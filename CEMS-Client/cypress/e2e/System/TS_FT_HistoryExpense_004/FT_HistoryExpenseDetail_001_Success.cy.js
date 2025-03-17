describe("FT_HistoryExpenseDetail_001_Success", () => {
  beforeEach(() => {
    cy.login("65160320", "admin");
  });

  it("ตรวจสอบการทำงานของของระบบ เมื่อสร้างใบเบิกเป็นค่าเดินทาง รถสาธารณะ", () => {
    // FT_HistoryExpenseDetail_001_Success
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
    cy.xpath(
      '//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[2]/a/a/div[1]'
    ).click();

    const dataArray = []; 

    for (let i = 2; i <= 6; i++) {
      cy.xpath(
        `//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table/tbody/tr[2]/th[${i}]`
      )
        .invoke("text") 
        .then((text) => {
          dataArray.push(text.trim()); 
        });
    }

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table/tbody/tr[2]/th[8]/span/div',
      { timeout: 10000 }
    )
      .should("be.visible")
      .click();

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]/div[1]/h3'
    ).then(($h3) => {
      const h3Text = $h3[0].childNodes[0].nodeValue.trim();
      expect(h3Text).to.eq(dataArray[0]);
    });

    //cy.screenshot();

    //FT_HistoryExpenseExport_002_Success
    cy.xpath('//*[@id="btn-พิมพ์"]').should("be.visible").click();

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div[2]/button[2]'
    )
      .should("be.visible")
      .click();

    // ตรวจสอบว่ามีการดาวน์โหลดไฟล์
    const downloadsFolder = Cypress.config("downloadsFolder");
    const filename = "ExportedExpenseData.pdf";
    cy.readFile(`${downloadsFolder}/${filename}`).should("exist");
    //cy.screenshot();
  });
});
