using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using Head;
using db;

using Testing.Builders;

// TODO: Constants.Errors.RowsDoesNotMatch -> приведение типов убрать. Ok
// TODO: параметризованные тесты. Ok
// + добавить классический тест. Ok. SimpleTests
// У меня лондонские 

namespace Testing.BL
{
    public class FacadeTests
    {
        private static bl.User userEmpty = null;

        [Theory]
        [MemberData(nameof(DataForAddUserTests))]
        public void AddUserTests(string userLogin, string userEmail, string userPassword, bl.User userByEmail, bl.User userByLogin, int res)
        {
            // Arrange 
            var user = new UserBLBuilder().WithLogin(userLogin)
                                          .WithEmail(userEmail)
                                          .WithPassword(userPassword)
                                          .Build();
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetUserByEmail(userEmail)).Returns(userByEmail);
            mockOnConFacadeBD.Setup(facade => facade.GetUserByLogin(userLogin)).Returns(userByLogin);
            mockOnConFacadeBD.Setup(facade => facade.AddUser(user)).Returns(0);
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

            // Act
            var result = facade.AddUser(user);

            // Assert
            Assert.Equal(res, result.returnValue);
        }

        [Theory]
        [MemberData(nameof(DataForCompareSolutionTests))]
		public void CompareSolutionTests(string userSolution, string teacherSolution, int res)
		{
			int taskId = 4;
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetTask(taskId)).Returns(new bl.Task() {Solution = teacherSolution});
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

			var result =  facade.CompareSolution(userSolution, taskId);

			Assert.Equal(res, result.returnValue);
		}

        [Fact]
        public void ShouldBeGetUsersTest()
        {
            var mock = new Mock<bl.IFacade>();
            var mustBeResult = new List<bl.User>() {new bl.User(), new bl.User()};
            mock.Setup(facade => facade.GetUsers()).Returns(mustBeResult);
            var facade = new Head.Facade(null, mock.Object);

            var result = facade.GetUsers();

            Assert.Equal(mustBeResult.Count, result.Count);
        }

        public static IEnumerable<object[]> DataForCompareSolutionTests =>
            new List<object[]>
            {
				new object[] {"select 5", "select 5", Constants.OK },
				new object[] {"select 2 union select 3 union select 4 union select 5", "select 2", Constants.getNumberValue(Constants.Errors.NumberOfRowsDoesNotMatch)}, 
				new object[] {"select 2, 4", "select 2", Constants.getNumberValue(Constants.Errors.NumberOfColumnsDoesNotMatch)},
				new object[] {"select 2", "select 3",  Constants.getNumberValue(Constants.Errors.RowsDoesNotMatch)},
            };

        public static IEnumerable<object[]> DataForAddUserTests =>
            new List<object[]>
            {
                new object[] {"Sunshine-ki", "Sinshine@mail.ru", "12345a", userEmpty, userEmpty, Constants.OK},
                new object[] {"Sunshine-ki", "Sinshine@mail.ru", "12345a", userEmpty, new bl.User(), Constants.getNumberValue(Constants.Errors.LoginUserExists)},
                new object[] {"Sunshine-ki", "Sinshine@mail.ru", "12345a", new bl.User(), userEmpty, Constants.getNumberValue(Constants.Errors.EmailUserExists)},
                new object[] {"Sunshine-ki", "Sinshine@mail.ru", "1", userEmpty, userEmpty, Constants.getNumberValue(Constants.Errors.ShortLengthPassword)},
            };


    }
}
