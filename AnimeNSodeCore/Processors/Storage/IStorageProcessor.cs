using AnimeNSodeCore.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AnimeNSodeCore.Processors.Storage
{
    public interface IStorageProcessor
    {
        Task LoadAnime(Collection<IRSSMediaRecord> allAnime);
        Task SaveRSSMedia(IRSSMediaRecord model);
        Task DeleteAnime(string animeName);
        string GetSaveFile();
    }
}
