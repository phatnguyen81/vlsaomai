using System;
using System.Collections.Generic;
using System.Linq;
using Toi.Plugin.Misc.DoItYourself.Domain;

namespace Toi.Plugin.Misc.DoItYourself.Services
{
    public static class DiyGroupExtensions
    {
        /// <summary>
        /// Sort categories for tree representation
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="parentId">Parent category identifier</param>
        /// <param name="ignoreCategoriesWithoutExistingParent">A value indicating whether categories without parent category in provided category list (source) should be ignored</param>
        /// <returns>Sorted categories</returns>
        public static IList<DiyGroup> SortCategoriesForTree(this IList<DiyGroup> source, int parentId = 0, bool ignoreCategoriesWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<DiyGroup>();

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
        public static string GetFormattedBreadCrumb(this DiyGroup group,
            IDiyService diyService,
            string separator = ">>")
        {
            if (group == null)
                throw new ArgumentNullException("group");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedDiyGroupIds = new List<int>() { };

            while (group != null &&  //not null
                !group.Deleted &&  //not deleted
                !alreadyProcessedDiyGroupIds.Contains(group.Id)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = group.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", group.Name, separator, result);
                }

                alreadyProcessedDiyGroupIds.Add(group.Id);

                group = diyService.GetDiyGroupById(group.ParentGroupId);

            }
            return result;
        }

        public static IList<DiyGroup> GetDiyGroupBreadCrumb(this DiyGroup diyGroup,
            IDiyService diyService,
            bool showHidden = false)
        {
            if (diyGroup == null)
                throw new ArgumentNullException("diyGroup");

            var result = new List<DiyGroup>();

            //used to prevent circular references
            var alreadyProcessedDiyGroupIds = new List<int>() { };

            while (diyGroup != null && //not null
                !diyGroup.Deleted && //not deleted
                !alreadyProcessedDiyGroupIds.Contains(diyGroup.Id)) //prevent circular references
            {
                result.Add(diyGroup);

                alreadyProcessedDiyGroupIds.Add(diyGroup.Id);

                diyGroup = diyService.GetDiyGroupById(diyGroup.ParentGroupId);
            }
            result.Reverse();
            return result;
        }

    }
}
