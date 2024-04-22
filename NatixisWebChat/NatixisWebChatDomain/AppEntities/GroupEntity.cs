namespace NatixisWebChatDomain.AppEntities
{
    using System.ComponentModel.DataAnnotations;

    public class GroupEntity
    {
        [Key]
        public int GroupId { get; set; }

        [MaxLength(50)]
        public string? GroupName { get; set; }
    }
}
