using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toi.Plugin.Misc.Projects.Models
{
    public class ProjectIndexModel
    {
        public ProjectIndexModel()
        {
            PagingFilteringContext = new ProjectsPagingFilteringModel();
        }
        public List<ProjectModel> Projects { get; set; }

        public ProjectsPagingFilteringModel PagingFilteringContext { get; set; }
    }
}
