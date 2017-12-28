using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP.Commands
{
    class LoadCommand : ICommand
    {
        Receiver receiver;

        public LoadCommand(Receiver rec)
        {
            receiver = rec;
        }

        public void Execute()
        {
            receiver.Load();
        }
    }
}
