import { Photo } from './photo';

export interface Announcement {
    id: string;
    title: string;
    description: string;
    rentPrice: string;
    rentPeriod: string;
    addedOn: string;
    postedBy: string;
    subcategoryName: string;
    photos?: Photo[];
}
