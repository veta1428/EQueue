import { Teacher } from "./teacher";

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

export interface SubjectInstanceDetails
{
    id: number;
    timetable: TimetableModel | null; 
    instanceDescription: string | null;
    instanceName: string;
    subjectName: string;
    teachers: Teacher[];
}

export interface TimetableModel
{
    appliedPeriodStart: string;
    appliedPeriodEnd: string;
    classes: Class[];
}