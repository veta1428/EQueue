export interface SubjectInstance
{
    id: number;
    timetable: String | null; 
    instanceDescription: String | null;
    instanceName: String[];
}

export interface SubjectInstanceList
{
    subjectInstances: SubjectInstance[];
}