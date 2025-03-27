/*
* ชื่อไฟล์: TC10_1_1_010-01.cy
* คำอธิบาย: ตรวจสอบการส่งออกรายงานการเบิกจ่ายของระบบ
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

        // กดปุ่ม "ส่งออก"
        cy.xpath('//*[@id="btn-พิมพ์"]').click();
        // เลือกการส่งออกแบบ PDF
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[4]/div/div/div[1]/button[1]').click();
        // กดปุ่มยืนยัน
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[4]/div/div/div[3]/button[2]').click();

        
        // กดปุ่ม "ส่งออก"
        cy.xpath('//*[@id="btn-พิมพ์"]').click();
        // เลือกการส่งออกแบบ XLSX
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[4]/div/div/div[1]/button[2]').click();
        // กดปุ่มยืนยัน
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[4]/div/div/div[3]/button[2]').click();
    });
});