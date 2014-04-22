using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Toi.Plugin.Misc.NewsExs.Data;
using Toi.Plugin.Misc.NewsExs.Domain;
using Toi.Plugin.Misc.NewsExs.Services;

namespace Toi.Plugin.Misc.NewsExs
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "toi_object_context_newsEx_management";
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
            builder.RegisterType<NewsExService>().As<INewsExService>();

            //Override the repository injection
            builder.RegisterType<EfRepository<NewsEx>>().As<IRepository<NewsEx>>().WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME)).InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 0; }
        }

        private NewsExsObjectContext RegisterIDbContext(IComponentContext componentContext, DataSettings dataSettings)
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

            return new NewsExsObjectContext(dataConnectionStrings);
        }
    }
}
