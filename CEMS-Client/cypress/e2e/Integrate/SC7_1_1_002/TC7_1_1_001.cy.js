describe(('SC7_1_1_001'), () => {
    beforeEach(() => {
        cy.login("65160095","admin")
    });

    it('TC7_1_1_001', () => {   
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]').click();
        cy.xpath('//*[@id="app"]/div/div/div/div/div[1]/div[2]/div[1]/a/button').click();

        cy.get('#rqName').type('เบิกค่าอาหาร')
        cy.get('#rqPayDate > .custom-select').click()
        cy.xpath('//*[@id="rqPayDate"]/div[2]/div[1]/div/select[2]').select(1)
        cy.get(':nth-child(35)').click();
        cy.get('.justify-end > .bg-blue-500').click();
        cy.get('#projectName').select(4)
        cy.wait(1000)
        cy.xpath('//*[@id="expenseType"]').select(1);
        cy.get('#rqExpenses').type('800')
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[2]/div[2]/textarea').type('ไปหาลูกค้า', {force: true})
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[2]/div[3]/div/input').selectFile('cypress/fixtures/image/evidence.pdf',{force: true})

    });
});