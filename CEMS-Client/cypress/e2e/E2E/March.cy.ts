describe('Test New Feature', () => {
    before(() => {
        cy.login(); // ใช้ custom command login
    });

    it('หลังจากเข้าสู่ระบบ', () => {
        // เลือกเมนู "การเบิกจ่าย" จาก Sidebar
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
        cy.wait(1000);
        // ตรวจสอบว่า dropdown แสดงผล
        cy.xpath("//ul[contains(@class, 'ml-8 mt-2')]").should('be.visible');
        // เลือก "รายการเบิกค่าใช้จ่าย" จาก dropdown
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]').click();

        // ตรวจสอบว่า URL เปลี่ยนไปตามที่คาดไว้
        cy.url().should('include', '/disbursement/listWithdraw');
        // ตรวจสอบว่าหน้ามีข้อความ "รายการเบิกค่าใช้จ่าย"
        cy.get('h1').contains('รายการเบิกค่าใช้จ่าย')
            .should('be.visible') // ตรวจสอบว่า h1 มองเห็นได้
            .and('have.css', 'font-size', '24px') // ตรวจสอบขนาดตัวอักษร
            .and('have.css', 'font-family', 'Sarabun');

        // ตรวจสอบ header ใน table[9] (th[1] ถึง th[8]) ว่ามี font-family = Sarabun  font-size = 14px และ font-weight = 700 (bold)
        for (let col = 1; col <= 8; col++) {
            cy.xpath(`//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[9]/thead/tr/th[${col}]`)
                .should('be.visible')
                .and('have.css', 'font-family', 'Sarabun') // ตรวจสอบฟอนต์เป็น Sarabun
                .and('have.css', 'font-size', '14px') // ตรวจสอบ font-size = 14px
                .and('have.css', 'font-weight', '700'); // ตรวจสอบ font-weight = bold
                .and('have.css', 'color', 'rgb(0, 0, 0)'); 
        }
        cy.wait(1000);

        // ตรวจสอบข้อมูลในตาราง ว่ามี font-family = Sarabun  font-size = 14px 
        cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr')
            .its('length')
            .then((rowCount) => {
                for (let row = 1; row <= rowCount; row++) {
                    for (let col = 1; col <= 6; col++) {
                        cy.xpath(`//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[${row}]/th[${col}]`)
                            .should('be.visible')
                            .and('have.css', 'font-family', 'Sarabun') // ตรวจสอบฟอนต์เป็น Sarabun
                            .and('have.css', 'font-size', '14px'); // ตรวจสอบ font-size = 14px
                            .and('have.css', 'color', 'rgb(0, 0, 0)'); 
                    }
                }
            });

    });
});