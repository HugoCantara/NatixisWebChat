namespace NatixisWebChatInfrastructure.Repositories.Interfaces
{
    using NatixisWebChatDomain.AppEntities;

    /// <summary>Interface IGroupsRepository</summary>
    public interface IGroupsRepository : IBaseRepository
    {
        /// <summary>Get all groups</summary>
        /// <returns>List<GroupEntity></returns>
        public List<GroupEntity> GetGroupsCollection();

        /// <summary>Get group by name</summary>
        /// <param name="groupName">The group name.</param>
        /// <returns>GroupEntity?</returns>
        public GroupEntity? GetGroupByName(string groupName);

        /// <summary>Add new group to database</summary>
        /// <param name="groupName">The group name.</param>
        public void AddGroup(string groupName);
    }
}
