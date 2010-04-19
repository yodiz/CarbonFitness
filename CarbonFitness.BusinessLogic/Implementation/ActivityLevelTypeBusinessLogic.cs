using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class ActivityLevelTypeBusinessLogic : IActivityLevelTypeBusinessLogic{
        private readonly IActivityLevelTypeRepository activityLevelTypeRepository;

        public ActivityLevelTypeBusinessLogic(IActivityLevelTypeRepository activityLevelTypeRepository) {
            this.activityLevelTypeRepository = activityLevelTypeRepository;
        }

        public IEnumerable<ActivityLevelType> GetActivityLevelTypes() {
            return activityLevelTypeRepository.GetAll();
        }

        public ActivityLevelType GetActivityLevelType(string activityLevelName) {
            return activityLevelTypeRepository.GetByName(activityLevelName);
        }

        public void ExportInitialValues() {
            activityLevelTypeRepository.Save(new ActivityLevelType { Name = "Mycket l�g", EnergyFactor = 1.2M});
            activityLevelTypeRepository.Save(new ActivityLevelType { Name = "L�g", EnergyFactor = 1.375M });
            activityLevelTypeRepository.Save(new ActivityLevelType { Name = "Medel", EnergyFactor = 1.55M });
            activityLevelTypeRepository.Save(new ActivityLevelType { Name = "H�g", EnergyFactor = 1.725M });
            activityLevelTypeRepository.Save(new ActivityLevelType { Name = "Mycket h�g", EnergyFactor = 1.9M });
        }
    }
}