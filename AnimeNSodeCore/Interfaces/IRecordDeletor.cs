using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AnimeNSodeCore
{
    public interface IRecordDeletor
    {
        ICommand DeleteRecordCommand { get; set; }
    }
}
