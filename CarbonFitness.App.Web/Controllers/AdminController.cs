using System.Web;
using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.AppLogic;

namespace CarbonFitness.App.Web.Controllers {
    public class AdminController : Controller {
        private readonly IIngredientImporterEngine ingredientImporterEngine;
        private readonly IInitialDataValuesExportEngine initialDataValuesExportEngine;

        private readonly ISchemaExportEngine schemaExportEngine;
        private readonly INutrientRecommendationImporter nutrientRecommendationImporter;
        private IPathFinder pathFinder;

        public AdminController(ISchemaExportEngine schemaExportEngine, INutrientRecommendationImporter nutrientRecommendationImporter, IIngredientImporterEngine ingredientImporterEngine, IInitialDataValuesExportEngine initialDataValuesExportEngine) {
            this.schemaExportEngine = schemaExportEngine;
            this.nutrientRecommendationImporter = nutrientRecommendationImporter;
            this.ingredientImporterEngine = ingredientImporterEngine;
            this.initialDataValuesExportEngine = initialDataValuesExportEngine;
        }

        public AdminController(ISchemaExportEngine schemaExportEngine, INutrientRecommendationImporter nutrientRecommendationImporter, IIngredientImporterEngine ingredientImporterEngine, IInitialDataValuesExportEngine initialDataValuesExportEngine, IPathFinder pathFinder)
            : this(schemaExportEngine, nutrientRecommendationImporter, ingredientImporterEngine, initialDataValuesExportEngine) {
            this.pathFinder = pathFinder;
        }


        private IPathFinder PathFinder {
            get {
                if (pathFinder == null) {
                    pathFinder = new PathFinder(Server);
                }
                return pathFinder;
            }
        }

        [HttpPost]
        public ActionResult DBInit(InputDbInitModel model) {
            schemaExportEngine.Export();

            initialDataValuesExportEngine.Export();

            ingredientImporterEngine.Import(PathFinder.GetPath(model.ImportFilePath));

            nutrientRecommendationImporter.Import();

            return View(model);
        }


        public ActionResult DBInit() {
            return View(new InputDbInitModel {ImportFilePath = "~/App_data/Ingredients.csv"});
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