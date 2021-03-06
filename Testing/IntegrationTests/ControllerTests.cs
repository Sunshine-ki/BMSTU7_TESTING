using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using Head;
using db;
using Microsoft.EntityFrameworkCore;

using Testing.Builders;
using System.Linq;


// Первичная очистка табл. Ok
// Регистрация тесты Ok.
// Параметризация тестов Ok.
// Два контекса ? Что бы кэширования не было.

namespace Testing.IntegrationTests
{
    public class ControllerTests
    {
		private DbContextOptions<ApplicationContext> options;
		private ApplicationContext context;
		int taskId = 0; 
		
		public ControllerTests()
		{
			initialization();
			cleanUp();
		}



		private void initialization()
		{
			DbContextOptionsBuilder options = new DbContextOptionsBuilder();
			options.UseNpgsql("Host=localhost;Port=5432;Database=coursework_db_backup;Username=lis;Password=password");
			context = new ApplicationContext(options.Options);
		}

		private void cleanUp()
		{
			DbContextOptionsBuilder options = new DbContextOptionsBuilder();
			options.UseNpgsql("Host=localhost;Port=5432;Database=coursework_db_backup;Username=lis;Password=password");
			var context = new ApplicationContext(options.Options);

			var repositoryUser = new PostgreSQLRepositoryUser(context); 
			var users = repositoryUser.GetUsers();

			foreach(var user in users)
			{
				repositoryUser.Delete(user.Id);
			}

			repositoryUser.Save();
		}


		[Theory]
        [MemberData(nameof(DataForRegistrationTests))]
		public void RegistrationEmailTests(bl.User user1, bl.User user2, int res)
		{
			cleanUp();
			var repositoryUser = new PostgreSQLRepositoryUser(context); 
			var repositoryTask = new PostgreSQLRepositoryTask(context); 
			var repositoryCompletedTask = new PostgreSQLRepositoryCompletedTask(context); 
			bl.IFacade conFacadeBD = new db.ConFacade(repositoryUser, repositoryTask, repositoryCompletedTask);  
			var facade = new Head.Facade(null, conFacadeBD);
			
			Head.Answer resFromFacade = facade.AddUser(user1);
			Head.Answer resFromFacade2 = facade.AddUser(user2);

			Assert.Equal(resFromFacade.returnValue, Constants.OK);
			Assert.Equal(resFromFacade2.returnValue, res);
		}


		public static IEnumerable<object[]> DataForRegistrationTests =>
            new List<object[]>
            {
				// Разные данные, все ок.
				new object[] {new bl.User() {Email="someemail@mail1.com", Login="someLogin1", Password = "123qwe"}, 
							  new bl.User() {Email="qwerty@mail1.com", Login="qwerty", Password = "123qwe"}, 
							  (int)Constants.OK},

				// Одинаковые логины. Ошибка.
				new object[] {new bl.User() {Email="someemail2@mail1.com", Login="someLogin2", Password = "123qwe"}, 
							  new bl.User() {Email="qwerty2@mail1.com", Login="someLogin2", Password = "123qwe"}, 
							  (int)Constants.Errors.LoginUserExists},

				// Одинаковая почта. Ошибка.
				new object[] {new bl.User() {Email="m@mail1.com", Login="Lis", Password = "123qwe"}, 
							  new bl.User() {Email="m@mail1.com", Login="NotLis", Password = "123qwe"}, 
							  (int)Constants.Errors.EmailUserExists},
            };

		
		~ControllerTests()
		{
			// cleanUp();
			context.Dispose();
		}
    }
}