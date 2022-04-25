using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Commands
{
    internal class DeleteXTFFileItemCommand : AsyncCommand
    {
        private XTFFileItemViewModel xtfFileItemVM;
        public DeleteXTFFileItemCommand(XTFFileItemViewModel xtfFileItemVM)
        {
            this.xtfFileItemVM = xtfFileItemVM;
        }
        public override bool CanExecute()
        {
            return true;
        }

        public override Task ExecuteAsync()
        {
            foreach (XTFFileItem item in xtfFileItemVM.parent.XTFFilesList)
            {
                if(item.xtfFileItemVM == xtfFileItemVM)
                {
                    xtfFileItemVM.parent.DeleteXTFFileItem(item);
                    break;
                }
            }
            return Task.CompletedTask;
        }
    }
}
