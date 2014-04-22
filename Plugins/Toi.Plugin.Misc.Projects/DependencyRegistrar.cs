using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Toi.Plugin.Misc.Projects.Data;
using Toi.Plugin.Misc.Projects.Domain;
using Toi.Plugin.Misc.Projects.Services;

namespace Toi.Plugin.Misc.Projects
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "toi_object_context_project_management";
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
            builder.RegisterType<ProjectService>().As<IProjectService>();

            //Override the repository injection
            builder.RegisterType<EfRepository<Project>>().As<IRepository<Project>>().WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME)).InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 0; }
        }

        private ProjectsObjectContext RegisterIDbContext(IComponentContext componentContext, DataSettings dataSettings)
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

            return new ProjectsObjectContext(dataConnectionStrings);
        }
    }
}
