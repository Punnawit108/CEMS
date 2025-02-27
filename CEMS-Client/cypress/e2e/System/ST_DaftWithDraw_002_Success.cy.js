describe("ทดสอบการอนุมัติ",() => {

    beforeEach("ล็อกอินเข้าสู่ระบบ",() => {
        cy.login("65160095", "admin"); 
      });

    // it("ตรวจสอบการแสดงผลหลังเข้าสู่ระบบ",() => {
    //     cy.url().should('include','/dashboard')
    //     cy.screenshot()
    // });

    it("ตรวจสอบการแจ้งเตือนการกรอกคำขอเบิกค่าใช้จ่ายไม่ครบ",() => {
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
                //cy.get('#rqExpenses').type("280");
                cy.get('select#rqInsteadEmail').select("Alisa Pakungpalung");
                cy.get('textarea').type("เบิกค่าอาหาร");
                //cy.get('.mt-1 > .flex').
                cy.screenshot();
                cy.get('#btn-บันทึก').click()
                cy.get('#rqExpenses').should('have.class','error');
                cy.screenshot();
        

            
          



                //})

       // })
    })


})