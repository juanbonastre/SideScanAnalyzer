using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SideScanAnalyzer.Commands
{
    public interface IAsyncCommand : ICommand
    {
        IEnumerable<Task> RunningTasks { get; }
        bool CanExecute();
        Task ExecuteAsync();
    }
}
