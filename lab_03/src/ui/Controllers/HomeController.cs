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
	public class HomeController : Controller
	{
		Head.Facade _facade;
		ui.Converter _converter;
		private readonly ILogger<HomeController> _logger;

		// public HomeController(ILogger<HomeController> logger, Head.Facade facade, ILogger<Head.Facade> loggerFacade)
		public HomeController(ILogger<HomeController> logger, ILogger<Head.Facade> loggerFacade)
		{
			_logger = logger;
			_converter = new Converter();
			// _facade = facade;
			_facade = new Head.Facade(loggerFacade);
		}

		public IActionResult Index()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
				return Redirect("/Home/Welcome");
			return Redirect("/Home/Tasks");
		}

		[HttpGet]
		public IActionResult Task(int taskId)
		{
			bl.Task taskBL = _facade.GetTask(taskId);
			if (taskBL is null)
			{
				return Redirect("/Home/Tasks");
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

		public IActionResult Tasks()
		{
			ViewBag.tasks = _facade.GetTasks();
			return View();
		}

		public IActionResult Welcome()
		{
			return View();
		}

		public IActionResult Statistics()
		{
			ViewBag.Tasks = _facade.GetTasks();
			return View();
		}

		[HttpGet]
		public IActionResult Registration()
		{
			ViewBag.Msg = "Заполните все поля";
			ViewBag.Colors = "alert alert-success"; // TODO: Это в константы...
			ViewBag.user = new ui.Models.User();
			return View();
		}

		[HttpPost]
		public IActionResult Registration(ui.Models.User user)
		{
			bl.User userBL = _converter.ConvertUserToBL(user);

			ViewBag.user = user;
			Head.Answer answer = _facade.AddUser(userBL);
			if (answer.returnValue != Constants.OK)
			{
				ViewBag.Msg = answer.Msg;
				ViewBag.Colors = "alert alert-danger";
				return View();
			}

			ViewBag.user = new ui.Models.User();
			bl.User newUser = _facade.GetUserByLogin(userBL.Login);
			HttpContext.Session.SetString("id", Convert.ToString(newUser.Id));

			_logger.LogInformation("Пользователь зарегистрировался");

			ViewBag.Msg = "Вы успешно зарегистрированы!";
			ViewBag.Colors = "alert alert-success";
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
