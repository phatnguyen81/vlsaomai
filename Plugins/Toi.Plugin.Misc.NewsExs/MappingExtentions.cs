using AutoMapper;
using Toi.Plugin.Misc.NewsExs.Domain;
using Toi.Plugin.Misc.NewsExs.Models;

namespace Toi.Plugin.Misc.NewsExs
{
    public static class MappingExtentions
    {
       

        #region NewsEx

        //news items
        public static NewsExModel ToModel(this NewsEx entity)
        {
            return Mapper.Map<NewsEx, NewsExModel>(entity);
        }

        public static NewsEx ToEntity(this NewsExModel model)
        {
            return Mapper.Map<NewsExModel, NewsEx>(model);
        }

        public static NewsEx ToEntity(this NewsExModel model, NewsEx destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion
    }
}
