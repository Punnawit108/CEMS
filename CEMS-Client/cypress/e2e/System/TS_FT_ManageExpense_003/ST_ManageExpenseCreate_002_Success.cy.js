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
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/a/button').click();
    cy.xpath('//*[@id="rqName"]').type(
      "เบิกค่าใช้จ่าย"
    );

    cy.xpath('//*[@id="projectName"]').select('งานเลี้ยง');
    cy.xpath('//*[@id="rqStartLocation"]').type("หมอชิด");
    cy.xpath('//*[@id="rqEndLocation"]').type("บางแสน");
    cy.xpath('//*[@id="rqDistance"]').type("180");
    cy.xpath('//*[@id="rqExpenses"]').type("500");
    cy.xpath('//*[@id="rqInsteadEmail"]').select('Alisa Pakungpalung');
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[2]/div[2]/textarea').type("เบิกค่าเดินทาง");
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[1]/button[3]').click();
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[1]/button[3]')
  .click({ force: true });
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[3]/div/div[2]/button[2]').click();

  });
  
   
 
});
