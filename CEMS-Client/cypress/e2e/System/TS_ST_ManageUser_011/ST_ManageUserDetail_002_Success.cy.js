describe("ทดสอบการแสดงรายละเอียดผู้ใชังาน",() => {

    beforeEach("ล็อกอินเข้าสู่ระบบ",() => {
        cy.login("65160341", "admin"); 
      });

    // it("ตรวจสอบการแสดงผลหลังเข้าสู่ระบบ",() => {
    //     cy.url().should('include','/dashboard')
    //     cy.screenshot()
    // });

    it("ทดสอบว่าผู้ใช้สามารถดูรายละเอียดข้อมูลผู้ใช้ได้หรือไม่ ซึ่งรวมไปถึงการดูสิทธิการดูรายงาน",() => {
        cy.get('ul.flex > :nth-child(4)').click();
        cy.contains('ผู้ใช้งาน').click();
        cy.get(':nth-child(1) > .py-\[10px\] > .flex > .cursor-pointer > div > .w-\[undefinedpx\]').click();

    })


})