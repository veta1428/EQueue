export interface Teacher
{
    id: number;
    firstName: String;
    middleName: String;
    lastName: String;
    description: String;
    note: String;
}

export interface TeacherList
{
    teachers: Teacher[];
}

export interface AddTeacherModel
{
    firstName: string;
    lastName: string;
    middleName: string | null;
    description: string | null;
    note: string | null;
}