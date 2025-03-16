/*
* ชื่อไฟล์: TC11_1_1_011-02.cy
* คำอธิบาย: E2e
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข:  
*/
describe("ManageExpeneFormTest", () => {
    beforeEach(() => {
        cy.login("65160093", "admin");
    });

    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
        cy.wait(1000);

        // เลือก "รายการเบิกค่าใช้จ่าย" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/approval/list');

        cy.wait(1000);

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr/th[8]').click();
        cy.wait(1000);

    });
});