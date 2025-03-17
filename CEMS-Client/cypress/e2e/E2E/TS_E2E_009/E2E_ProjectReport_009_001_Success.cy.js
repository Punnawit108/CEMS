describe("LoginTest", () => {
  before(() => {
    cy.login("65160095", "admin");
  });
  it("รายงานโครงการ", () => {
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/button/div[1]').click();
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/ul/li[1]/a/a/div[1]').click();
    cy.url().should('include', '/report/project');
    cy.get('#barChart').should('be.visible');
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[2]').should('be.visible');
  });
});
