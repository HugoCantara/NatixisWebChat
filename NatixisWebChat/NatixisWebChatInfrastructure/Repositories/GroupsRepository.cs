namespace NatixisWebChatInfrastructure.Repositories
{
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Context;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using System.Collections.Generic;

    /// <summary>Class GroupsRepository</summary>
    public class GroupsRepository : BaseRepository, IGroupsRepository
    {
        /// <summary>The context</summary>
        private readonly NatixisDbContext _context;

        /// <summary>The constructor</summary>
        /// <param name="context">The context.</param>
        public GroupsRepository(NatixisDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>Save group in database</summary>
        /// <param name="group">The group.</param>
        private void SaveGroup(GroupEntity group)
        {
            _context.Groups.Add(group);
            SaveChanges();
        }

        /// <summary>Get all groups</summary>
        /// <returns>List<GroupEntity></returns>
        public List<GroupEntity> GetGroupsCollection()
        {
            return _context.Groups.ToList();
        }

        /// <summary>Get group by name</summary>
        /// <param name="groupName">The group name.</param>
        /// <returns>GroupEntity?</returns>
        public GroupEntity? GetGroupByName(string groupName)
        {
            return this.GetGroupsCollection().FirstOrDefault(g => g.GroupName == groupName);
        }

        /// <summary>Add new group to database</summary>
        /// <param name="groupName">The group name.</param>
        public void AddGroup(string groupName)
        {
            this.SaveGroup(new GroupEntity() { GroupName = groupName });
        }   
    }
}
