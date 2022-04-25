using SideScanAnalyzer.Commands;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using SideScanAnalyzer.Core;
using SideScanAnalyzer.Core.xtfreader;
using SideScanAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.WindowsAPICodePack.Dialogs;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace SideScanAnalyzer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private ProjectInfo _ProjectInfo;
        public ProjectInfo ProjectInfo
        { 
            get
            {
                if (_ProjectInfo==null)
                    _ProjectInfo = new ProjectInfo(this);
                return _ProjectInfo;
            }
            set
            {
                _ProjectInfo = value;
                OnPropertyChanged("ProjectInfo");
            }
        }
        private string _statusString;
        public string StatusString 
        {
            get
            {
                return _statusString;
            }
            set
            {
                _statusString = value;
                OnPropertyChanged("StatusString");
            }
        }
        private ObservableCollection<XTFFileItem> xtfFilesList;
        public ObservableCollection<XTFFileItem> XTFFilesList
        {
            get
            {
                return xtfFilesList;
            }
            set
            {
                xtfFilesList = value;
                OnPropertyChanged("XTFFileList");
            }
        }
        private ImageSource? xtfImageSource;
        public ImageSource? XTFImageSource
        {
            get
            {
                return xtfImageSource;
            }
            set
            {
                xtfImageSource = value;
                OnPropertyChanged("XTFImageSource");
            }
        }
        public IAsyncCommand LoadFileCommand { get; }
        public IAsyncCommand LoadFolderCommand { get; }
        public IAsyncCommand AnalyzeCommand { get; }
        public IAsyncCommand TestCommmand { get; }
        public IAsyncCommand SaveAsCommand { get; }
        public IAsyncCommand SaveCommand { get; }
        public CloseCommand CloseCommand { get; }
        public IAsyncCommand NewProjectCommand { get; }
        public IAsyncCommand OpenProjectCommand { get; }
        public IAsyncCommand ShowProjectInfoCommand { get; }
        public IAsyncCommand ExportAllAnalysisCommand { get; }


        public MainWindowViewModel()
        {
            LoadFileCommand = new LoadFileCommand(this);
            LoadFolderCommand = new LoadFolderCommand(this);
            AnalyzeCommand = new AnalyzeCommand(this);
            TestCommmand = new TestCommand(this);
            SaveAsCommand = new SaveAsCommand(this);
            SaveCommand = new SaveCommand(this);
            CloseCommand = new CloseCommand(this);
            NewProjectCommand = new NewProjectCommand(this);
            OpenProjectCommand = new OpenProjectCommand(this);
            ShowProjectInfoCommand = new ShowProjectInfoCommand(this);
            ExportAllAnalysisCommand = new ExportAllAnalysisCommand(this); 

             _statusString = "Versión 1.0";

            xtfImageSource = null;
            xtfFilesList = new ObservableCollection<XTFFileItem>();

            // XTFImageSource = new BitmapImage(new Uri(@"C:\Users\juanb\Desktop\XTF TFG\Proyectos XTF\ProyectosNET\SideScanAnalyzer\bin\Debug\net6.0-windows\images\NBP050501A.XTF.bmp"));
        }

        public void UpdateXTFItemsBackground()
        {
            foreach (XTFFileItem item in XTFFilesList)
            {
                if (!item.xtfFileItemVM.imagePath.Equals(ProjectInfo.SelectedImagePath))
                    item.xtfFileItemVM.Background = System.Windows.Media.Brushes.WhiteSmoke;
                else
                    item.xtfFileItemVM.Background = System.Windows.Media.Brushes.LightGray;
            }
        }

        internal void SetProjectInfo(ProjectInfo newProjectInfo)
        {
            ProjectInfo = newProjectInfo;
        }

        public async Task ReloadProject()
        {
            // Load XTF File items in ./xtf_files/
            this.XTFFilesList.Clear();
            await ReloadXTFFiles();

            // Load image of selected XTF File item
            // TODO
            UpdateXTFItemsBackground();
            if (ProjectInfo.SelectedImagePath != null)
                XTFImageSource = new BitmapImage(new Uri(ProjectInfo.SelectedImagePath));

            ProjectInfo.ProjectLoaded = true;
        }
        public void SetImageSource(ImageSource imageSource)
        {
            this.XTFImageSource = imageSource;
        }

        public void SetStatusString(string statusString)
        {
            StatusString = statusString;
        }

        public void DeleteXTFFileItem(XTFFileItem item)
        {
            XTFFilesList.Remove(item);
            SetStatusString("Ítem XTF eliminado: " + item.xtfFileItemVM.FileName);
        }

        public async Task AnalyzeImage(XTFFileItemViewModel xtfFileItemVM)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (File.Exists(xtfFileItemVM.imagePath))
                    {
                        xtfFileItemVM.parent.SetStatusString("Analizando " + xtfFileItemVM.FileName + "...");
                        HttpResponseMessage analysysResponse = await GetAnalysisResults(client, xtfFileItemVM.imagePath);
                        analysysResponse.EnsureSuccessStatusCode();

                        xtfFileItemVM.parent.SetStatusString("Procesando resultados");
                        var results = await analysysResponse.Content.ReadAsStringAsync();
                        JObject jResults = JObject.Parse(results);

                        if (Utils.CheckJSONAttributes(jResults, new List<string> { "message", "results", "image_size" }))
                        {
                            JArray items = (JArray)jResults["results"];
                            int length = items.Count;

                            for (int i = 0; i < length; i++)
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
                                xtfFileItemVM.predictions.Add(p);

                            }

                            xtfFileItemVM.StatusLabel = "Analizada";
                            if (xtfFileItemVM.imagePath.Equals(ProjectInfo.SelectedImagePath))
                                await xtfFileItemVM.PaintImage();
                        }
                        else
                        {
                            xtfFileItemVM.parent.SetStatusString("Error analizando imagen");
                            xtfFileItemVM.SetStatusLabel("Error");
                        }
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show("Error, no se pudo hacer conexión con la API flask de predicciones");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            xtfFileItemVM.parent.SetStatusString("Análisis terminado");
        }

        private async Task<HttpResponseMessage> GetAnalysisResults(HttpClient client, string path)
        {
            JSONImage jsonImage = new JSONImage(path);
            StringContent content = new StringContent(jsonImage.ToJSON(), Encoding.UTF8, "application/json");
            return await client.PostAsync(APIURIs.ANALYZE, content);
        }

        public async Task ReadXTFFile(string path)
        {
            SetStatusString("Leyendo archivo: " + path);
            try
            {
                XTFFile xtfFile = null;
                await Task.Run(() =>
                {
                    xtfFile = new XTFFile(path);
                    xtfFile.ReadSideScanData();
                });

                XTFFileItemViewModel xtfFileItemVM = new XTFFileItemViewModel(this, xtfFile);
                XTFFileItem xtfFileItem = new XTFFileItem(xtfFileItemVM);
                SetStatusString("Lectura correcta, ayadiendo a lista");
                xtfFilesList.Add(xtfFileItem);

                string fileName = Path.GetFileName(path);
                string imagePath = Path.Join(ProjectInfo.GetFullImagesDirPath(), fileName)+".bmp";
                if (!File.Exists(imagePath))
                    await RasterAndSaveImage(xtfFileItem);
                else
                    xtfFileItem.xtfFileItemVM.imagePath = imagePath;
            }
            catch (Exception ex)
            {
                SetStatusString("Error leyendo el archivo XTF");
            }
            SetStatusString("Fin de lectura de " + path);
        }

        public async Task RasterAndSaveImage(XTFFileItem xtfFileItem)
        {
            SetStatusString("Rasterizando imágenes");
            DirectBitmap bitmap_PORT = BitmapConverter.ConvertToBitmap(xtfFileItem.xtfFileItemVM.xtfFile.pingPacketsManager.processedPingData[0], xtfFileItem.xtfFileItemVM.xtfFile.pingPacketsManager.maxValue);
            DirectBitmap bitmap_STDB = BitmapConverter.ConvertToBitmap(xtfFileItem.xtfFileItemVM.xtfFile.pingPacketsManager.processedPingData[1], xtfFileItem.xtfFileItemVM.xtfFile.pingPacketsManager.maxValue);
            DirectBitmap merged = BitmapConverter.MergedBitmaps(bitmap_PORT, bitmap_STDB);

            await Task.Run(() =>
            {
                // xtfFileItem.image = merged;
                SetStatusString("Guardando imagen en disco");
                ProjectInfo.CheckImagesDirectory();
                xtfFileItem.xtfFileItemVM.imagePath = System.IO.Path.Join(ProjectInfo.GetFullImagesDirPath(), xtfFileItem.xtfFileItemVM.FileName+".bmp");
                merged.Bitmap.Save(xtfFileItem.xtfFileItemVM.imagePath);
            });

            bitmap_PORT.Dispose();
            bitmap_STDB.Dispose();
            merged.Dispose();
        }

        public async Task LoadImage(string path)
        {
            SetStatusString("Copiando archivo: " + path);
            string fileName = System.IO.Path.GetFileName(path);
            string newPath = System.IO.Path.Join(ProjectInfo.GetFullXTFFilesDirPath(), fileName);
            File.Copy(path, newPath);

            SetStatusString("Leyendo archivo: " + newPath);
            try
            {
                XTFFile xtfFile = null;
                await Task.Run(() =>
                {
                    xtfFile = new XTFFile(newPath);
                    xtfFile.ReadSideScanData();
                });

                XTFFileItemViewModel xtfFileItemVM = new XTFFileItemViewModel(this, xtfFile);
                XTFFileItem xtfFileItem = new XTFFileItem(xtfFileItemVM);
                SetStatusString("Lectura correcta, ayadiendo a lista");
                xtfFilesList.Add(xtfFileItem);

                SetStatusString("Rasterizando imágenes");
                DirectBitmap bitmap_PORT = BitmapConverter.ConvertToBitmap(xtfFile.pingPacketsManager.processedPingData[0], xtfFile.pingPacketsManager.maxValue);
                DirectBitmap bitmap_STDB = BitmapConverter.ConvertToBitmap(xtfFile.pingPacketsManager.processedPingData[1], xtfFile.pingPacketsManager.maxValue);
                DirectBitmap merged = BitmapConverter.MergedBitmaps(bitmap_PORT, bitmap_STDB);

                await Task.Run(() =>
                {
                    // xtfFileItem.image = merged;
                    SetStatusString("Guardando imagen en disco");
                    ProjectInfo.CheckImagesDirectory();
                    xtfFileItem.xtfFileItemVM.imagePath = System.IO.Path.Join(ProjectInfo.GetFullImagesDirPath(), xtfFileItem.xtfFileItemVM.FileName+".bmp");
                    merged.Bitmap.Save(xtfFileItem.xtfFileItemVM.imagePath);
                });

                bitmap_PORT.Dispose();
                bitmap_STDB.Dispose();
                merged.Dispose();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error leyendo el archivo XTF");
                SetStatusString("Error leyendo el archivo XTF");
            }
            SetStatusString("Fin de lectura de " + newPath);
        }

        private async Task ReloadXTFFiles()
        {
            ProjectInfo.CheckXTFDirectory();
            string[] xtfFiles = Directory.GetFiles(ProjectInfo.GetFullXTFFilesDirPath());
            xtfFiles = xtfFiles.Where(p => p.ToUpper().EndsWith(".XTF")).ToArray();
            foreach (string xtfFile in xtfFiles)
            {
                Trace.WriteLine(xtfFile);
                await ReadXTFFile(xtfFile);
                OnPropertyChanged("XTFFilesList");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
