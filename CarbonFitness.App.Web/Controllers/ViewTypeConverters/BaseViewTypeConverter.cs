using System.Collections.Generic;
using System.Web.Mvc;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Controllers.ViewTypeConverters {
    public interface IViewTypeConverter {
        IEnumerable<SelectListItem> GetViewTypes(User user);
    }

    public class BaseViewTypeConverter {
        protected IEnumerable<SelectListItem> PopulateViewTypes(IEnumerable<ILookUpTypes> types, ILookUpTypes selectedType) {
            var result = new List<SelectListItem>();
            foreach (var type in types) {
                result.Add(new SelectListItem {
                    Text = type.Name,
                    Value = type.GetId().ToString(),
                    Selected = selectedType.Name == type.Name
                });
            }
            return result;
        }
    }
}