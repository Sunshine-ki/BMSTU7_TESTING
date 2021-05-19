using System;
using System.Collections.Generic;

namespace db
{
	public interface IRepositoryUser // : IDisposable
	{
		List<User> GetUsers();
		User GetUser(int id);
		User GetUserByEmail(string email);
		User GetUserByLogin(string login);
		void Add(User user);
		void Delete(int id);
		void UpdateName(int id, string newName);
		void UpdateSurname(int id, string newSurname);
		void UpdateEmail(int id, string newEmail);
		void UpdateLogin(int id, string newLogin);
		void Save();
	}

}
