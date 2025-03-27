/*
* ชื่อไฟล์: TC05_1_1_005-01.cy
* คำอธิบาย: ทดสอบการกรองหน้ารายการรออนุมัติ
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
        //พิมพ์ชื่อค้นหา
        cy.get('#SearchBar').type('Pornchai');
        //เลือกโครงการ
        cy.get('#SelectProject').select('งานเลี้ยง');

        //เลือกโครงการ
        cy.get('#SelectRequisitionType').select('ค่าอาหาร');

        // กดปุ่ม "ค้นหา"
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[5]/div[2]/div[2]/button[2]').click();
        cy.wait(1000);

    });
});