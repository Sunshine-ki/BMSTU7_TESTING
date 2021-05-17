using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace bl
{
	public class CompletedTask
	{
		public CompletedTask(int userId, int taskId)
		{
			UserId = userId;
			TaskId = taskId;
		}

		public int Id { get; set; }

		public int UserId { get; set; } // Внешний ключ.
		public int TaskId { get; set; }

	}
}