describe("Dashboard Tests", () => {
    beforeEach(() => {
      cy.login("65160341", "admin");
    });
  
    it("ตรวจสอบหน้า สร้างใบเบิกค่าใช้จ่าย", () => {
      cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
      cy.xpath(
        '//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]'
      ).click();
      cy.wait(1000);
      cy.xpath('//*[@id="SearchRqName"]').type('เบิกค่าไปเที่ยว');

      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[5]/div[2]/div[2]/button[2]').click();
      cy.contains('เบิกค่าไปเที่ยว').should('not.exist');
    });
    
     
   
  });
  