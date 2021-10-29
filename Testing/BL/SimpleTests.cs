using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using Head;
using db;
using Microsoft.EntityFrameworkCore;

using Testing.Builders;
using System.Linq;

namespace Testing.BL
{
    public class SimpleTests
    {
		private DbContextOptions<ApplicationContext> options;
		private ApplicationContext context;
		int taskId = 57; 
		
		public SimpleTests()
		{
			this.options = new DbContextOptionsBuilder<ApplicationContext>()
									.UseInMemoryDatabase(databaseName: "coursework_db")
									.Options;
			context =  new ApplicationContext(options);
			context.Tasks.Add(new db.Task() {Id = taskId, Solution = "select * from test where test.a + test.b = 11"});
		}


		[Theory]
        [MemberData(nameof(DataForCompareSolutionTests))]
		public void CompareSolutionTests(string userSolution, int res)
		{
			IRepositoryCompletedTask repositoryCompletedTask = new PostgreSQLRepositoryCompletedTask(context); 
			IRepositoryUser repositoryCompletedUser = new PostgreSQLRepositoryUser(context); 
			IRepositoryTask repositoryTask = new PostgreSQLRepositoryTask(context); 
			bl.IFacade conFacadeBD = new db.ConFacade(repositoryCompletedUser, repositoryTask, repositoryCompletedTask);  
			var facade = new Head.Facade(null, conFacadeBD);

			var result =  facade.CompareSolution(userSolution, taskId);

			Assert.Equal(res, result.returnValue);

		}


        public static IEnumerable<object[]> DataForCompareSolutionTests =>
            new List<object[]>
            {
				new object[] {"select * from test where test.a + test.b = 11", Constants.OK },
				new object[] {"select * from test where test.a + test.b = 11 union select * from test", Constants.getNumberValue(Constants.Errors.NumberOfRowsDoesNotMatch)},
				new object[] {"select a from test where test.a + test.b = 11",  Constants.getNumberValue(Constants.Errors.NumberOfColumnsDoesNotMatch)},
				new object[] {"select a, b + 5 from test where test.a + test.b = 11",  Constants.getNumberValue(Constants.Errors.RowsDoesNotMatch)},
            };

		~SimpleTests()
		{
			context.Dispose();
		}
    }
}


// IT IS BAD. USE .UseInMemoryDatabase
// [Theory]
// [MemberData(nameof(DataForCompareSolutionTests))]
// public void CompareSolutionTests(string userSolution, string teacherSolution, int res)
// {
// 	int taskId = 57; 
// 	IRepositoryCompletedTask repositoryCompletedTask = new PostgreSQLRepositoryCompletedTask(); 
// 	IRepositoryUser repositoryCompletedUser = new PostgreSQLRepositoryUser(); 
// 	var mockOnTasksDbSet = new Mock<DbSet<db.Task>>();
//     mockOnTasksDbSet.Setup(facade => facade.Find(taskId)).Returns(new db.Task() {Solution = teacherSolution});
// 	ApplicationContext appCtx = new ApplicationContext() {Tasks = mockOnTasksDbSet.Object };
// 	IRepositoryTask repositoryTask = new PostgreSQLRepositoryTask(appCtx); 
// 	bl.IFacade conFacadeBD = new db.ConFacade(repositoryCompletedUser, repositoryTask, repositoryCompletedTask);  
//     var facade = new Head.Facade(null, conFacadeBD);

// 	var result =  facade.CompareSolution(userSolution, taskId);

// 	Assert.Equal(res, result.returnValue);
// }
