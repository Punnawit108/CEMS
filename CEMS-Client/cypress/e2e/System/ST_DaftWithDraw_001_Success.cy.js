describe("ทดสอบการอนุมัติ",() => {

    beforeEach("ล็อกอินเข้าสู่ระบบ",() => {
        cy.login("65160095", "admin"); 
      });

    it("ตรวจสอบการบันทึกคำขอเบิกค่าใช้จ่าย โดยกระบวนการตั้งแต่ต้นจนจบ",() => {
         cy.url().should('include','/dashboard')
         cy.screenshot()
    // });

        cy.get(':nth-child(2) > .relative > .flex').click();
        cy.contains('รายการเบิกค่าใช้จ่าย').click();
        cy.url().should('include','/disbursement/listWithdraw');
        cy.screenshot();

        //it("ตรวจสอบการแสดงผลหน้าสร้างใบเบิกค่าใช้จ่าย",()=>{
            cy.get('.btn-สร้างใบเบิกค่าใช้จ่าย').click();
            cy.url().should('include','/createExpenseForm');
            cy.screenshot();

            
           // it("ตรวจสอบการบันทึกข้อมูล",()=>{
                cy.get('#rqName').type("เบิกค่าใช้จ่าย");
                cy.get('#rqPayDate > .custom-select').click().get(':nth-child(31)').click().get('.justify-end > .bg-blue-500').click();
                cy.get('select#projectName').select("งานเลี้ยง");
                cy.get('select#expenseType').select("ค่าอาหาร");
                cy.get('#rqExpenses').type("280");
                cy.get('select#rqInsteadEmail').select("Alisa Pakungpalung");
                cy.get('textarea').type("เบิกค่าอาหาร");
                cy.screenshot();
                //cy.get('.mt-1 > .flex').
                
                cy.get('#btn-บันทึก').click()
                cy.contains("ยืนยันการบันทึกคำขอเบิกค่าใช้จ่าย").should('be.visible');
                cy.screenshot();
                cy.get('.space-x-4 > .btn-ยืนยัน').click();
                cy.get('.m-4 > :nth-child(4) > .bg-white').should('be.visible');
                cy.screenshot();

                cy.url().should('include','/disbursement/listWithdraw');
                cy.contains("เบิกค่าใช้จ่าย").should('be.visible');
                cy.contains("งานเลี้ยง").should('be.visible');
                cy.contains("ค่าอาหาร").should('be.visible');
                cy.contains("2568-02-25").should('be.visible');
                cy.contains("แบบร่าง").should('be.visible');
                cy.get('path').should('be.visible');
                cy.contains("280.00").should('be.visible');
                cy.screenshot();



                //})

       // })
    })


})