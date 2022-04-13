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
    internal class OpenFileCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public OpenFileCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }
        public override bool CanExecute()
        {
            return RunningTasks.Count() == 0;
        }

        public override async Task ExecuteAsync()
        {
            /*await Task.Delay(20000);
            MessageBox.Show("HOLA");*/
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                await parent.LoadImage(openFileDialog.FileName);
        }
    }
}
