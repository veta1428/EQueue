import {Moment} from "moment/moment"
export interface QueueModel
{
    id: number;
    subjectInstanceName: string;
    startTime: string; // ToDo date
    peopleIn: number;
    currentUserPosition: number;
    isActive: boolean;
}

export enum QueueSearchMode
{
    Active = 'active',
    Inactive = 'inactive',
}

export interface QueueModelList
{
    queues: QueueModel[];
}

export interface RecordModel
{
    recordId : number;
    studentFirstName : string;
    studentLasName : string;
    created: string; // ToDo: date
    position: number;
    isCurrentUser: boolean;
    canSendRequest: boolean;
}

export interface DetailedQueueModel
{
    queueId : number;
    subjectInstanceName : string;
    startTime : string; // ToDo: date
    isActive: boolean;
    records : RecordModel[];
}

export interface AddQueueModel
{
    subjectInstanceId: number;
    startTime: string;
    duration: number;
    description: string | null;
}