import { Photo } from './photo';

export interface Announcement {
    id: string;
    title: string;
    description: string;
    rentPrice: string;
    rentPeriod: string;
    addedOn: string;
    postedById: string;
    postedByName: string;
    subcategoryName: string;
    photos?: Photo[];
}
