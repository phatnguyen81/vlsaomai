using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Stores;
using Nop.Services.Events;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Articles.Domain;

namespace Toi.Plugin.Misc.Articles.Services
{
    public class ArticleService : IArticleService
    {
        #region Constants
        private const string GROUPS_BY_ID_KEY = "Toi.group.id-{0}";
        private const string GROUPS_PATTERN_KEY = "Toi.group.";
        private const string ARTICLEGROUPS_PATTERN_KEY = "Toi.articlegroup.";
        private const string GROUPS_BY_PARENT_GROUP_ID_KEY = "Toi.group.byparent-{0}-{1}-{2}-{3}";
        private const string ARTICLETOGROUP_ALLBYARTICLEID_KEY = "Toi.articletogroup.allbyarticleid-{0}-{1}-{2}-{3}";
        private const string ARTICLETOGROUP_PATTERN_KEY = "Toi.articletogroup.";
        #endregion

        #region Fields

        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<ArticleGroup> _articleGroupRepository;
        private readonly IRepository<ArticleToGroup> _articleToGroupRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public ArticleService(IRepository<StoreMapping> storeMappingRepository,
            IEventPublisher eventPublisher, 
            ILanguageService languageService, 
            IStoreContext storeContext, 
            IRepository<ArticleGroup> articleGroupRepository,
            ICacheManager cacheManager,
            IWorkContext workContext, IRepository<Article> articleRepository, IRepository<ArticleToGroup> articleToGroupRepository)
        {
            this._storeMappingRepository = storeMappingRepository;
            this._eventPublisher = eventPublisher;
            _storeContext = storeContext;
            _articleGroupRepository = articleGroupRepository;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _articleRepository = articleRepository;
            _articleToGroupRepository = articleToGroupRepository;
        }

        #endregion

        //#region Methods

        ///// <summary>
        ///// Deletes a news
        ///// </summary>
        ///// <param name="article">Article item</param>
        //public virtual void DeleteArticle(Article article)
        //{
        //    if (article == null)
        //        throw new ArgumentNullException("article");

        //    _articleRepository.Delete(article);
            
        //    //event notification
        //    _eventPublisher.EntityDeleted(article);
        //}

        ///// <summary>
        ///// Gets a news
        ///// </summary>
        ///// <param name="articleId">The news identifier</param>
        ///// <returns>Article</returns>
        //public virtual Article GetArticleById(int articleId)
        //{
        //    if (articleId == 0)
        //        return null;

        //    return _articleRepository.GetById(articleId);
        //}

        //public IPagedList<Article> SearchArticles(
        //    string keywords = null,
        //    int pageIndex = 0, 
        //    int pageSize = Int32.MaxValue, 
        //    IList<int> channelIds = null, 
        //    int languageId = 0,
        //    bool showHidden = false)
        //{
            
        //    //validate "channelIds" parameter
        //    if (channelIds != null && channelIds.Contains(0))
        //        channelIds.Remove(0);

        //    var query = _articleRepository.Table;

        //    if (languageId > 0)
        //        query = query.Where(n => languageId == n.LanguageId);
        //    if (!showHidden)
        //    {
        //        var utcNow = DateTime.UtcNow;
        //        query = query.Where(n => n.Published);
        //        query = query.Where(n => !n.StartDateUtc.HasValue || n.StartDateUtc <= utcNow);
        //        query = query.Where(n => !n.EndDateUtc.HasValue || n.EndDateUtc >= utcNow);
        //    }

        //    if (!String.IsNullOrWhiteSpace(keywords))
        //    {
        //        query =
        //            query.Where(
        //                q => q.Title.Contains(keywords) || q.Short.Contains(keywords) || q.Full.Contains(keywords));
        //    }

        //    if (channelIds != null && channelIds.Count > 0)
        //    {
        //        //search in subarticleGroups
        //        query = from p in query
        //                from pc in p.ArticleChannels.Where(pc => channelIds.Contains(pc.ChannelId))
        //                select p;
        //    }
        //    query = query.OrderByDescending(n => n.CreatedOnUtc);

            

        //    var news = new PagedList<Article>(query, pageIndex, pageSize);
        //    return news;
        //}

        ///// <summary>
        ///// Gets all news
        ///// </summary>
        ///// <param name="languageId">Language identifier; 0 if you want to get all records</param>
        ///// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        ///// <param name="pageIndex">Page index</param>
        ///// <param name="pageSize">Page size</param>
        ///// <param name="showHidden">A value indicating whether to show hidden records</param>
        ///// <returns>Article items</returns>
        //public virtual IPagedList<Article> GetAllArticles(int languageId, 
        //    int pageIndex, int pageSize, bool showHidden = false)
        //{
        //    var query = _articleRepository.Table;
        //    if (languageId > 0)
        //        query = query.Where(n => languageId == n.LanguageId);
        //    if (!showHidden)
        //    {
        //        var utcNow = DateTime.UtcNow;
        //        query = query.Where(n => n.Published);
        //        query = query.Where(n => !n.StartDateUtc.HasValue || n.StartDateUtc <= utcNow);
        //        query = query.Where(n => !n.EndDateUtc.HasValue || n.EndDateUtc >= utcNow);
        //    }
        //    query = query.OrderByDescending(n => n.CreatedOnUtc);

          

