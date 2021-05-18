using System;
using System.Linq;
using Xunit;
using db;

// https://xunit.net/docs/getting-started/netcore/cmdline

namespace tests
{
	public class UnitTestUser
	{
		[Fact]
		public void TestUpdateLogin()
		{
			const string newLogin = "New Login";
			const int id = 4;

			IRepositoryUser db = new PostgreSQLRepositoryUser();

			db.UpdateLogin(db.GetUser(id).Id, newLogin);
			Assert.Equal(db.GetUser(id).Login, newLogin);
		}

		[Fact]
		public void TestUpdateName()
		{
			const string newName = "New Name";
			const int id = 4;

			IRepositoryUser db = new PostgreSQLRepositoryUser();

			db.UpdateName(db.GetUser(id).Id, newName);
			Assert.Equal(db.GetUser(id).Name, newName);
		}

		[Fact]
		public void TestUpdateSurname()
		{
			const string newSurname = "New Surname";
			const int id = 4;

			IRepositoryUser db = new PostgreSQLRepositoryUser();

			db.UpdateSurname(db.GetUser(id).Id, newSurname);
			Assert.Equal(db.GetUser(id).Surname, newSurname);
		}

		[Fact]
		public void TestUpdateEmail()
		{
			const string newEmail = "NewEmail@mail.ru";
			const int id = 4;

			IRepositoryUser db = new PostgreSQLRepositoryUser();

			db.UpdateEmail(db.GetUser(id).Id, newEmail);
			Assert.Equal(db.GetUser(id).Email, newEmail);
		}

		[Fact]
		public void TestDelete()
		{
			const int id = 13;

			IRepositoryUser db = new PostgreSQLRepositoryUser();
			db.Delete(id);

			if (db.GetUser(id) is null)
				throw new Exception("Problem in delete user");
		}

		[Fact]
		public void TestDeleteSave()
		{
			const int id = 14;

			IRepositoryUser db = new PostgreSQLRepositoryUser();
			db.Delete(id);
			db.Save();

			if (db.GetUser(id) != null)
				throw new Exception("Problem in delete user");

			// Assert.IsNull(null);
			// Assert.False(false, "error text");
		}

		[Fact]
		public void TestAdd()
		{
			IRepositoryUser db = new PostgreSQLRepositoryUser();
			User newUser = new User()
			{
				Name = "Nik",
				Surname = "Filk",
				Email = "email@mail.ru",
				Login = "FilkNik",
				Password = "123qwe",
				UserType = 0
			};

			db.Add(newUser);
			db.Save();

			int id = db.GetUsers().Max(p => p.Id);
			var user = (from u in db.GetUsers()
						where u.Id == id
						select u).ToList()[0];

			Assert.Equal(newUser.Name, user.Name);
			Assert.Equal(newUser.Surname, user.Surname);
			Assert.Equal(newUser.Email, user.Email);
			Assert.Equal(newUser.Password, user.Password);
			Assert.Equal(newUser.Login, user.Login);
			Assert.Equal(newUser.UserType, user.UserType);
		}

	}
}
