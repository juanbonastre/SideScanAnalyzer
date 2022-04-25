using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SideScanAnalyzer.Commands
{
    public class CloseCommand : ICommand
    {
        private MainWindowViewModel parent;
        public CloseCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object e)
        {
            CancelEventArgs cea = (CancelEventArgs)e;
            if (parent.ProjectInfo.IsChanged)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Desea guardar su progreso?", "No se han guardado los cambios", System.Windows.MessageBoxButton.YesNoCancel, System.Windows.MessageBoxImage.Warning);
                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        IAsyncCommand saveCommand = new SaveCommand(parent);
                        saveCommand.Execute(e);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        cea.Cancel = true;
                        break;
                }
            }
        }
    }
}
