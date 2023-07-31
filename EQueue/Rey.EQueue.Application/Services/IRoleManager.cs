namespace Rey.EQueue.Application.Services
{
    public interface IRoleManager
    {
        bool IsUserInGroup();

        bool IsAdminInGroup();
    }
}
