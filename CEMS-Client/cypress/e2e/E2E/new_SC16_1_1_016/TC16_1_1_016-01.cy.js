/*
* ชื่อไฟล์: TC16_1_1_016-01.cy
* คำอธิบาย: E2e
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข:  
*/
describe("NotificationTest", () => {
    beforeEach(() => {
        cy.login("65160095", "admin");
    });

    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การแจ้งเตือน" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[6]/a/button/div[1]').click();
        cy.wait(1000);

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/notification');
        cy.wait(1000);

    });
});