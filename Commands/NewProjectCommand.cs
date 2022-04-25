using Microsoft.WindowsAPICodePack.Dialogs;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using SideScanAnalyzer.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Commands
{
    internal class NewProjectCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public NewProjectCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            if (parent.ProjectInfo.ProjectLoaded && parent.ProjectInfo.IsChanged)
            {
                MessageBoxResult messageBoxResult2 = System.Windows.MessageBox.Show("¿Desea guardar su progreso?", "No se han guardado los cambios", System.Windows.MessageBoxButton.YesNoCancel, System.Windows.MessageBoxImage.Warning);
                switch (messageBoxResult2)
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
                string folderPath = openFolderDialog.FileName;

                ProjectNameDialog pnd = new ProjectNameDialog();
                if (pnd.ShowDialog() == false) return;

                string projectName = pnd.ProjectName;
                string fullPath = Path.Join(folderPath, projectName);
                if (Directory.Exists(fullPath))
                {
                    MessageBox.Show("Una carpeta con el nombre \""+projectName+"\" ubicada en \""+folderPath+"\" ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Directory.CreateDirectory(fullPath);

                ProjectInfo newProjectInfo = new ProjectInfo(parent, folderPath, projectName);
                parent.SetProjectInfo(newProjectInfo);
                await parent.ProjectInfo.Save();
                parent.SetStatusString("Configuración de proyecto guardada. Recargando proyecto.");
                await parent.ReloadProject();
                parent.SetStatusString("Proyecto \""+ parent.ProjectInfo.ProjectName +"\" cargado");
            }
        }
    }
}
