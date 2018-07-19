using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP
{
    class Group
    {
        List<Figure> Figures = new List<Figure>();
        List<Group> Groups = new List<Group>();

        public void AddToGroup(Figure figure)
        {
            Figures.Add(figure);
        }

        public void AddToGroup(Group group)
        {
            Groups.Add(group);
        }
    }
}
