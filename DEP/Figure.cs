using System;
using System.Drawing;
using System.Windows.Forms;
using static DEP.Figure;

namespace DEP
{
    public class Figure : IVisitable
    {
        // The point user press Mouse Down
        public Point start;
        // The point user let Mouse Up
        public Point end;
        // Direction comparing the end point and the start point
        public Direction direction;

        public IFigure StrategyFigure;

        public bool IsSelected { get; set; }
        public bool IsSelectedInGroup { get; set; }
        public bool IsMainGroupFigure { get; set; }
        public bool IsUnderlyingGroup { get; set; }
        public Group group{get;set; }
        public Pen color
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
        public Figure(Figure item)
        {
            start = item.start;
            end = item.end;
            group = item.group;
            StrategyFigure = item.StrategyFigure;
        }


        public Figure(IFigure figure)
        {
            this.StrategyFigure = figure;
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
           
        public override bool Equals(object obj)
        {
            Figure compareFigure = (Figure)obj;
            return (start == compareFigure.start && end == compareFigure.end);
        }

    }

    public class Ellipse : IFigure
    {
        // Drawthis by its start, end points and direction
        public void Draw(PaintEventArgs e, Figure figure)
        {

            switch (figure.direction)
            {
                case Direction.One:
                    e.Graphics.DrawEllipse(figure.color, new Rectangle(new Point(figure.start.X,
                       figure.end.Y), new Size(figure.end.X - figure.start.X,
                       figure.start.Y - figure.end.Y)));
                    break;
                case Direction.Two:
                    e.Graphics.DrawEllipse(figure.color, new Rectangle(figure.end,
                        new Size(figure.start.X - figure.end.X,
                           figure.start.Y - figure.end.Y)));
                    break;
                case Direction.Three:
                    e.Graphics.DrawEllipse(figure.color, new Rectangle(new Point(figure.end.X,
                       figure.start.Y), new Size(figure.start.X - figure.end.X,
                       figure.end.Y - figure.start.Y)));
                    break;
                case Direction.Four:
                    e.Graphics.DrawEllipse(figure.color, new Rectangle(figure.start,
                        new Size(figure.end.X - figure.start.X,
                           figure.end.Y - figure.start.Y)));
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }
        }

        public string ToString(Figure figure)
        {
            int width = Math.Abs(figure.start.X - figure.end.X);
            int height = Math.Abs(figure.start.Y - figure.end.Y);
            int startX = figure.start.X < figure.end.X ? figure.start.X : figure.end.X;
            int startY = figure.start.Y < figure.end.Y ? figure.start.Y : figure.end.Y;
            return $"ellipse {startX} {startY} {width} {height}{Environment.NewLine}";
        }
    }

    public sealed class xRectangle : IFigure
    {
        // Draw this by its start, end points and direction
        public void Draw(PaintEventArgs e, Figure figure)
        {
            switch (figure.direction)
            {
                case Direction.One:
                    e.Graphics.DrawRectangle(figure.color, new Rectangle(new Point(figure.start.X,
                       figure.end.Y), new Size(figure.end.X - figure.start.X,
                       figure.start.Y - figure.end.Y)));
                    break;
                case Direction.Two:
                    e.Graphics.DrawRectangle(figure.color, new Rectangle(figure.end,
                        new Size(figure.start.X - figure.end.X,
                           figure.start.Y - figure.end.Y)));
                    break;
                case Direction.Three:
                    e.Graphics.DrawRectangle(figure.color, new Rectangle(new Point(figure.end.X,
                       figure.start.Y), new Size(figure.start.X - figure.end.X,
                       figure.end.Y - figure.start.Y)));
                    break;
                case Direction.Four:
                    e.Graphics.DrawRectangle(figure.color, new Rectangle(figure.start,
                        new Size(figure.end.X - figure.start.X,
                           figure.end.Y - figure.start.Y)));
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }
        }

        public string ToString(Figure figure)
        {
            int width = Math.Abs(figure.start.X - figure.end.X);
            int height = Math.Abs(figure.start.Y - figure.end.Y);
            int startX = figure.start.X < figure.end.X ? figure.start.X : figure.end.X;
            int startY = figure.start.Y < figure.end.Y ? figure.start.Y : figure.end.Y;
            return $"rectangle {startX} {startY} {width} {height}{Environment.NewLine}";
        }
    }
}