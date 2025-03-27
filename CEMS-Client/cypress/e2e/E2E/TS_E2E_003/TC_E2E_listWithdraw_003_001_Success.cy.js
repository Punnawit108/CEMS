/*
* ชื่อไฟล์: TC_E2E_listWithdraw_003_001_Success.cy.js
* คำอธิบาย: E2e สร้างใบเบิกค่าใช้จ่าย ยืนยัน Success
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

        cy.xpath('//*[@id="rqName"]').type('เบิกค่าใช้จ่าย');

        cy.xpath('/html/body/div/div/div/div/div/div[2]/form/div[2]/div[1]/div/div[5]/div/select')
            .select('งานเลี้ยง');

        cy.xpath('/html/body/div/div/div/div/div/div[2]/form/div[2]/div[1]/div/div[6]/select')
            .should('be.visible')
            .select('ค่าเดินทาง');

        cy.xpath('/html/body/div/div/div/div/div/div[2]/form/div[2]/div[1]/div/div[8]/div/select')
            .should('be.visible')
            .select('ประเภทสาธารณะ');

        cy.xpath('/html/body/div/div/div/div/div/div[2]/form/div[2]/div[1]/div/div[9]/div/select')
            .should('be.visible')
            .select('รถเมล์');

        cy.xpath('//*[@id="rqStartLocation"]').type('ศรีราชา');

        cy.xpath('//*[@id="rqEndLocation"]').type('บางแสน');

        cy.xpath('//*[@id="rqDistance"]').type('20');

        cy.xpath('//*[@id="rqExpenses"]').type('100');

        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[2]/div[2]/textarea').type('เบิกค่าเดินทาง');

        cy.wait(1000); // รอ 1 วินาที (ปรับตามสถานการณ์)
        cy.xpath('/html/body/div/div/div/div/div/div[2]/form/div[2]/div[3]/div/input')
            .attachFile('KARINA.pdf');

        cy.wait(1000);
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[1]/button[3]').click()
        cy.wait(1000);
        cy.get('.space-x-4 > .btn-ยืนยัน').click();

    });
});