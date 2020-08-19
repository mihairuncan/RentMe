import { Category } from './category';

export const CATEGORIES: Category[] = [
    {
        displayName: 'Clothes and accessories',
        name: 'clothes-and-accessories',
        subcategories: [
            {
                name: 'clothes-for-women',
                displayName: 'Clothes and accessories for women',
                imageUrl: 'https://i.ibb.co/kJzGR99/clothes-women.jpg'
            },
            {
                name: 'clothes-for-men',
                displayName: 'Clothes and accessories for men',
                imageUrl: 'https://i.ibb.co/x1d93B3/clothes-men.jpg'
            },
            {
                name: 'clothes-for-kids',
                displayName: 'Clothes and accessories for kids',
                imageUrl: 'https://i.ibb.co/zrmrWfM/clothes-kids.jpg'
            },
        ]
    },
    {
        displayName: 'Games',
        name: 'games',
        subcategories: [
            {
                name: 'desktop',
                displayName: 'Desktop',
                imageUrl: 'https://i.ibb.co/Q8MzrK8/games-desktop.png'
            },
            {
                name: 'playStation',
                displayName: 'PlayStation',
                imageUrl: 'https://i.ibb.co/LZrF1BY/games-playstation.jpg'
            },
            {
                name: 'xbox',
                displayName: 'Xbox',
                imageUrl: 'https://i.ibb.co/D5yY72G/games-xbox.jpg'
            },
        ]
    },
    {
        displayName: 'Real Estate',
        name: 'real-estate',
        subcategories: [
            {
                name: 'apartments',
                displayName: 'Apartments',
                imageUrl: 'https://i.ibb.co/B2Gj3dX/apartments.jpg'
            },
            {
                name: 'houses',
                displayName: 'Houses',
                imageUrl: 'https://i.ibb.co/QCFwpPg/houses.jpg'
            },
            {
                name: 'commercial',
                displayName: 'Commercial',
                imageUrl: 'https://i.ibb.co/nQBQ9b0/commercial.jpg'
            }
        ]
    },
    {
        displayName: 'Vehicles',
        name: 'vehicles',
        subcategories: [
            {
                name: 'cars',
                displayName: 'Cars',
                imageUrl: 'https://i.ibb.co/0JN63v7/cars.jpg'
            },
            {
                name: 'caravans',
                displayName: 'Caravans',
                imageUrl: 'https://i.ibb.co/nfh1Lxv/caravans.jpg'
            },
            {
                name: 'motorcycles',
                displayName: 'Motorcycles',
                imageUrl: 'https://i.ibb.co/RpSp30r/motorcycles.jpg'
            },
            {
                name: 'bicycles',
                displayName: 'Bicycles',
                imageUrl: 'https://i.ibb.co/VLxpSwD/bicycles.jpg'
            },
            {
                name: 'electric-scooters',
                displayName: 'Electric Scooters',
                imageUrl: 'https://i.ibb.co/SsWXHrh/electric-scooter.jpg'
            },
        ]
    },
    {
        displayName: 'Others',
        name: 'others',
        subcategories: [
            {
                name: 'books',
                displayName: 'Books',
                imageUrl: 'https://i.ibb.co/rHjM3vM/books.jpg'
            },
            {
                name: 'board-games',
                displayName: 'Board Games',
                imageUrl: 'https://i.ibb.co/fFy3wFW/board-games.jpg'
            },
        ]
    }
];
