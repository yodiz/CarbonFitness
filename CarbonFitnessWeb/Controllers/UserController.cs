using System.Web.Mvc;
using CarbonFitness.Repository;

namespace CarbonFitnessWeb.Controllers {
    public class UserController : Controller {

        public IUserRepository UserRepository { get; private set; }

        public UserController() : this(new UserRepository()) {
        }

        public UserController(IUserRepository ur) {
            UserRepository = ur;
        }

        public ActionResult Index() {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string userName) {
            return RedirectToAction("Details");
        }

        // /User/View/23
        public ActionResult Details(string userName)
        {
            
            var user = this.UserRepository.Get(userName);

            this.ViewData["user"] = user;

            return View();
        }
    }
}