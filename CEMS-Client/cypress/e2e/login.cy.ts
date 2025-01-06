describe('Test New Feature', () => {
    before(() => {
        cy.login(); // ใช้ custom command login
    });

    it('should perform actions after login', () => {
        cy.get('.dashboard').should('be.visible');
    });
});