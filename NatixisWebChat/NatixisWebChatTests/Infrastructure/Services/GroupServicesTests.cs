namespace NatixisWebChatTests.Infrastructure.Services
{
    using Moq;
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using NatixisWebChatInfrastructure.Services;
    using System.Collections.Generic;
    using Xunit;

    public class GroupServicesTests
    {
        private readonly Mock<IBaseRepository<GroupEntity>> _userEntityRepositoryMock;
        private readonly GroupServices _groupServices;
        private readonly List<GroupEntity> FakeGroupCollection = new List<GroupEntity>
        {
            new GroupEntity { GroupId = 1, GroupName = "Group1" },
            new GroupEntity { GroupId = 2, GroupName = "Group2" },
            new GroupEntity { GroupId = 3, GroupName = "Group3" },
            new GroupEntity { GroupId = 4, GroupName = "Group4" },
        };

        public GroupServicesTests()
        {
            _userEntityRepositoryMock = new Mock<IBaseRepository<GroupEntity>>();
            _groupServices = new GroupServices(_userEntityRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnGroupEntityIfGroupnameExists()
        {
            // Arrange
            string groupname = "Group1";
            var expected = new GroupEntity { GroupId = 1, GroupName = "Group1" };

            // Act
            _userEntityRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(FakeGroupCollection);
            var result = _groupServices.GetGroupByName(groupname);

            // Assert
            Assert.Equal(expected.GroupId, result.GroupId);
            Assert.Equal(expected.GroupName, result.GroupName);
        }

        [Fact]
        public void ShouldReturnNullIfGroupnameNotExists()
        {
            // Arrange
            string groupname = "None";

            // Act
            _userEntityRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(FakeGroupCollection);
            var result = _groupServices.GetGroupByName(groupname);

            // Assert
            Assert.Null(result);
        }
    }
}
