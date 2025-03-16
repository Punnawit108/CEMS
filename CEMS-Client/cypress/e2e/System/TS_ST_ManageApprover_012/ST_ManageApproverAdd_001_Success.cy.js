describe("ทดสอบการเพิ่มผู้อนุมัติ",() => {

    beforeEach("ล็อกอินเข้าสู่ระบบ",() => {
        cy.login("65160341", "admin"); 
      });

    // it("ตรวจสอบการแสดงผลหลังเข้าสู่ระบบ",() => {
    //     cy.url().should('include','/dashboard')
    //     cy.screenshot()
    // });

    it("ทดสอบว่าผู้ดูแลระบบสามารถเพิ่มผู้อนุมัติได้หรือไม่",() => {
        cy.get('ul.flex > :nth-child(4)').click();
        cy.contains('ผู้อนุมัติการเบิกจ่าย').click();
        cy.get('.bg-green').click();
        cy.screenshot();
    })


})