using AutoMapper;
using Toi.Plugin.Misc.DoItYourself.Domain;
using Toi.Plugin.Misc.DoItYourself.Models;

namespace Toi.Plugin.Misc.DoItYourself
{
    public static class MappingExtentions
    {
        #region DiyGroup

        public static DiyGroupModel ToModel(this DiyGroup entity)
        {
            return Mapper.Map<DiyGroup, DiyGroupModel>(entity);
        }

        public static DiyGroup ToEntity(this DiyGroupModel model)
        {
            return Mapper.Map<DiyGroupModel, DiyGroup>(model);
        }

        public static DiyGroup ToEntity(this DiyGroupModel model, DiyGroup destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Project

        //news items
        public static DiyProjectModel ToModel(this DiyProject entity)
        {
            return Mapper.Map<DiyProject, DiyProjectModel>(entity);
        }

        public static DiyProject ToEntity(this DiyProjectModel model)
        {
            return Mapper.Map<DiyProjectModel, DiyProject>(model);
        }

        public static DiyProject ToEntity(this DiyProjectModel model, DiyProject destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion
    }
}
