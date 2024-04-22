namespace NatixisWebChatDomain.AppEntities
{
    using System.ComponentModel.DataAnnotations;

    public class GroupMemberEntity
    {
        [Key]
        public int Id { get; set; }

        public GroupEntity? Group { get; set; }

        public UserEntity? User { get; set; }
    }
}