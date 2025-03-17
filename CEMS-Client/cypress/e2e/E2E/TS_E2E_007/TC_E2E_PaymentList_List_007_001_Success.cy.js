/*
 * ชื่อไฟล์: TC_E2E_PaymentList_List_007_001_Success.cy.js
 * คำอธิบาย: E2E
 * ชื่อผู้เขียน/แก้ไข: นางสาวอังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข: 14/03/2564
 */
describe("PaymentList Tests", () => {
  beforeEach(() => {
    cy.login("65160095", "admin");
  });

  it("ตรวจสอบหน้า PaymentList", () => {
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
    cy.wait(1000);
    cy.xpath(
      '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]'
    ).click();
    cy.wait(1000);
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]'
    ).should("contain.text", "ลำดับ");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]'
    ).should("contain.text", "ชื่อ-นามสกุล");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]'
    ).should("contain.text", "รายการเบิก");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]'
    ).should("contain.text", "โครงการ");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]'
    ).should("contain.text", "ประเภทค่าใช้จ่าย");
     cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]').should(
       "contain.text",
       "วันที่ขอเบิก"
     );
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]'
    ).should("contain.text", "จำนวนเงิน (บาท)");

    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]'
    ).should("contain.text", "จัดการ");
  });
});
