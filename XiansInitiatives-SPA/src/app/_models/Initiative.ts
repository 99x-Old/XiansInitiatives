import { InitiativeYear } from './InitiativeYear';
import { User } from './User';

export interface Initiative {
    id: string;
    name: string;
    description: string;
    department: string;
    createdAt: Date;
    createdBy: User;
    lead: User;
    coLead: User;
    mentor: User;
    initiativeYearId: string;
    initiativeYear: InitiativeYear;
    rating: number;
    numberOfMembers: number;
}
