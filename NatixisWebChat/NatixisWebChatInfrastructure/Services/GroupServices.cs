namespace NatixisWebChatInfrastructure.Services
{
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;

    public class GroupServices
    {
        /// <summary>The generic repository</summary>
        private readonly IBaseRepository<GroupEntity> _groupEntityRepository;

        /// <summary>The constructor</summary>
        /// <param name="groupEntityRepository">The generic repository</param>
        public GroupServices(IBaseRepository<GroupEntity> groupEntityRepository)
        {
            _groupEntityRepository = groupEntityRepository;
        }

        /// <summary>Get group by name</summary>
        /// <param name="groupName">The group name.</param>
        /// <returns>GroupEntity?</returns>
        public GroupEntity? GetGroupByName(string groupName)
        {
            var groupCollection = _groupEntityRepository.GetAllAsync().Result.ToList();
            return groupCollection.FirstOrDefault(g => g.GroupName == groupName);
        }

        /// <summary>Add new group to database</summary>
        /// <param name="groupName">The group name.</param>
        public void AddGroup(string groupName)
        {
            _groupEntityRepository.AddAsync(new GroupEntity() { GroupName = groupName });
        }
    }
}
