using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DEP.Figure;

namespace DEP
{
    class Receiver
    {
        SaveData DataLink = SaveData.Instance;

        public DrawState CloneFigures(DrawState OriginalDrawState)
        {
            var figures = new List<Figure>();
            foreach (var item in OriginalDrawState.Figures)
            {
                figures.Add(new Figure(item));
            }

            var groups = new List<Group>();
            foreach (var item in OriginalDrawState.Groups)
            {
                groups.Add(new Group(item));
            }

            var decorations = new List<Decoration>();
            foreach (var item in OriginalDrawState.Decorations)
            {
                decorations.Add(new Decoration(item));
            }

            var drawState = new DrawState(figures, groups, decorations);
            return drawState;
        }

        public void Undo()
        {
            if(DataLink.HistoryList.Count > 0)
            {
                DataLink.FutureList.Add(CloneFigures(DataLink.CurrentDrawState));
                DataLink.CurrentDrawState = CloneFigures(DataLink.HistoryList.Last());
                DataLink.HistoryList.Remove(DataLink.HistoryList.Last());
            }
        }

        public void Redo()
        {
            if(DataLink.FutureList.Count > 0)
            {
                DataLink.HistoryList.Add(CloneFigures(DataLink.CurrentDrawState));
                DataLink.CurrentDrawState = CloneFigures(DataLink.FutureList.Last());
                DataLink.FutureList.Remove(DataLink.FutureList.Last());
            }
        }
        
        public void Save()
        {
            FileManager manager = new FileManager();
            IVisitor visitor = new SaveVisitor();
            manager.Accept(visitor);
            //manager.Save();
        }

        public void Load()
        {
            FileManager manager = new FileManager();
            manager.Load();
        }
    }
}
