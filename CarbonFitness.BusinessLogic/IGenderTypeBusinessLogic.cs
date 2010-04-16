using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface IGenderTypeBusinessLogic {
        IEnumerable<GenderType> GetGenderTypes();
        void ExportInitialValues();
        GenderType GetGenderType(string genderName);
    }
}