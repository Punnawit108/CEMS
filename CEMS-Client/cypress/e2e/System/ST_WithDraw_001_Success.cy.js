describe(('ทดสอบกระบวนการการนำจ่ายของนักบัญชี'), () => {
    beforeEach(() => {
        cy.login("65160095", "admin")
    });

    it('ST_WithDraw_001_Success', () => {
        cy.get(':nth-child(5) > .relative > .flex').click();
        cy.get('[href="/payment/list"] > .relative > .absolute').click();
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[2]/table/tbody/tr/th[7]/span/div/div/div').click();
        cy.get('.btn-นำจ่าย').click();
        cy.get('.btn-ยืนยัน').click();
    });
});