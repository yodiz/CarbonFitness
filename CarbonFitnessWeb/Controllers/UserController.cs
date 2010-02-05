using System.Web.Mvc;
using CarbonFitness.Model;
using CarbonFitness.Repository;

namespace CarbonFitnessWeb.Controllers {
    public class UserController : Controller {
        public UserController() : this(new UserRepository()) {
        }

        public UserController(IUserRepository ur) {
            UserRepository = ur;
        }

        public IUserRepository UserRepository { get; private set; }

        public ActionResult Index() {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string userName) {
            //Save...
            var user = UserRepository.SaveOrUpdate(new User { Username= userName});

            return RedirectToAction("Details", new { id = user.Id });
        }

        public ActionResult Details(string userName) {
            var user = UserRepository.Get(userName);
            ViewData["user"] = user;
            return View();
        }
    }
}