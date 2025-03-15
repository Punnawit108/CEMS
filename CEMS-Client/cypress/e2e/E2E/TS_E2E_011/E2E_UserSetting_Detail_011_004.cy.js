/*
* ชื่อไฟล์: TC11_1_1_011-03.cy
* คำอธิบาย: ทดสอบการแก้ไขข้อมูลผู้ใช้
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข:  
*/
describe("UserSetting", () => {
    beforeEach(() => {
        cy.login("65160341", "admin");
    });

    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "ตั้งค่าระบบ" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
        // เลือกเมนู "ผู้ใช้งาน" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]').click();

        cy.wait(1000);

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/systemSettings/user');
        //กดไอคอนเพื่อดูรายละเอียดผู้ใช้่
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table/tbody/tr[1]/th[9]/span').click();

        cy.wait(1000);
        //กดปุ่มแก้ไข
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[1]/div/button').click();
        //กด Checkbox ให้สิทธิการดูรายงาน
        cy.xpath('//*[@id="viewReportPermission"]').click();
        //กดปุ่ม"ยืนยัน"
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[1]/div/button[1]').click();
        //กดปุ่ม "ยืนยัน" การเปลี่ยนแปลง
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[2]/button[2]').click();
    });
});