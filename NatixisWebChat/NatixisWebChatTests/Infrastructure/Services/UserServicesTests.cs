namespace NatixisWebChatTests.Infrastructure.Services
{
    using Moq;
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using NatixisWebChatInfrastructure.Services;
    using System.Collections.Generic;
    using Xunit;

    public class UserServicesTests
    {
        private readonly Mock<IBaseRepository<UserEntity>> _userEntityRepositoryMock = new Mock<IBaseRepository<UserEntity>>();
        private readonly UserServices _userServices;
        private readonly List<UserEntity> FakeUserCollection = new List<UserEntity>
        {
            new UserEntity { UserId = 1, Username = "User1", Password = "1234", Email = "user1@test.com" },
            new UserEntity { UserId = 2, Username = "User2", Password = "1234", Email = "user2@test.com" },
            new UserEntity { UserId = 3, Username = "User3", Password = "1234", Email = "user3@test.com" },
            new UserEntity { UserId = 4, Username = "User4", Password = "1234", Email = "user4@test.com" }
        };

        public UserServicesTests()
        {
            _userServices = new UserServices(_userEntityRepositoryMock.Object);
        } 

        [Fact(DisplayName= "ShouldReturnUserEntityIfUserIdExists")]
        public void ShouldReturnUserEntityIfUserIdExists()
        {
            // Arrange
            int userId = 1;
            var expected = new UserEntity { UserId = 1, Username = "User1", Password = "1234", Email = "user1@test.com" };

            // Act
            _userEntityRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(FakeUserCollection);
            var user = _userServices.GetUserById(userId);

            // Assert
            Assert.Equal(expected.UserId, user.UserId);
            Assert.Equal(expected.Username, user.Username);
            Assert.Equal(expected.Password, user.Password);
            Assert.Equal(expected.Email, user.Email);
        }

        [Fact(DisplayName = "ShouldReturnNullIfUserIdNotExists")]
        public void ShouldReturnNullIfUserIdNotExists()
        {
            // Arrange
            int userId = 0;

            // Act
            _userEntityRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(FakeUserCollection);
            var user = _userServices.GetUserById(userId);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public void ShouldReturnUserEntityIfUsernameExists()
        {
            // Arrange
            string username = "User1";
            var expected = new UserEntity { UserId = 1, Username = "User1", Password = "1234", Email = "user1@test.com" };

            // Act
            _userEntityRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(FakeUserCollection);
            var user = _userServices.GetUserByUsername(username);

            // Assert
            Assert.Equal(expected.UserId, user.UserId);
            Assert.Equal(expected.Username, user.Username);
            Assert.Equal(expected.Password, user.Password);
            Assert.Equal(expected.Email, user.Email);
        }

        [Fact]
        public void ShouldReturnNullIfUsernameNotExists()
        {
            // Arrange
            string username = "None";

            // Act
            _userEntityRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(FakeUserCollection);
            var user = _userServices.GetUserByUsername(username);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public void ShouldReturnUserEntityIfUsernameAndEmailExists()
        {
            // Arrange
            var input = new UserEntity { UserId = 1, Username = "User1", Password = "1234", Email = "user1@test.com" };
            var expected = new UserEntity { UserId = 1, Username = "User1", Password = "1234", Email = "user1@test.com" };

            // Act
            _userEntityRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(FakeUserCollection);
            var user = _userServices.GetUserByUsernameAndEmail(input);

            // Assert
            Assert.Equal(expected.UserId, user.UserId);
            Assert.Equal(expected.Username, user.Username);
            Assert.Equal(expected.Password, user.Password);
            Assert.Equal(expected.Email, user.Email);
        }

        [Fact]
        public void ShouldReturnNullIfUsernameAndEmailNotExists()
        {
            // Arrange
            var input = new UserEntity { UserId = 1, Username = "None", Password = "1234", Email = "none@test.com" };

            // Act
            _userEntityRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(FakeUserCollection);
            var user = _userServices.GetUserByUsernameAndEmail(input);

            // Assert
            Assert.Null(user);
        }
    }
}
