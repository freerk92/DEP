using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public List<Figure> ResizeGroup(List<Figure> newFigures, MouseEventArgs e)
        {
            var groupFigures = new List<Figure>();
                    foreach (var item in newFigures)
                    {
                        if (item.group.ID.Equals(this.ID))
                            groupFigures.Add(item);
                    }


                var mainFigure = groupFigures.First();
                var width = mainFigure.end.X-mainFigure.start.X;
                var height = mainFigure.end.Y - mainFigure.start.Y;
                var EndPoint = mainFigure.end;
                var StartPoint = mainFigure.start;

                var XDifference = 1.0 - ((double)EndPoint.X - (double)e.X)/width;
                var YDifference = 1.0 - ((double)EndPoint.Y - (double)e.Y)/height;

                if(XDifference < 0 || YDifference < 0)
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

            return newFigures;
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

        internal List<Figure> MoveGroup(List<Figure> newFigures, MouseEventArgs e)
        {
            var groupFigures = new List<Figure>();
            foreach (var item in newFigures)
            {
                if (item.group.ID.Equals(this.ID))
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
            return newFigures;
        }
    }
}
