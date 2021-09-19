using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeNSodeCore
{
    public interface IRSSMediaRecord : ILinkedRSS
    {
        string Title { get; set; }
    }
}
