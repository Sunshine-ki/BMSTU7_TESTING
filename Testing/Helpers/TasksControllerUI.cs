using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ui.Models;
using System.Web;
using Microsoft.AspNetCore.Http;

using bl;
using Head;

namespace Testing.Helpers
{
	public class TasksControllerUI : Controller
	{
		private int ok = 0;
		private int error = -1;
		private Head.Facade _facade;
		private ui.Converter _converter;

		public TasksControllerUI(Head.Facade facade)
		{
			_converter = new ui.Converter();
			_facade = facade;
		}

		public int Tasks()
		{
			ViewBag.tasks = _facade.GetTasks();
			return ok;
		}

		public int Task(string userSolution, int taskId)
		{
			bl.Task taskBL = _facade.GetTask(taskId);
			if (taskBL is null)
			{
				return error;
			}

			ui.Models.Task task = _converter.ConvertTaskToUI(taskBL);
			ViewBag.task = task;

			var result = _facade.CompareSolution(userSolution, taskId); 
			
			if (result.returnValue != Head.Constants.OK)
			{
				return error;
			}
			
			return ok;
		}
	}
}
