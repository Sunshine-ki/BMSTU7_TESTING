using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace project
{
	public class CompletedTask
	{
		public int Id { get; set; }

		public int UserId { get; set; } // Внешний ключ.
		public int TaskId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }

		[ForeignKey("TaskId")]
		public Task Task { get; set; }
	}
}