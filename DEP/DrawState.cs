using System;
using System.Collections.Generic;

namespace DEP
{
    public class DrawState
    {
        private DrawState currentDrawState;

        public DrawState()
        { }

        public DrawState(DrawState currentDrawState)
        {
            this.Figures = currentDrawState.Figures;
            this.Groups = currentDrawState.Groups;
        }

        public DrawState(List<Figure> figures, List<Group> groups)
        {
            Figures = figures;
            Groups = groups;
        }

        public List<Figure> Figures { get; set; }
        public List<Group> Groups { get; set; }
    }
}
