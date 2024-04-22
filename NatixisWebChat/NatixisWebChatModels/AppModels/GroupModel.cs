namespace NatixisWebChatModels.AppModels
{
    using System.ComponentModel.DataAnnotations;

    public class GroupModel
    {
        [Key]
        public int GroupId { get; set; }

        [MaxLength(50)]
        public string? GroupName { get; set; }
    }
}