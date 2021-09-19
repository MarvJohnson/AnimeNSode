using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace AnimeNSodeCore
{
    public interface IRSSProcessor
    {
        Task<SyndicationFeed> GetFeed(string rssFeedLink);
        Task<IRSSMediaRecord> CreateRSSMediaRecordFromFeed(string rssFeedLink, SyndicationFeed feed);
        Task CreateRSSItemViewModelsFromFeed<T>(SyndicationFeed feed, Collection<IRSSItem> rssItemViewModels) where T : IRSSItem, new();
    }
}
