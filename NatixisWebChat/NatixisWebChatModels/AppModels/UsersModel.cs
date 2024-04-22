namespace NatixisWebChatModels.AppModels
{
    using NatixisWebChatModels.AppModelsConstants;
    using System.ComponentModel.DataAnnotations;

    public class UsersModel
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.UsernameRequiredErrorMessage)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = ErrorMessagesConstants.UsernameStringLengthErrorMessage)]
        public string? Username { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.EmailRequiredErrorMessage)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = ErrorMessagesConstants.EmailValidationErrorMessage)]
        public string? Email { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.PasswordRequiredErrorMessage)]
        [RegularExpression(@"^((?=^.{8,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = ErrorMessagesConstants.PasswordValidationErrorMessage)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.ConfirmPasswordRequiredErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = ErrorMessagesConstants.ConfirmPasswordCompareErrorMessage)]
        public string? ConfirmPassword { get; set; }
    }
}
