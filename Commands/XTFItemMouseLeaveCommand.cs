using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Commands
{
    internal class XTFItemMouseLeaveCommand : AsyncCommand
    {
        private XTFFileItemViewModel xtfFileItemVM;
        public XTFItemMouseLeaveCommand(XTFFileItemViewModel xtfFileItemVM)
        {
            this.xtfFileItemVM = xtfFileItemVM;
        }

        public override bool CanExecute()
        {
            return !xtfFileItemVM.parent.ProjectInfo.SelectedImagePath.Equals(xtfFileItemVM.imagePath);
        }

        public override Task ExecuteAsync()
        {
            xtfFileItemVM.Background = System.Windows.Media.Brushes.WhiteSmoke;
            return Task.CompletedTask;
        }
    }
}
