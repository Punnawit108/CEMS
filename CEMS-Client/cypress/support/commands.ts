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
Cypress.Commands.add('login', () => {
    cy.visit('http://localhost:5173/login');
    Cypress.on('uncaught:exception', (err) => {
        if (err.message.includes('lockSystem.ts') || err.message.includes('AxiosError') || err.message.includes("Cannot read properties of null (reading 'usrId')")) {
            return false;
        }
        return true;
    });
    cy.get('.login button').should('be.visible');
    //cy.wait(1500);
    cy.get('.login button').click();
    cy.get('.modal-action').should('be.visible');
    //cy.wait(1500);
    cy.get('select.select.select-bordered').select(12);
    //cy.wait(1500);
    cy.get('button.btn').click();
});

Cypress.Commands.add('ExpenseManage', () => {
    cy.visit('http://localhost:5173/systemSettings/disbursementType/expense');
    Cypress.on('uncaught:exception', (err) => {
        if (err.message.includes('lockSystem.ts') || err.message.includes('AxiosError') || err.message.includes("Cannot read properties of null (reading 'usrId')")) {
            return false;
        }
        return true;
    });
    cy.get('.login button').should('be.visible');
    //cy.wait(1500);
    cy.get('.login button').click();
    cy.get('.modal-action').should('be.visible');
    //cy.wait(1500);
    cy.get('select.select.select-bordered').select(12);
    //cy.wait(1500);
    cy.get('button.btn').click();
});