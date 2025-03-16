/*
 * ชื่อไฟล์: TS_E2E_DisbursementApprover_012_002_Success.cy.js
 * คำอธิบาย: E2e
 * ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข:  14/03/2568
 */
describe("DisbursementApprover Test", () => {
  beforeEach(() => {
    cy.login("65160341", "admin");
  });

  it("หลังจากเข้าสู่ระบบ", () => {
    // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
    cy.wait(1000);

    // เลือก "รายการเบิกค่าใช้จ่าย" จาก dropdown
    cy.xpath(
      '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[2]/a/a/div[1]'
    ).click();
    cy.wait(3000);
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div/div/div[2]/button[3]'
    ).click();
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[3]/div/div[1]/form/div/select'
    ).select("Angkana Aunseam");
    cy.get(".btn-ยืนยัน").click();

    cy.get(":nth-child(4) > .w-[460px]").should(
      "contain.text",
      "ยืนยันการเพิ่มผู้มีสิทธิ์อนุมัติสำเร็จ"
    );
  });
});
