using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Commands
{
    internal class SaveCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public SaveCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }
        public override bool CanExecute()
        {
            return !parent.ProjectInfo.ProjectParentDirectoryPath.Equals("");
        }

        public override async Task ExecuteAsync()
        {
            if (parent.ProjectInfo.ProjectParentDirectoryPath.Equals(""))
            {
                IAsyncCommand saveAssCommand = new SaveAsCommand(parent);
                await saveAssCommand.ExecuteAsync();
            }
            else
            {
                await parent.ProjectInfo.Save();
            }
        }
    }
}
