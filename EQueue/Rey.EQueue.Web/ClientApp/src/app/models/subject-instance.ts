export interface SubjectInstance
{
    id: number;
    classes: Class[]; 
    instanceDescription: String | null;
    instanceName: String[];
}

export interface SubjectInstanceList
{
    subjectInstances: SubjectInstance[];
}

export interface Class
{
    dayOfWeek: string;
    startTime: string;
    duration: number;
}

export interface AddSubjectInstanceModel
{
    name: string;
    description: string | null;
    subjectId: number;
    teacherIds: number[];
}