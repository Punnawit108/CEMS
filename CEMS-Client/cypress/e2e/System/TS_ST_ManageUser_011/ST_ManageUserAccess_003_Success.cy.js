describe("ทดสอบการการดูรายงานของผู้ใช้",() => {

    beforeEach("ล็อกอินเข้าสู่ระบบ",() => {
        cy.login("65160341", "admin"); 
      });

    // it("ตรวจสอบการแสดงผลหลังเข้าสู่ระบบ",() => {
    //     cy.url().should('include','/dashboard')
    //     cy.screenshot()
    // });

    it("ทดสอบว่าผู้ใช้สามารถจัดการสิทธิการเข้าถึงได้",() => {
        cy.get('ul.flex > :nth-child(4)').click();
        cy.contains('ผู้ใช้งาน').click();
        cy.get(':nth-child(1) > .py-\[10px\] > .flex > .cursor-pointer > div > .w-\[undefinedpx\]').click();
        cy.get('.btn-แก้ไขผู้ใช้').click();
        cy.get('#viewReportPermission').click();
        cy.get('#btn-บันทึก').click();
        cy.get('.space-x-4 > .btn-ยืนยัน').click();
    })


})