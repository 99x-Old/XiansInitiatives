import Chance from 'chance';
const chance = new Chance();

describe('Login', () => {
    const username = 'MalithW';
    const password = 'adM/461';

    beforeEach(() => {
        cy.visit('http://localhost:4200');
    })

    it('User should able to go to member edit option', () => {
        
        cy.get('input[name=username]').type(username);
        cy.get('input[name=password]').type(password);
        cy.get('button[type=submit]').click();

        cy.contains('Home');
        cy.contains('Member').click();
        cy.contains('Active').click();

        cy.contains('Edit -');
    });
    
});