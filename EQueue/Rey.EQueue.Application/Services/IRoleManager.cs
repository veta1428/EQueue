namespace Rey.EQueue.Application.Services
{
    public interface IRoleManager
    {
        bool IsUser();

        bool IsAdmin();
    }
}
