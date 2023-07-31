export interface UserModel 
{
    id: number;
    firstName: string;
    lastName: string;
    roles: UserRoleModel[];
}

export interface UserRoleModel
{
    groupId: number;
    role: string;
}