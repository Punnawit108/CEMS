describe("Dashboard Tests", () => {
  beforeEach(() => {
    cy.login("65160341", "admin");
  });

  it("ตรวจสอบหน้า Dashboard", () => {
    cy.get(":nth-child(1) > .font35");
    cy.wait(1000);
    cy.get(":nth-child(2) > .font35");
    cy.wait(1000);
    cy.get(":nth-child(3) > .font35");
    cy.wait(1000);
    cy.get(":nth-child(4) > .font35");

  });
});
