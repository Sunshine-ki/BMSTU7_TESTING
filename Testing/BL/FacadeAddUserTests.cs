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
    public class FacadeAddUserTests
    {
        bl.User userEmpty = null;

        [Fact]
        public void ShouldBeAddUserTest()
        {
            // Arrange 
            var user = new UserBLBuilder().WithLogin("Sunshine-ki")
                                          .WithEmail("Sinshine@mail.ru")
                                          .WithPassword("12345a")
                                          .Build();
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetUserByEmail("Sinshine@mail.ru")).Returns(userEmpty);
            mockOnConFacadeBD.Setup(facade => facade.GetUserByLogin("Sunshine-ki")).Returns(userEmpty);
            mockOnConFacadeBD.Setup(facade => facade.AddUser(user)).Returns(0);
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

            // Act
            var result = facade.AddUser(user);

            // Assert
            Assert.Equal(Head.Constants.OK, result.returnValue);
        }

        [Fact]
        public void ShouldBeReturnShortLengthPasswordErrorrTest()
        {
            var user = new UserBLBuilder().WithLogin("Sunshine-ki")
                                          .WithEmail("Sinshine@mail.ru")
                                          .WithPassword("1")
                                          .Build();
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetUserByEmail("Sinshine@mail.ru")).Returns(userEmpty);
            mockOnConFacadeBD.Setup(facade => facade.GetUserByLogin("Sunshine-ki")).Returns(userEmpty);
            mockOnConFacadeBD.Setup(facade => facade.AddUser(user)).Returns(0);
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

            var result = facade.AddUser(user);

            Assert.Equal((int)Head.Constants.Errors.ShortLengthPassword, result.returnValue);
        }


        [Fact]
        public void ShouldBeReturnEmailUserExistsTest()
        {
            var user = new UserBLBuilder().WithLogin("Sunshine-ki")
                                          .WithEmail("Sinshine@mail.ru")
                                          .WithPassword("12345a")
                                          .Build();
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetUserByEmail("Sinshine@mail.ru")).Returns(new bl.User());
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

            var result = facade.AddUser(user);

            Assert.Equal((int)Head.Constants.Errors.EmailUserExists, result.returnValue);
        }

        [Fact]
        public void ShouldBeReturnLoginUserExistsTest()
        {
            var user = new UserBLBuilder().WithLogin("Sunshine-ki")
                                          .WithEmail("Sinshine@mail.ru")
                                          .WithPassword("12345a")
                                          .Build();
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetUserByEmail("Sinshine@mail.ru")).Returns(userEmpty);
            mockOnConFacadeBD.Setup(facade => facade.GetUserByLogin("Sunshine-ki")).Returns(new bl.User());
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

            var result = facade.AddUser(user);

            Assert.Equal((int)Head.Constants.Errors.LoginUserExists, result.returnValue);
        }
    }
}
