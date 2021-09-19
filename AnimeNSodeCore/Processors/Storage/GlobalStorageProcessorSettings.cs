using AnimeNSodeCore.Properties;
using AnimeNSodeCore.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;

namespace AnimeNSodeCore.Processors.Storage
{
    public static class GlobalStorageProcessorSettings
    {
        public static string FullSaveFilePath { get; private set; }
        public static IStorageProcessor Processor { get; private set; }

        static GlobalStorageProcessorSettings()
        {
            Processor = GetProcessor(ConfigurationManager.AppSettings.Get(Settings.Default.AnimeSaveModeConfigurationKey));
            FullSaveFilePath = Path.Combine(ConfigurationManager.AppSettings.Get(Settings.Default.AnimeSavePathConfigurationKey), Processor.GetSaveFile());
        }

        private static IStorageProcessor GetProcessor(string saveMode)
        {
            return saveMode switch
            {
                "TextCSV" => new TextCSVStorageProcessor(),
                _ => throw new ArgumentException($"[{saveMode}] is not a valid save mode!"),
            };
        }
    }
}
