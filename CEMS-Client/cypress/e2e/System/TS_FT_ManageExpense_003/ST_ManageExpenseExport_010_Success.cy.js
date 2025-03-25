describe("Dashboard Tests", () => {
    beforeEach(() => {
      cy.login("65160341", "admin");
    });
  
    it("ตรวจสอบหน้า สร้างใบเบิกค่าใช้จ่าย", () => {
      cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
      cy.xpath(
        '//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]'
      ).click();
      cy.wait(4000);
      // ใช้ cy.get() กับ class ของไอคอน
      cy.get(".cursor-pointer") // เลือกทุกไอคอนที่มี class 'cursor-pointer'
        .first() // เลือกไอคอนตัวแรกในกรณีที่มีหลายตัว
        .click({ force: true }); // คลิกที่ไอคอนตัวนั้นทันที
    cy.xpath('//*[@id="btn-พิมพ์"]').click();
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div[2]/button[2]').click();
    });
  });
  