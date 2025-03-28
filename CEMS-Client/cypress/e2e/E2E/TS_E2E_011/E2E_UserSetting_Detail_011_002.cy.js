/*
* ชื่อไฟล์: TC11_1_1_011-02.cy
* คำอธิบาย: ตรวจสอบข้อมูลรรายละัเอียดผู้ใช้งาน
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
    });
});