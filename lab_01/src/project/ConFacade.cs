using System;
using System.Collections.Generic;
using bl;

namespace db
{
	public class ConFacade : bl.IFacade
	{

		public List<bl.Task> GetTasks()
		{
			return new List<bl.Task>();
		}
		public List<bl.User> GetUsers()
		{
			return new List<bl.User>();
		}
		public List<bl.CompletedTask> GetCompletedTasks()
		{
			return new List<bl.CompletedTask>();
		}
	}
}
