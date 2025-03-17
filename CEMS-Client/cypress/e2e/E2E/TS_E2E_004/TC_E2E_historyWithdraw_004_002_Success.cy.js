/*เทสยังไม่เสร็จ Popup ไม่สมบูรณ์
* ชื่อไฟล์: TC_E2E_historyWithdraw_004_002_Success.cy.js
* คำอธิบาย: E2E ดูรายละเอียดคำขอเบิกค่าใช้จ่าย ที่มีสถานะ "ไม่อนุมัติ" ส่งออก(สำเร็จ)
* ชื่อผู้เขียน/แก้ไข: นายเทียนชัย คูเมือง
* วันที่จัดทำ/แก้ไข:  13 มีนาคม 2567
*/
describe("Dashboard Tests", () => {
  beforeEach(() => {
    cy.login("65160221", "admin");
  });

  it('หลังจากเข้าสู่ระบบ', () => {
    // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
    cy.wait(1000);
    // ตรวจสอบว่า dropdown แสดงผล
    cy.xpath("//ul[contains(@class, 'ml-8 mt-2')]").should('be.visible');
    // เลือก "ประวัติการเบิกจ่าย" จาก dropdown
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[2]/a/a/div[1]').click();

    // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
    cy.url().should('include', '/disbursement/historyWithdraw');
    // กดปุ่มไอคอน "จัดการ"
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table/tbody/tr[2]/th[8]/span/div').click();
    // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
    cy.url().should('include', '/disbursement/historyWithdraw/detail/');
    // ตรวจสอบว่าสถานะเป็น "ไม่อนุมัติ" หรือไม่
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div/div[1]/div[1]/h3/span')
    .should('have.text', 'ไม่อนุมัติ'); 

    cy.wait(1000);
    cy.xpath('//*[@id="btn-พิมพ์"]').click();
    

  });
});
