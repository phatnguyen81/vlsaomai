using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toi.Plugin.Misc.Services.Models;

namespace Toi.Plugin.Misc.Services.Models
{
    public class ServiceIndexModel
    {
        public ServiceIndexModel()
        {
            PagingFilteringContext = new ServicesPagingFilteringModel();
        }
        public List<ServiceModel> Services { get; set; }

        public ServicesPagingFilteringModel PagingFilteringContext { get; set; }
    }
}
