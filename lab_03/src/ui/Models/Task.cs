using System;
using System.Collections.Generic;

namespace ui.Models
{
	public class Task
	{
		public Task()
		{
			Id = 0;
			Name = "";
			ShortDescription = "";
			DetailedDescription = "";
			Solution = "";
			TableName = "";
			// AuthorId = 0;
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