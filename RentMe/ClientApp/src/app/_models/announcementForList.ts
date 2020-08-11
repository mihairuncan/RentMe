import { Photo } from './photo';

export interface AnnouncementForList {
    id: string;
    title: string;
    description: string;
    rentPrice: string;
    rentPeriod: string;
    addedOn: string;
    isApproved: boolean;
    subcategoryName: string;
    mainPhotoUrl?: string;
}
