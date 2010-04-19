using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface IActivityLevelTypeBusinessLogic {
        IEnumerable<ActivityLevelType> GetActivityLevelTypes();
        ActivityLevelType GetActivityLevelType(string activityLevelName);
        void ExportInitialValues();
    }
}