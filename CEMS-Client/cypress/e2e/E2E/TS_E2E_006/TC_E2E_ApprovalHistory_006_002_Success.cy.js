/*
 * ชื่อไฟล์: TC_E2E_ApprovalHistory_006_002_Success.cy.js
 * คำอธิบาย: E2E
 * ชื่อผู้เขียน/แก้ไข: นางสาวอังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข: 13/03/2568
 */
describe("ApprovalHistory Tests", () => {
  beforeEach(() => {
    cy.login("65160093", "admin");
  });

  it("ตรวจสอบหน้า ApprovalHistory", () => {
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
    cy.wait(1000);
    cy.xpath(
      '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[2]/a/a/div[1]'
    ).click();
    cy.wait(1000);
   
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[19]/tfoot/tr/th[3]/span[2]'
    ).click();
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[19]/tfoot/tr/th[3]/span[1]'
    ).click();
  });
});
