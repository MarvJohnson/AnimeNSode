using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace AnimeNSodeCore
{
    public class ThreadSafeObservableCollection<T> : ObservableCollection<T>
    {
        private readonly object syncObject = new object();
        protected override void ClearItems()
        {
            lock (syncObject)
            {
                base.ClearItems();
            }
        }

        protected override void InsertItem(int index, T item)
        {
            lock (syncObject)
            {
                base.InsertItem(index, item);
            }
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            lock (syncObject)
            {
                base.MoveItem(oldIndex, newIndex);
            }
        }

        protected override void RemoveItem(int index)
        {
            lock (syncObject)
            {
                base.RemoveItem(index);
            }
        }

        protected override void SetItem(int index, T item)
        {
            lock (syncObject)
            {
                base.SetItem(index, item);
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            lock (syncObject)
            {
                base.OnCollectionChanged(e);
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            lock (syncObject)
            {
                base.OnPropertyChanged(e);
            }
        }
    }
}
