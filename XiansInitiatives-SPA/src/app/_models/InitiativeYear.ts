import { User } from './User';

export interface InitiativeYear {
    id: string;
    year: number;
    description: string;
    createdAt: Date;
    createdBy: User;
}
