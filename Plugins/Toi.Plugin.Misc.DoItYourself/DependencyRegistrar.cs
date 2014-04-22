using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Toi.Plugin.Misc.DoItYourself.Data;
using Toi.Plugin.Misc.DoItYourself.Domain;
using Toi.Plugin.Misc.DoItYourself.Services;

namespace Toi.Plugin.Misc.DoItYourself
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "toi_object_context_diy_management";
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //Load custom data settings
            var dataSettingsManager = new DataSettingsManager();
            DataSettings dataSettings = dataSettingsManager.LoadSettings();

            //Register custom object context
            builder.Register<IDbContext>(c => RegisterIDbContext(c, dataSettings)).Named<IDbContext>(CONTEXT_NAME).InstancePerHttpRequest();
            builder.Register(c => RegisterIDbContext(c, dataSettings)).InstancePerHttpRequest();

            //Register services
            //builder.RegisterType<ArticleGroupService>().As<IArticleGroupService>();
            builder.RegisterType<DiyService>().As<IDiyService>();

            //Override the repository injection
            builder.RegisterType<EfRepository<DiyGroup>>().As<IRepository<DiyGroup>>().WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME)).InstancePerHttpRequest();
            builder.RegisterType<EfRepository<DiyProject>>().As<IRepository<DiyProject>>().WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME)).InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 0; }
        }

        private DoItYourselfObjectContext RegisterIDbContext(IComponentContext componentContext, DataSettings dataSettings)
        {
            string dataConnectionStrings;

            if (dataSettings != null && dataSettings.IsValid())
            {
                dataConnectionStrings = dataSettings.DataConnectionString;
            }
            else
            {
                dataConnectionStrings = componentContext.Resolve<DataSettings>().DataConnectionString;
            }

            return new DoItYourselfObjectContext(dataConnectionStrings);
        }
    }
}
