using AnimeNSodeCore.Models;

namespace AnimeNSodeCore.ViewModels
{
    public class MediaViewModel : ViewModel, IRSSMediaRecord
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
        public string RSSFeedLink
        {
            get => Model.RSSFeedLink;
            set
            {
                if (Model.RSSFeedLink != value)
                {
                    Model.RSSFeedLink = value;
                    OnPropertyChanged();
                }
            }
        }
        private MediaModel Model { get; set; }

        public MediaViewModel()
        {
            Model = new MediaModel();
        }
    }
}
