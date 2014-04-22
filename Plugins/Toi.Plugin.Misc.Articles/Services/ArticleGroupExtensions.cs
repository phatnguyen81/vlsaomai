using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Services.Stores;
using Toi.Plugin.Misc.Articles.Domain;

namespace Toi.Plugin.Misc.Articles.Services
{
    public static class ArticleGroupExtensions
    {
        /// <summary>
        /// Sort categories for tree representation
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="parentId">Parent category identifier</param>
        /// <param name="ignoreCategoriesWithoutExistingParent">A value indicating whether categories without parent category in provided category list (source) should be ignored</param>
        /// <returns>Sorted categories</returns>
        public static IList<ArticleGroup> SortCategoriesForTree(this IList<ArticleGroup> source, int parentId = 0, bool ignoreCategoriesWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<ArticleGroup>();

            foreach (var cat in source.ToList().FindAll(c => c.ParentGroupId == parentId))
            {
                result.Add(cat);
                result.AddRange(SortCategoriesForTree(source, cat.Id, true));
            }
            if (!ignoreCategoriesWithoutExistingParent && result.Count != source.Count)
            {
                //find categories without parent in provided category source and insert them into result
                foreach (var cat in source)
                    if (result.FirstOrDefault(x => x.Id == cat.Id) == null)
                        result.Add(cat);
            }
            return result;
        }
        public static string GetFormattedBreadCrumb(this ArticleGroup category,
            IArticleService articleService,
            string separator = ">>")
        {
            if (category == null)
                throw new ArgumentNullException("category");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedArticleGroupIds = new List<int>() { };

            while (category != null &&  //not null
                !category.Deleted &&  //not deleted
                !alreadyProcessedArticleGroupIds.Contains(category.Id)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", category.Name, separator, result);
                }

                alreadyProcessedArticleGroupIds.Add(category.Id);

                category = articleService.GetArticleGroupById(category.ParentGroupId);

            }
            return result;
        }

        public static IList<ArticleGroup> GetArticleGroupBreadCrumb(this ArticleGroup articleGroup,
            IArticleService articleService,
            bool showHidden = false)
        {
            if (articleGroup == null)
                throw new ArgumentNullException("articleGroup");

            var result = new List<ArticleGroup>();

            //used to prevent circular references
            var alreadyProcessedArticleGroupIds = new List<int>() { };

            while (articleGroup != null && //not null
                !articleGroup.Deleted && //not deleted
                (showHidden || articleGroup.Published) && //published
                !alreadyProcessedArticleGroupIds.Contains(articleGroup.Id)) //prevent circular references
            {
                result.Add(articleGroup);

                alreadyProcessedArticleGroupIds.Add(articleGroup.Id);

                articleGroup = articleService.GetArticleGroupById(articleGroup.ParentGroupId);
            }
            result.Reverse();
            return result;
        }

    }
}
