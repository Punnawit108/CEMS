/*
* ชื่อไฟล์: TC05_1_1_005-03.cy
* คำอธิบาย: ไม่อนุมัติคำขอเบิก
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข:  
*/
describe("ApprovalLis", () => {
    beforeEach(() => {
        cy.login("65160356", "admin");
    });

    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การอนุมัติ" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/button/div[1]').click();
        cy.wait(1000);

        // เลือก "รายการรออนุมัติ" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/ul/li[1]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/approval/list');

        cy.wait(1000);

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr/th[8]').click();
        cy.wait(1000);


        cy.xpath('//*[@id="btn-ไม่อนุมัติ"]').click();

        cy.xpath('//*[@id="rqReason"]').type('cypress test');

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div/div[2]/button[2]').click();

        cy.wait(1000);



    });
});