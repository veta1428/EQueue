export interface ChangeRequest
{
    id: number;
    queueId: number;
    queueStartTime: string;
    subjectInstanceName: string;
    peopleIn: number;
    currentUserPosition: number;
    anotherUserPosition: number;
    userFirstName: string;
    userLastName: string;
    status: string;
}

export enum SearchChangeRequestMode
{
    Incoming = 'incoming',
    Outcoming = 'outcoming'
}

export interface ChangeRequestList
{
    changeRequests: ChangeRequest[];
}