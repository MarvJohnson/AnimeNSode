using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AnimeNSodeCore;
using AnimeNSodeCore.Commands;
using AnimeNSodeCore.Listers;
using AnimeNSodeCore.Processors;
using AnimeNSodeCore.Processors.Storage;
using AnimeNSodeCore.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AnimeNSodeUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider ServiceProvider { get; set; }

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<Main>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<RSSMediaCreationWindow>();
            services.AddSingleton<IRSSProcessor, RSSProcessor>();
            services.AddTransient<Collection<IRSSItem>, ThreadSafeObservableCollection<IRSSItem>>();
            services.AddSingleton<ILister<IRSSItem>, RSSFeedLister>();
            services.AddTransient<ISetRSSItemsCommand, Command<string>>(provider =>
            {
                return new Command<string>(async rssFeedLink =>
                {
                    var rssFeedLister = provider.GetRequiredService<ILister<IRSSItem>>();
                    var processor = provider.GetRequiredService<IRSSProcessor>();
                    rssFeedLister.List?.Clear();
                    var feed = await processor.GetFeed(rssFeedLink);
                    processor.CreateRSSItemViewModelsFromFeed<RSSItemViewModel>(feed, rssFeedLister.List);
                });
            });
            services.AddTransient<Collection<IRSSMediaRecord>, ThreadSafeObservableCollection<IRSSMediaRecord>>();
            services.AddSingleton<IListerSelector<IRSSMediaRecord>, RSSMediaLister>();
            services.AddSingleton<IAddRSSMediaCommand, Command<string>>(provider =>
            {
                return new Command<string>(async rssFeedLink =>
                {
                    var rssMediaCreationWindow = provider.GetRequiredService<RSSMediaCreationWindow>();
                    var processor = provider.GetRequiredService<IRSSProcessor>();
                    var feed = await processor.GetFeed(rssFeedLink);
                    var newRSSMedia = await processor.CreateRSSMediaRecordFromFeed(rssFeedLink, feed);
                    var rssMediaLister = provider.GetRequiredService<IListerSelector<IRSSMediaRecord>>();
                    GlobalStorageProcessorSettings.Processor.SaveRSSMedia(newRSSMedia);
                    rssMediaLister.List.Add(newRSSMedia);
                    rssMediaLister.SelectedListItem = newRSSMedia;
                    rssMediaCreationWindow.Close();
                });
            });
            services.AddSingleton<IDeleteRecordCommand, Command<IRSSMediaRecord>>(provider =>
            {
                return new Command<IRSSMediaRecord>(rssMediaRecord =>
                {
                    var rssMediaSelector = provider.GetRequiredService<IListerSelector<IRSSMediaRecord>>();
                    var rssFeedLister = provider.GetRequiredService<ILister<IRSSItem>>();
                    var confirmationResult = MessageBox.Show($"Are you sure you want to delete [{rssMediaRecord.Title}]?", "Anime Deletion Confirmation Window", MessageBoxButton.YesNo);
                    if (confirmationResult == MessageBoxResult.No)
                        return;

                    rssMediaSelector.List.Remove(rssMediaRecord);
                    GlobalStorageProcessorSettings.Processor.DeleteAnime(rssMediaRecord.Title);
                    rssFeedLister.List.Clear();
                });
            });
            services.AddSingleton<IShowRSSMediaCreationWindowCommand, Command<object>>(provider =>
            {
                return new Command<object>(_ =>
                {
                    var main = provider.GetRequiredService<Main>();
                    var window = provider.GetRequiredService<RSSMediaCreationWindow>();
                    if (main.AnimeCreationWindow == null)
                    {
                        window.Closing += (sender, e) =>
                        {
                            e.Cancel = true;
                            window.Visibility = Visibility.Hidden;
                        };

                        main.AnimeCreationWindow = window;
                        main.AnimeCreationWindow.DataContext = main;
                    }
                    main.AnimeCreationWindow.ShowDialog();
                });
            });
            services.AddSingleton<IOpenMediaLinkCommand, Command<string>>(provider =>
            {
                return new Command<string>(link =>
                {
                    var startInfo = new ProcessStartInfo { FileName = link, UseShellExecute = true };
                    Process.Start(startInfo);
                });
            });
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var main = ServiceProvider.GetRequiredService<Main>();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = main;
            mainWindow.Show();
        }
    }
}
