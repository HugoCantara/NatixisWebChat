namespace NatixisWebChatDomain.AppEntities
{
    using System.ComponentModel.DataAnnotations;

    public class MessageEntity
    {
        [Key]
        public int MessageId { get; set; }

        public UserEntity? MessageFrom { get; set; }

        public UserEntity? MessageToUser { get; set; }

        public GroupEntity? MessageToGroup { get; set; }

        public string? MessageContent { get; set; }

        public DateTime MessageTime { get; set; }
    }
}
