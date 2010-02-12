using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;

namespace CarbonFitnessWeb.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserBusinessLogic userBusinessLogic;
		private readonly IUserContext userContext;

		public UserController(IUserBusinessLogic userBusinessLogic, IUserContext userContext)
		{
			this.userBusinessLogic = userBusinessLogic;
			this.userContext = userContext;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(string userName, string password)
		{
			User user = userBusinessLogic.SaveOrUpdate(new User {Username = userName, Password = password});
			userContext.LogIn(user, false);

			return RedirectToAction("Details", new {id = user.Id});
		}

		public ActionResult Details(int Id)
		{
			User user = userBusinessLogic.Get(Id);
			return View(user);
		}
	}
}