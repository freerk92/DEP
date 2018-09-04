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
            this.Decorations = currentDrawState.Decorations;
        }

        public DrawState(List<Figure> figures, List<Group> groups, List<Decoration> decorations)
        {
            Figures = figures;
            Groups = groups;
            Decorations = decorations;
        }

        public List<Figure> Figures { get; set; }
        public List<Group> Groups { get; set; }
        public List<Decoration> Decorations { get; set; }
    }
}
