import "cypress-file-upload";

describe("SC7_1_2_003", () => {
  beforeEach(() => {
    cy.login("65160093", "admin");
  });

  it("TC7_1_3_001", () => {
    const data = {
      day: "24",
      month: "ก.พ.",
      year: "2567",
      expenses_type: "ค่าอาหาร",
      name: "เบิกค่าอาหาร",
      expenses: "800",
      purpose: "ไปหาลูกค้า",
      project: "ไปต่างประเทศ",
      file: "image/Topaz.png",
    };

    cy.url().should("eq", "http://localhost:5173/dashboard");

    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/button/div[1]')
      .should("be.visible")
      .click();
    cy.xpath('//*[@id="app"]/div/div/div/nav/ul/li[2]/ul/li[1]/a/a/div[1]')
      .should("be.visible")
      .click();

    cy.url().should("eq", "http://localhost:5173/disbursement/listWithdraw");

    cy.get(".btn-สร้างใบเบิกค่าใช้จ่าย").should("be.visible").click();
    cy.wait(2000);

    cy.get("#rqName").should("be.visible").type(data.name);
    cy.get("#rqPayDate > .custom-select").should("be.visible").click();
    cy.xpath('//*[@id="rqPayDate"]/div[2]/div[1]/div/select[1]')
      .should("be.visible")
      .select(data.month);
    cy.xpath('//*[@id="rqPayDate"]/div[2]/div[1]/div/select[2]')
      .should("be.visible")
      .select(data.year);
    cy.xpath('//*[@id="rqPayDate"]/div[2]')
      .should("be.visible")
      .contains(data.day)
      .click();
    cy.xpath('//*[@id="rqPayDate"]/div[2]/div[3]/button[2]')
      .should("be.visible")
      .click();
    cy.get("#projectName").should("be.visible").select(data.project);
    cy.get("#rqExpenses").should("be.visible").type(data.expenses);
    cy.get("#expenseType").should("be.visible").select(data.expenses_type);
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[2]/div[2]/textarea')
      .should("be.visible")
      .type(data.purpose);
    cy.get('input[type="file"]').attachFile(data.file);

    cy.intercept("POST", "/api/expense").as("submitRequest");

    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[1]/button[3]').click();
    cy.xpath('//*[@id="app"]/div/div/div/div/div[2]/form/div[3]/div/div[2]/button[2]').click();

    cy.wait("@submitRequest").its("response.statusCode").should("eq", 201);

    cy.url().should("eq", "http://localhost:5173/disbursement/listWithdraw");
  });
});
