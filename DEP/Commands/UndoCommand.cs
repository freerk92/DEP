using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP
{
    class UndoCommand : ICommand
    {
        Receiver receiver;

        public UndoCommand(Receiver rec)
        {
            receiver = rec;
        }

        public void Execute()
        {
            receiver.Undo();
        }
    }
}
