using System.Collections;
using System.Collections.Generic;
using Nop.Core;
using Toi.Plugin.Misc.Articles.Domain;

namespace Toi.Plugin.Misc.Articles.Services
{
    public interface IArticleService
    {
        ///// <summary>
        ///// Deletes a article
        ///// </summary>
        ///// <param name="article">Article</param>
        //void DeleteArticle(Article article);

        ///// <summary>
        ///// Gets a article
        ///// </summary>
        ///// <param name="articleId">The article identifier</param>
        ///// <returns>Article</returns>
        //Article GetArticleById(int articleId);

        //IPagedList<Article> SearchArticles(string keywords = null, int pageIndex = 0,
        //    int pageSize = Int32.MaxValue, //Int32.MaxValue
        //    IList<int> channelIds = null,
        //    int languageId = 0,
        //    bool showHidden = false);

        ///// <summary>
        ///// Gets all article
        ///// </summary>
        ///// <param name="languageId">Language identifier; 0 if you want to get all records</param>
        ///// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        ///// <param name="pageIndex">Page index</param>
        ///// <param name="pageSize">Page size</param>
        ///// <param name="showHidden">A value indicating whether to show hidden records</param>
        ///// <returns>Articles</returns>
        //IPagedList<Article> GetAllArticles(int languageId, int pageIndex, int pageSize, bool showHidden = false);

        ///// <summary>
        ///// Inserts a article item
        ///// </summary>
        ///// <param name="article">Article</param>
        //void InsertArticle(Article article);

        ///// <summary>
        ///// Updates the article item
        ///// </summary>
        ///// <param name="article">Article</param>
        //void UpdateArticle(Article article);

        #region Article Group
        ArticleGroup GetArticleGroupById(int groupId);
        IPagedList<ArticleGroup> GetAllArticleGroups(string groupName = "",
           int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        void InsertArticleGroup(ArticleGroup articleGroup);
        void UpdateArticleGroup(ArticleGroup articleGroup);
        void DeleteArticleGroup(ArticleGroup articleGroup);
        IList<ArticleGroup> GetAllGroupsByParentGroupId(int parentGroupId, bool showHidden = false);
        #endregion

        #region Article

        IPagedList<Article> GetAllArticles(string title,
            int pageIndex, int pageSize, bool showHidden = false);
        void InsertArticle(Article article);
        void UpdateArticle(Article article);
        Article GetArticleById(int articleId);
        #endregion

        #region ArticleToGroup
        IPagedList<ArticleToGroup> GetArticleToGroupsByArticleId(int articleId = 0, int pageIndex = 0, int pageSize = 2147483646, bool showHidden = false);
        void InsertArticleToGroup(ArticleToGroup articleToGroup);
        #endregion

        ArticleToGroup GetArticleToGroupById(int id);
        void DeleteArticleToGroup(ArticleToGroup articleToGroupById);
        void UpdateArticleToGroup(ArticleToGroup articleToGroupById);
    }
}
