describe("FT_Notification_001_Success", () => {
  beforeEach(() => {
    cy.login("65160356", "admin");
  });

  it("ตรวจสอบการทำงานของของระบบ เมื่อสร้างใบเบิกเป็นค่าเดินทาง รถสาธารณะ", () => {
    // FT_Notification_001_Success
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[5]/a/button/div[1]').click();

    // ตรวจสอบปุ่มที่ 1
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/nav/ul/li[1]/button'
    ).should("be.visible");

    // ตรวจสอบปุ่มที่ 2
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/nav/ul/li[2]/button'
    ).should("be.visible");

    // ตรวจสอบปุ่มที่ 3
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/nav/ul/li[3]/button'
    ).should("be.visible");

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/article/section[1]/div/h2/strong'
    )
      .should("be.visible")
      .should("not.be.empty");

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/article/section[1]/div/p'
    )
      .should("be.visible")
      .should("not.be.empty");

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/article/section[1]/time'
    )
      .should("be.visible")
      .should("not.be.empty");

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/article/section[1]'
    ).click(); // คลิกที่ element

    cy.xpath("//p[contains(@class, 'item')]").each(($el) => {
      // ดึงข้อความจาก element
      const text = $el.text().trim();

      // ตรวจสอบว่าข้อความไม่ว่างเปล่า
      expect(text).to.not.be.empty; // ตรวจสอบว่าข้อความไม่ว่าง
      expect(text).to.not.eq(" "); // ตรวจสอบว่าข้อความไม่ใช่ช่องว่างเดียว
    });
    //cy.screenshot();
  });
});
