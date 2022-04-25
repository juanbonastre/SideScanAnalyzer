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
using System.Windows.Shapes;

namespace SideScanAnalyzer.Views
{
    /// <summary>
    /// Interaction logic for ProjectNameDialog.xaml
    /// </summary>
    public partial class ProjectNameDialog : Window
    {

        public string ProjectName { get; set; }
        public ProjectNameDialog()
        {
            InitializeComponent();
            ProjectName = "";
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (!ProjectNameLabel.Text.Equals("")) 
            {
                ProjectName = ProjectNameLabel.Text;
                Exit();
            }
        }

        private void Exit()
        {
            this.DialogResult = true;
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }
    }
}
