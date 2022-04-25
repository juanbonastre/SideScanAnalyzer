using Newtonsoft.Json.Linq;
using SideScanAnalyzer.Core;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Commands
{
    internal class AnalyzeCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public AnalyzeCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }
        public override bool CanExecute()
        {
            return (RunningTasks.Count() == 0 && parent.XTFFilesList.Count() > 0);
        }

        public override async Task ExecuteAsync()
        {
            foreach (XTFFileItem item in parent.XTFFilesList)
            {
                await parent.AnalyzeImage(item.xtfFileItemVM);
            }
        }
        private async Task<HttpResponseMessage> GetAnalysisResults(HttpClient client, string path)
        {
            JSONImage jsonImage = new JSONImage(path);
            StringContent content = new StringContent(jsonImage.ToJSON(), Encoding.UTF8, "application/json");
            return await client.PostAsync(APIURIs.ANALYZE, content);
        }
    }
}
