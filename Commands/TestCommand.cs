using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Commands
{
    internal class TestCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public TestCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }
        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            XTFFileItem item = parent.XTFFilesList[0];
            Prediction p = item.predictions[0];
            MessageBox.Show(p.ToString());
        }
    }
}
