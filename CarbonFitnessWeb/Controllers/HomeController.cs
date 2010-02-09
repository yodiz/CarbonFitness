﻿using System.Web.Mvc;

namespace CarbonFitnessWeb.Controllers {
	[HandleError]
	public class HomeController : Controller {
		public ActionResult Index() {
			ViewData["Message"] = "Welcome to ASP.NET MVC!";

			return View();
		}

		public ActionResult About() {
			return View();
		}

		[Authorize]
		public ActionResult AddFood() {
			return View();
		}
	}
}