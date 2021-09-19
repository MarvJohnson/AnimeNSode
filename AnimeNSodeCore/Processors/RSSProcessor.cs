using AnimeNSodeCore.Properties;
using AnimeNSodeCore.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace AnimeNSodeCore.Processors
{
    public class RSSProcessor : IRSSProcessor
    {
        public CultureInfo Culture { get; set; } = new CultureInfo("en-US", false);

        public async Task<SyndicationFeed> GetFeed(string rssFeedLink)
        {
            return await Task.Run(() =>
            {
                using var reader = XmlReader.Create(rssFeedLink);
                var feed = SyndicationFeed.Load(reader);
                return feed;
            });
        }

        public async Task<IRSSMediaRecord> CreateRSSMediaRecordFromFeed(string rssFeedLink, SyndicationFeed feed)
        {
            return await Task.Run(() =>
            {
                IRSSMediaRecord newAnime = new MediaViewModel();
                newAnime.Title = GetFormattedTitle(feed.Title.Text);
                newAnime.RSSFeedLink = rssFeedLink;
                return newAnime;
            });
        }

        public async Task CreateRSSItemViewModelsFromFeed<T>(SyndicationFeed feed, Collection<IRSSItem> rssItemViewModels) where T : IRSSItem, new()
        {
            await Task.Run(() =>
            {
                foreach (var item in feed.Items)
                {
                    var rssItemVM = new T();
                    // TODO - DRY
                    rssItemVM.Title = GetFormattedTitle(item.Title.Text);
                    var cleanSummary = RemoveUselessSummaryMarkup(item.Summary.Text);
                    var ellipsesSummary = GetFeedItemSummaryWithEllipses(cleanSummary);
                    rssItemVM.Description = ellipsesSummary;
                    rssItemVM.EpisodeLink = item.Id;
                    rssItemViewModels.Add(rssItemVM);
                }
            });
        }

        public string GetFormattedTitle(string title)
        {
            var textInfo = Culture.TextInfo;
            // TODO - Refactor such that " Episodes" is no longer a string literal.
            title = textInfo.ToTitleCase(title.Replace(" Episodes", string.Empty).ToLower());
            return title;
        }

        public string GetFeedItemSummaryWithEllipses(string itemSummary)
        {
            var ellipsesSummary = RemoveUselessSummaryMarkup(itemSummary);
            var maxSummeryLength = Convert.ToInt32(ConfigurationManager.AppSettings.Get(Settings.Default.AnimeSummaryMaxLengthConfigurationKey));
            ellipsesSummary = ellipsesSummary.Length > maxSummeryLength ? $"{ellipsesSummary.Substring(0, maxSummeryLength - 3)}..." : ellipsesSummary;
            return ellipsesSummary;
        }

        public string RemoveUselessSummaryMarkup(string summary)
        {
            // TODO - Refactor such that the pattern for the regex below is not a string literal.
            var resultingSummary = Regex.Replace(summary, @"<img.*br.*/>", string.Empty);
            return resultingSummary;
        }
    }
}
