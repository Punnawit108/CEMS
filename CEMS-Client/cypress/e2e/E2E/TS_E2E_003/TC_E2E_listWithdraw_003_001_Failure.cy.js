/*
* ชื่อไฟล์: TC_E2E_listWithdraw_003_001_Failure.cy.js
* คำอธิบาย: E2e สร้างใบเบิกค่าใช้จ่าย ยืนยัน Failure
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข: 13 มีนาคม 2568
*/
describe("ManageExpeneFormTest", () => {
    beforeEach(() => {
        cy.login("65160221", "admin");
    });

    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
        cy.wait(1000);
        // ตรวจสอบว่า dropdown แสดงผล
        cy.xpath("//ul[contains(@class, 'ml-8 mt-2')]").should('be.visible');
        // เลือก "รายการเบิกค่าใช้จ่าย" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/disbursement/listWithdraw');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/a/button').click()

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[1]/button[3]').click()
        cy.wait(1000);


    });
});