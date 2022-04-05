using SideScanAnalyzer.Core.xtfreader;
using System;
using System.Collections.Generic;
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
        public XTFFileItem(XTFFile xtfFile)
        {
            InitializeComponent();
            this.xtfFile = xtfFile;
            fileName = (xtfFile.filePath).Split("\\").Last().Split("/").Last();
            FileNameLabel.Content = fileName;
        }
    }
}
