using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Controllers {

	[HandleError]
	public class UserController : Controller {
		private readonly IUserBusinessLogic userBusinessLogic;
		private readonly IUserContext userContext;

		public UserController(IUserBusinessLogic userBusinessLogic, IUserContext userContext) {
			this.userBusinessLogic = userBusinessLogic;
			this.userContext = userContext;
		}

		[Authorize]
		public ActionResult Index() {
			return View();
		}

		public ActionResult Create() {
			return View();
		}

		[HttpPost]
		public ActionResult Create(string userName, string password) {
			var user = userBusinessLogic.SaveOrUpdate(new User {Username = userName, Password = password});
			userContext.LogIn(user, false);

			return RedirectToAction("Details", new {id = user.Id});
		}

		[Authorize]
		public ActionResult Details(int id) {
			var user = userBusinessLogic.Get(id);
			return View(user);
		}
	}
}