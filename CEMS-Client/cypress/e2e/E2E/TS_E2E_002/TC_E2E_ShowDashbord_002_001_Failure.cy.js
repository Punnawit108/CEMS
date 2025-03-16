/*
* ชื่อไฟล์: TC_E2E_ShowDashbord_001_Failure.cy.js
* คำอธิบาย: E2e ShowDashbord Failure
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข: 12 มีนาคม 2568
*/
describe("Dashboard Tests", () => {
  beforeEach(() => {
    cy.login("65160095", "admin");
  });

  it("ตรวจสอบหน้า Dashboard", () => {
    cy.url().should('include', '/dashboard');
    cy.contains("ลำดับการเบิกสูงสุดของโครงการ").should('not.be.visible');
    cy.wait(1000);
    cy.get("#pieChart").should('not.be.visible');
    cy.wait(1000);
    cy.get("#lineChart").should('not.be.visible');
  });
});
