/*
 * ชื่อไฟล์: TC_E2E_ApprovalHistory_006_001_Success.cy.js
 * คำอธิบาย: E2E
 * ชื่อผู้เขียน/แก้ไข: นางสาวอังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข: 13/03/2568
 */
describe("ApprovalHistory Tests", () => {
  beforeEach(() => {
    cy.login("65160093", "admin");
  });

  it("ตรวจสอบหน้า ApprovalHistory", () => {
       cy.xpath(
         '//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]'
       ).click();
       cy.wait(1000);
       cy.xpath(
         '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[2]/a/a/div[1]'
       ).click();
       cy.wait(1000);
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[8]'
    ).should("contain.text", "ลำดับ");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[8]'
    ).should("contain.text", "ชื่อ-นามสกุล");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[8]'
    ).should("contain.text", "รายการเบิก");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[8]'
    ).should("contain.text", "โครงการ");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[8]'
    ).should("contain.text", "ประเภทค่าใช้จ่าย");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[8]'
    ).should("contain.text", "จำนวนเงิน(บาท)");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[8]'
    ).should("contain.text", "สถานะ");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[8]'
    ).should("contain.text", "จัดการ");
    // cy.get(".text-[16px] > .w-14");
    // cy.get(".text-[16px] > :nth-child(2)");
    // cy.get(".text-[16px] > :nth-child(4)");
    // cy.get(".text-[16px] > .px-5.text-start");
    // cy.get(".text-[16px] > .text-end");
    // cy.get(".text-[16px] > .w-28");
    // cy.get(".text-[16px] > .w-20");
  });
});
