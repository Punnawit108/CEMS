describe("ทดสอบการลบผู้อนุมัติ",() => {

    beforeEach("ล็อกอินเข้าสู่ระบบ",() => {
        cy.login("65160341", "admin"); 
      });

    // it("ตรวจสอบการแสดงผลหลังเข้าสู่ระบบ",() => {
    //     cy.url().should('include','/dashboard')
    //     cy.screenshot()
    // });

    it("ทดสอบว่าผู้ดูแลระบบสามารถลบลำดับผู้อนุมัติได้หรือไม่",() => {
        cy.get('ul.flex > :nth-child(4)').click();
        cy.contains('ผู้อนุมัติการเบิกจ่าย').click();
        cy.get(':nth-child(2) > .ml-5 > .mx-3 > .w-\[undefinedpx\] > path').click();
        cy.screenshot();
    })


})