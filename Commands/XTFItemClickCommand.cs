using SideScanAnalyzer.Core;
using SideScanAnalyzer.Models;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Commands
{
    internal class XTFItemClickCommand : AsyncCommand
    {
        private XTFFileItemViewModel xtfFileItemVM;
        public XTFItemClickCommand(XTFFileItemViewModel xtfFileItemVM)
        {
            this.xtfFileItemVM = xtfFileItemVM;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            await xtfFileItemVM.PaintImage();
        }
    }
}
