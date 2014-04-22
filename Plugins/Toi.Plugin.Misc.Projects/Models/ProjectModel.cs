using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.UI.Paging;
using Toi.Plugin.Misc.Projects.Domain;
using Toi.Plugin.Misc.Projects.Validators;

namespace Toi.Plugin.Misc.Projects.Models
{
    public partial class ProjectListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.Projects.Project.List.SearchProjectName")]
        [AllowHtml]
        public string SearchProjectName { get; set; }

        public IList<ProjectModel> Projects { get; set; }
    }

    [Validator(typeof(ProjectValidator))]
    public class ProjectModel : BaseNopEntityModel, ILocalizedModel<ProjectLocalizedModel>
    {
        public ProjectModel()
        {
            Locales = new List<ProjectLocalizedModel>();
        }


        [NopResourceDisplayName("Toi.Plugin.Misc.Projects.Project.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }


        [NopResourceDisplayName("Toi.Plugin.Misc.Projects.Project.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

      
        [NopResourceDisplayName("Toi.Plugin.Misc.Projects.Project.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.Projects.Project.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Projects.Project.Fields.Picture")]
        public int DisplayOrder { get; set; }

        public PictureModel PictureModel { get; set; }
        public IList<ProjectLocalizedModel> Locales { get; set; }
    }
    public partial class ProjectLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Projects.Project.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }
        [NopResourceDisplayName("Toi.Plugin.Misc.Projects.Project.Fields.Short")]
        [AllowHtml]
        public string Full { get; set; }

    
    }
}
