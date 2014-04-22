using AutoMapper;
using Toi.Plugin.Misc.Services.Domain;
using Toi.Plugin.Misc.Services.Models;

namespace Toi.Plugin.Misc.Services
{
    public static class MappingExtentions
    {
       

        #region Service

        //news items
        public static ServiceModel ToModel(this Service entity)
        {
            return Mapper.Map<Service, ServiceModel>(entity);
        }

        public static Service ToEntity(this ServiceModel model)
        {
            return Mapper.Map<ServiceModel, Service>(model);
        }

        public static Service ToEntity(this ServiceModel model, Service destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion
    }
}
