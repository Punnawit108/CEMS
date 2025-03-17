describe('LoginTest', () => {
    before(() => {
        cy.login("65160000","admin");
    });
    it('หลังจากเข้าสู่ระบบ', () => {
        cy.on('window:alert', (text) => {
            expect(text).to.equal('Username or Password wrong !');
          });          
    });
});