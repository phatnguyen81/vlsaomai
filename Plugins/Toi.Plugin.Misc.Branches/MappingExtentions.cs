using AutoMapper;
using Toi.Plugin.Misc.Branches.Domain;
using Toi.Plugin.Misc.Branches.Models;

namespace Toi.Plugin.Misc.Branches
{
    public static class MappingExtentions
    {
        #region BranchGroup

        public static BranchGroupModel ToModel(this BranchGroup entity)
        {
            return Mapper.Map<BranchGroup, BranchGroupModel>(entity);
        }

        public static BranchGroup ToEntity(this BranchGroupModel model)
        {
            return Mapper.Map<BranchGroupModel, BranchGroup>(model);
        }

        public static BranchGroup ToEntity(this BranchGroupModel model, BranchGroup destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Branch

        //news items
        public static BranchModel ToModel(this Branch entity)
        {
            return Mapper.Map<Branch, BranchModel>(entity);
        }

        public static Branch ToEntity(this BranchModel model)
        {
            return Mapper.Map<BranchModel, Branch>(model);
        }

        public static Branch ToEntity(this BranchModel model, Branch destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion
    }
}