        //    var news = new PagedList<Article>(query, pageIndex, pageSize);
        //    return news;
        //}

        ///// <summary>
        ///// Inserts a news item
        ///// </summary>
        ///// <param name="news">Article item</param>
        //public virtual void InsertArticle(Article news)
        //{
        //    if (news == null)
        //        throw new ArgumentNullException("news");

        //    _articleRepository.Insert(news);

        //    //event notification
        //    _eventPublisher.EntityInserted(news);
        //}

        ///// <summary>
        ///// Updates the news item
        ///// </summary>
        ///// <param name="news">Article item</param>
        //public virtual void UpdateArticle(Article news)
        //{
        //    if (news == null)
        //        throw new ArgumentNullException("news");

        //    _articleRepository.Update(news);
            
        //    //event notification
        //    _eventPublisher.EntityUpdated(news);
        //}


        //#endregion

        #region Article
        public virtual IPagedList<Article> GetAllArticles(string title, 
            int pageIndex, int pageSize, bool showHidden = false)
        {
            var query = _articleRepository.Table;
            if (!showHidden)
            {
                var utcNow = DateTime.UtcNow;
                query = query.Where(n => n.Published);
                query = query.Where(n => !n.StartDateUtc.HasValue || n.StartDateUtc <= utcNow);
                query = query.Where(n => !n.EndDateUtc.HasValue || n.EndDateUtc >= utcNow);
            }
            query = query.OrderByDescending(n => n.Id);


            var news = new PagedList<Article>(query, pageIndex, pageSize);
            return news;
        }

        public void InsertArticle(Article article)
        {
            _articleRepository.Insert(article);
        }

        public void UpdateArticle(Article article)
        {
            _articleRepository.Update(article);
        }

        public Article GetArticleById(int articleId)
        {
            return _articleRepository.GetById(articleId);
        }

        public IPagedList<ArticleToGroup> GetArticleToGroupsByArticleId(int articleId = 0, int pageIndex = 0, int pageSize = 2147483646,
            bool showHidden = false)
        {
            if (articleId == 0)
                return new PagedList<ArticleToGroup>(new List<ArticleToGroup>(), pageIndex, pageSize);

            string key = string.Format(ARTICLETOGROUP_ALLBYARTICLEID_KEY, showHidden, articleId, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = from pc in _articleToGroupRepository.Table
                            join c in _articleGroupRepository.Table on pc.ArticleGroupId equals c.Id
                            where pc.ArticleId == articleId &&
                                  !c.Deleted &&
                                  (showHidden || c.Published)
                            orderby pc.DisplayOrder
                            select pc;
                return new PagedList<ArticleToGroup>(query, pageIndex, pageSize);
            });
        }

        

        

        #endregion

        #region Article Group
        public ArticleGroup GetArticleGroupById(int groupId)
        {
            if (groupId == 0)
                return null;

            string key = string.Format(GROUPS_BY_ID_KEY, groupId);
            return _cacheManager.Get(key, () => _articleGroupRepository.GetById(groupId));
        }

