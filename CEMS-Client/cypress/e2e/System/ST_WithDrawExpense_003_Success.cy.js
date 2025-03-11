describe("Dashboard Tests", () => {
  beforeEach(() => {
    //1
    cy.login("65160341", "admin");
  });

  it("ตรวจสอบการทำงานของของระบบ เมื่อสร้างใบเบิกเป็นค่าใช้จ่ายทั่วไป", () => {
    //2
    cy.get(":nth-child(2) > .relative > .flex").click();
    cy.contains("รายการเบิกค่าใช้จ่าย").click();
    cy.url().should("include", "/disbursement/listWithdraw");
    cy.screenshot();

    //3
    cy.contains("button", "สร้างใบเบิกค่าใช้จ่าย").click();
    cy.url().should("include", "/createExpenseForm");
    cy.screenshot();

    //4
    cy.get("#rqName").type("เบิกค่าใช้จ่าย");
    cy.get("#rqPayDate > .custom-select")
      .click()
      .get("svg.lucide-chevron-left-icon")
      .click()
      .get(":nth-child(31)")
      .click()
      .get(".justify-end > .bg-blue-500")
      .click();
    cy.get("#projectName").select(1);
    cy.get("#expenseType").select(1);
    cy.get("#rqExpenses").type(280);
    cy.get("#rqInsteadEmail").select("65160242@go.buu.ac.th");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/form/div[2]/div[2]/textarea'
    ).type("เบิกค่าอาหาร");
    cy.screenshot();

    //5
    cy.get("button.btn-ยืนยัน") // เลือกปุ่มด้วยคลาส
      .click(); // คลิกปุ่ม
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/form/div[3]/div/div[2]/button[2]'
    ).click(); // คลิกปุ่ม
    cy.screenshot();

    //6
    //ประกาศ array ของข้อมูลที่ต้องการตรวจสอบ
    const expectedListData = [
      "เบิกค่าใช้จ่าย",
      "งานเลี้ยง",
      "ค่าอาหาร",
      "2568-03-01",
      "280.00",
      "รออนุมัติ",
    ];

    //เบิกค่าใช้จ่าย

    for (let i = 2; i <= 7; i++) {
      let xpath = `//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr/th[${i}]`;
      if (i === 7) {
        xpath += "/span/div/div/span";
      }
      cy.xpath(xpath)
        .invoke("text")
        .then((text) => {
          const trimmedText = text.trim();
          expect(trimmedText).to.eq(expectedListData[i - 2]);
          cy.log(`ตำแหน่งที่ ${i}:`, trimmedText);
        });
    }

    cy.screenshot();

    // ฟังก์ชันสำหรับ login และเรียกใช้กระบวนการตรวจสอบ
    function loginAndCheckProcess(username, password) {
      cy.get("#btn-logout").click();
      cy.login(username, password);
      cy.wait(1000);

      // เรียกใช้ฟังก์ชันตรวจสอบต่าง ๆ
      checkProcess7("รอดำเนินการอนุมัติ", 6);
      checkProcess8();
      checkProcess9();
      checkProcess10();
      checkProcess11();
    }

    // ฟังก์ชันตรวจสอบข้อ 7
    function checkProcess7(keyword, index) {
      // คลิกปุ่มนำทางโดยใช้ค่า index
      cy.xpath(
        `//*[@id="app"]/div/div/div/nav/ul/li[${index}]/a/button/div[1]`
      ).click();

      // ดึง section ทั้งหมดขึ้นมาแล้วใช้ .then() เพื่อวนลูปแบบ synchronous
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div/article/section')
        .should("have.length.gt", 0)
        .then(($sections) => {
          let foundSection = null;

          // ใช้ jQuery loop ผ่าน sections แบบ synchronous
          Cypress._.forEach($sections, (section) => {
            const $p = Cypress.$(section).find("p");
            const text = $p.text().trim();
            if (text === `รหัส : 26a7d99c ${keyword}`) {
              foundSection = section;
              // break out จาก loop ด้วย return false ใน forEach ของ Lodash
              return false;
            }
          });

          // ตรวจสอบว่าพบ element ที่ต้องการ
          expect(foundSection, "พบ section ที่มีข้อความที่ต้องการ").to.not.be
            .null;

          // ถ้า keyword ตรงกับเงื่อนไข ให้คลิกที่ <p> ภายใน section นั้น
          if (keyword === "ได้รับการนำจ่ายเรียบร้อยแล้ว") {
            cy.wrap(foundSection)
              .find("p")
              .should("be.visible")
              .click({ force: true });
          }
        });

      cy.screenshot();
    }

    // ฟังก์ชันตรวจสอบข้อ 8
    function checkProcess8() {
      cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]').click();
      cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]').click();
      cy.wait(1000);

      const expectedData = [
        "Pongsatorn Boonyama",
        "เบิกค่าใช้จ่าย",
        "งานเลี้ยง",
        "ค่าอาหาร",
        "2568-03-01",
        "280.00",
      ];

      for (let i = 2; i <= 7; i++) {
        const xpath = `//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[1]/th[${i}]`;
        cy.xpath(xpath)
          .invoke("text")
          .then((text) => {
            const trimmedText = text.trim();
            expect(trimmedText).to.include(expectedData[i - 2]);
            cy.log(`ตำแหน่งที่ ${i}:`, trimmedText);
          });
      }
      cy.screenshot();
    }

    // ฟังก์ชันตรวจสอบข้อ 9
    function checkProcess9() {
      cy.xpath(
        '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]'
      ).click();
      cy.xpath(
        '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[1]/th[8]/div/div/div'
      ).click({ force: true });

      const expectedData = [
        "CN-000040",
        "งานเลี้ยง",
        "2568-02-18",
        "2568-03-01",
        "Mr. Pongsatorn Boonyama",
        "-",
        "ค่าอาหาร",
        "280.00",
        "-",
        "-",
        "-",
        "-",
        "-",
        "-",
        "เบิกค่าอาหาร",
      ];

      cy.get("p.item").each(($p, index) => {
        cy.wrap($p)
          .invoke("text")
          .then((text) => {
            const trimmedText = text.trim();
            expect(trimmedText).to.eq(expectedData[index]);
          });
      });
      cy.screenshot();
    }

    // ฟังก์ชันตรวจสอบข้อ 10
    function checkProcess10() {
      cy.xpath(
        '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]'
      ).click();
      cy.xpath(
        '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[1]/th[8]/div/div/div'
      ).click({ force: true });
      cy.xpath(
        '//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div/button[3]'
      ).click();
      cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div/h2')
        .should("be.visible")
        .and("have.text", "ยืนยันการอนุมัติรายการเบิกค่าใช้จ่าย");
      cy.xpath(
        '//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div/div[2]/button[2]'
      ).click();
      cy.url().should("include", "/approval/list");
      cy.screenshot();
    }

    // ฟังก์ชันตรวจสอบข้อ 11
    function checkProcess11() {
      cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[2]').click();

      const expectedHistoryApproveData = [
        "Pongsatorn Boonyama",
        "เบิกค่าใช้จ่าย",
        "งานเลี้ยง",
        "ค่าอาหาร",
        "280.00",
        "อนุมัติ",
      ];

      for (let i = 2; i <= 7; i++) {
        const xpath = `//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[1]/th[${i}]`;
        cy.xpath(xpath)
          .invoke("text")
          .then((text) => {
            const trimmedText = text.trim();
            expect(trimmedText).to.include(expectedHistoryApproveData[i - 2]);
          });
      }
      cy.screenshot();
    }

    //ตรวจสอบหน้า list นำจ่าย
    function checkProcess12(index) {
      // คลิกที่เมนูหลัก
      cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[5]/button/div[1]').click();

      // คลิกที่เมนูย่อย โดยใช้ Template Literals เพื่อแทรกค่า index
      cy.xpath(
        `//*[@id="app"]/div/div/div/nav/ul/li[5]/ul/li[${index}]/a/a/div[1]`
      ).click();
      cy.wait(1000); // รอให้หน้าโหลดเสร็จ

      const expectedData = [
        "Pongsatorn Boonyama",
        "เบิกค่าใช้จ่าย",
        "งานเลี้ยง",
        "ค่าอาหาร",
        "2568-03-01",
        "280.00",
      ];

      // วนลูปตรวจสอบแต่ละ <th>
      for (let i = 2; i <= expectedData.length + 1; i++) {
        let xpath;

        // เลือก XPath ตามค่า index
        if (index === 1) {
          xpath = `//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[2]/table/tbody/tr/th[${i}]`;
        } else if (index === 2) {
          xpath = `//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table/tbody/tr[3]/th[${i}]`;
        }

        // ตรวจสอบข้อความใน <th> ปัจจุบัน
        cy.xpath(xpath)
          .invoke("text")
          .then((text) => {
            const trimmedText = text.trim();
            expect(trimmedText).to.include(expectedData[i - 2]); // ตรวจสอบว่าข้อความประกอบด้วยข้อมูลใน array
            cy.log(`ตำแหน่งที่ ${i}:`, trimmedText); // แสดงข้อความใน Cypress log
          });
      }
      cy.screenshot(); // ถ่ายภาพหน้าจอ
    }

    function checkProcess13(status) {
      cy.wait(100);
      if (status !== "validateOnly") {
        cy.xpath(
          '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/div[2]/table/tbody/tr[1]/th[7]/span/div/div/div'
        )
          .should("be.visible")
          .click({ force: true });
      }

      const expectedData = [
        "CN-000040",
        "งานเลี้ยง",
        "2568-02-18",
        "2568-03-01",
        "Mr. Pongsatorn Boonyama",
        "-",
        "ค่าอาหาร",
        "280.00",
        "-",
        "-",
        "-",
        "-",
        "-",
        "-",
        "เบิกค่าอาหาร",
      ];

      // ตรวจสอบข้อความในแต่ละ <p> ที่มี class "item"
      cy.get("p.item").each(($p, index) => {
        cy.wrap($p)
          .invoke("text")
          .then((text) => {
            const trimmedText = text.trim();
            expect(trimmedText).to.eq(expectedData[index]); // ตรวจสอบว่าข้อความตรงกับข้อมูลใน array
            cy.log(`ตำแหน่งที่ ${index}:`, trimmedText); // แสดงข้อความใน Cypress log
          });
      });

      cy.screenshot(); // ถ่ายภาพหน้าจอหลังจากตรวจสอบเสร็จ
    }

    loginAndCheckProcess("65160093", "admin"); //7-11
    loginAndCheckProcess("65160095", "admin"); //12-13
    loginAndCheckProcess("65160321", "admin"); //14-15

    //16
    cy.get("#btn-logout").click();
    cy.login("65160341", "admin");
    cy.wait(1000);
    checkProcess7("รอดำเนินการอนุมัติ", 5);

    //16
    cy.get("#btn-logout").click();
    cy.login("65160095", "admin");
    cy.wait(1000);
    checkProcess7("กรุณาดำเนินการตรวจสอบรายละเอียดเพื่อนำจ่ายคำขอเบิก", 6);

    //18
    checkProcess12(1);

    // 19
    checkProcess13("all");

    //20
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div/button'
    ).click(); // คลิกที่ปุ่ม
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/h2[1]')
      .should("be.visible") // รอจนกว่า <h2> จะปรากฏ
      .invoke("text") // ดึงข้อความจาก <h2>
      .then((text) => {
        const trimmedText = text.trim(); // ตัดช่องว่างข้างเคียง
        expect(trimmedText).to.eq("ยืนยันการอัปเดตสถานะคำขอเบิก"); // ตรวจสอบว่าข้อความตรงกับที่กำหนด
      });
    cy.screenshot();
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div[2]/div/div[2]/button[2]'
    )
      .should("be.visible") // รอจนกว่าปุ่มจะปรากฏ
      .click(); // คลิกที่ปุ่ม

    //21
    checkProcess12(2);

    //22 - 23
    cy.get("#btn-logout").click();
    cy.login("65160341", "admin");
    cy.wait(1000);
    checkProcess7("ได้รับการนำจ่ายเรียบร้อยแล้ว", 5);
    checkProcess13("validateOnly"); //23
  });
});
