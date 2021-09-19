using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeNSodeCore
{
    public interface IListerSelector<T> : ILister<T>
    {
        T SelectedListItem { get; set; }
    }
}
