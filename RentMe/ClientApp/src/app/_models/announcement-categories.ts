import { Category } from './category';

export const CATEGORIES: Category[] = [
    {
        displayName: 'Clothes and accessories',
        name: 'clothes-and-accessories',
        subcategories: [
            {
                name: 'clothes-for-women',
                displayName: 'Clothes and accessories for women'
            },
            {
                name: 'clothes-for-men',
                displayName: 'Clothes and accessories for men'
            },
            {
                name: 'clothes-for-kids',
                displayName: 'Clothes and accessories for kids'
            },
        ]
    },
    {
        displayName: 'Games',
        name: 'games',
        subcategories: [
            {
                name: 'desktop',
                displayName: 'Desktop'
            },
            {
                name: 'playStation',
                displayName: 'PlayStation'
            },
            {
                name: 'xbox',
                displayName: 'Xbox'
            },
        ]
    },
    {
        displayName: 'Real Estate',
        name: 'real-estate',
        subcategories: [
            {
                name: 'apartments',
                displayName: 'Apartments'
            },
            {
                name: 'houses',
                displayName: 'Houses'
            },
            {
                name: 'commercial',
                displayName: 'Commercial'
            }
        ]
    },
    {
        displayName: 'Vehicles',
        name: 'vehicles',
        subcategories: [
            {
                name: 'cars',
                displayName: 'Cars'
            },
            {
                name: 'caravans',
                displayName: 'Caravans'
            },
            {
                name: 'motorcycles',
                displayName: 'Motorcycles'
            },
            {
                name: 'bicycles',
                displayName: 'Bicycles'
            },
            {
                name: 'electric-scooters',
                displayName: 'Electric Scooters'
            },
        ]
    },
    {
        displayName: 'Others',
        name: 'others',
        subcategories: [
            {
                name: 'books',
                displayName: 'Books'
            },
            {
                name: 'board-games',
                displayName: 'Board Games'
            },
        ]
    }
];
