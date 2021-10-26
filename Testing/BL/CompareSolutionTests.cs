using System;
using Xunit;
using Moq;

using Head;
using db;

namespace Testing.BL
{
    public class FacadeCompareSolutionTests
    {
		[Fact]
		public void ShouldBeCompareCorrect()
		{
			int taskId = 4;
			string userSolution = "select 5";
			string teacherSolution = "select 5";
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetTask(taskId)).Returns(new bl.Task() {Solution = teacherSolution});
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

			var result =  facade.CompareSolution(userSolution, taskId);

            Assert.Equal(Head.Constants.OK, result.returnValue);
		}	

		[Fact]
		public void ShouldBeReturnRowsDoesNotMatch()
		{
			int taskId = 4;
			string userSolution = "select 2";
			string teacherSolution = "select 3";
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetTask(taskId)).Returns(new bl.Task() {Solution = teacherSolution});
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

			var result =  facade.CompareSolution(userSolution, taskId);

			Assert.Equal((int)Constants.Errors.RowsDoesNotMatch, result.returnValue);
		}

		[Fact]
		public void ShouldBeReturnNumberOfColumnsDoesNotMatch()
		{
			int taskId = 4;
			string userSolution = "select 2, 4";
			string teacherSolution = "select 2";
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetTask(taskId)).Returns(new bl.Task() {Solution = teacherSolution});
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

			var result =  facade.CompareSolution(userSolution, taskId);

			Assert.Equal((int)Constants.Errors.NumberOfColumnsDoesNotMatch, result.returnValue);
		}

		[Fact]
		public void ShouldBeReturnNumberOfRowsDoesNotMatch()
		{
			int taskId = 4;
			string userSolution = "select 2 union select 3 union select 4 union select 5";
			string teacherSolution = "select 2";
            var mockOnConFacadeBD = new Mock<bl.IFacade>();
            mockOnConFacadeBD.Setup(facade => facade.GetTask(taskId)).Returns(new bl.Task() {Solution = teacherSolution});
            var facade = new Head.Facade(null, mockOnConFacadeBD.Object);

			var result =  facade.CompareSolution(userSolution, taskId);

			Assert.Equal((int)Constants.Errors.NumberOfRowsDoesNotMatch, result.returnValue);
		}


    }
}
