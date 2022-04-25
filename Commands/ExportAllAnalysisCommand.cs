using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Commands
{
    internal class ExportAllAnalysisCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public ExportAllAnalysisCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            await Task.Run(() =>
            {
                JArray list = new();
                foreach(XTFFileItem item in parent.XTFFilesList)
                {
                    foreach(Prediction pred in item.xtfFileItemVM.predictions)
                    {
                        if (pred.prediction > 0.5)
                        {
                            JObject predJson = new JObject();
                            predJson["prediction"] = pred.prediction;
                            predJson["x1"] = pred.x1;
                            predJson["x2"] = pred.x2;
                            predJson["y1"] = pred.y1;
                            predJson["y2"] = pred.y2;
                            list.Add(predJson);
                        }
                    }
                }

                if (list.Count == 0)
                {
                    MessageBox.Show("No hay análisis que exportar", "No se puede exportar", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllText(saveFileDialog.FileName, list.ToString());

            });
        }
    }
}
