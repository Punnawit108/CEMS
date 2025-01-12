describe('ตรวจสอบข้อมูลหน้า details ของรายการเบิก', () => {
    beforeEach(() => {
        cy.login(); // ทำการ login ก่อนทดสอบแต่ละ test case
    });

    it('should navigate to the details page', () => {
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]').click();
        cy.wait(1000); // รอให้ dropdown แสดง
        cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]').click();
        cy.url().should('include', '/disbursement/listWithdraw'); // ตรวจสอบ URL
        cy.wait(1500);
        cy.get('th[data-v-9c67ba91] svg').eq(5).click();
        cy.wait(1500);

        //header 

        //ข้อความรายละเอียด
        cy.get('div.w-full.inline-flex.justify-between.items-center.pt-6')
            .find('h1')
            .should('be.visible')
            .and('have.text', 'รายละเอียด');

        const expectedTexts = [
            'การเบิกค่าใช้จ่าย',  // ข้อความใน <a> ตัวแรก
            'รายการเบิกค่าใช้จ่าย',      // ข้อความใน <a> ตัวที่สอง
            'รายละเอียด'       // ข้อความใน <span> ตัวที่สาม
        ];

        //ข้อความ breadcrumb
        cy.get('nav.bg-white.px-\\[24px\\]')
            .find('li.flex.items-center.text-Breadcrumb-Navigation')
            .each(($li, index) => {
                // ตรวจสอบว่า <a> มีอยู่ใน <li> หรือไม่
                const $a = $li.find('a');
                const $span = $li.find('span');

                // ถ้ามี <a> tag, ให้ตรวจสอบข้อความและ href
                if ($a.length > 0) {
                    cy.wrap($a)
                        .should('be.visible')  // ตรวจสอบว่า <a> แสดงอยู่
                        .and('have.text', expectedTexts[index]);  // ตรวจสอบข้อความที่ถูกต้อง
                } else if ($span.length > 0) {
                    // ถ้ามี <span> tag (เช่นในกรณีของ <li> ที่ 3)
                    cy.wrap($span)
                        .should('be.visible')  // ตรวจสอบว่า <span> แสดงอยู่
                        .and('have.text', expectedTexts[index]);  // ตรวจสอบข้อความที่ถูกต้อง
                }
            });

        //ตรวจสอบสถานะ ระหว่างข้อความ และ progress
        cy.get('h3.text-base.font-bold.text-black')
            .eq(0)
            .find('span')
            .then(($span) => {
                if ($span.text().trim() === 'รออนุมัติ') {
                    cy.get('div.flex.justify-end')
                        .find('div.border')
                        .find('div.row')
                        .find('svg path')
                        .first()
                        .should('have.attr', 'fill', '#12B669');
                }
            });

        //ตรวจสอบ font , font size , color 
        cy.get('div.col').each(($div) => {
            cy.wrap($div).then($el => {
                if ($el.find('p.head').length > 0) {
                    cy.wrap($div)
                        .find('p.head')
                        .and($head => {
                            expect($head).to.have.css('color', 'rgb(128, 128, 128)')
                            expect($head).to.have.css('font-family').to.contain('Sarabun')
                            expect($head).to.have.css('font-size', '14px')
                        })
                }
                if ($el.find('p.item').length > 0) {
                    cy.wrap($div)
                        .find('p.item')
                        .and($item => {
                            expect($item).to.have.css('color', 'rgb(0, 0, 0)')
                            expect($item).to.have.css('font-family').to.contain('Sarabun')
                            expect($item).to.have.css('font-size', '14px')
                        })
                }
            })
        })
    });
});
