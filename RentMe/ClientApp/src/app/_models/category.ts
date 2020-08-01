import { Subcategory } from './subcategory';

export interface Category {
    displayName: string;
    name: string;
    subcategories: Subcategory[];
}
