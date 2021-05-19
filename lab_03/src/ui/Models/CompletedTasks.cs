using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ui.Models
{
	public class CompletedTask
	{
		public CompletedTask() { }

		public int Id { get; set; }

		public int UserId { get; set; } // Внешний ключ.
		public int TaskId { get; set; }

	}
}