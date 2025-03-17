/*
* ชื่อไฟล์: TC_E2E_listWithdraw_003_005_Success.cy.js
* คำอธิบาย: E2e ดูรายละเอียดคำขอเบิกค่าใช้จ่าย ที่มีสถานะ "แบบร่าง" ส่งออก Success
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
        // คลิกไอคอน จัดการ ของคนที่ 2
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[3]/table[11]/tbody/tr[2]/th[8]/span/div[1]').click();
        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/disbursement/listWithdraw/detail/');
        // ตรวจสอบว่าสถานะเป็น "แบบร่าง" หรือไม่
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]/div[1]/h3/span')
            .should('have.text', 'แบบร่าง'); 
        // กดปุ่ม "ส่งออก"
        cy.xpath('//*[@id="btn-พิมพ์"]').click();
        // กดปุ่ม "ยืนยัน"
        cy.get('.space-x-4 > .btn-ยืนยัน').click();
        cy.wait(1000);

    });
});