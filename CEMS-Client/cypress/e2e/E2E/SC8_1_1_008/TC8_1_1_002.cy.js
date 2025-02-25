describe("รายงานโครงการ Tests", () => {
  beforeEach(() => {
    cy.login("65160093", "admin");
  });

  it("ตรวจสอบหน้า รายงานโครงการ", () => {
    cy.get(":nth-child(2) > .relative > .flex").click();
    cy.get(
      '[href="/disbursement/historyWithdraw"] > .relative > .absolute'
    ).click();
    const searchKeyword = "โครงการที่ 1";
    cy.get(
      ".โครงการ > :nth-child(1) > .grid > .relative > .appearance-none"
    ).select(searchKeyword);
  });
});
