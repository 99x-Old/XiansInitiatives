export interface Meeting {
    id: string;
    createdAt: Date;
    createdBy: string;
    purpose: string;
    meetingNotes: string;
    initiativeId: string;
    organizerId: string;
    organizerName: string;
    channelId: string;
}
