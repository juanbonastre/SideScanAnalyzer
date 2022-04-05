using Microsoft.Win32;
using SideScanAnalyzer.Core;
using SideScanAnalyzer.Core.xtfreader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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

namespace SideScanAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public MainWindow()
        {
            InitializeComponent();
            XTFItemsControl.ItemsSource = XTFFilesList;
        }

        private ObservableCollection<XTFFileItem> _xtfFilesList;
        public ObservableCollection<XTFFileItem> XTFFilesList
        {
            get
            {
                if (_xtfFilesList == null)
                {
                    _xtfFilesList = new ObservableCollection<XTFFileItem>();
                }
                return _xtfFilesList;
            }
            set
            {
                _xtfFilesList = value;
                OnPropertyChanged("XTFFilesList");
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Stopwatch stopwatch = new Stopwatch();
                TimeSpan stopwatchElapsed;
                float time;

                XTFFile xtfFile = new XTFFile(openFileDialog.FileName);
                xtfFile.ReadSideScanData();
                /*XTFFileModel xtfFileModel = new XTFFileModel(xtfFile);
                XTFFileViewModel xtfFileViewModel = new XTFFileViewModel(xtfFileModel);
                XTFFileView xtfFileView = new XTFFileView(xtfFileViewModel);*/
                XTFFilesList.Add(new XTFFileItem(xtfFile));

                stopwatch.Start();
                Bitmap bitmap = BitmapConverter.ConvertToBitmap(xtfFile.pingPacketsManager.processedPingData[0], xtfFile.pingPacketsManager.maxValue);
                //Bitmap bitmap = BitmapConverter.ConvertToBitmap2(xtfFile.pingPacketsManager.processedPingData[0], xtfFile.pingPacketsManager.maxValue);

                stopwatch.Stop();
                stopwatchElapsed = stopwatch.Elapsed;
                time = ((float)Convert.ToInt32(stopwatchElapsed.TotalMilliseconds)/1000);
                Trace.WriteLine("Time rastering one image: " + time.ToString());

                bitmap.Save(System.IO.Path.Join(Directory.GetCurrentDirectory(), "image.bmp"));
                XTFImageViewer.Source = Utils.ImageSourceFromBitmap(bitmap);
            }
        }
    }
}
