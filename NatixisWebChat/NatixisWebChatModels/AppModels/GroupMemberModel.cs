namespace NatixisWebChatModels.AppModels
{
    using System.ComponentModel.DataAnnotations;

    public class GroupMemberModel
    {
        [Key]
        public int Id { get; set; }

        public int GroupId { get; set; }

        public string? UserName { get; set; }
    }
}