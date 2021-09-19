using AnimeNSodeCore.Commands;
using AnimeNSodeCore.Listers;
using AnimeNSodeCore.Processors;
using AnimeNSodeCore.Processors.Storage;
using AnimeNSodeCore.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AnimeNSodeCore
{
    public class Main
    {
        public IRSSProcessor RSSProcessor { get; }
        public ILister<IRSSItem> RSSFeedLister { get; }
        public ICommand SetRSSItemsCommand { get; }
        public IListerSelector<IRSSMediaRecord> RSSMediaLister { get; }
        public ICommand AddRSSMediaCommand { get; }
        public ICommand DeleteRecordCommand { get; }
        public ICommand ShowRSSMediaCreationWindowCommand { get; }
        public ICommand OpenMediaLinkCommand { get; }
        public Window AnimeCreationWindow { get; set; }

        public Main(IRSSProcessor rssProcessor,
                    ILister<IRSSItem> rssFeedLister,
                    ISetRSSItemsCommand setRssItemsCommand,
                    IListerSelector<IRSSMediaRecord> rssMediaLister,
                    IAddRSSMediaCommand addRSSMediaCommand,
                    IDeleteRecordCommand deleteRecordCommand,
                    IShowRSSMediaCreationWindowCommand showRSSMediaCreationWindowCommand,
                    IOpenMediaLinkCommand openMediaLinkCommand)
        {
            RSSProcessor = rssProcessor;
            RSSFeedLister = rssFeedLister;
            SetRSSItemsCommand = setRssItemsCommand;
            RSSMediaLister = rssMediaLister;
            AddRSSMediaCommand = addRSSMediaCommand;
            DeleteRecordCommand = deleteRecordCommand;
            ShowRSSMediaCreationWindowCommand = showRSSMediaCreationWindowCommand;
            OpenMediaLinkCommand = openMediaLinkCommand;
        }
    }
}
