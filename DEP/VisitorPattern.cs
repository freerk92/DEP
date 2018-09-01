using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DEP
{
    public interface IVisitor
    {
        void Visit(Figure figure);
        void Visit(Group group);
        void Visit();
    }

    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }

    public class ResizeVisitor : IVisitor
    {
        private MouseEventArgs e;
        List<Figure> newFigures;
        List<Group> newGroup;


        public ResizeVisitor(MouseEventArgs e)
        {
            this.e = e;
            newFigures = new List<Figure>(SaveData.Instance.CurrentDrawState.Figures);
            newGroup = new List<Group>(SaveData.Instance.CurrentDrawState.Groups);
        }

        public void Visit(Figure figure)
        {
            int index = newFigures.FindIndex(a => a == figure);
            newFigures[index].end = e.Location;

            var drawState = new DrawState(newFigures, newGroup);
            SaveData.Instance.CurrentDrawState = drawState;
        }

        public void Visit(Group group)
        {
            var groupFigures = new List<Figure>();

            foreach (var item in newFigures)
            {
                if (item.group.ID.Equals(group.ID))
                    groupFigures.Add(item);
            }


            var mainFigure = groupFigures.First();
            var width = mainFigure.end.X - mainFigure.start.X;
            var height = mainFigure.end.Y - mainFigure.start.Y;
            var EndPoint = mainFigure.end;
            var StartPoint = mainFigure.start;

            var XDifference = 1.0 - ((double)EndPoint.X - (double)e.X) / width;
            var YDifference = 1.0 - ((double)EndPoint.Y - (double)e.Y) / height;

            if (XDifference < 0 || YDifference < 0)
            {
                throw new Exception();
            }


            foreach (var item in groupFigures)
            {
                var index = newFigures.IndexOf(item);
                var resizedItem = resizeFigure(item, XDifference, YDifference);
                if (!item.IsMainGroupFigure)
                    //resizedItem = AddOffsetToResize(mainFigure, resizedItem, XDifference, YDifference);
                newFigures[index] = resizedItem;
            }

            var drawState = new DrawState(newFigures, newGroup);
            SaveData.Instance.CurrentDrawState = drawState;
        }

        private Figure resizeFigure(Figure item, double xDifference, double yDifference)
        {
            var width = item.end.X - item.start.X;
            var height = (item.end.Y - item.start.Y);
            var ItemSizeX = Math.Abs(width * xDifference);
            var ItemSizeY = Math.Abs(height * yDifference);
            int newX = Math.Abs((int)(item.start.X + ItemSizeX));
            int newY = Math.Abs((int)(item.start.Y + ItemSizeY));
            item.end = new Point(newX, newY);
            return item;
        }

        public void Visit()
        {
            throw new NotImplementedException();
        }
    }

    public class MoveVisitor : IVisitor
    {
        private MouseEventArgs e;
        List<Figure> newFigures;
        List<Group> newGroup;


        public MoveVisitor(MouseEventArgs e)
        {
            this.e = e;
            newFigures = new List<Figure>(SaveData.Instance.CurrentDrawState.Figures);
            newGroup = new List<Group>(SaveData.Instance.CurrentDrawState.Groups);
        }

        public void Visit(Figure figure)
        {
            int x = Math.Abs(figure.start.X - figure.end.X);
            int y = Math.Abs(figure.start.Y - figure.end.Y);

            figure.start = e.Location;
            figure.end = new Point(e.X + x, e.Y + y);

            var drawState = new DrawState(newFigures, newGroup);
            SaveData.Instance.CurrentDrawState = drawState;
        }

        public void Visit(Group group)
        {
            var groupFigures = new List<Figure>();
            foreach (var item in newFigures)
            {
                if (item.group.ID.Equals(group.ID))
                    groupFigures.Add(item);
            }

            var StartPoint = groupFigures[0].start;
            var EndPoint = e.Location;
            var XDifference = StartPoint.X - EndPoint.X;
            var YDifference = StartPoint.Y - EndPoint.Y;

            foreach (var item in groupFigures)
            {
                foreach (var groupFigure in newFigures)
                {
                    if (groupFigure.Equals(item))
                    {
                        groupFigure.start = new Point(groupFigure.start.X - XDifference, groupFigure.start.Y - YDifference);
                        groupFigure.end = new Point(groupFigure.end.X - XDifference, groupFigure.end.Y - YDifference);
                        item.start = groupFigure.start;
                        item.end = groupFigure.end;
                    }
                }
            }

            var drawState = new DrawState(newFigures, newGroup);
            SaveData.Instance.CurrentDrawState = drawState;
        }

        public void Visit()
        {
            throw new NotImplementedException();
        }
    }


}
