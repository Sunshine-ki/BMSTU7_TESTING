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
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
				return Redirect("/Home/Registration");

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
		public IActionResult Task()
		{
			ViewBag.task = new ui.Models.Task();
			ViewBag.info_text = "Задача на проверке!";
			ViewBag.colors = "alert alert-success";
			return View();
		}
	}
}
