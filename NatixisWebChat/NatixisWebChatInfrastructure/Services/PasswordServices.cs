namespace NatixisWebChatInfrastructure.Services
{
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using System.Text.RegularExpressions;

    /// <summary>Class PasswordServices</summary>
    public class PasswordServices
    {
        /// <summary>The users repository</summary>
        private readonly IUsersRepository _usersRepository;

        /// <summary>The constructor</summary>
        /// <param name="usersRepository">The users repository.</param>
        public PasswordServices(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        /// <summary>Update user password</summary>
        /// <param name="user">The user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>UserEntity</returns>
        public UserEntity UpdateUserPassword(UserEntity user, string newPassword)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            return _usersRepository.UpdateUserPassword(user);
        }

        /// <summary>Update user password by email</summary>
        /// <param name="user">The user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? UpdateUserPasswordByEmail(UserEntity user, string newPassword)
        {
            var databaseUser = _usersRepository.GetUserCollection().FirstOrDefault(u => u.Username == user.Username && u.Email == user.Email);

            if (databaseUser == null)
            {
                return null;
            }

            return this.UpdateUserPassword(databaseUser, newPassword);
        }

        /// <summary>Method that generates random secure password</summary>
        /// <returns>string</returns>
        public string GenerateNewPassword()
        {
            var randomString = "";
            Random random = new Random();

            var upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 2; i++)
            {
                int a = random.Next(26);
                randomString += upperChars.ElementAt(a);
            }

            var lowerChars = "abcdefghijklmnopqrstuvwxy";
            for (int i = 0; i < 3; i++)
            {
                int a = random.Next(26);
                randomString += lowerChars.ElementAt(a);
            }

            var numbers = "0123456789";
            for (int i = 0; i < 3; i++)
            {
                int a = random.Next(10);
                randomString += numbers.ElementAt(a);
            }

            var specialChars = "!@#$%^&*()";
            int b = random.Next(10);
            randomString += specialChars.ElementAt(b);

            //to shuffle string charss and generate random password
            return new string(randomString.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

        /// <summary>
        /// Check if inputted password is secure: one uppercase, lowercase letter, 
        /// special character, digit, not less than 8 characters.
        /// </summary>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public bool IsSecurePassword(string password)
        {
            //regex check that password should have at least one lowercase, 
            //uppercase letter, digit and special character and it is not less than 8 char
            var regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
            var match = Regex.Match(password, regex, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return false;
            }

            return true;
        }
    }
}
