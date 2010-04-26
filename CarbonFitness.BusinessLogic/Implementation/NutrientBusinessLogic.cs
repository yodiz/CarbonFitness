using System;
using System.Collections.Generic;
using System.Web.Caching;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class NutrientBusinessLogic : INutrientBusinessLogic {
        private readonly INutrientRepository nutrientRepository;
        private readonly Dictionary<NutrientEntity, Nutrient> getNutrientCache = new Dictionary<NutrientEntity, Nutrient>();
        private static readonly object cacheLock = Guid.NewGuid();

        public NutrientBusinessLogic(INutrientRepository nutrientRepository) {
            this.nutrientRepository = nutrientRepository;
        }

        public IEnumerable<Nutrient> GetNutrients() {
            return nutrientRepository.GetAll();
        }

        public void ExportInitialValues() {
            foreach (var name in Enum.GetNames(typeof(NutrientEntity))) {
                if(nutrientRepository.GetByName(name) == null) {
                    nutrientRepository.Save(new Nutrient { Name = name });
                }
            }
        } 

        public Nutrient GetNutrient(NutrientEntity nutrientEntity) {
            //var cache = new Cache();

            //cache.Get(nutrientEntity.ToString());
            lock (cacheLock) {
                if (!getNutrientCache.ContainsKey(nutrientEntity)) {
                    var nutrient = nutrientRepository.GetByName(Enum.GetName(typeof(NutrientEntity), nutrientEntity));
                    //cache.Add(nutrientEntity.ToString(), nutrient, null, DateTime.MaxValue, TimeSpan.MaxValue, CacheItemPriority.NotRemovable, null);
                    getNutrientCache[nutrientEntity] = nutrient;
                }
            }

            return getNutrientCache[nutrientEntity];
        }
    }
}