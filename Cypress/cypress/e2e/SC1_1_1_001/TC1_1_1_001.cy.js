describe('ทดสอบการเข้าสู่ระบบ', () => {
    it('เปิดหน้าเว็บ', () => {
        cy.visit('http://localhost:8080/');
    });
    it('กรอกชื่อผู้ใช้', () => {
        cy.get('#username').type('admin');
    });
    it('กรอกรหัสผ่าน', () => {
        cy.get('#password').type('admin');
    });
    it('คลิกที่ปุ่มเข้าสู่ระบบ', () => {
        cy.get('.btn').click();
    });
    it('เปลี่ยนหน้าไปที่หน้าแรกของระบบ', () => {
        cy.get('.message').should('contain', 'เข้าสู่ระบบสำเร็จ');
    });

});