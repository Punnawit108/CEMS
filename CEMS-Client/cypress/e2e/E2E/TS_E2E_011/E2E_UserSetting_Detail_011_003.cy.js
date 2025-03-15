/*
* ชื่อไฟล์: TC11_1_1_011-05.cy
* คำอธิบาย: ทดสอบการกรอง
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
       
        //พิมพ์ค้นหาเลขพนักงาน
        cy.get('#SearchBar').type('65160356');

        //เลือกแผนก "Development" 
        cy.get('#SelectDepartment').select('Development'); 
        //เลือกฝ่าย "IT" 
        cy.get('#SelectDivision').select('IT');
        //เลือกบทบาท "Admin" 
        cy.get('#SelectRole').select('Admin');

        cy.wait(1000);

        //กดปุ่ม "ค้นหา" 
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[5]/div[2]/button[2]').click();

        cy.wait(1000);

        //กดปุ่ม "ล้าง" 
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[5]/div[2]/button[1]').click();
    });
});