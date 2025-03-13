describe("FT_ManageRequestApprove_001_Success", () => {
  beforeEach(() => {
    cy.login("65160356", "admin");
  });

  it("ตรวจสอบการทำงานของของระบบ เมื่อสร้างใบเบิกเป็นค่าเดินทาง รถสาธารณะ", () => {
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/button/div[1]').click();
    cy.xpath(
      '//*[@id="app"]/div/div/div/nav/ul/li[3]/ul/li[1]/a/a/div[1]'
    ).click();

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[1]/th[8]/div'
    ).click();

    let rqId; // สร้างตัวแปรเพื่อเก็บ rqId

    cy.location("pathname").then((pathname) => {
      const pathParts = pathname.split("/"); 
      rqId = pathParts[pathParts.length - 1]; 
    });

    let name, withdrawDate;

    // เก็บข้อมูลจาก element name
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[1]/div[3]/div[1]/p[2]'
    )
      .invoke("text")
      .then((text) => {
        name = text.trim();
      });

    // เก็บข้อมูล withdrawDate
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[1]/div[2]/div[4]/p[2]'
    )
      .invoke("text")
      .then((text) => {
        withdrawDate = "วันที่ขอเบิก " + text.trim(); //
      });

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div/button[3]'
    ).click();

    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div').should(
      "be.visible"
    );
    //ตรวจสอบว่าข้อความตรงกับ name
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div/div[1]/h1[1]'
    )
      .invoke("text")
      .then((h1Text) => {
        expect(h1Text.trim()).to.eq(name);
      });

    // ตรวจสอบว่าข้อความตรงกับ withdrawDate
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div/div[1]/h1[2]'
    )
      .invoke("text")
      .then((h1Text) => {
        expect(h1Text.trim()).to.eq(withdrawDate);
      });

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div/div[2]/button[2]'
    ).click();
    //cy.screenshot();

  });
});
