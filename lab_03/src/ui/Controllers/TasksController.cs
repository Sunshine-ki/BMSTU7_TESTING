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

namespace ui.Controllers
{
	public class TasksController : Controller
	{
		Head.Facade _facade;
		ui.Converter _converter;
		private readonly ILogger<HomeController> _logger;

		// public TasksController(ILogger<HomeController> logger, ILogger<Head.Facade> loggerFacade)
		public TasksController(ILogger<HomeController> logger, Head.Facade facade)
		{
			_logger = logger;
			_converter = new Converter();
			// _facade = new Head.Facade(loggerFacade);
			_facade = facade;
		}

		[ActionName("Index")]
		public IActionResult Tasks()
		{
			ViewBag.tasks = _facade.GetTasks();
			return View();
		}

		[HttpGet]
		public IActionResult Task(int taskId)
		{
			// if (string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
			// 	return Redirect("/Home/Registration");

			bl.Task taskBL = _facade.GetTask(taskId);
			if (taskBL is null)
			{
				return Redirect("/Tasks");
			}
			ui.Models.Task task = _converter.ConvertTaskToUI(taskBL);
			ViewBag.task = task;
			ViewBag.info_text = "Решите задачу";
			ViewBag.colors = "alert alert-success";
			return View();
		}

		[HttpPost]
		public IActionResult Task(string userSolution, int taskId)
		{
			Console.WriteLine($"user_solution = {userSolution} TaskId = {taskId}");
			
			bl.Task taskBL = _facade.GetTask(taskId);
			if (taskBL is null)
			{
				return Redirect("/Tasks");
			}

			ui.Models.Task task = _converter.ConvertTaskToUI(taskBL);
			ViewBag.task = task;

			var result = _facade.CompareSolution(userSolution, taskId); 
			
			if (result.returnValue == Head.Constants.OK)
			{
				ViewBag.info_text = "Задача решена!";
				ViewBag.colors = "alert alert-success";
			}
			else 
			{
				ViewBag.info_text = result.Msg;
				ViewBag.colors = "alert alert-danger";
			}

			return View();
		}
	}
}
