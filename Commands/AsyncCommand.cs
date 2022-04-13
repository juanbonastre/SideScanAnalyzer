using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SideScanAnalyzer.Commands
{
    internal abstract class AsyncCommand : IAsyncCommand
    {
        private readonly ObservableCollection<Task> runningTasks;
        public AsyncCommand ()
        {
            runningTasks = new ObservableCollection<Task> ();
            runningTasks.CollectionChanged += OnRunningTaskChanged;
        }

        public IEnumerable<Task> RunningTasks
        {
            get => runningTasks;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute();
        }

        async void ICommand.Execute(object? parameter)
        {
            Task runningTask = ExecuteAsync();

            runningTasks.Add(runningTask);

            try
            {
                await runningTask;
            }
            finally
            {
                runningTasks.Remove(runningTask);
            }
        }

        public abstract bool CanExecute();
        public abstract Task ExecuteAsync();

        private void OnRunningTaskChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
