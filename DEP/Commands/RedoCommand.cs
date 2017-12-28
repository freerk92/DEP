using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP
{
    class RedoCommand : ICommand
    {
        Receiver receiver;

        public RedoCommand(Receiver rec)
        {
            receiver = rec;
        }

        public void Execute()
        {
            receiver.Redo();
        }
    }
}