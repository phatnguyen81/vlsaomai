using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toi.Plugin.Misc.NewsExs.Models;

namespace Toi.Plugin.Misc.NewsExs.Models
{
    public class NewsExIndexModel
    {
        public NewsExIndexModel()
        {
            PagingFilteringContext = new NewsExsPagingFilteringModel();
        }
        public List<NewsExModel> NewsExs { get; set; }

        public NewsExsPagingFilteringModel PagingFilteringContext { get; set; }
    }
}
