using Microsoft.WindowsAPICodePack.Dialogs;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Commands
{
    internal class SaveAsCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public SaveAsCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }

        public override bool CanExecute()
        {
            return !parent.ProjectInfo.ProjectParentDirectoryPath.Equals("");
        }

        public override async Task ExecuteAsync()
        {
            if (parent.ProjectInfo.IsChanged)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Desea guardar su progreso?", "No se han guardado los cambios", System.Windows.MessageBoxButton.YesNoCancel, System.Windows.MessageBoxImage.Warning);
                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        IAsyncCommand saveCommand = new SaveCommand(parent);
                        await saveCommand.ExecuteAsync();
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            
            CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog();
            openFolderDialog.IsFolderPicker = true;
            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                /*if (File.Exists(Path.Join(openFolderDialog.FileName,ProjectInfo.PROJECT_FILENAME)))
                {
                    MessageBox.Show("La ubicación ya contiene un archivo de proyecto.");
                    return;
                }*/

                parent.ProjectInfo.ProjectParentDirectoryPath = openFolderDialog.FileName;
                await parent.ProjectInfo.Save();
            }

        }
    }
}
