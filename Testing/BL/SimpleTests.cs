using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using Head;
using db;

using Testing.Builders;

namespace Testing.BL
{
    public class SimpleTests
    {
        [Theory]
        [MemberData(nameof(DataForCompareSolutionTests))]
		public void CompareSolutionTests(string userSolution, int res)
		{
			int taskId = 57; 
			IRepositoryCompletedTask repositoryCompletedTask = new PostgreSQLRepositoryCompletedTask(); 
			IRepositoryTask repositoryTask = new PostgreSQLRepositoryTask(); 
			IRepositoryUser repositoryCompletedUser = new PostgreSQLRepositoryUser(); 
			bl.IFacade conFacadeBD = new db.ConFacade(repositoryCompletedUser, repositoryTask, repositoryCompletedTask);  
            var facade = new Head.Facade(null, conFacadeBD);

			var result =  facade.CompareSolution(userSolution, taskId);

			Assert.Equal(res, result.returnValue);
		}

        public static IEnumerable<object[]> DataForCompareSolutionTests =>
            new List<object[]>
            {
				new object[] {"select * from test where test.a + test.b = 11", Constants.OK },
				new object[] {"select a from test where test.a + test.b = 11", Constants.getNumberValue(Constants.Errors.NumberOfColumnsDoesNotMatch)},
				new object[] {"select * from test where test.a + test.b = 11 union select * from test",  Constants.getNumberValue(Constants.Errors.NumberOfRowsDoesNotMatch)},
				new object[] {"select a, b + 5 from test where test.a + test.b = 11",  Constants.getNumberValue(Constants.Errors.RowsDoesNotMatch)},
            };

    }
}
