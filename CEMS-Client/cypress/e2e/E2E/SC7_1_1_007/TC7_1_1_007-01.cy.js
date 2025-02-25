/*
 * ชื่อไฟล์: TC7_1_1_007-01.cy
 * คำอธิบาย: E2E
 * ชื่อผู้เขียน/แก้ไข: นางสาวอังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข: 24/03/2564
 */
describe("รายงานโครงการ Tests", () => {
  beforeEach(() => {
    cy.login("65160095", "admin");
  });

  it("ตรวจสอบหน้า รายงานโครงการ", () => {
    cy.get(":nth-child(2) > .relative > .flex").click();
    cy.get(
      '[href="/disbursement/listWithdraw"] > .relative > .absolute'
    ).click();
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table/tbody/tr[1]/th[8]/span/div/div/div'
    ).click();
  });
});
