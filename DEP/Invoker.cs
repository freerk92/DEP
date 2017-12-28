using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP
{
    class Invoker
    {
        private IList<ICommand> Commands = new List<ICommand>();

        public void InsertCommands(ICommand command)
        {
            Commands.Add(command);
        }

        public void PressButtonOn(int number)
        {
            Commands[number].Execute();
        }
    }
}
