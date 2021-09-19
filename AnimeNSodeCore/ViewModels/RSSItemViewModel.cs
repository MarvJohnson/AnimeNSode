using AnimeNSodeCore.Models;

namespace AnimeNSodeCore.ViewModels
{
    public class RSSItemViewModel : ViewModel, IRSSItem
    {
        public string Title
        {
            get => Model.Title;
            set
            {
                if (Model.Title != value)
                {
                    Model.Title = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => Model.Description;
            set
            {
                if (Model.Description != value)
                {
                    Model.Description = value;
                    OnPropertyChanged();
                }
            }
        }
        public string EpisodeLink
        {
            get => Model.EpisodeLink;
            set
            {
                if (Model.EpisodeLink != value)
                {
                    Model.EpisodeLink = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool PremiumOnly
        {
            get => Model.PremiumOnly;
            set
            {
                if (Model.PremiumOnly != value)
                {
                    Model.PremiumOnly = value;
                    OnPropertyChanged();
                }
            }
        }
        private RSSItemModel Model { get; set; }

        public RSSItemViewModel()
        {
            Model = new RSSItemModel();
        }
    }
}
