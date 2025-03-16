describe("Dashboard Tests", () => {
  beforeEach(() => {
    cy.login("65160341", "admin");
  });

  it("ตรวจสอบหน้า สร้างใบเบิกค่าใช้จ่าย", () => {
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
    cy.xpath(
      '//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]'
    ).click();
    cy.wait(1000);
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[3]/table[11]/tbody/tr[3]/th[8]/span/div[2]'
    ).click({ force: true });

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[4]/div/div[2]/form/button[2]'
    ).click();
  });
});
