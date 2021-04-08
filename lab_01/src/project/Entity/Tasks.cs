using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace project
{
	public class Task
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ShortDescription { get; set; }
		public string DetailedDescription { get; set; }
		public string Solution { get; set; }
		public string TableName { get; set; }
		public DateTime CreationTime { get; set; } = DateTime.Now;


		public int AuthorId { get; set; } // Внешний ключ.
		[ForeignKey("AuthorId")]
		public User User { get; set; } // Навигационное свойство.

		public List<CompletedTask> CompletedTasks { get; set; }
	}
}