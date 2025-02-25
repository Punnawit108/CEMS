describe("รายงานโครงการ Tests", () => {
  beforeEach(() => {
    cy.login("65160341", "admin");
  });

  it("ตรวจสอบหน้า รายงานโครงการ", () => {
    cy.get(":nth-child(3) > .relative > .flex").click();
    cy.get(".text-lg").click();
    cy.get('[href="/report/project"] > .relative > .absolute').click();
    cy.get("#btn-พิมพ์").click();
    cy.get(".space-x-6 > :nth-child(1)").click();
    cy.get(".space-x-4 > .text-white").click();
  });
});
