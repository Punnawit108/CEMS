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

    it("TC10_1_2_001 - ตรวจการดูรายละเอียดผู้ใช้งาน", () => {
        // ดักจับ API request เพื่อตรวจสอบสถานะ
        cy.intercept('GET', '/api/user/*').as('getUserDetails');

        // 1. กดดูรายละเอียดผู้ใช้ที่ต้องการจะตรวจสอบในตาราง
        cy.get('table tbody tr:first-child th:nth-child(9) svg')
            .should('be.visible')
            .click();

        // ตรวจสอบว่า URL เปลี่ยนไปเป็น URL รายละเอียดผู้ใช้
        cy.url().should("include", "/systemSettings/user/detail/");


        // ตรวจสอบว่ามีการแสดงหน้าต่างรายละเอียดผู้ใช้
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div').should('be.visible');

        // 2. ตรวจสอบว่ามีการแสดงข้อมูลของผู้ใช้
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[3]/div[1]/div[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[3]/div[2]/div[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[3]/div[3]/div[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[3]/div[4]/div[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[3]/div[5]/div[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[3]/div[6]/div[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[3]/div[7]/div[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[3]/div[8]/div[2]').should('be.visible');
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/main/div/div/div[3]/div[9]/div[2]').should('be.visible');

        // รอและตรวจสอบ API response status
        cy.intercept("GET", "/api/user/*").as("getUserDetails");

        cy.wait('@getUserDetails').its("response.statusCode").should("eq", 200);
    });
});

