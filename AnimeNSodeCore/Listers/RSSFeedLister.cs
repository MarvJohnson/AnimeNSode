using AnimeNSodeCore.Commands;
using AnimeNSodeCore.Processors;
using AnimeNSodeCore.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace AnimeNSodeCore.Listers
{
    public class RSSFeedLister : ILister<IRSSItem>
    {
        public Collection<IRSSItem> List { get; set; }

        public RSSFeedLister(Collection<IRSSItem> list)
        {
            List = list;
        }
    }
}
