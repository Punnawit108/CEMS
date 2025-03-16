describe("Dashboard Tests", () => {
    beforeEach(() => {
        cy.login("65160341", "admin");
    });

    it("เข้าสู่หน้า รายงานการเบิกค่าใช้จ่าย", () => {
        cy.url().should('eq', 'http://localhost:5173/dashboard');
        cy.get(":nth-child(2) > .relative > .flex").click();
        cy.contains("รายการเบิกค่าใช้จ่าย").click();
        cy.url().should("include", "/disbursement/listWithdraw");
        cy.screenshot();

        cy.contains("button", "สร้างใบเบิกค่าใช้จ่าย").click();
        cy.url().should("include", "/createExpenseForm");
        cy.screenshot();

        cy.get("#rqName").type("เบิกค่าใช้จ่าย");
        cy.get("#rqPayDate > .custom-select")
            .click()
            .get(":nth-child(31)")
            .click()
            .get(".justify-end > .bg-blue-500")
            .click();
        cy.get("#projectName").select(1);
        cy.get("#expenseType").select(2);
        cy.get("#travelType").select("public");
        cy.wait(1000);
        cy.get("#vehicle").select("รถไฟฟ้า");
        cy.get("#rqStartLocation").type("หมอชิต");
        cy.get("#rqEndLocation").type("บางแสน");
        cy.get("#rqDistance").type("180");
        cy.get("#rqExpenses").type(500);
        cy.get("#rqInsteadEmail").select("65160242@go.buu.ac.th");
        cy.xpath(
            '//[@id="app"]/div/div/div/div/div[2]/form/div[2]/div[2]/textarea'
        ).type("เบิกค่าเดินทาง");
        cy.screenshot();

        cy.get("button.btn-ยืนยัน")
            .click();
        cy.xpath(
            '//[@id="app"]/div/div/div/div/div[2]/form/div[3]/div/div[2]/button[2]'
        ).click();
        cy.screenshot();
    });
});