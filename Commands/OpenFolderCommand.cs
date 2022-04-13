using Microsoft.WindowsAPICodePack.Dialogs;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Commands
{
    internal class OpenFolderCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public OpenFolderCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }
        public override bool CanExecute()
        {
            return RunningTasks.Count() == 0;
        }

        public override async Task ExecuteAsync()
        {
            CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog();
            openFolderDialog.IsFolderPicker = true;
            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string[] paths = Directory.GetFiles(openFolderDialog.FileName);
                paths = paths.Where(p => p.ToUpper().EndsWith(".XTF")).ToArray();
                foreach (string path in paths)
                {
                    Trace.WriteLine(path);
                    await parent.LoadImage(path);
                }
            }
        }
    }
}
