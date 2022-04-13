using Newtonsoft.Json.Linq;
using SideScanAnalyzer.Core;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
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
            return RunningTasks.Count() == 0;
        }

        public override async Task ExecuteAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(APIURIs.PING);
                    response.EnsureSuccessStatusCode();

                    foreach (XTFFileItem item in parent.XTFFilesList)
                    {
                        if (File.Exists(item.imagePath))
                        {
                            HttpResponseMessage analysysResponse = await GetAnalysisResults(client, item.imagePath);
                            response.EnsureSuccessStatusCode();

                            var results = await analysysResponse.Content.ReadAsStringAsync();
                            JObject jResults = JObject.Parse(results);

                            if (Utils.CheckJSONAttributes(jResults, new List<string> { "message", "results" }))
                            {
                                JArray items = (JArray)jResults["results"];
                                int length = items.Count;

                                for(int i = 0; i < length; i++)
                                {
                                    string strPrediction = jResults["results"][i]["prediction"].ToString();
                                    double prediction = double.Parse(strPrediction, new CultureInfo("en-US"));

                                    string strX1 = jResults["results"][i]["coordinates"]["x1"].ToString();
                                    double x1 = double.Parse(strX1, new CultureInfo("en-US"));
                                    string strX2 = jResults["results"][i]["coordinates"]["x2"].ToString();
                                    double x2 = double.Parse(strX2, new CultureInfo("en-US"));
                                    string strY1 = jResults["results"][i]["coordinates"]["y1"].ToString();
                                    double y1 = double.Parse(strY1, new CultureInfo("en-US"));
                                    string strY2 = jResults["results"][i]["coordinates"]["y2"].ToString();
                                    double y2 = double.Parse(strY2, new CultureInfo("en-US"));

                                    // MessageBox.Show("Result: "+strPrediction+"\nCoordinates: ["+strX1+"-"+strX2+"] ["+strY1+"-"+strY2+"]");

                                    Prediction p = new Prediction(prediction, x1, x2, y1, y2);
                                    item.predictions.Add(p);
                                }
                                item.StatusLabel.Content = "Analyzed";
                            }
                            else
                            {
                                item.StatusLabel.Content = "Analysis error";
                            }
                        }
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show("Error, could not make connection with the API");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
