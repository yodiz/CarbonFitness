﻿using System.Web.Mvc;

namespace CarbonFitness.App.Web.Controllers {
	[HandleError]
	public class HomeController : Controller {
		public ActionResult Index() {
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