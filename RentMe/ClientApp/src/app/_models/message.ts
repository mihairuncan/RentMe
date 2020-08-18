export interface Message {
    id: string;
    senderId: string;
    senderUserName: string;
    recipientId: string;
    recipientUserName: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    messageSent: Date;
}
