/*
 * ชื่อไฟล์: TC8_1_1_008-03.cy
 * คำอธิบาย: E2E
 * ชื่อผู้เขียน/แก้ไข: นางสาวอังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข: 24/03/2564
 */
describe("รายงานโครงการ Tests", () => {
  beforeEach(() => {
    cy.login("65160093", "admin");
  });

  it("ตรวจสอบหน้า รายงานโครงการ", () => {
    cy.get(":nth-child(2) > .relative > .flex").click();
    cy.get(
      '[href="/disbursement/historyWithdraw"] > .relative > .absolute'
    ).click();
    const searchKeyword = "อังคณา อุ่นเสียม";
    cy.get(
      ".ค้นหา > :nth-child(1) > .grid > .relative > .appearance-none"
    ).tpye(searchKeyword);
  });
});
