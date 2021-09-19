using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeNSodeCore
{
    public interface IRSSItem : IDescribable, ILinkedEpisode, IPremium
    {
        string Title { get; set; }
    }
}
