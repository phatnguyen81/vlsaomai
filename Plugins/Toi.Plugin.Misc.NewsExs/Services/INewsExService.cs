using Nop.Core;
using Toi.Plugin.Misc.NewsExs.Domain;

namespace Toi.Plugin.Misc.NewsExs.Services
{
    public interface INewsExService
    {
      
        #region NewsEx

        IPagedList<NewsEx> GetAllNewsExs(string title = "", 
            int pageIndex = 0, int pageSize = int.MaxValue);
        void InsertNewsEx(NewsEx newsEx);
        void UpdateNewsEx(NewsEx newsEx);
        NewsEx GetNewsExById(int newsExId);

        void DeleteService(NewsEx newsEx);
        #endregion
    }
}
