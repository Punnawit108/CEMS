/*
 * ชื่อไฟล์: TC5_1_3_005-04.cy
 * คำอธิบาย: E2E
 * ชื่อผู้เขียน/แก้ไข: นางสาวอังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข: 24/03/2564
 */
describe("รายงานโครงการ Tests", () => {
  beforeEach(() => {
    cy.login("65160341", "admin");
  });

  it("ตรวจสอบหน้า รายงานโครงการ", () => {
    cy.get(":nth-child(3) > .relative > .flex").click();
    cy.get(".text-lg").click();
    cy.get('[href="/report/project"] > .relative > .absolute').click();
    const searchKeyword = "ประเภทที่ 1";
    cy.get(".custom-select").select(searchKeyword);
  });
});
