using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using Head;
using db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using Testing.Builders;
using System.Linq;

using ui.Controllers;

namespace Testing.E2E
{
    public class ControllerTests
    {
		private DbContextOptions<ApplicationContext> options;
		private ApplicationContext context;
		int taskId = 57; 
		string teacherSolution = "select * from test where test.a + test.b = 11";
		
		int secondTaskId = 58; 
		string secondTeacherSolution = "select a from test;";
		
		public ControllerTests()
		{
			this.options = new DbContextOptionsBuilder<ApplicationContext>()
									.UseInMemoryDatabase(databaseName: "coursework_db")
									.Options;
			context =  new ApplicationContext(options);
			context.Tasks.Add(new db.Task() {Id = taskId, Solution = teacherSolution});
			context.Tasks.Add(new db.Task() {Id = secondTaskId, Solution = secondTeacherSolution});
		}


		public void UserSolvesTasksTest()
		{
			// Arrange
			IRepositoryCompletedTask repositoryCompletedTask = new PostgreSQLRepositoryCompletedTask(context); 
			IRepositoryUser repositoryCompletedUser = new PostgreSQLRepositoryUser(context); 
			IRepositoryTask repositoryTask = new PostgreSQLRepositoryTask(context); 
			bl.IFacade conFacadeBD = new db.ConFacade(repositoryCompletedUser, repositoryTask, repositoryCompletedTask);  
			var facade = new Head.Facade(null, conFacadeBD);
			TasksController tasksController = new TasksController(null, facade); 
			var goodSolution = teacherSolution;
			var goodSecondSolution = secondTeacherSolution;
			var badSolutions = new string[] {"How it works?", 
											 "Help me...", 
											 "select 5", 
											 "I got it!", 
											 "",
											 "select a from test;", 
											 "select b from test;", 
											 "SELECR * from test where test.a + test.b ...",
											 "select * from test where test.a + test.b = what?"
											 };


			// Act-Assert (See all tasks)
			tasksController.Tasks();
			Assert.Equal(0, tasksController.ViewBag.code);

			// // Act-Assert (Bad solution)
			// foreach (var badSolution in badSolutions)
			// {
			// 	tasksController.Task(badSolution, taskId);
			// 	Assert.Equal(-1, tasksController.ViewBag.code);
			// }

			// // Act-Assert (Good solution)
			// tasksController.Task(goodSolution, taskId);
			// Assert.Equal(0, tasksController.ViewBag.code);

			// // Act-Assert (See all tasks again)
			// tasksController.Tasks();
			// Assert.Equal(0, tasksController.ViewBag.code);

			// // Act-Assert (Good solution)
			// tasksController.Task(goodSecondSolution, secondTaskId);
			// Assert.Equal(0, tasksController.ViewBag.code);
		}

		[Theory]
		[InlineData(100)]
		public void UserSolvesTasksTestManyTimes(int count)
		{
			// for (int i = 0; i < count; i++)
			// {
				UserSolvesTasksTest();
			// }
		}

		~ControllerTests()
		{
			context.Dispose();
		}
    }
}
