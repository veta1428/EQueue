namespace Rey.EQueue.Application.Queries.QueryModels
{
    public class UserModel
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<UserRoleModel> Roles { get; set; } = new List<UserRoleModel>();
    }

    public class UserRoleModel
    {
        public int GroupId { get; set; }

        public string Role { get; set; } = null!;
    }
}
