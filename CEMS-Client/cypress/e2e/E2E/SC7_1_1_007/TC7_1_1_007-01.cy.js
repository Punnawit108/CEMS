describe("รายงานโครงการ Tests", () => {
  beforeEach(() => {
    cy.login("65160095", "admin");
  });

  it("ตรวจสอบหน้า รายงานโครงการ", () => {
    cy.get(":nth-child(2) > .relative > .flex").click();
    cy.get(
      '[href="/disbursement/listWithdraw"] > .relative > .absolute'
    ).click();
cy.get(
  ':nth-child(1) > .py-[10px] > .flex > [data-v-9c67ba91=""] > :nth-child(1) > div > svg'
).click();

  });
});
