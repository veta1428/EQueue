using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IEnumerable<GroupModel>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserAccessor _userAccessor;

        public GetGroupsQueryHandler(
            IGroupRepository groupRepository, 
            IUserAccessor userAccessor)
        {
            _groupRepository = groupRepository;
            _userAccessor = userAccessor;
        }

        public async Task<IEnumerable<GroupModel>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var user = _userAccessor.CurrentUser ?? throw new Exception("No user in context");

            var groupIds = user.Roles.Select(ur => ur.GroupId).Distinct();
            var groups = await _groupRepository.GetByIdsAsync(groupIds, cancellationToken);
            return groups.Select(g => new GroupModel() { Id = g.Id, Name = g.Name });
        }
    }
}
