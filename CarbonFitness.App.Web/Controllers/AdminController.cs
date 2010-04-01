using System.Web;
using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.AppLogic;

namespace CarbonFitness.App.Web.Controllers {
	public class AdminController : Controller {
		private readonly IIngredientImporterEngine ingredientImporterEngine;
		private IPathFinder pathFinder; 
		private IPathFinder PathFinder { 
			get {
				if(pathFinder== null) {
					pathFinder = new PathFinder(Server);
				}
				return pathFinder;
			} 
		}
		private readonly ISchemaExportEngine schemaExportEngine;

		public AdminController(ISchemaExportEngine schemaExportEngine, IIngredientImporterEngine ingredientImporterEngine)
		{
			this.schemaExportEngine = schemaExportEngine;
			this.ingredientImporterEngine = ingredientImporterEngine;
			
		}

		public AdminController(ISchemaExportEngine schemaExportEngine, IIngredientImporterEngine ingredientImporterEngine, IPathFinder pathFinder)
			: this(schemaExportEngine, ingredientImporterEngine)
		{
			this.pathFinder = pathFinder;
		}
      
		[HttpPost]
		public ActionResult DBInit(InputDbInitModel model) {
			schemaExportEngine.Export();
			ingredientImporterEngine.Import(PathFinder.GetPath(model.ImportFilePath));

			return View(model);
		}
      
		
		public ActionResult DBInit() {
			return View(new InputDbInitModel { ImportFilePath = "~/App_data/Ingredients.csv" });
		}
	}

	public interface IPathFinder {
		string GetPath(string path);
	}

	public class PathFinder : IPathFinder {
		private readonly HttpServerUtilityBase server;

		public PathFinder(HttpServerUtilityBase server) {
			this.server = server;
		}

		public string GetPath(string path) {
			return server.MapPath(path);
		}
	}
}