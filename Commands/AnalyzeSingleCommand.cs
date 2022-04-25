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
    internal class AnalyzeSingleCommand : AsyncCommand
    {
        private XTFFileItemViewModel xtfFileItemVM;
        public AnalyzeSingleCommand(XTFFileItemViewModel xtfFileItemVM)
        {
            this.xtfFileItemVM = xtfFileItemVM;
        }
        public override bool CanExecute()
        {
            return RunningTasks.Count() == 0;
        }

        public override async Task ExecuteAsync()
        {
            await xtfFileItemVM.parent.AnalyzeImage(xtfFileItemVM);
        }
        private async Task<HttpResponseMessage> GetAnalysisResults(HttpClient client, string path)
        {
            JSONImage jsonImage = new JSONImage(path);
            StringContent content = new StringContent(jsonImage.ToJSON(), Encoding.UTF8, "application/json");
            return await client.PostAsync(APIURIs.ANALYZE, content);
        }
    }
}
