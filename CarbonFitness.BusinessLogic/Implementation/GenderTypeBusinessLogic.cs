using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class GenderTypeBusinessLogic : IGenderTypeBusinessLogic {
        private readonly IGenderTypeRepository genderTypeRepository;

        public GenderTypeBusinessLogic(IGenderTypeRepository genderTypeRepository) {
            this.genderTypeRepository = genderTypeRepository;
        }

        public IEnumerable<GenderType> GetGenderTypes() {
            return genderTypeRepository.GetAll();
        }

        public void ExportInitialValues() {
            genderTypeRepository.Save(new GenderType {Name = "Man" });
            genderTypeRepository.Save(new GenderType { Name = "Kvinna" });
        }

        public GenderType GetGenderType(string genderName) {
           return genderTypeRepository.GetByName(genderName);
        }
    }
}