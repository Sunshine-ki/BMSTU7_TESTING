using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ui.Models;

using bl;
using Head;

namespace ui.Controllers
{
	public class HomeController : Controller
	{
		Head.Facade _facade;
		ui.Converter _converter;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			_converter = new Converter();
			_facade = new Head.Facade();
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Statistics()
		{
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
			ViewBag.Msg = "Вы успешно зарегистрированы!";
			ViewBag.Colors = "alert alert-success";
			return View();
			// return $"User: {user.Name} {user.Surname} {user.UserType} ";
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
