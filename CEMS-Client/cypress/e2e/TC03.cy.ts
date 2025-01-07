describe('Test New Feature', () => {
    before(() => {
        cy.login(); // ใช้ custom command login
    });

    it('should perform actions after login', () => {
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();

        // เพิ่มเวลารอให้ dropdown แสดง
        cy.wait(1000); // รอ 1 วินาที

        // ตรวจสอบว่า dropdown เปิดขึ้น
        cy.xpath("//ul[contains(@class, 'ml-8 mt-2')]").should('be.visible');

        // เลือกตัวเลือกจาก dropdown โดยใช้ XPath
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/disbursement/listWithdraw');   

        cy.wait(1500);
        cy.get('th[data-v-9c67ba91] svg').eq(10).click();

        
    });
});
