export interface Subject
{
    id: number;
    name: String; 
    description: String;
}

export interface SubjectList
{
    subjects: Subject[];
}

export interface AddSubjectModel
{
    name: string;
    description: string;
}