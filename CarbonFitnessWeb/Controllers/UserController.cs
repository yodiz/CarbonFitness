using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;


namespace CarbonFitnessWeb.Controllers {
    public class UserController : Controller {
        public UserController(IUserBusinessLogic ur) {
            UserBusinessLogic = ur;
        }

        public IUserBusinessLogic UserBusinessLogic { get; private set; }

        public ActionResult Index() {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string userName, string password) {
            //Save...
            var user = UserBusinessLogic.SaveOrUpdate(new User { Username= userName, Password=password });
            return RedirectToAction("Details", new { id = user.Id });
        }

        public ActionResult Details(int Id) {
            var user = UserBusinessLogic.Get(Id);
            return View(user);
        }
    }
}