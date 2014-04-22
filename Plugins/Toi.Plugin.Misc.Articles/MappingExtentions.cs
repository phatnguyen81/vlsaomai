using AutoMapper;
using Toi.Plugin.Misc.Articles.Domain;
using Toi.Plugin.Misc.Articles.Models;

namespace Toi.Plugin.Misc.Articles
{
    public static class MappingExtentions
    {
        #region ArticleGroup

        public static ArticleGroupModel ToModel(this ArticleGroup entity)
        {
            return Mapper.Map<ArticleGroup, ArticleGroupModel>(entity);
        }

        public static ArticleGroup ToEntity(this ArticleGroupModel model)
        {
            return Mapper.Map<ArticleGroupModel, ArticleGroup>(model);
        }

        public static ArticleGroup ToEntity(this ArticleGroupModel model, ArticleGroup destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Article

        //news items
        public static ArticleModel ToModel(this Article entity)
        {
            return Mapper.Map<Article, ArticleModel>(entity);
        }

        public static Article ToEntity(this ArticleModel model)
        {
            return Mapper.Map<ArticleModel, Article>(model);
        }

        public static Article ToEntity(this ArticleModel model, Article destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion
        //#region Articles

        ////articles 
        //public static ArticleModel ToModel(this Article entity)
        //{
        //    return Mapper.Map<Article, ArticleModel>(entity);
        //}

        //public static Article ToEntity(this ArticleModel model)
        //{
        //    return Mapper.Map<ArticleModel, Article>(model);
        //}

        //public static Article ToEntity(this ArticleModel model, Article destination)
        //{
        //    return Mapper.Map(model, destination);
        //}

        //#endregion
    }
}
