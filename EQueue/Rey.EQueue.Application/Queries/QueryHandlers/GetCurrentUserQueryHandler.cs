using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserModel?>
    {
        private readonly IUserAccessor _userAccessor;

        public GetCurrentUserQueryHandler(
            IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public Task<UserModel?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = _userAccessor.CurrentUser;

            if(user is null)
                return Task.FromResult<UserModel?>(null);

            return Task.FromResult((UserModel?)(new UserModel() { Id = user!.Id, FirstName = user!.FirstName, LastName = user!.LastName, Roles = user!.Roles.Select(ur => new UserRoleModel() { GroupId = ur.GroupId, Role = ur.Role.Name }).ToList() }));
        }
    }
}
