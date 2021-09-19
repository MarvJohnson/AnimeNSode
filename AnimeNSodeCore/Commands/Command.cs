using System;
using System.Windows.Input;

namespace AnimeNSodeCore.Commands
{
    public class Command<T> : IDICommand
    {
        private Action<T> Action { get; set; }
        private Predicate<T> Predicate { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action<T> action, Predicate<T> predicate = null)
        {
            Action = action;
            Predicate = predicate ?? (x => true);
        }

        public bool CanExecute(object parameter)
        {
            return Predicate((T)parameter);
        }

        public void Execute(object parameter)
        {
            Action((T)parameter);
        }
    }
}
