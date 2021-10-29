using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace db
{
	public class PostgreSQLRepositoryUser : IRepositoryUser
	{
		private ApplicationContext db;

		public PostgreSQLRepositoryUser()
		{
			DbContextOptionsBuilder options = new DbContextOptionsBuilder();
			options.UseNpgsql("Host=localhost;Port=5432;Database=coursework_db;Username=lis;Password=password");
			this.db = new ApplicationContext(options.Options);
		}

		public PostgreSQLRepositoryUser(ApplicationContext db)
		{
			this.db = db;
		}


		public User GetUser(int id)
		{
			// Find - Поиск по PK.
			return db.Users.Find(id);
		}

		public User GetUserByEmail(string email)
		{
			User user = (from p in db.Users
						 where p.Email == email
						 select p).FirstOrDefault();
			return user;
		}

		public User GetUserByLogin(string login)
		{
			User user = (from p in db.Users
						 where p.Login == login
						 select p).FirstOrDefault();
			return user;
		}

		public List<User> GetUsers()
		{
			return db.Users.ToList();
		}

		public void Add(User user)
		{
			db.Users.Add(user);
		}

		public void Save()
		{
			db.SaveChanges();
		}


		public void UpdateName(int id, string newName)
		{
			User user = db.Users.Find(id);
			if (user != null)
			{
				user.Name = newName;
			}
		}
		public void UpdateSurname(int id, string newSurname)
		{
			User user = db.Users.Find(id);
			if (user != null)
			{
				user.Surname = newSurname;
			}
		}
		public void UpdateEmail(int id, string newEmail)
		{
			User user = db.Users.Find(id);
			if (user != null)
			{
				user.Email = newEmail;
			}
		}
		public void UpdateLogin(int id, string newLogin)
		{
			User user = db.Users.Find(id);
			if (user != null)
			{
				user.Login = newLogin;
			}
		}

		public void Delete(int id)
		{
			User user = db.Users.Find(id);
			if (user != null)
				db.Users.Remove(user);
		}
	}
}