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