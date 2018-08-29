﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP
{
    public class Group
    {
        public int ID;
        public List<Figure> Figures = new List<Figure>();
        public List<Group> Groups = new List<Group>();
        public int? IsInGroup { get; set; }


        public Group(int index)
        {
            ID = index;
        }

        public Group(Group group)
        {
            this.ID = group.ID;
            this.Figures = group.Figures;
            this.Groups = group.Groups;
            this.IsInGroup = group.IsInGroup;
        }

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
