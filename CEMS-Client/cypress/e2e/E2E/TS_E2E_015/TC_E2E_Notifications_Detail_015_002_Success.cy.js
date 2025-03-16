/*
 * ชื่อไฟล์: TC_E2E_Notifications_List_015_001_Success.cy.js
 * คำอธิบาย: E2E
 * ชื่อผู้เขียน/แก้ไข: อังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข:  14/03/2568
 */
describe("Notifications Test", () => {
  beforeEach(() => {
    cy.login("65160095", "admin");
  });

  it("หลังจากเข้าสู่ระบบ", () => {
    // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[5]/a/button/div[1]').click();
    cy.wait(1000);

    cy.get(".whitespace-nowrap > .flex-wrap").click();
    cy.wait(1000);

    cy.get(".whitespace-nowrap > .flex-wrap").click();
    cy.wait(1000);

    cy.get(".whitespace-nowrap > .flex-wrap").click();
    cy.wait(1000);
  });
});
