export interface QueueModel
{
    id: number;
    subjectInstanceName: string;
    startTime: string; // ToDo date
    peopleIn: number;
    currentUserPosition: number;
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
}

export interface DetailedQueueModel
{
    queueId : number;
    subjectInstanceName : string;
    startTime : string; // ToDo: date
    records : RecordModel[];
}