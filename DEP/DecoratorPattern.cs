using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static DEP.Figure;

namespace DEP
{
    public class DecoratorPattern
    {
        public DecoratorPattern()
        {
        }
    }

    public enum DecorationPosition{
        Top,
        Bottom,
        Left,
        Right
    }

    public class Decoration : Figure
    {
        public string Text { get; set; }
        public Point Location = new Point();
        public Figure decoratedFigure;
        public Group decoratedGroup;
        public DecorationPosition Position;

        public Decoration(Group item, string text, DecorationPosition position) : base(item)
        {
            this.decoratedGroup = item;
            this.Text = text;
            this.Position = position;
            this.Location = GetPosition(item);
        }

        public Decoration(Figure item, string text, DecorationPosition position) : base(item)
        {
            this.decoratedFigure = item;
            this.Text = text;
            this.Position = position;
            this.Location = GetPosition(item);
        }

        public Decoration(Decoration item)
        {
            this.Text = item.Text;
            if (item.decoratedFigure != null)
                this.decoratedFigure = item.decoratedFigure;
            else
                this.decoratedGroup = item.decoratedGroup;
            this.Location = item.Location;
            this.Position = item.Position;

        }

        public void SetLocation()
        {
            if (decoratedGroup == null)
                Location = GetPosition(this.decoratedFigure);
            else
                Location = GetPosition(this.decoratedGroup);
        }

        public Point GetPosition(Group group)
        {
            var figures = new List<Figure>();
            foreach (var item in SaveData.Instance.CurrentDrawState.Figures)
            {
                if(item.group.ID == group.ID)
                {
                    figures.Add(new Figure(item));
                }
            }
            var GroupPoints = GetSizeOfGroup(figures);
            var width = GroupPoints[1].X - GroupPoints[0].X;
            var height = GroupPoints[1].Y - GroupPoints[0].Y;
            var v = GroupPoints[0];

            switch (this.Position)
            {
                case DecorationPosition.Top:
                    return new Point(v.X + (width / 2), v.Y - 25);
                case DecorationPosition.Bottom:
                    return new Point(v.X + (width / 2), v.Y + height + 25);
                case DecorationPosition.Left:
                    return new Point(v.X - 25, v.Y + (height / 2));
                case DecorationPosition.Right:
                    return new Point(v.X + width + 25, v.Y + (height / 2));
            }
            return new Point(0, 0);
        }

        private List<Point> GetSizeOfGroup(List<Figure> figures)
        {
            int biggestX = 0;
            int smallestX = 99999;
            int biggestY = 0;
            int smallestY = 99999;

            foreach (var item in figures)
            {
                int itemStartX = item.start.X;
                int itemStartY = item.start.Y;
                int itemEndX = item.end.X;
                int itemEndY = item.end.Y;

                biggestX = item.start.X > biggestX ? item.start.X : biggestX;
                biggestX = item.end.X > biggestX ? item.end.X : biggestX;
                smallestX = item.start.X < smallestX ? item.start.X : smallestX;
                smallestX = item.end.X < smallestX ? item.end.X : smallestX;

                biggestY = item.start.Y > biggestY ? item.start.Y : biggestY;
                biggestY = item.end.Y > biggestY ? item.end.Y : biggestY;
                smallestY = item.start.Y < smallestY ? item.start.Y : smallestY;
                smallestY = item.end.Y < smallestY ? item.end.Y : smallestY;
            }

            var list = new List<Point>();
            list.Add(new Point(smallestX, smallestY));
            list.Add(new Point(biggestX, biggestY));
            return list;
        }

        private Point GetPosition(Figure item)
        {
            var v = GetTopLeftPoint(item);
            var width = item.start.X < item.end.X ? item.end.X - item.start.X : item.start.X - item.end.X;
            var height = item.start.Y < item.end.Y ? item.end.Y - item.start.Y : item.start.Y - item.end.Y;

            switch(this.Position)
            {
                case DecorationPosition.Top:
                    return new Point(v.X + (width / 2), v.Y - 25);
                case DecorationPosition.Bottom:
                    return new Point(v.X + (width / 2), v.Y+ height + 25);
                case DecorationPosition.Left:
                    return new Point(v.X - 25, v.Y+(height/2));
                case DecorationPosition.Right:
                    return new Point(v.X + width + 25, v.Y + (height / 2));
            }
            return new Point(0,0);
        }

        private Point GetTopLeftPoint(Figure item)
        {
            int startX = item.start.X;
            int startY = item.start.Y;
            int endX = item.end.X;
            int endY = item.end.Y;
            int X = startX < endX ? startX: endX;
            int Y = startY < endY ? startY: endY;

            return new Point(X, Y);
        }

        public string ToString(Figure figure)
        {
            var position = "top";
            switch(Position)
            {
                case DecorationPosition.Top:
                    break;
                case DecorationPosition.Bottom:
                    position = "bottom";
                    break;
                case DecorationPosition.Left:
                    position = "left";
                    break;
                case DecorationPosition.Right:
                    position = "right";
                    break;
            }

            return $"ornament {position} \"{Text}\"{Environment.NewLine}";
        }

        public void Draw(PaintEventArgs e, Figure figure)
        {
            SolidBrush brush = new SolidBrush(Color.Black); 
            Font font = new Font("Arial", 8); 
            StringFormat stringformat = new StringFormat(); 
            stringformat.Alignment = StringAlignment.Center; 
            e.Graphics.DrawString(Text, font, brush, Location, stringformat);  
        }
    }

}
