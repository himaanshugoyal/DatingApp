import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    knownAs: string;
    age: number;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    city: string;
    country: string; // Properties we are going to get from users dto
    interestids?: string;
    introduction?: string;
    lookingFor?: string;
    photos?: Photo[];
}
