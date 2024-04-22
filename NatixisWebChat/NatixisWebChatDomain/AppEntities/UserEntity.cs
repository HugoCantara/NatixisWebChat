namespace NatixisWebChatDomain.AppEntities
{
    using System.ComponentModel.DataAnnotations;

    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Email { get; set; }
    }
}
