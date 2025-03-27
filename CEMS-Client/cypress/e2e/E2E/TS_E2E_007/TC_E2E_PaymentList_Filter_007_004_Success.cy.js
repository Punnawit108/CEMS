/*
 * ชื่อไฟล์: TC_E2E_PaymentList_Filter_007_004_Success.cy.js
 * คำอธิบาย: E2E
 * ชื่อผู้เขียน/แก้ไข: นางสาวอังคณา อุ่นเสียม
 * วันที่จัดทำ/แก้ไข: 13/03/2568
 */
describe("PaymentList Tests", () => {
  beforeEach(() => {
    cy.login("65160095", "admin");
  });

  it("ตรวจสอบหน้า PaymentList", () => {
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[4]/button/div[1]').click();
    cy.wait(1000);
    cy.xpath(
      '//*[@id="app"]/div/div/div/nav/ul/li[4]/ul/li[1]/a/a/div[1]'
    ).click();
    cy.wait(1000);

    cy.xpath('//*[@id="SearchBar"]').type("Thianchai Khumueang");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[5]/div[2]/div[2]/button[2]'
    ).click();
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[2]/table[11]/tbody/tr[1]/th[2]'
    ).should("contain.text", "Thianchai Khumueang");
    cy.get(".bg-transparent").click();

    cy.get("#SelectProject").select("งานเลี้ยง");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[5]/div[2]/div[2]/button[2]'
    ).click();
    cy.get("tbody > :nth-child(1) > :nth-child(4)").should(
      "contain.text",
      "งานเลี้ยง"
    );
    cy.get(".bg-transparent").click();

    cy.get("#SelectRequisitionType").select("ค่าอาหาร");
    cy.xpath(
      '//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[5]/div[2]/div[2]/button[2]'
    ).click();
    cy.get("tbody > :nth-child(1) > :nth-child(4)").should(
      "contain.text",
      "งานเลี้ยง"
    );
    cy.get(".bg-transparent").click();

    // cy.get(
    //   ":nth-child(4) > .grid > .date-picker-container > .relative > .custom-select"
    // ).type("01/01/2567");
    // cy.get(
    //   ".flex-col > .min-w-[200px] > .grid > .date-picker-container > .relative > .custom-select"
    // ).type("01/03/2568");
    // cy.xpath(
    //   '//*[@id="app"]/div/div/div/div/div[2]/div/div[1]/div[5]/div[2]/div[2]/button[2]'
    // ).click();
  });
});
