/*
* ชื่อไฟล์: TC05_1_1_005-01.cy
* คำอธิบาย: ทดสอบแสดงหน้ารายการรออนุมัติ
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
        
        // กดปุ่มถัดไป
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[19]/tfoot/tr/th[3]/span[2]').click();
        // กดปุ่มย้อนกลับ
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[19]/tfoot/tr/th[3]/span[1]').click();
    });
});