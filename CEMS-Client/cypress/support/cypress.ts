/// <reference types="cypress" />

declare namespace Cypress {
    interface Chainable {
      /**
       * Custom command to perform login
       * @example cy.login()
       */
      login(id: string, password: string): Chainable<void>;
    }
  }