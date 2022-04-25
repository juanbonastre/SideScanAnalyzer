using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Commands
{
    internal class ShowProjectInfoCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public ShowProjectInfoCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }

        public override bool CanExecute()
        {
            return parent.ProjectInfo.ProjectLoaded;
        }

        public override async Task ExecuteAsync()
        {
            MessageBox.Show(parent.ProjectInfo.ToStringTest());
        }
    }
}
