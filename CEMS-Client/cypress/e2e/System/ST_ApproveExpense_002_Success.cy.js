describe("ทดสอบการอนุมัติ", () => {
    beforeEach("ล็อกอินเข้าสู่ระบบ", () => {
      cy.login("65160093", "admin");
    });
  
    it("ตรวจสอบการอนุมมัติคำขอเบิก โดยกระบวนการตั้งแต่ต้นจนจบ", () => {
      cy.xpath(
        '//*[@id="app"]/div/div/div/main/div/div[2]/div/div[2]/form/button'
      ).click();
      cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
      cy.xpath(
        '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]'
      ).click();
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[1]/th[8]/div/div/div')
    .click({ force: true });
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div/button[2]').click();
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div/div[2]/button[2]').click();
      cy.xpath('//*[@id="rqReason"]').type('หลักฐานไม่ชัดเจน', { force: true });
      cy.wait(1000);
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div/div[2]/button[2]').click();
      //})
  
      // })
    });
  });
  