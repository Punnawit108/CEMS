/*
* ชื่อไฟล์: TC10_1_1_010-01.cy
* คำอธิบาย: E2E จัดการผู้ใข้
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข: 27 กุมภาพันธ์ 2567
*/
describe("Dashboard Tests", () => {
    beforeEach(() => {
      cy.login("65160341", "admin"); 
    });
  
    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "ตั้งค่า" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
        cy.wait(1000);
        // ตรวจสอบว่า dropdown แสดงผล
        cy.xpath("//ul[contains(@class, 'ml-8 mt-2')]").should('be.visible');
        // เลือก "ผู้ใช้งาน" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/systemSettings/user');
        cy.wait(1000);

        //เลือกไอคอน "จัดการ" คนที่ 1
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table/tbody/tr[1]/th[9]/span/div/div/div/div').click();
        //เลือกปุ่ม "แก้ไข"
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[1]/div').click();
        //กดปุ่ม "ยืนยัน"
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[1]/div/button[1]').click();
        //กดปุ่ม "ยืนยัน" ใน Popup
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[2]/button[2]').click();
    });
  });