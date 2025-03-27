/*
 * ชื่อไฟล์: TC_E2E_ApprovalHistory_006_004_Success.cy.js
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
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[1]/th[8]/div'
    ).click();
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "รหัสรายการเบิก"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "โครงการ"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "วันที่เกิดค่าใช้จ่าย"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "วันที่ทำรายการเบิกค่าใช้จ่าย"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "ชื่อผู้เบิก"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "ชื่อผู้เบิกแทน"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "ประเภทค่าใช้จ่าย"
    );

    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "จำนวนเงิน(บาท)"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "ประเภทการเดินทาง"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "ประเภทรถ"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "ระยะทาง"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "อัตราค่าเดินทาง"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "สถานที่เริ่มต้น"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "สถานที่สิ้นสุด"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "รายละเอียด"
    );
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]').should(
      "contain.text",
      "อัปโหลดไฟล์"
    );
  });
});
