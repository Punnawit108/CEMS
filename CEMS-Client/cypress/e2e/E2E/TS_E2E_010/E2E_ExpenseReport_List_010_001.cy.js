/*
* ชื่อไฟล์: TC10_1_1_010-01.cy
* คำอธิบาย: ตรวจสอบหน้ารายงานเบิกค่าใช้จ่าย
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข:  
*/
describe("ExpenseReport", () => {
    beforeEach(() => {
        cy.login("65160341", "admin");
    });

    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "รายงาน" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/button/div[1]').click();
        // เลือกเมนู "รายงานเบิกค่าใช้จ่าย" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[3]/ul/li[2]/a/a/div[1]').click();

        cy.wait(1000);

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/report/expense');
    });
});