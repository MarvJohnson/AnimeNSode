using AnimeNSodeCore.Commands;
using AnimeNSodeCore.Processors;
using AnimeNSodeCore.Processors.Storage;
using AnimeNSodeCore.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AnimeNSodeCore.Listers
{
    public class RSSMediaLister : IListerSelector<IRSSMediaRecord>
    {
        public Collection<IRSSMediaRecord> List { get; set; }
        private IRSSMediaRecord _selectedListItem;
        public IRSSMediaRecord SelectedListItem
        {
            get => _selectedListItem;
            set
            {
                if (_selectedListItem != value && value != null)
                {
                    _selectedListItem = value;
                    OnNewlySelectedListItemCommand.Execute(_selectedListItem.RSSFeedLink);
                }
            }
        }
        private ISetRSSItemsCommand OnNewlySelectedListItemCommand { get; set; }

        public RSSMediaLister(Collection<IRSSMediaRecord> list, ISetRSSItemsCommand onNewlySelectedListItemCommand)
        {
            OnNewlySelectedListItemCommand = onNewlySelectedListItemCommand;
            List = list;
            GlobalStorageProcessorSettings.Processor.LoadAnime(List);
        }
    }
}
