describe('Test New Feature', () => {
    before(() => {
        cy.login("65160095","8888888");
    });
    it('หลังจากเข้าสู่ระบบ', () => {
        cy.on('window:alert', (text) => {
            expect(text).to.equal('Username or Password wrong !');
          });          
    });
});