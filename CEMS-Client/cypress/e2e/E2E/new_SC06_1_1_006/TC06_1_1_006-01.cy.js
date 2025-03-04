/*
* ชื่อไฟล์: TC05_1_1_006-01.cy
* คำอธิบาย: E2e
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข:  
*/
describe("ManageApprovalHistory", () => {
    beforeEach(() => {
        cy.login("65160095", "admin");
    });

    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การอนุมัติ" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
        

        // เลือก "ประวัติการอนุมัติ" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[2]/a/a/div[1]').click();

        cy.wait(1000);
        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/approval/history');


    });
});