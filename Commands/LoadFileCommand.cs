using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SideScanAnalyzer.ViewModels;

namespace SideScanAnalyzer.Commands
{
    internal class LoadFileCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public LoadFileCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }
        public override bool CanExecute()
        {
            return RunningTasks.Count() == 0 && !parent.ProjectInfo.ProjectParentDirectoryPath.Equals("");
        }

        public override async Task ExecuteAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                await parent.LoadImage(openFileDialog.FileName);
        }
    }
}
