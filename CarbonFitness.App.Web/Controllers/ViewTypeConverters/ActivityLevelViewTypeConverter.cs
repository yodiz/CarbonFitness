using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Controllers.ViewTypeConverters {
    public interface IActivityLevelViewTypeConverter : IViewTypeConverter { }
    public class ActivityLevelViewTypeConverter : BaseViewTypeConverter, IActivityLevelViewTypeConverter {
        public ActivityLevelViewTypeConverter(IUserProfileBusinessLogic userProfileBusinessLogic, IActivityLevelTypeBusinessLogic activityLevelTypeBusinessLogic) {
            UserProfileBusinessLogic = userProfileBusinessLogic;
            ActivityLevelTypeBusinessLogic = activityLevelTypeBusinessLogic;
        }

        public IUserProfileBusinessLogic UserProfileBusinessLogic { get; private set; }
        public IActivityLevelTypeBusinessLogic ActivityLevelTypeBusinessLogic { get; private set; }
        public IEnumerable<SelectListItem> GetViewTypes(User user) {
            return PopulateViewTypes(ActivityLevelTypeBusinessLogic.GetActivityLevelTypes().ToArray(), UserProfileBusinessLogic.GetActivityLevel(user));
        }
    }
}