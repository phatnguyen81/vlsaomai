using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toi.Plugin.Misc.Articles.Domain;

namespace Toi.Plugin.Misc.Articles.Services
{
    public static class ArticlesExtensions
    {
        public static ArticleToGroup FindArticleToGroup(this IList<ArticleToGroup> source, int groupId, int articleId)
        {
            ArticleToGroup articleToGroup;
            using (IEnumerator<ArticleToGroup> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ArticleToGroup current = enumerator.Current;
                    if (current.ArticleGroupId != groupId || current.ArticleId != articleId)
                    {
                        continue;
                    }
                    articleToGroup = current;
                    return articleToGroup;
                }
                return null;
            }
            return articleToGroup;
        }
    }
}
