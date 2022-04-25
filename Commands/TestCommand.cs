using SideScanAnalyzer.JSON_Models;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
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
    internal class TestCommand : AsyncCommand
    {
        private MainWindowViewModel parent;
        public TestCommand(MainWindowViewModel parent)
        {
            this.parent = parent;
        }
        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            //await TestJsonSerialize1();
            //await TestJsonSerialize2();
            await TestJsonSerialize3();
        }

        private async Task TestJsonSerialize1()
        {
            string fileName = "C:/Users/juanb/Desktop/test.json";

            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot",
                Test = false
            };

            string jsonString1 = JsonSerializer.Serialize(weatherForecast);
            MessageBox.Show(jsonString1);


            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, weatherForecast);
            await createStream.DisposeAsync();


            string jsonString2 = File.ReadAllText(fileName);
            MessageBox.Show(jsonString2);

            WeatherForecast? parsedObject = JsonSerializer.Deserialize<WeatherForecast>(jsonString2);
            MessageBox.Show(parsedObject.ToString());

            string jsonString3 = @"{""Date"":""2019-08-01T00:00:00+02:00"",""TemperatureCelsius"":25,""Summary"":""Hot""}";
            WeatherForecast? parsedObject2 = JsonSerializer.Deserialize<WeatherForecast>(jsonString3);
            MessageBox.Show(parsedObject2.ToString());
        }

        private async Task TestJsonSerialize2()
        {
            string fileName = "C:/Users/juanb/Desktop/test.json";

            var projectInfo = new ProjectInfo(parent);

            string jsonString1 = JsonSerializer.Serialize(projectInfo);
            MessageBox.Show(jsonString1);


            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, projectInfo);
            await createStream.DisposeAsync();


            string jsonString2 = File.ReadAllText(fileName);
            MessageBox.Show(jsonString2);

            ProjectInfo? parsedObject = JsonSerializer.Deserialize<ProjectInfo>(jsonString2);
            MessageBox.Show(parsedObject.ToString());

            /*string jsonString3 = @"{""ProjectLoaded"":false,""ProjectName"":""Proyecto sin guardar"",""ProjectParentDirectoryPath"":"""",""CreationTime"":""2022-04-22T23: 28:58.5645206+02:00"",""LastModificationTime"":""2022-04-22T23: 28:58.5645201+02:00"",""IsChanged"":true}";
            ProjectInfoTest? parsedObject = JsonSerializer.Deserialize<ProjectInfoTest>(jsonString3);
            MessageBox.Show(parsedObject.ToString());*/
        }
        private async Task TestJsonSerialize3()
        {
            string fileName = "C:/Users/juanb/Desktop/test.json";

            ProjectInfo projectInfo1 = new ProjectInfo(parent);
            string jsonString1 = JSONProjectInfoConverter.ToJSONString(projectInfo1);
            MessageBox.Show(jsonString1);

            File.WriteAllText(fileName, jsonString1);

            string jsonString2 = File.ReadAllText(fileName);
            ProjectInfo projectInfo2 = JSONProjectInfoConverter.ToProjectInfo(jsonString2);
            MessageBox.Show(projectInfo2.ToString());
        }
    }
}
