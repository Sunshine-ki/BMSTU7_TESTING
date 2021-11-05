using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using Head;
using db;
using Microsoft.EntityFrameworkCore;

using Testing.Builders;
using System.Linq;

namespace Testing.IntegrationTests
{
    public class ControllerTests
    {
		private DbContextOptions<ApplicationContext> options;
		private ApplicationContext context;
		int taskId = 0; 
		
		public ControllerTests()
		{
			DbContextOptionsBuilder options = new DbContextOptionsBuilder();
			options.UseNpgsql("Host=localhost;Port=5432;Database=coursework_db;Username=lis;Password=password");
			context = new ApplicationContext(options.Options);
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
				new object[] {"SELECT * FROM test;", Constants.OK },
				new object[] {"ABSBS ASBSA;", Constants.getNumberValue(Constants.Errors.UserExecTask)},
				new object[] {"", Constants.getNumberValue(Constants.Errors.UserExecTask)},
				new object[] {"select * from test where 1 = 1 + 1", Constants.getNumberValue(Constants.Errors.NumberOfRowsDoesNotMatch)},
            };

		~ControllerTests()
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
