using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace bl
{
	public class Task
	{
		public Task(int id, string name, string shortDescription, string detailedDescription, string solution, string tableName, int authorId)
		{
			Id = id;
			Name = name;
			ShortDescription = shortDescription;
			DetailedDescription = detailedDescription;
			Solution = solution;
			TableName = tableName;
			AuthorId = authorId;
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string ShortDescription { get; set; }
		public string DetailedDescription { get; set; }
		public string Solution { get; set; }
		public string TableName { get; set; }
		// public DateTime CreationTime { get; set; } = DateTime.Now;
		public int AuthorId { get; set; } // Внешний ключ.
	}
}