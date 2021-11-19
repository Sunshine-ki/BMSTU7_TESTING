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

using Testing.Helpers;
using ui.Controllers;
using System.Text.Json;
using ui.dto;
using ui.Helpers;

namespace Testing.E2E
{
    public class ControllerTests
    {
		private int ok = 0;
		private int error = -1;
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

		// Переписать на json. Ok. 
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
			string resJson = tasksController.Tasks();
			List<bl.Task> resultTask = (List<bl.Task>) JsonSerializer.Deserialize(resJson, typeof(List<bl.Task>), Options.JsonOptions());
			Assert.NotNull(resultTask);

			// Act-Assert (Bad solution)
			ResultDTO result;
			foreach (var badSolution in badSolutions)
			{
				resJson = tasksController.Task(badSolution, taskId);
				result = (ResultDTO) JsonSerializer.Deserialize(resJson, typeof(ResultDTO), Options.JsonOptions());
				Assert.Equal(error, result.Code);
			}

			// Act-Assert (Good solution)
			resJson = tasksController.Task(goodSolution, taskId);
			result = (ResultDTO) JsonSerializer.Deserialize(resJson, typeof(ResultDTO), Options.JsonOptions());
			Assert.Equal(ok, result.Code);

			// Act-Assert (See all tasks again)
			resJson = tasksController.Tasks();
			resultTask = (List<bl.Task>) JsonSerializer.Deserialize(resJson, typeof(List<bl.Task>), Options.JsonOptions());
			Assert.NotNull(resultTask);

			// Act-Assert (Good solution)
			resJson = tasksController.Task(goodSecondSolution, secondTaskId);
			result = (ResultDTO) JsonSerializer.Deserialize(resJson, typeof(ResultDTO), Options.JsonOptions());
			Assert.Equal(ok, result.Code);
		}

		[Theory]
		[InlineData(100)]
		public void UserSolvesTasksTestManyTimes(int count)
		{
			for (int i = 0; i < count; i++)
			{
				UserSolvesTasksTest();
			}
		}

		~ControllerTests()
		{
			context.Dispose();
		}
    }
}
