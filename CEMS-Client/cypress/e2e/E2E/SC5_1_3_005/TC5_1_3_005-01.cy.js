/*
 * ชื่อไฟล์: TC5_1_3_005-01.cy
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
    cy.get("#barChart");

    // cy.get(".bg-[#F2F4F8] > .text-[16px] > .w-auto");
  });
});
