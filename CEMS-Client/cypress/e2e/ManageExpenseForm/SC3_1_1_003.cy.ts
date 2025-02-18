describe('SC3_1_1_003', () => {
    before(() => {
        cy.login();
        cy.url().should('eq', 'http://localhost:5173/dashboard');
    });

    it('TC3_1_1_001', () => {
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]').click();
        cy.url().should('include', '/disbursement/listWithdraw');
        cy.wait(1000);

        for (let col = 1; col <= 8; col++) {
            cy.xpath(`//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[9]/thead/tr/th[${col}]`)
                .should('be.visible')
                .and('have.css', 'font-family', 'Sarabun') // ตรวจสอบฟอนต์เป็น Sarabun
                .and('have.css', 'font-size', '14px') // ตรวจสอบ font-size = 14px
                .and('have.css', 'font-weight', '700')
                .and('have.css', 'color', 'rgb(0, 0, 0)'); 

        }
        cy.wait(1000);
        

        cy.get('th[data-v-9c67ba91] svg').eq(0).click();
    });
});




