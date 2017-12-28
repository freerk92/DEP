using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP.Commands
{
    class SaveCommand : ICommand
    {
        Receiver receiver;

        public SaveCommand(Receiver rec)
        {
            receiver = rec;
        }

        public void Execute()
        {
            receiver.Save();
        }
    }
}
