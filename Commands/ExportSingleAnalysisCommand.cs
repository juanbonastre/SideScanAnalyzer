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
    internal class ExportSingleAnalysisCommand : AsyncCommand
    {
        private XTFFileItemViewModel xtfFileItemVM;
        public ExportSingleAnalysisCommand(XTFFileItemViewModel xtfFileItemVM)
        {
            this.xtfFileItemVM = xtfFileItemVM;
        }

        public override bool CanExecute()
        {
            return RunningTasks.Count() == 0;
        }

        public override async Task ExecuteAsync()
        {
            JArray list = new();
            await Task.Run(() =>
            {
                foreach (Prediction pred in xtfFileItemVM.predictions)
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
            });

            if (list.Count == 0)
            {
                MessageBox.Show("No hay análisis que exportar", "No se puede exportar", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, list.ToString());
        }
    }
}
