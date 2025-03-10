describe("Dashboard Tests", () => {
  beforeEach(() => {
    cy.login("65160341", "admin");
  });

  it("ตรวจสอบหน้า Dashboard", () => {
    cy.contains("ผู้ใช้งานทั้งหมด");
    cy.wait(1000);
    cy.contains("คำขอเบิกจ่ายทั้งหมด");
    cy.wait(1000);
    cy.contains("โครงการทั้งหมด");
    cy.wait(1000);
    cy.contains("ยอดเบิกจ่ายแล้ว (บาท)");
    cy.wait(1000);
    cy.contains("ลำดับการเบิกสูงสุดของโครงการ");
    cy.wait(1000);
    cy.contains("ลำดับ");
    cy.wait(1000);
    cy.contains("โครงการ");
    cy.wait(1000);
    cy.contains("จำนวนเงิน");
    cy.wait(1000);
    cy.contains("ประเภทค่าใช้จ่ายของรายการเบิก");
    cy.wait(1000);
    cy.contains("ยอดการเบิกจ่ายจริง");
    cy.wait(1000);
    cy.get("#pieChart");
    cy.wait(1000);
    cy.get("#lineChart");
  });
});
