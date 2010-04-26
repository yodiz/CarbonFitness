using System.Reflection;
using System.Resources;

namespace CarbonFitness.Translation {
    public class BaseTranslationHelper {
        private readonly ResourceManager resourceManager;
        public BaseTranslationHelper(string baseName, Assembly assembly) {
            resourceManager = new ResourceManager(baseName ,assembly);
        }

        public string GetString(string name) { 
            return resourceManager.GetString(name);
        }
    }
}