using SideScanAnalyzer.Commands;
using SideScanAnalyzer.Core;
using SideScanAnalyzer.Core.xtfreader;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SideScanAnalyzer.ViewModels
{
    public class XTFFileItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string FileName { get; set; }

        private string _statusLabel;
        public string StatusLabel
        {
            get
            {
                return _statusLabel;
            }
            set
            {
                _statusLabel = value;
                OnPropertyChanged("StatusLabel");
            }
        }
        private System.Windows.Media.Brush _Background;
        public System.Windows.Media.Brush Background
        {
            get
            {
                if (_Background == null)
                    _Background = System.Windows.Media.Brushes.WhiteSmoke;
                return _Background;
            }
            set
            {
                _Background = value;
                OnPropertyChanged("Background");
            }
        }
        public XTFFile xtfFile;
        public string imagePath;
        public MainWindowViewModel parent;
        public List<Prediction> predictions;

        public IAsyncCommand MouseEnterCommand { get; }
        public IAsyncCommand MouseLeaveCommand { get; }
        public IAsyncCommand ItemClickCommand { get; }
        public IAsyncCommand AnalyzeSingleCommand { get; }
        public IAsyncCommand DeleteXTFFileItemCommand { get; }


        public XTFFileItemViewModel() { }

        public XTFFileItemViewModel(MainWindowViewModel parent, XTFFile xtfFile)
        {
            MouseEnterCommand = new XTFItemMouseEnterCommand(this);
            MouseLeaveCommand = new XTFItemMouseLeaveCommand(this);
            ItemClickCommand = new XTFItemClickCommand(this);
            AnalyzeSingleCommand = new AnalyzeSingleCommand(this);
            DeleteXTFFileItemCommand = new DeleteXTFFileItemCommand(this);


            this.xtfFile = xtfFile;
            this.parent = parent;
            FileName = (xtfFile.filePath).Split("\\").Last().Split("/").Last();
            _statusLabel = "No analizada";
            imagePath = "";
            // image =null;
            predictions = new List<Prediction>();
        }

        internal void SetStatusLabel(string content)
        {
            StatusLabel = content;
        }

        internal async Task PaintImage()
        {
            if (!File.Exists(imagePath)) // (image==null)
                MessageBox.Show("Image path could not be found: " + imagePath);
            else
            {
                Bitmap bmp = new Bitmap(imagePath);
                DirectBitmap dBmp = new DirectBitmap(bmp.Width, bmp.Height);
                using (Graphics g = Graphics.FromImage(dBmp.Bitmap))
                    g.DrawImage(bmp, System.Drawing.Point.Empty);
                foreach (Prediction p in predictions)
                    if (p.prediction>0.5)
                        dBmp.PaintLine(p.x1, p.x2, p.y1, p.y2, p.prediction);
                var imageSource = Utils.ImageSourceFromBitmap(dBmp.Bitmap);
                //dBmp.Bitmap.Save(@"C:\Users\juanb\Desktop\test.bmp");
                parent.SetImageSource(imageSource);
                parent.ProjectInfo.SelectedImagePath = imagePath;

                parent.UpdateXTFItemsBackground();
            }
        }
    }
}
