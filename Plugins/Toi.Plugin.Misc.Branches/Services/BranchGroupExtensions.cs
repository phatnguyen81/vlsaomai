using System;
using System.Collections.Generic;
using System.Linq;
using Toi.Plugin.Misc.Branches.Domain;

namespace Toi.Plugin.Misc.Branches.Services
{
    public static class BranchGroupExtensions
    {
        /// <summary>
        /// Sort categories for tree representation
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="parentId">Parent category identifier</param>
        /// <param name="ignoreCategoriesWithoutExistingParent">A value indicating whether categories without parent category in provided category list (source) should be ignored</param>
        /// <returns>Sorted categories</returns>
        public static IList<BranchGroup> SortCategoriesForTree(this IList<BranchGroup> source, int parentId = 0, bool ignoreCategoriesWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<BranchGroup>();

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
        public static string GetFormattedBreadCrumb(this BranchGroup category,
            IBranchService branchService,
            string separator = ">>")
        {
            if (category == null)
                throw new ArgumentNullException("category");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedBranchGroupIds = new List<int>() { };

            while (category != null &&  //not null
                !category.Deleted &&  //not deleted
                !alreadyProcessedBranchGroupIds.Contains(category.Id)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", category.Name, separator, result);
                }

                alreadyProcessedBranchGroupIds.Add(category.Id);

                category = branchService.GetBranchGroupById(category.ParentGroupId);

            }
            return result;
        }

        public static IList<BranchGroup> GetBranchGroupBreadCrumb(this BranchGroup branchGroup,
            IBranchService branchService,
            bool showHidden = false)
        {
            if (branchGroup == null)
                throw new ArgumentNullException("branchGroup");

            var result = new List<BranchGroup>();

            //used to prevent circular references
            var alreadyProcessedBranchGroupIds = new List<int>() { };

            while (branchGroup != null && //not null
                !branchGroup.Deleted && //not deleted
                !alreadyProcessedBranchGroupIds.Contains(branchGroup.Id)) //prevent circular references
            {
                result.Add(branchGroup);

                alreadyProcessedBranchGroupIds.Add(branchGroup.Id);

                branchGroup = branchService.GetBranchGroupById(branchGroup.ParentGroupId);
            }
            result.Reverse();
            return result;
        }

    }
}
