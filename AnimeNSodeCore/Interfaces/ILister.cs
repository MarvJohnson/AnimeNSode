using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AnimeNSodeCore
{
    public interface ILister<T>
    {
        Collection<T> List { get; set; }
    }
}
