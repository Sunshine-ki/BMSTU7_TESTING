using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using PostSharp.Aspects;

using Head;
using db;

using Testing.Builders;

namespace Testing.BL
{
    public class FacadeGetUsersTests
    {
        [Fact]
        public void GetUsersTest()
        {
            // Arrange 
            var mock = new Mock<bl.IFacade>();
            var mustBeResult = new List<bl.User>() {new bl.User(), new bl.User()};
            // var mustBeResult = new UsersBLListBuilder(3).Build();
            mock.Setup(facade => facade.GetUsers()).Returns(mustBeResult);
            var facade = new Head.Facade(null, mock.Object);

            // Act
            var result = facade.GetUsers();

            // Assert
            Assert.Equal(mustBeResult.Count, result.Count);
        }
    }
}
