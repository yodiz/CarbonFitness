using System.IO;
using System.Text;

namespace CarbonFitness.BusinessLogic.IngredientImporter.Implementation {
	public class IngredientFileReader : IIngredientFileReader {
		public string ReadIngredientFile(string filePath) {
			return File.ReadAllText(filePath, Encoding.UTF7);
		}
	}
}