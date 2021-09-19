using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AnimeNSodeCore.Commands
{
    // Marker for all the different Command<T> implementations so each service can be registered properly for Dependency Injection.
    public interface IDICommand : 
        ICommand, 
        ISetRSSItemsCommand, 
        IAddRSSMediaCommand, 
        IDeleteRecordCommand, 
        IShowRSSMediaCreationWindowCommand,
        IOpenMediaLinkCommand
    {
    }
}
