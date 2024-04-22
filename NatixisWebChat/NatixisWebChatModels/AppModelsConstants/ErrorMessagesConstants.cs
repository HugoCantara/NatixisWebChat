namespace NatixisWebChatModels.AppModelsConstants
{
    public static class ErrorMessagesConstants
    {
        public const string UsernameRequiredErrorMessage = "Username is required";
        public const string UsernameStringLengthErrorMessage = "Username must be 3-20 characters long";
        public const string EmailRequiredErrorMessage = "Email is required";
        public const string EmailValidationErrorMessage = "Please enter valid email adress";
        public const string PasswordRequiredErrorMessage = "Password is required";
        public const string PasswordValidationErrorMessage = "The password must be at least 8 characters long contain at least one uppercase and one lowercase character, number and special character!";
        public const string ConfirmPasswordRequiredErrorMessage = "Please confirm your password";
        public const string ConfirmPasswordCompareErrorMessage = "The password and confirmation password do not match.";
    }
}
