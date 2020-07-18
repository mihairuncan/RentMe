export interface User {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    userName: string;
    phoneNumber: string;
    dateOfBirth: string;
    city: string;
    country: string;
    roles?: string[];
}
