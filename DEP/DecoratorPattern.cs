using System;
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

    public class Decoration : IFigure
    {
        public string Text { get; set; }
        public Point ornamentLocation = new Point();

        public Figure decoratedFigure;

        public Decoration(Figure item, string text)
        {
            this.decoratedFigure = item;
            this.Text = text;
            this.ornamentLocation = GetPosition(item);
        }

        private Point GetPosition(Figure item)
        {
            switch(item.direction)
            {
                case Direction.One:
                    return GetPoint(item, Direction.One);
                case Direction.Two:
                    return GetPoint(item, Direction.Two);
                case Direction.Three:
                    return GetPoint(item, Direction.Three);
                case Direction.Four:
                    return GetPoint(item, Direction.Four);
            }
            return new Point(0,0);
        }

        private Point GetPoint(Figure item, Direction direction)
        {
            int startX = item.start.X;
            int startY = item.start.Y;
            int endX = item.end.X;
            int endY = item.end.Y;
            int X = startX < endX ? startX+((endX-startX) /2) : endX+((startX - endX) / 2);
            int Y = startY < endY ? startY -25: endY-25;

            return new Point(X, Y);

        }

        public string ToString(Figure figure)
        {
            throw new NotImplementedException();
        }

        public void Draw(PaintEventArgs e, Figure figure)
        {
            SolidBrush brush = new SolidBrush(Color.Black);
            Font font = new Font("Arial", 8);
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(Text, font, brush, ornamentLocation, stringformat);
        }
    }



    public class TopDecoration : Decoration
    {
        public TopDecoration(Figure item, string text) : base(item, text)
        {
        }
    }

    public class BottomDecoration : Decoration
    {
        public BottomDecoration(Figure item, string text) : base(item, text)
        {
        }
    }

    public class LeftDecoration : Decoration
    {
        public LeftDecoration(Figure item, string text) : base(item, text)
        {
        }
    }

    public class RightDecoration : Decoration
    {
        public RightDecoration(Figure item, string text) : base(item, text)
        {
        }
    }


}
