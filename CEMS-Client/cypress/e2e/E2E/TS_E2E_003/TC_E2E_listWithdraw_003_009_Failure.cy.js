/*
* ชื่อไฟล์: TC_E2E_listWithdraw_003_009_Failure.cy.js
* คำอธิบาย: E2e Filter หน้ารายการเบิกค่าใช้จ่าย Failure
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข: 14 มีนาคม 2568
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

        // กรองข้อมูลด้วยคำว่า "เบิกค่าใช้จ่าย"
        cy.xpath('//*[@id="SearchRqName"]').type('เบิกค่าใช้จ่าย');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[5]/div[2]/div[2]/button[2]').click();
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[3]/table[11]/tbody/tr').each(($row) => {
            cy.wrap($row).should('contain', 'มาชจ้า');
        });
        cy.wait(1000);
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[5]/div[2]/div[2]/button[1]').click();

        // กรองข้อมูลด้วยคำว่า "งานเลี้ยง"
        cy.xpath('/html/body/div/div/div/div/div/div[2]/div/div[2]/div[2]/form/div/select').select('งานเลี้ยง');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[5]/div[2]/div[2]/button[2]').click();
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[3]/table[11]/tbody/tr').each(($row) => {
            cy.wrap($row).should('contain', 'สัมนา');
        });
        cy.wait(1000);
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[5]/div[2]/div[2]/button[1]').click();

        // กรองข้อมูลด้วยคำว่า "ค่าเดินทาง"
        cy.xpath('/html/body/div/div/div/div/div/div[2]/div/div[2]/div[3]/form/div/select').select('ค่าเดินทาง');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[5]/div[2]/div[2]/button[2]').click();
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[3]/table[11]/tbody/tr').each(($row) => {
            cy.wrap($row).should('contain', 'ค่าอาหาร');
        });
        cy.wait(1000);
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[5]/div[2]/div[2]/button[1]').click();
        
        
    });
});