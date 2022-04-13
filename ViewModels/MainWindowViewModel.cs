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
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SideScanAnalyzer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static readonly string IMAGES_DIRECTORY = System.IO.Path.Join(Directory.GetCurrentDirectory(), "images");

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
                // OnPropertyChanged("XTFFileList");
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
        public IAsyncCommand OpenFileCommand { get; }
        public IAsyncCommand OpenFolderCommand { get; }
        public IAsyncCommand AnalyzeCommand { get; }
        public IAsyncCommand TestCommmand { get; }
        public MainWindowViewModel()
        {
            OpenFileCommand = new OpenFileCommand(this);
            OpenFolderCommand = new OpenFolderCommand(this);
            AnalyzeCommand = new AnalyzeCommand(this);
            TestCommmand = new TestCommand(this);
            xtfImageSource = null;
            xtfFilesList = new ObservableCollection<XTFFileItem>();

            if (!System.IO.Directory.Exists(IMAGES_DIRECTORY))
                System.IO.Directory.CreateDirectory(IMAGES_DIRECTORY);

            // XTFImageSource = new BitmapImage(new Uri(@"C:\Users\juanb\Desktop\XTF TFG\Proyectos XTF\ProyectosNET\SideScanAnalyzer\bin\Debug\net6.0-windows\images\NBP050501A.XTF.bmp"));
        }

        public void SetImageSource(ImageSource imageSource)
        {
            this.XTFImageSource = imageSource;
        }


        public async Task LoadImage(string path)
        {
            try
            {
                XTFFile xtfFile = null;
                await Task.Run(() =>
                {
                    xtfFile = new XTFFile(path);
                    xtfFile.ReadSideScanData();
                });

                XTFFileItem xtfFileItem = new XTFFileItem(this, xtfFile);
                xtfFilesList.Add(xtfFileItem);
                DirectBitmap bitmap_PORT = BitmapConverter.ConvertToBitmap(xtfFile.pingPacketsManager.processedPingData[0], xtfFile.pingPacketsManager.maxValue);
                DirectBitmap bitmap_STDB = BitmapConverter.ConvertToBitmap(xtfFile.pingPacketsManager.processedPingData[1], xtfFile.pingPacketsManager.maxValue);
                DirectBitmap merged = BitmapConverter.MergedBitmaps(bitmap_PORT, bitmap_STDB);

                await Task.Run(() =>
                {
                    xtfFileItem.image = merged;
                    xtfFileItem.imagePath = System.IO.Path.Join(IMAGES_DIRECTORY, xtfFileItem.fileName+".bmp");
                    merged.Bitmap.Save(xtfFileItem.imagePath);
                }); 
            }
            catch (Exception ex)
            {
                Trace.WriteLine("ERROR READING XTF FILE");
            }
        }
    }

}
