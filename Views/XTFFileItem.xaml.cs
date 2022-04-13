using SideScanAnalyzer.Core;
using SideScanAnalyzer.Core.xtfreader;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
    /// Interaction logic for XTFFileItem.xaml
    /// </summary>
    public partial class XTFFileItem : UserControl
    {
        public string fileName;
        public XTFFile xtfFile;
        public DirectBitmap? image;
        public string imagePath;
        public MainWindowViewModel parent;
        public List<Prediction> predictions;
        public XTFFileItem(MainWindowViewModel parent, XTFFile xtfFile)
        {
            InitializeComponent();
            this.xtfFile = xtfFile;
            this.parent = parent;
            fileName = (xtfFile.filePath).Split("\\").Last().Split("/").Last();
            FileNameLabel.Content = fileName;
            StatusLabel.Content = "Not analyzed";
            imagePath = "";
            image =null;
            predictions = new List<Prediction>();
        }

        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (image==null)
                MessageBox.Show("Image of file is null");
            else
            {
                foreach (Prediction p in predictions)
                    image.PaintLine(p.x1, p.x2, p.y1, p.y2);
                var imageSource = Utils.ImageSourceFromBitmap(image.Bitmap);
                image.Bitmap.Save(@"C:\Users\juanb\Desktop\test.bmp");
                parent.SetImageSource(imageSource);
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {;
            this.Background = System.Windows.Media.Brushes.Yellow;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = System.Windows.Media.Brushes.WhiteSmoke;
        }
    }
}
