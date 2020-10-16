import Chance from 'chance';
const chance = new Chance();

describe('Login', () => {
    const username = 'MalithW';
    const password = 'adM/461';

    beforeEach(() => {
        cy.visit('http://localhost:4200');
    })

    it('User should redirected to landing page after logged in', () => {
        
        cy.get('input[name=username]').type(username);
        cy.get('input[name=password]').type(password);
        cy.get('button[type=submit]').click();

        cy.contains('Home');
    });
    it('Warn user if not registered', () => {
        
        cy.get('input[name=username]').type(chance.email());
        cy.get('input[name=password]').type(password);
        cy.get('button[type=submit]').click();

        cy.contains('User not exists');
    })
});