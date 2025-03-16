describe('Test New Feature', () => {
    before(() => {
        
        cy.login("65160095","admin");
    });
    it('หลังจากเข้าสู่ระบบ', () => {
        cy.url().should('include', '/dashboard');
    });
});