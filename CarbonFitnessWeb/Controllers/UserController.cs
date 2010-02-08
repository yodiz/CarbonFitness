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
        public ActionResult Create(string userName, string password) {
            //Save...
            var user = UserRepository.SaveOrUpdate(new User { Username= userName, Password=password });

            return RedirectToAction("Details", new { id = user.Id });
        }

        public ActionResult Details(int Id) {
            var user = UserRepository.Get(Id);
            return View(user);
        }
    }
}