import "cypress-file-upload";

describe("SC10_1_2_005", () => {
  beforeEach(() => {
    // ล็อกอินเข้าระบบก่อนเริ่มแต่ละกรณีทดสอบ
    cy.login("65160321", "admin");
    
    // นำทางไปหน้าจัดการผู้ใช้งาน
    cy.url().should("include", "/dashboard");
    // คลิกเมนูจัดการผู้ใช้งาน
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[5]/button/div[1]').should("be.visible").click();
    // คลิกเมนูย่อยผู้ใช้งาน
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[5]/ul/li[1]/a/a/div[1]').should("be.visible").click();
    // ตรวจสอบว่านำทางมาที่หน้าผู้ใช้งาน
    cy.url().should("include", "/systemSettings/user");
  });
  
  it("TC10_1_2_002 - แก้ไขผู้ใช้งาน (เปลี่ยนบทบาท)", () => {
    // ดักจับ API request เพื่อดูรายละเอียดผู้ใช้
    cy.intercept('GET', '/api/user/*', (req) => {
      // เพิ่ม headers ป้องกันการแคช
      req.headers['cache-control'] = 'no-cache, no-store, must-revalidate';
      req.headers['pragma'] = 'no-cache';
      req.headers['expires'] = '0';
      req.continue();
    }).as('getUserDetails');
    
    // ดักจับ API request สำหรับการแก้ไขบทบาทผู้ใช้
    cy.intercept('PUT', '/api/user/*').as('updateUser');
    
    // 1. กดดูรายละเอียดผู้ใช้ที่ต้องการจะตรวจสอบในตาราง
    cy.get('table tbody tr:first-child th:nth-child(9) svg')
      .should('be.visible')
      .click();

    // ตรวจสอบว่า URL เปลี่ยนไปเป็น URL รายละเอียดผู้ใช้
    cy.url().should("include", "/systemSettings/user/detail/");
    
    // รอให้โหลดข้อมูลผู้ใช้เสร็จสิ้น
    cy.wait('@getUserDetails', { timeout: 10000 });
    
    // 2. กดปุ่มแก้ไขผู้ใช้
    cy.get('button.btn-แก้ไขผู้ใช้')
      .should('be.visible')
      .click();
    
    // ตรวจสอบว่าเข้าสู่โหมดแก้ไข (URL ควรเปลี่ยนเป็นหน้าแก้ไข)
    cy.url().should("include", "/editUser");
    
    // 3. เลือกบทบาทใหม่
    cy.get('select#role')
      .should('be.visible')
      .select('ผู้ดูแลระบบ');
    
    // 4. กดปุ่มยืนยัน
    cy.get('button.btn-ยืนยัน')
      .should('be.visible')
      .click();
    
    // 5. กดยืนยันอีกครั้งในป๊อปอัพ
    cy.get('.fixed.inset-0.bg-black.bg-opacity-50 button.btn-ยืนยัน')
      .should('be.visible')
      .click();
    
    // รอให้ API request เสร็จสิ้น
    cy.wait('@updateUser', { timeout: 10000 }).then(interception => {
      expect(interception.response.statusCode).to.eq(200);
      cy.log('อัพเดทผู้ใช้สำเร็จ - status code 200');
    });
    
    // 6. ตรวจสอบว่าแสดงข้อความยืนยันการแก้ไขสำเร็จ
    cy.contains('ยืนยันการแก้ไขผู้ใช้สำเร็จ').should('be.visible');
    
    // รอให้ redirect กลับไปหน้ารายละเอียด
    cy.url().should('not.include', '/editUser');
  });
});

