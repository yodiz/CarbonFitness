using Autofac;
using Autofac.Builder;
using CarbonFitness;
using CarbonFitness.BusinessLogic;

namespace CarbonFitnessWeb {
    public class ComponentBuilder {
        private static IContainer _current;

        public static IContainer Current {
            get {
                if (_current == null) {
                    var builder = new ContainerBuilder();
                    builder.Register<MembershipBusinessLogic>().As<IMembershipBusinessLogic>();
                    builder.Register<UserBusinessLogic>().As<IUserBusinessLogic>();

                    var bootstrapper = new Bootstrapper();
                    bootstrapper.InitAutofac(builder);


                    _current = builder.Build();
                }

                return _current;
            }
        }
    }
}