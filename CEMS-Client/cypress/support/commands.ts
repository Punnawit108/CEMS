/// <reference types="cypress" />
// ***********************************************
// This example commands.ts shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add('login', (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add('drag', { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add('dismiss', { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite('visit', (originalFn, url, options) => { ... })
//
// declare global {
//   namespace Cypress {
//     interface Chainable {
//       login(email: string, password: string): Chainable<void>
//       drag(subject: string, options?: Partial<TypeOptions>): Chainable<Element>
//       dismiss(subject: string, options?: Partial<TypeOptions>): Chainable<Element>
//       visit(originalFn: CommandOriginalFn, url: string, options: Partial<VisitOptions>): Chainable<Element>
//     }
//   }
// }

import 'cypress-xpath';

Cypress.Commands.add('login', (id: string, password: string) => {
    cy.visit('http://localhost:5173/login');

    // จัดการกับข้อผิดพลาดที่ไม่ต้องการให้ Cypress หยุดทำงาน
    Cypress.on('uncaught:exception', (err) => {
        if (
            err.message.includes('lockSystem.ts') ||
            err.message.includes('AxiosError') ||
            err.message.includes("Cannot read properties of null (reading 'usrId')")
        ) {
            return false; // Cypress จะไม่หยุดทำงานเมื่อเจอ Error เหล่านี้
        }
        return true;
    });

    cy.get('.login button').should('be.visible');

    cy.xpath('//*[@id="app"]/div/div/div/main/div/div[2]/div/div[2]/form/input[1]').type(id);
    cy.xpath('//*[@id="app"]/div/div/div/main/div/div[2]/div/div[2]/form/input[2]').type(password);

    
    cy.xpath('//*[@id="app"]/div/div/div/main/div/div[2]/div/div[2]/form/button').click();
});
