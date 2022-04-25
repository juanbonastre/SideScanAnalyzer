using Microsoft.WindowsAPICodePack.Dialogs;
using SideScanAnalyzer.JSON_Models;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using SideScanAnalyzer.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Commands
{
    internal class OpenProjectCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public OpenProjectCommand(MainWindowViewModel parent)
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
                string[] files = Directory.GetFiles(openFolderDialog.FileName);
                files = files.Where(p => p.ToUpper().EndsWith(ProjectInfo.PROJECT_EXTENSION.ToUpper())).ToArray();
                if (files.Length <= 0)
                {
                    MessageBox.Show("No hay ningún archivo de configuracion \""+ProjectInfo.PROJECT_EXTENSION+"\"","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (files.Length > 1)
                {
                    MessageBox.Show("Hay más de un fichero de configuración \""+ProjectInfo.PROJECT_EXTENSION+"\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    string jsonText = File.ReadAllText(files[0]);
                    ProjectInfo newProjectInfo = JSONProjectInfoConverter.ToProjectInfo(jsonText);
                    newProjectInfo.IsChanged = false;
                    parent.ProjectInfo = newProjectInfo;
                    parent.SetStatusString("Configuración cargada. Recargando proyecto.");
                    await parent.ReloadProject();
                    parent.SetStatusString("Proyecto \""+parent.ProjectInfo.ProjectName+"\" cargado con éxito");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar abrir el fichero de configuración", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
    }
}
