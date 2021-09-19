using AnimeNSodeCore.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using static AnimeNSodeCore.Processors.Storage.GlobalStorageProcessorSettings;

namespace AnimeNSodeCore.Processors.Storage
{
    public class TextCSVStorageProcessor : IStorageProcessor
    {
        private const string TextCSVFile = "AnimeSave.csv";

        public async Task DeleteAnime(string animeName)
        {
            var allSavedAnime = new Collection<IRSSMediaRecord>();
            await LoadAnime(allSavedAnime);

            for (int i = 0; i < allSavedAnime.Count; i++)
            {
                if (allSavedAnime[i].Title == animeName)
                {
                    allSavedAnime.RemoveAt(i);
                    break;
                }
            }

            SaveAnime(allSavedAnime);
        }

        public string GetSaveFile()
        {
            return TextCSVFile;
        }

        public async Task LoadAnime(Collection<IRSSMediaRecord> result)
        {
            var lines = File.Exists(FullSaveFilePath) ? await File.ReadAllLinesAsync(FullSaveFilePath) : new string[0];

            foreach (var line in lines)
            {
                var parameters = line.Split(',');
                var loadedAnime = new MediaViewModel();
                loadedAnime.Title = parameters[0];
                loadedAnime.RSSFeedLink = parameters[1];
                result.Add(loadedAnime);
            }
        }

        public async Task SaveRSSMedia(IRSSMediaRecord model)
        {
            var allSavedAnime = new Collection<IRSSMediaRecord>();
            await LoadAnime (allSavedAnime);
            allSavedAnime.Add(model);
            SaveAnime(allSavedAnime);
        }

        private void SaveAnime(IEnumerable<IRSSMediaRecord> models)
        {
            var lines = ConvertAnimeViewModelsToCSV(models);
            File.WriteAllLinesAsync(FullSaveFilePath, lines);
        }

        private List<string> ConvertAnimeViewModelsToCSV(IEnumerable<IRSSMediaRecord> models)
        {
            var lines = new List<string>();

            foreach (var anime in models)
            {
                lines.Add($"{anime.Title},{anime.RSSFeedLink}");
            }

            return lines;
        }
    }
}
