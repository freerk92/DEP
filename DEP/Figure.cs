using System;
using System.Drawing;
using System.Windows.Forms;

namespace DEP
{
    public abstract class Figure : IVisitable
    {
        // The point user press Mouse Down
        public Point start;
        // The point user let Mouse Up
        public Point end;
        // Direction comparing the end point and the start point
        public Direction direction;
        public bool IsSelected { get; set; }
        public bool IsSelectedInGroup { get; set; }
        public bool IsMainGroupFigure { get; set; }
        public bool IsUnderlyingGroup { get; set; }
        public abstract void Draw(PaintEventArgs e);
        public Group group{get;set; }
        Pen color
        {
            get
            {
                if (IsUnderlyingGroup)
                    return Pens.Orange;
                if (IsMainGroupFigure && IsSelectedInGroup)
                    return Pens.Blue;
                if (IsSelectedInGroup)
                    return Pens.Red;
                return IsSelected ? Pens.Red : Pens.Black;
            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        // Four quadrants in coordinate
        public enum Direction
        {
            One,
            Two,
            Three,
            Four
        }

        public class Ellipse : Figure
        {
            public Ellipse(){}

            public Ellipse(Figure item)
            {
                this.start = item.start;
                this.end = item.end;
                this.group = item.group;
            }

            // Drawthis by its start, end points and direction
            public override void Draw(PaintEventArgs e)
            {
                
                switch (this.direction)
                {
                    case Direction.One:
                        e.Graphics.DrawEllipse(color, new Rectangle(new Point(this.start.X,
                           this.end.Y), new Size(this.end.X - this.start.X,
                           this.start.Y - this.end.Y)));
                        break;
                    case Direction.Two:
                        e.Graphics.DrawEllipse(color, new Rectangle(this.end,
                            new Size(this.start.X - this.end.X,
                               this.start.Y - this.end.Y)));
                        break;
                    case Direction.Three:
                        e.Graphics.DrawEllipse(color, new Rectangle(new Point(this.end.X,
                           this.start.Y), new Size(this.start.X - this.end.X,
                           this.end.Y - this.start.Y)));
                        break;
                    case Direction.Four:
                        e.Graphics.DrawEllipse(color, new Rectangle(this.start,
                            new Size(this.end.X - this.start.X,
                               this.end.Y - this.start.Y)));
                        break;
                    default:
                        MessageBox.Show("Error");
                        break;
                }
            }
        }

        public class xRectangle : Figure
        {
            public xRectangle(){}

            public xRectangle(Figure item)
            {
                this.start = item.start;
                this.end = item.end;
                this.group = item.group;
            }

            // Draw this by its start, end points and direction
            public override void Draw(PaintEventArgs e)
            {
                switch (this.direction)
                {
                    case Direction.One:
                        e.Graphics.DrawRectangle(color, new Rectangle(new Point(this.start.X,
                           this.end.Y), new Size(this.end.X - this.start.X,
                           this.start.Y - this.end.Y)));
                        break;
                    case Direction.Two:
                        e.Graphics.DrawRectangle(color, new Rectangle(this.end,
                            new Size(this.start.X - this.end.X,
                               this.start.Y - this.end.Y)));
                        break;
                    case Direction.Three:
                        e.Graphics.DrawRectangle(color, new Rectangle(new Point(this.end.X,
                           this.start.Y), new Size(this.start.X - this.end.X,
                           this.end.Y - this.start.Y)));
                        break;
                    case Direction.Four:
                        e.Graphics.DrawRectangle(color, new Rectangle(this.start,
                            new Size(this.end.X - this.start.X,
                               this.end.Y - this.start.Y)));
                        break;
                    default:
                        MessageBox.Show("Error");
                        break;
                }
            }

        }

        public override bool Equals(object obj)
        {
            Figure compareFigure = (Figure)obj;
            return (this.start == compareFigure.start && this.end == compareFigure.end);
        }

    }
}