        public IPagedList<ArticleGroup> GetAllArticleGroups(string groupName = "", int pageIndex = 0, int pageSize = Int32.MaxValue,
            bool showHidden = false)
        {
            var query = _articleGroupRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!String.IsNullOrWhiteSpace(groupName))
                query = query.Where(c => c.Name.Contains(groupName));
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.ParentGroupId).ThenBy(c => c.DisplayOrder);

            if (!showHidden)
            {


                //Store mapping
                var currentStoreId = _storeContext.CurrentStore.Id;
                //Store mapping
                var storeQry = from sm in _storeMappingRepository.Table
                               where (sm.EntityName == "ArticleGroup" && currentStoreId == sm.StoreId)
                               select sm.EntityId;
                var storeMap = storeQry.ToList();
                //query = from c in query
                //        join sm in _storeMappingRepository.Table
                //        on new { c1 = c.Id, c2 = "ArticleGroup" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                //        from sm in c_sm.DefaultIfEmpty()
                //        where !c.LimitedToStores || currentStoreId == sm.StoreId
                //        select c;
                query = from g in query
                        where !g.LimitedToStores || storeMap.Contains(g.Id)
                        select g;

                //only distinct articleGroups (group by ID)
                query = from c in query
                        group c by c.Id
                            into cGroup
                            orderby cGroup.Key
                            select cGroup.FirstOrDefault();
                query = query.OrderBy(c => c.ParentGroupId).ThenBy(c => c.DisplayOrder);
            }

            var unsortedCategories = query.ToList();

            //sort articleGroups
            var sortedCategories = unsortedCategories.SortCategoriesForTree();

            //paging
            return new PagedList<ArticleGroup>(sortedCategories, pageIndex, pageSize);
        }

        public void InsertArticleGroup(ArticleGroup articleGroup)
        {
            if (articleGroup == null)
                throw new ArgumentNullException("articleGroup");

            _articleGroupRepository.Insert(articleGroup);

            //cache
            _cacheManager.RemoveByPattern(GROUPS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(ARTICLEGROUPS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(articleGroup);
        }

        public void UpdateArticleGroup(ArticleGroup articleGroup)
        {
            if (articleGroup == null)
                throw new ArgumentNullException("articleGroup");

            //validate articleGroup hierarchy
            var parentArticleGroup = GetArticleGroupById(articleGroup.ParentGroupId);
            while (parentArticleGroup != null)
            {
                if (articleGroup.Id == parentArticleGroup.Id)
                {
                    articleGroup.ParentGroupId = 0;
                    break;
                }
                parentArticleGroup = GetArticleGroupById(parentArticleGroup.ParentGroupId);
            }

            _articleGroupRepository.Update(articleGroup);

            //cache
            _cacheManager.RemoveByPattern(GROUPS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(ARTICLEGROUPS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(articleGroup);
        }

        public void DeleteArticleGroup(ArticleGroup articleGroup)
        {
            if (articleGroup == null)
                throw new ArgumentNullException("articleGroup");

            articleGroup.Deleted = true;
            UpdateArticleGroup(articleGroup);

            //set a ParentArticleGroup property of the children to 0
            var subarticleGroups = GetAllGroupsByParentGroupId(articleGroup.Id);
            foreach (var subarticleGroup in subarticleGroups)
            {
                subarticleGroup.ParentGroupId = 0;
                UpdateArticleGroup(subarticleGroup);
            }
        }

        public IList<ArticleGroup> GetAllGroupsByParentGroupId(int parentGroupId, bool showHidden = false)
        {
            string key = string.Format(GROUPS_BY_PARENT_GROUP_ID_KEY, parentGroupId, showHidden,
                _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = _articleGroupRepository.Table;
                if (!showHidden)
                    query = query.Where(c => c.Published);
                query = query.Where(c => c.ParentGroupId == parentGroupId);
                query = query.Where(c => !c.Deleted);
                query = query.OrderBy(c => c.DisplayOrder);

                if (!showHidden)
                {
                   

                    //Store mapping
                    var currentStoreId = _storeContext.CurrentStore.Id;
                    //Store mapping
                    var storeQry = from sm in _storeMappingRepository.Table
                                   where (sm.EntityName == "ArticleGroup" && currentStoreId == sm.StoreId)
                                   select sm.EntityId;
                    var storeMap = storeQry.ToList();
                    //query = from c in query
                    //        join sm in _storeMappingRepository.Table
                    //        on new { c1 = c.Id, c2 = "ArticleGroup" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                    //        from sm in c_sm.DefaultIfEmpty()
                    //        where !c.LimitedToStores || currentStoreId == sm.StoreId
                    //        select c;
                    query = from g in query
                            where !g.LimitedToStores || storeMap.Contains(g.Id)
                            select g;

                    //only distinct articleGroups (group by ID)
                    query = from c in query
                            group c by c.Id
                                into cGroup
                                orderby cGroup.Key
                                select cGroup.FirstOrDefault();
                    query = query.OrderBy(c => c.DisplayOrder);
                }

                var articleGroups = query.ToList();
                return articleGroups;
            });
        }

        #endregion

        #region Article To Group
        public ArticleToGroup GetArticleToGroupById(int id)
        {
            return _articleToGroupRepository.GetById(id);

        }

        public void DeleteArticleToGroup(ArticleToGroup articleToGroup)
        {
            _articleToGroupRepository.Delete(articleToGroup);
            _cacheManager.RemoveByPattern(GROUPS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(ARTICLETOGROUP_PATTERN_KEY);
        }

        public void UpdateArticleToGroup(ArticleToGroup articleToGroup)
        {
            _articleToGroupRepository.Update(articleToGroup);
            _cacheManager.RemoveByPattern(GROUPS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(ARTICLETOGROUP_PATTERN_KEY);
        }
        public void InsertArticleToGroup(ArticleToGroup articleToGroup)
        {
            _articleToGroupRepository.Insert(articleToGroup);
            _cacheManager.RemoveByPattern(GROUPS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(ARTICLETOGROUP_PATTERN_KEY);
        }
        #endregion
    }
}
